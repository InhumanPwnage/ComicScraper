using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Helpers
{
    public static class LinqHelper
    {
        /// <summary>
        /// https://stackoverflow.com/a/11867402
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        //public static IEnumerable<T> SortAndDedupe<T>(this IEnumerable<T> input)
        //{
        //    var toDedupe = input.OrderBy(x => x);

        //    T prev;
        //    foreach (var element in toDedupe)
        //    {
        //        if (element.Equals(prev)) continue;

        //        yield return element;
        //        prev = element;
        //    }
        //}

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static List<T> ToUniqueList<T>(this IEnumerable<T> input)
        {
            return input.Where(s => s != null && !s.Equals(string.Empty)).Distinct().ToList();
        }

        //.Where(s => !string.IsNullOrWhiteSpace(s) || !string.IsNullOrEmpty(s)).Distinct().ToList()
    }
}
