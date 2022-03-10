using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class MeterProperty
    {
        public int MeterID { get; set; }
        public decimal? IndexT { get; set; }
        public decimal? RI { get; set; }
        public decimal? RC { get; set; }
        public DateTime? LastConnectionDate { get; set; }
    }
}
