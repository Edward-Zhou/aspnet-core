using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdwardAbp
{
    public static class Helper
    {
        public static IEnumerable<T> WhereIfIgnore<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.AsQueryable().Where(predicate) : source;
        }
    }
}
