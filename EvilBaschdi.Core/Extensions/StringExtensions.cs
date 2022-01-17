using System;
using System.Text;
using JetBrains.Annotations;
#if NETSTANDARD2_0
using System.ComponentModel;

#endif

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    ///     Class to extend the functionality of type <see cref="string" />
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public static class StringExtensions
    {
#if NETSTANDARD2_0
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
        // ReSharper disable once UnusedMember.Global
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
#endif
        /// <summary>
        ///     Replace with <see cref="StringComparison" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static string Replace([NotNull] this string source, [NotNull] string oldValue, string newValue, StringComparison comparisonType)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (oldValue == null)
            {
                throw new ArgumentNullException(nameof(oldValue));
            }

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
        ///     Returns a new string in which a specified number of characters in the current instance beginning at from right have
        ///     been deleted.
        /// </summary>
        /// <param name="value">The string to modify to this instance. </param>
        /// <param name="count">The number of characters to delete. </param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
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
        // ReSharper disable once UnusedMember.Global
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

        /// <summary>
        ///     Decodes a given string to UTF8
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static string DecodeString(this string input)
        {
            var bytes = Encoding.Default.GetBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}