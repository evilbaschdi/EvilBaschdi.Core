using System;

namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Summary description for CalculateTimeSpan
    /// </summary>
    public class CalculateTimeSpan
    {
        public static int Month(TimeSpan span)
        {
            return InternalClasses._month(span);
        }

        public static int Year(TimeSpan span)
        {
            return InternalClasses._year(span);
        }
    }
}