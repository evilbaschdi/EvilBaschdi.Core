namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public abstract class ChainLinkTaskOfValue<TResult> : IChainLinkTaskOfValue<TResult>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChainLinkTaskOfValue([NotNull] IChainLinkTaskOfValue<TResult> chainLinkValue)
    {
        NextChain = chainLinkValue ?? throw new ArgumentNullException(nameof(chainLinkValue));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainLinkTaskOfValue()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected abstract Task<TResult> InnerValueAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public IChainLinkTaskOfValue<TResult> NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public async Task<TResult> ValueAsync(CancellationToken cancellationToken = default)
    {
        return AmIResponsible
            ? await InnerValueAsync(cancellationToken)
            : NextChain != null
                ? await ValueAsync(cancellationToken)
                : default;
    }
}