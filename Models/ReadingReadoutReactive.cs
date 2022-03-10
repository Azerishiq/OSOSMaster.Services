using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class ReadingReadoutReactive
    {
        [Key]
        public int MeterId { get; set; }
        public long PartitionId { get; set; }
        public decimal? RI { get; set; }
        public decimal? RC { get; set; }
    }
}
