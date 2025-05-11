namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
/// <typeparam name="TIn"></typeparam>
public interface IChainLinkTaskRunFor<in TIn> : ITaskRunFor<TIn>
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkTaskRunFor<TIn> NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}