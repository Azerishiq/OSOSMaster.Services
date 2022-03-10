using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class ConsumptionLPHourly
    {
        public int MeterID { get; set; }
        public DateTime PrevPartitionDate { get; set; }
        public DateTime PartitionDate { get; set; }
        public decimal? PrevIndexT { get; set; }
        public decimal? IndexT { get; set; }
        public decimal? ConsumptionT { get; set; }
    }
}
