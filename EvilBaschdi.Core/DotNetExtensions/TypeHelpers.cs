using System;
using System.Windows.Media;

namespace EvilBaschdi.Core.DotNetExtensions
{
    public static class TypeHelpers
    {
        public static byte Subtract(this byte value, int integer)
        {
            var result = Convert.ToInt32(value) - integer;
            if (result < 0)
            {
                return 0;
            }
            return Convert.ToByte(result);
        }

        public static byte Add(this byte value, int integer)
        {
            return Convert.ToByte(Convert.ToInt32(value) + integer);
        }

        public static Color ToColor(this string hex)
        {
            var value = hex.PadLeft(8, 'F').PadLeft(9, '#');
            var convertFromString = ColorConverter.ConvertFromString(value);
            if (convertFromString != null)
            {
                return (Color) convertFromString;
            }
            return Colors.Black;
        }
    }
}