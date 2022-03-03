using Aim.Core.Services.Database;
using Aim.Core.Services.Dtos;
using Aim.Core.Services.Models;
using Aim.Core.Services.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aim.Core.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly AimContext _db;
        private readonly OracleContext _oracle;
        public CountController(AimContext db, OracleContext oracle)
        {
            _db = db;
            _oracle = oracle;
        }
        [HttpGet("customercounter/{id}")]
        public IActionResult CustomerCounts(int id)
        {
            ResponseMessage<List<CustomerCounter>> data = new ResponseMessage<List<CustomerCounter>>
            {
                Data = _db.CustomerCounter.FromSqlRaw($@"EXEC [WebCustomerPanel].[SP_counters]@UserID = " + id + "")
            .ToList()
            };
            if (data is null)
            {
                return StatusCode(400, "Belə bir məlumat tapılmadı");
            }
            return StatusCode(200, data);
        }
        [HttpGet("modemlist/{id}")]
        public IActionResult ModemList(int id)
        {
            ResponseMessage<List<ModemList>> data = new ResponseMessage<List<ModemList>>
            {
                Data = _db.ModemList.FromSqlRaw($@"EXEC [WebCustomerPanel].[SP_ModemList] @UserID = " + id + "")
            .ToList()
            };
            if (data is null)
            {
                return StatusCode(400, "Belə bir məlumat tapılmadı");
            }
            return StatusCode(200, data);
        }
        [HttpGet("meter/{modemId}")]
        public IActionResult MeterByModemId(int modemId)
        {
            ResponseMessage<List<MeterDto>> data = new ResponseMessage<List<MeterDto>>();
            var obj = (from meter in _db.Meters.FromSqlRaw($@"EXEC [WebCustomerPanel].[SP_MeterByModemId] @ModemID = " + modemId + "").ToList()
                       select new MeterDto
                       {
                           IsActive = meter.IsActive,
                           IsDeleted = meter.IsDeleted,
                           ModemID = meter.ModemID,
                           SerialNumber = meter.SerialNumber,
                           MeterModelID = meter.MeterModelID,
                           ParentNodeID = meter.ParentNodeID,
                           DeviceType = meter.DeviceType,
                           CreateDate = meter.CreateDate,
                           ProductionDate = meter.ProductionDate,
                           ProtocolType = meter.ProtocolType,
                           FlagTransparent = meter.FlagTransparent,
                           CreateUserID = meter.CreateUserID,
                           ModifyDate = meter.ModifyDate,
                           ModifyUserID = meter.ModifyUserID,
                           MeterAdvanced = _oracle.SUBMETER.Where(a => a.METERNO.StartsWith(meter.SerialNumber)).FirstOrDefault()
                       }).ToList();
            data.Data = obj;
            // { meter, sub = _oracle.SUBMETER.Where(a=>a.METERNO.StartsWith(meter.SerialNumber)) });
            if (data is null)
            {
                return StatusCode(400, "Belə bir məlumat tapılmadı");
            }
            return StatusCode(200, data);
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            var data = _oracle.SUBMETER.ToList();
            return StatusCode(200, data);
        }
        //[HttpGet("[action]/{id}")]
        //public async Task<IActionResult> RayonAdd(int id)
        //{
        //    List<Node> nodes = new();
        //    var client = new HttpClient();
        //    var subjectresp = await client.GetAsync($"http://akt.azerishiq.az/api/v1/region/{id}/cities");
        //    List<SubjectDto> subjects = await subjectresp.Content.ReadAsAsync<List<SubjectDto>>();
        //    foreach (var item in subjects)
        //    {
        //        nodes.Add(new Node { Name = item.name, NodeTypeId = 2, OracleId = item.code, ParentId = item.region_id });
        //    }
        //    _db.Nodes.AddRange(nodes);
        //    _db.SaveChanges();
        //    return Ok("success");
        //}

    }
}
