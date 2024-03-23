using System.Collections.Generic;
using System.Linq;

namespace WebPersonal.BackEnd.Service.Extensions;


/// <summary>
/// This should not be here but i don't want to create a new project for this
/// </summary>
public static class IEnumerableUtils
{
    public static string JoinStrings(this IEnumerable<string> strings, string separator)
    {
        return string.Join(separator, strings);
    }

    public static List<T> ListOrEmpty<T>(this IEnumerable<T> list)
    {
        return list.Any() ? list.ToList() : new List<T>();
    }
}