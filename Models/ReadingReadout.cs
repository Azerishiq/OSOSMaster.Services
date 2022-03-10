using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class ReadingReadout
    {
        [Key]
        public int MeterID { get; set; }
        public decimal? IndexT { get; set; }
        public long PartitionId { get; set; }
        public DateTime MeterDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
