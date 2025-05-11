namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IChainLinkTaskOfValueFor<in TIn, TResult> : ITaskOfValueFor<TIn, TResult>
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkTaskOfValueFor<TIn, TResult> NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}