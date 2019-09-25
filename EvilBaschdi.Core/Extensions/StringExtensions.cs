using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    ///     Class to extend the functionality of the String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Contains <see cref="StringComparison" />.
        /// </summary>
        /// <param name="source">The string to seek in.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="value" /> is found in <paramref name="source" />; else
        ///     <c>false</c>
        /// </returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
            {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (int) comparisonType, typeof(StringComparison));
            }

            return source.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        ///     Replace with <see cref="System.StringComparison" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string Replace(this string source, string oldValue, string newValue, StringComparison comparisonType)
        {
            var index = source.IndexOf(oldValue, comparisonType);

            // Determine if we found a match
            var matchFound = index >= 0;

            if (!matchFound)
            {
                return source;
            }

            // Remove the old text
            source = source.Remove(index, oldValue.Length);

            // Add the replacement text
            source = source.Insert(index, newValue);

            return source;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitToEnumerable(this string value, string separator)
        {
            var list = new List<string>();
            if (value.Contains(separator))
            {
                list.AddRange(value.Split(separator.ToCharArray()[0]));
            }
            else
            {
                list.Add(value);
            }

            return list;
        }

        /// <summary>
        ///     Returns a new string in which a specified number of characters in the current instance beginning at from right have
        ///     been deleted.
        /// </summary>
        /// <param name="value">The string to modify to this instance. </param>
        /// <param name="count">The number of characters to delete. </param>
        /// <returns></returns>
        public static string RemoveRight(this string value, int count)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return value.Length > count ? value.Remove(value.Length - count, count) : value;
        }

        /// <summary>
        ///     Returns a new string in which a specified number of characters in the current instance beginning at from left have
        ///     been deleted.
        /// </summary>
        /// <param name="value">The string to modify to this instance. </param>
        /// <param name="count">The number of characters to delete. </param>
        /// <returns></returns>
        public static string RemoveLeft(this string value, int count)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return value.Length > count ? value.Remove(0, count) : value;
        }
    }
}