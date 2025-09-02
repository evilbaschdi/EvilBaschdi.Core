namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public abstract class ChainLinkTaskRunFor<TIn> : IChainLinkTaskRunFor<TIn>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChainLinkTaskRunFor([NotNull] IChainLinkTaskRunFor<TIn> chainLinkRunFor)
    {
        NextChain = chainLinkRunFor ?? throw new ArgumentNullException(nameof(chainLinkRunFor));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainLinkTaskRunFor()
    {
    }

    /// <inheritdoc />
    public IChainLinkTaskRunFor<TIn> NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public async Task RunForAsync([NotNull] TIn input, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (AmIResponsible)
        {
            await InnerRunForAsync(input, cancellationToken);
        }
        else
        {
            if (NextChain != null)
            {
                await RunForAsync(input, cancellationToken);
            }
            else
            {
                await Task.CompletedTask;
            }
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected abstract Task InnerRunForAsync([NotNull] TIn input, CancellationToken cancellationToken = default);
}