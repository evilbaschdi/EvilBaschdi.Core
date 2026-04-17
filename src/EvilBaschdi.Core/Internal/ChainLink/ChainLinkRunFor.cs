namespace EvilBaschdi.Core.Internal.ChainLink;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public abstract class ChainLinkRunFor<TIn> : IChainLinkRunFor<TIn>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChainLinkRunFor([NotNull] IChainLinkRunFor<TIn> chainLinkRunFor)
    {
        NextChain = chainLinkRunFor ?? throw new ArgumentNullException(nameof(chainLinkRunFor));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainLinkRunFor()
    {
    }

    /// <inheritdoc />
    public IChainLinkRunFor<TIn> NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public void RunFor([NotNull] TIn input)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (AmIResponsible)
        {
            InnerRunFor(input);
        }
        else
        {
            NextChain?.RunFor(input);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected abstract void InnerRunFor([NotNull] TIn input);
}