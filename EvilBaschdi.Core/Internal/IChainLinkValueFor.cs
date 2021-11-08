namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface IChainLinkValueFor<in TIn, out TOut> : IValueFor<TIn, TOut>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable UnusedMemberInSuper.Global
        bool AmIResponsible { get; }
        // ReSharper restore UnusedMemberInSuper.Global

        /// <summary>
        /// </summary>
        // ReSharper disable UnusedMemberInSuper.Global
        IChainLinkValueFor<TIn, TOut> NextChain { get; }
        // ReSharper restore UnusedMemberInSuper.Global
    }
}