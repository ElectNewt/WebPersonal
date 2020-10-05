using System.Collections.Generic;
using System.Linq;

namespace WebPersonal.Shared.ROP
{
    public static class IEnumerableUtils
    {
        /// <summary>
        /// Shorthand for string.Join(separator, strings)
        /// </summary>
        public static string JoinStrings(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings);
        }

        public static List<T> ListOrEmpty<T>(this IEnumerable<T> list)
        {
            return list.Any() ? list.ToList() : new List<T>();
        }
    }
}
