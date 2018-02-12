using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    ///     HelperClass to extend ConcurrentBag with "AddRange".
    /// </summary>
    public static class ConcurrentBagExtensions
    {
        /// <summary>
        ///     Add Range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bag"></param>
        /// <param name="toAdd"></param>
        public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> toAdd)
        {
            foreach (var element in toAdd)
            {
                bag.Add(element);
            }
        }
    }
}