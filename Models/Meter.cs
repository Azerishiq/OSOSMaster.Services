using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class Meter
    {
        public int? ID { get; set; }
        public int? ModemID { get; set; }
        public string? SerialNumber { get; set; }
        public short? MeterModelID { get; set; }
        public short? ParentNodeID { get; set; }
        public short? DeviceType { get; set; }
        public DateTime? ProductionDate { get; set; }
        public short? ProtocolType { get; set; }
        public bool FlagTransparent { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public short? CreateUserID { get; set; }
        public DateTime? ModifyDate { get; set; }
        public short? ModifyUserID { get; set; }

    }
}
