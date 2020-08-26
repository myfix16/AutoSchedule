using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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

        /// <summary>
        /// Get a List<T> with elementNeedRemove removed. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="elementNeedRemove"></param>
        /// <returns></returns>
        public static List<T> WithRemoved<T>(this List<T> list, T elementNeedRemove)
        {
            var outcome = list.ShallowCopy();
            bool success = outcome.Remove(elementNeedRemove);

            return (success) ? outcome : throw new ArgumentException("No such element.");
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
