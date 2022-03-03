using Aim.Core.Services.Database;
using Aim.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Extensions
{
    public static class AimContextExtension
    {
        internal static List<Node> Regions(this AimContext db)
        {
            return db.Nodes.Where(a => a.NodeTypeId == 1).ToList();
        }
        internal static List<Node> SubRegions(this AimContext db, int regionId)
        {
            return db.Nodes.Where(a => a.NodeTypeId == 2 && a.ParentId == regionId).ToList();
        }
    }
}
