namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface IChainHelperFor<in TIn, out TOut> : IValueFor<TIn, TOut>
    {
        /// <summary>
        /// </summary>
        IChainHelperFor<TIn, TOut> NextChain { get; }

        /// <summary>
        /// </summary>
        bool AmIResponsible { get; }
    }
}