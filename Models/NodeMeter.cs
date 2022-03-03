using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class NodeMeter
    {
        [Key]
        public int MeterId { get; set; }
        public int NodeId { get; set; }
        public string SubscriberId { get; set; }
        public bool IsSupplier { get; set; }
        public int MeterCategoryId { get; set; }
        public int MeterPowerId { get; set; }
    }
}
