namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Generic Interface construct to encapsulate Classes without Interfaces
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    /// <typeparam name="TIn3"></typeparam>
    /// <typeparam name="TIn4"></typeparam>
    public interface IValueFor4<in TIn1, in TIn2, in TIn3, in TIn4, out TOut>
    {
        /// <summary>Value</summary>
        TOut ValueFor(TIn1 valueIn1, TIn2 valueIn2, TIn3 valueIn3, TIn4 valueIn4);
    }
}