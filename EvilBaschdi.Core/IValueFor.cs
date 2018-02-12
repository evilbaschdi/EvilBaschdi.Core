namespace EvilBaschdi.Core
{
    /// <summary>
    ///     Generic Interface construct to encapsulate Classes without Interfaces
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    public interface IValueFor<in TIn, out TOut>
    {
        /// <summary>Value</summary>
        TOut ValueFor(TIn value);
    }
}