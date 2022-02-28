namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
public interface IChainLinkRun : IRun
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkRun NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}