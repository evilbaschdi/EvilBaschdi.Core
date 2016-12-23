namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Generic Interface construct to encapsulate Classes without Interfaces
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    /// <typeparam name="TIn3"></typeparam>
    public interface IValueFor3<in TIn1, in TIn2, in TIn3, out TOut>
    {
        /// <summary>Value</summary>
        TOut ValueFor(TIn1 valueIn1, TIn2 valueIn2, TIn3 valueIn3);
    }
}