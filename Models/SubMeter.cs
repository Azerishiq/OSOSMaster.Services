using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class SubMeter
    {
        [Key]
        #nullable enable
        public string? METERNO { get; set; }
        public string? SUBID { get; set; }
        public int? TRANSCOEF { get; set; }
    }
}
