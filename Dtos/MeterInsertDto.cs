using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Dtos
{
    public class MeterInsertDto
    {
        public int MeterId { get; set; }
        public int CategoryId { get; set; }
        public int PowerId { get; set; }
        public bool IsSupplier { get; set; }
    }
}
