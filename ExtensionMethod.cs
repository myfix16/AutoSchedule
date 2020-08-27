using System;
using System.Collections.Generic;

namespace AutoSchedule
{
    public static class ExtensionMethod
    {
        public static List<T> ShallowCopy<T>(this List<T> list)
        {
            var newList = new List<T>();
            newList.AddRange(list);
            return newList;
        }

        public static IEnumerable<T> ForEachObject<T>(this IEnumerable<T> collection, Action<T> predicate)
        {
            foreach (var item in collection)
            {
                predicate(item);
            }

            return collection;
        }

        public static TResult Map<T, TResult>(this T element, Func<T, TResult> predicate)
            => predicate(element);
    }
}
