namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Generic Interface construct to encapsulate Classes without Interfaces
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    public interface IValueFor2<in TIn1, in TIn2, out TOut>
    {
        /// <summary>Value</summary>
        TOut ValueFor(TIn1 valueIn1, TIn2 valueIn2);
    }
}