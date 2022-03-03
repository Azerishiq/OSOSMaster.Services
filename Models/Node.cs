using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Models
{
    public class Node
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int NodeTypeId { get; set; }
        public NodeType NodeType { get; set; }
        public string OracleId { get; set; }
    }
}
