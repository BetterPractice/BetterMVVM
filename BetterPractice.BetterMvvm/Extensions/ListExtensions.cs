using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterPractice.BetterMvvm.Extensions
{
    public static class ListExtensions
    {
        public static void RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var doomed = list.Where(predicate).ToList();
            foreach (var item in doomed)
            {
                list.Remove(item);
            }
        }
    }

}
