using System.Linq;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    ///     Class to provide generic extension methods
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        ///     Returns the default instance of type T (to use it with var)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
        {
            return default;
        }

        /// <summary>
        ///     Extension to validate if a value is contained in a provided bunch of values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] values)
        {
            return values.Contains(value);
        }
    }
}