namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
/// <typeparam name="TOut"></typeparam>
public interface IChainLinkValue<out TOut> : IValue<TOut>
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkValue<TOut> NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}