namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public abstract class ChainLinkTaskOfValueFor<TIn, TResult> : IChainLinkTaskOfValueFor<TIn, TResult>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChainLinkTaskOfValueFor([NotNull] IChainLinkTaskOfValueFor<TIn, TResult> chainLinkValueFor)
    {
        NextChain = chainLinkValueFor ?? throw new ArgumentNullException(nameof(chainLinkValueFor));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainLinkTaskOfValueFor()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected abstract Task<TResult> InnerValueForAsync([NotNull] TIn input, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public IChainLinkTaskOfValueFor<TIn, TResult> NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public async Task<TResult> ValueForAsync([NotNull] TIn input, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        return AmIResponsible
            ? await InnerValueForAsync(input, cancellationToken)
            : NextChain != null
                ? await ValueForAsync(input, cancellationToken)
                : default;
    }
}