namespace EvilBaschdi.Core
{
    /// <summary>
    ///     Generic Interface construct to encapsulate cached Classes without Interfaces
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface ICachedValueFor<in TIn, out TOut> : IValueFor<TIn, TOut>
    {
    }
}