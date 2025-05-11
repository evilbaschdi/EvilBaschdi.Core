namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public abstract class ChainLinkValue<TOut> : IChainLinkValue<TOut>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChainLinkValue([NotNull] IChainLinkValue<TOut> chainLinkValue)
    {
        NextChain = chainLinkValue ?? throw new ArgumentNullException(nameof(chainLinkValue));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainLinkValue()
    {
    }

    /// <summary>
    /// </summary>
    protected abstract TOut InnerValue { get; }

    /// <inheritdoc />
    public IChainLinkValue<TOut> NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public TOut Value => AmIResponsible
        ? InnerValue
        : NextChain != null
            ? NextChain.Value
            : default;
}