using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Dtos
{
    public class SubjectDto
    {
        public int id { get; set; }
        public int region_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
