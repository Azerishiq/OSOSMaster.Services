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
        public MeterController(AimContext db)
        {
            _db = db;
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
    }
}
