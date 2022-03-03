using System.Collections.Generic;

namespace Aim.Core.Services.Models
{
    public class NodeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Node> Nodes { get; set; }
    }
}