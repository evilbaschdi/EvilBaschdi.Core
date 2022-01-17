using System;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public abstract class ChainLinkValueFor<TIn, TOut> : IChainLinkValueFor<TIn, TOut>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        protected ChainLinkValueFor(IChainLinkValueFor<TIn, TOut> chainLinkValueFor)
        {
            NextChain = chainLinkValueFor ?? throw new ArgumentNullException(nameof(chainLinkValueFor));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected ChainLinkValueFor()
        {
        }

        /// <inheritdoc />
        public IChainLinkValueFor<TIn, TOut> NextChain { get; }

        /// <inheritdoc />
        public abstract bool AmIResponsible { get; }

        /// <inheritdoc />
        public TOut ValueFor(TIn input) => AmIResponsible ? InnerValueFor(input) : NextChain != null ? NextChain.ValueFor(input) : default;

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected abstract TOut InnerValueFor(TIn input);
    }
}