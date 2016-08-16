using System;

namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Summary description for CalculateTimeSpan
    /// </summary>
    public static class CalculateTimeSpan
    {
        /// <summary>
        ///     Get TimeSpan in months.
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static int Month(TimeSpan span)
        {
            return InternalClasses._month(span);
        }

        /// <summary>
        ///     Get TimeSpan in years.
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static int Year(TimeSpan span)
        {
            return InternalClasses._year(span);
        }

        /// <summary>
        ///     Get TimeSpan in quarters.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Quarter(this DateTime date)
        {
            return (date.Month + 2)/3;
        }
    }
}