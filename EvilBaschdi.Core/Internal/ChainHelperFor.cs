using System;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public abstract class ChainHelperFor<TIn, TOut> : IChainHelperFor<TIn, TOut>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once NotAccessedField.Global
        protected TIn Input;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        protected ChainHelperFor(IChainHelperFor<TIn, TOut> chainHelperFor)
        {
            NextChain = chainHelperFor ?? throw new ArgumentNullException(nameof(chainHelperFor));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected ChainHelperFor()
        {
        }

        /// <inheritdoc />
        public IChainHelperFor<TIn, TOut> NextChain { get; }

        /// <inheritdoc />
        public abstract bool AmIResponsible { get; }

        /// <inheritdoc />
        public TOut ValueFor(TIn input)
        {
            Input = input;
            return AmIResponsible ? InnerValueFor(input) : NextChain != null ? NextChain.ValueFor(input) : default;
        }


        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected abstract TOut InnerValueFor(TIn input);
    }
}