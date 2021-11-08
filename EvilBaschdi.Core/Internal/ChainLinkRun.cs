using System;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public abstract class ChainLinkRun : IChainLinkRun
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        protected ChainLinkRun(IChainLinkRun chainLinkRun)
        {
            NextChain = chainLinkRun ?? throw new ArgumentNullException(nameof(chainLinkRun));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected ChainLinkRun()
        {
        }

        /// <inheritdoc />
        public IChainLinkRun NextChain { get; }

        /// <inheritdoc />
        public abstract bool AmIResponsible { get; }

        /// <inheritdoc />
        public void Run()
        {
            if (AmIResponsible)
            {
                InnerRun();
            }
            else
            {
                NextChain?.Run();
            }
        }

        /// <summary>
        /// </summary>
        protected abstract void InnerRun();
    }
}