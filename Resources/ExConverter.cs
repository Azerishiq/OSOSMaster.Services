using Aim.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Resources
{
    public static class ExConverter
    {
        internal static List<NodeMeter> Filterize(List<NodeMeter> meters, int nodeId = 0, string subid = null)
        {
            List<NodeMeter> nodeMeters = meters;
            if (nodeId > 0)
            {
                nodeMeters = nodeMeters.Where(a => a.NodeId == nodeId).ToList();
            }
            if (subid is object)
            {
                nodeMeters = nodeMeters.Where(a => a.SubscriberId.Contains(subid)).ToList();
            }
            return nodeMeters;
        }
    }
}
