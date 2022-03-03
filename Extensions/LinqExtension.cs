using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aim.Core.Services.Extensions
{
    public static class LinqExtension
    {
        public static IEnumerable<T> TakeLastSafe<T>(this IEnumerable<T> lst, int count)
        {
            if (lst.Count() < count)
            {
                return lst;
            }
            else
            {
                return lst.TakeLast(count).ToList();
            }
        }
        public static IEnumerable<T> TakeSafe<T>(this IEnumerable<T> lst, int count)
        {
            if (lst.Count() < count)
            {
                return lst;
            }
            else
            {
                return lst.Take(count).ToList();
            }
        }
    }
}
