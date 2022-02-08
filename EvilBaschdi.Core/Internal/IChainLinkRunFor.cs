namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
/// <typeparam name="TIn"></typeparam>
public interface IChainLinkRunFor<in TIn> : IRunFor<TIn>
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkRunFor<TIn> NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}