using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class ReadingReadoutExport
    {
        [Key]
        public int MeterID { get; set; }
        public decimal? ExpIndexT { get; set; }
        public decimal? ExpRI { get; set; }
        public decimal? ExpRC { get; set; }
        public long PartitionId { get; set; }
        public DateTime MeterDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
