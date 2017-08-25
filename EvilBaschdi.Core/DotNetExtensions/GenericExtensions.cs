namespace EvilBaschdi.Core.DotNetExtensions
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
    }
}