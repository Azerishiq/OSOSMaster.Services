using Aim.Core.Services.Database;
using Aim.Core.Services.Dtos;
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
    public class ResourceController : ControllerBase
    {
        private readonly AimContext _db;
        private readonly OracleContext _oracle;
        public ResourceController(AimContext db, OracleContext oracle)
        {
            _db = db;
            _oracle = oracle;
        }
        [HttpGet("[action]")]
        public IActionResult Regions()
        {
            return Ok(_db.Regions());
        }
        [HttpGet("[action]/{regionId}")]
        public IActionResult SubRegions(int regionId)
        {
            return Ok(_db.SubRegions(regionId));
        }
        [HttpGet("[action]")]
        public IActionResult GetMeterInsert()
        {
            var responseMessage = new ResponseMessage<object>();
            IEnumerable<int> nodemetersId = _db.NodeMeters.ToList().Select(a=>a.MeterId);
            object meters = _db.Meters.Where(a=>!nodemetersId.Contains(a.ID)).ToList().Select(a => new { MeterId = a.ID, Meterno = a.SerialNumber });
            object categories = _db.MetersCategories.ToList();
            object powers = _db.MeterPowers.ToList();
            responseMessage.Data = new { meters, categories, powers };
            return Ok(responseMessage);
        }
        [HttpPost("[action]")]
        public IActionResult MeterInsert([FromBody] MeterInsertDto req)
        {
            ResponseMessage<object> responseMessage = new ResponseMessage<object>(); 
            Meter meter = _db.Meters.Where(a => a.ID == req.MeterId).FirstOrDefault();
            if (meter is null)
            {
                responseMessage.Message = "Meter not found";
                return StatusCode(400, responseMessage);
            }
            if (_db.NodeMeters.Any(a=>a.MeterId == req.MeterId))
            {
                responseMessage.Message = "Meter is Existed";
                return StatusCode(400, responseMessage);
            }
            SubMeter submeter = _oracle.SUBMETER.Where(a => a.METERNO.StartsWith(meter.SerialNumber)).FirstOrDefault();
            if (submeter is null)
            {
                responseMessage.Message = "Submeter not found";
                return StatusCode(400, responseMessage);
            }
            Subscriber subscriber = _oracle.SUBSCRIBER.Where(a => a.SUBID == submeter.SUBID).FirstOrDefault();
            if (subscriber is null)
            {
                responseMessage.Message = "Subscriber not found";
                return StatusCode(400, responseMessage);
            }
            string nodeId = subscriber.SUBID.Substring(0, 2);
            Node node = _db.Nodes.Where(a => Convert.ToInt32(a.OracleId) == Convert.ToInt32(nodeId) && a.NodeTypeId == 2).FirstOrDefault();
            if (node is null)
            {
                responseMessage.Message = "Node not found";
                return StatusCode(400, responseMessage);
            }
            _db.NodeMeters.Add(new NodeMeter { IsSupplier = req.IsSupplier, MeterCategoryId = req.CategoryId, MeterId = req.MeterId, MeterPowerId = req.PowerId, NodeId = node.Id, SubscriberId = subscriber.SUBID });
            _db.SaveChanges();
            responseMessage.Message = "Added Successfully";
            return StatusCode(200, responseMessage);
        }
    }
}
