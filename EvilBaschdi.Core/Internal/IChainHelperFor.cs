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
        // ReSharper disable UnusedMemberInSuper.Global
        bool AmIResponsible { get; }
        // ReSharper restore UnusedMemberInSuper.Global

        /// <summary>
        /// </summary>
        // ReSharper disable UnusedMemberInSuper.Global
        IChainHelperFor<TIn, TOut> NextChain { get; }
        // ReSharper restore UnusedMemberInSuper.Global
    }
}