namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
public interface IChainLinkTaskRun : ITaskRun
{
    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    bool AmIResponsible { get; }
    // ReSharper restore UnusedMemberInSuper.Global

    /// <summary>
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    IChainLinkTaskRun NextChain { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}