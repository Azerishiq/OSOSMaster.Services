using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class ModemList
    {
        public int? ID { get; set; }
        public int? SubsidiaryID { get; set; }
        public int? TransformerID { get; set; }
        public string IMEI { get; set; }
        public short? DeviceType { get; set; }
        public short? ModemModelId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string AddressDescription { get; set; }
        public string SWVersion { get; set; }
        public string HWVersion { get; set; }
        public string GSMNumber { get; set; }
        public string IPAddress { get; set; }
        public string APNName { get; set; }
        public string APNServer { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Password { get; set; }
        public DateTime? LastConnectionDate { get; set; }
        public short? ConnectionType { get; set; }
        public short? PullPort { get; set; }
        public short? PushPort { get; set; }
        public DateTime? ProductionDate { get; set; }
        public short? QueueNumber { get; set; }
        public short? ConnectionTimeout { get; set; }
        public short? ReceiverTimeout { get; set; }
        public short? SenderTimeout { get; set; }
        public short? AliveInterval { get; set; }
        public bool HandShake { get; set; }
        public short? ProtocolType { get; set; }
        public DateTime? LastDeviceDate { get; set; }
        public DateTime? LastSystemDate { get; set; }
        public DateTime? ModemDateOffsetSecond { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public short? CreateUserID { get; set; }
        public DateTime? ModifyDate { get; set; }
        public short? ModifyUserID { get; set; }
    }
}
