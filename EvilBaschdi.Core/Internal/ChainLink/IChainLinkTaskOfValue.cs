namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
/// <typeparam name="TResult"></typeparam>
public interface IChainLinkTaskOfValue<TResult> : ITaskOfValue<TResult>
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkTaskOfValue<TResult> NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}