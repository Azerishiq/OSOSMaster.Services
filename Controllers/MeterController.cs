using Aim.Core.Services.Database;
using Aim.Core.Services.Extensions;
using Aim.Core.Services.Models;
using Aim.Core.Services.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterController : ControllerBase
    {
        private readonly AimContext _db;
        private readonly OracleContext _oracle;
        public MeterController(AimContext db, OracleContext oracle)
        {
            _db = db;
            _oracle = oracle;
        }
        [HttpGet("[action]")]
        public IActionResult Get(int nodeId, string subid, int page = 0)
        {
            ResponseMessage<object> responseMessage = new();
            List<Meter> data = _db.Meters.ToList();
            List<NodeMeter> nodeMeters = ExConverter.Filterize(_db.NodeMeters.ToList(), nodeId, subid);
            var returnValue = (from nodemeter in nodeMeters
                               join meter in _db.Meters on nodemeter.MeterId equals meter.ID
                               join node in _db.Nodes on nodemeter.NodeId equals node.Id
                               join parentNode in _db.Nodes on node.ParentId equals parentNode.Id
                               join power in _db.MeterPowers on nodemeter.MeterPowerId equals power.Id
                               join category in _db.MetersCategories on nodemeter.MeterCategoryId equals category.Id
                               where parentNode.ParentId == 0
                               select new { meter.ID, meter.SerialNumber, nodemeter.SubscriberId, node.Name, Power = power.Name, Category = category.Name, ParentNode = parentNode.Name }).ToList();
            float pagecount = returnValue.Count;
            int count = (int)Math.Ceiling(pagecount / 10);
            returnValue = returnValue.Skip(page * 10).TakeSafe(10).ToList();
            responseMessage.Data = new { meter = returnValue, page = count };
            return Ok(responseMessage);
        }
        [HttpGet("[action]")]
        public IActionResult Metrics(int nodeId, string subid, string meterno, int page = 0)
        {
            ResponseMessage<object> responseMessage = new();
            List<Meter> data = _db.Meters.ToList();
            List<NodeMeter> nodeMeters = ExConverter.Filterize(_db.NodeMeters.ToList(), nodeId, subid);
            var returnValue = (from nodemeter in nodeMeters
                               join meter in _db.Meters on nodemeter.MeterId equals meter.ID
                               join node in _db.Nodes on nodemeter.NodeId equals node.Id
                               join parentNode in _db.Nodes on node.ParentId equals parentNode.Id
                               join category in _db.MetersCategories on nodemeter.MeterCategoryId equals category.Id
                               where parentNode.ParentId == 0
                               select new
                               {
                                   meter.ID,
                                   meter.SerialNumber,
                                   nodemeter.SubscriberId,
                                   node.Name,
                                   Category = category.Name,
                                   ParentNode = parentNode.Name,
                                   metrics = _db.ReadingReadout.Where(a => a.MeterID == nodemeter.MeterId).OrderByDescending(a => a.CreateDate).FirstOrDefault()
                               }).ToList();
            float pagecount = returnValue.Count;
            int count = (int)Math.Ceiling(pagecount / 10);
            returnValue = returnValue.Skip(page * 10).TakeSafe(10).ToList();
            responseMessage.Data = new { meter = returnValue, page = count };
            return Ok(responseMessage);
        }
        [HttpGet("[action]/{meterId}")]
        public IActionResult Details(int meterId)
        {
            ResponseMessage<object> responseMessage = new();
            DateTime date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var meter = _db.Meters.Where(s => s.ID == meterId).FirstOrDefault();
            SubMeter transcoef = _oracle.SUBMETER.Where(a => a.METERNO.StartsWith(meter.SerialNumber)).FirstOrDefault();
            var resultlog = (from a in _db.ReadingReadout
                             join b in _db.ReadingReadoutExport on a.PartitionId equals b.PartitionId
                             join c in _db.ReadingReadoutReactive on a.PartitionId equals c.PartitionId
                             where a.MeterID == meterId && a.MeterDate >= date && date <= a.MeterDate
                             orderby a.MeterDate
                             select new
                             {
                                 a.MeterDate,
                                 a.IndexT,
                                 b.ExpIndexT,
                                 c.RI,
                                 b.ExpRI
                             })
                             .AsEnumerable()
                             .GroupBy(a => a.MeterDate.Hour)
                             .Select(a => a.FirstOrDefault())
                             .ToList();
            responseMessage.Data = new { resultlog, transcoef, meter };
            return Ok(responseMessage);
        }
    }
}
