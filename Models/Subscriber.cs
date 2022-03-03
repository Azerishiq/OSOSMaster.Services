using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Database
{
    public class Subscriber
    {
        [Key]
        public string SUBID { get; set; }
    }
}
