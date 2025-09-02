namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public abstract class ChainLinkTaskRun : IChainLinkTaskRun
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChainLinkTaskRun([NotNull] IChainLinkTaskRun chainLinkRun)
    {
        NextChain = chainLinkRun ?? throw new ArgumentNullException(nameof(chainLinkRun));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainLinkTaskRun()
    {
    }

    /// <inheritdoc />
    public IChainLinkTaskRun NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        if (AmIResponsible)
        {
            await InnerRunAsync(cancellationToken);
        }
        else
        {
            if (NextChain != null)
            {
                await RunAsync(cancellationToken);
            }
        }
    }

    /// <summary>
    /// </summary>
    protected abstract Task InnerRunAsync(CancellationToken cancellationToken = default);
}