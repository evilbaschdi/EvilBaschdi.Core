using System;

namespace EvilBaschdi.Core.Extensions
{
    internal static class InternalClasses
    {
        internal static int _month(TimeSpan span)
        {
            var calc = DateTime.MinValue + span;
            return calc.Month - 1;
        }

        internal static int _year(TimeSpan span)
        {
            var calc = DateTime.MinValue + span;
            return calc.Year - 1;
        }
    }
}