using System;
using System.Collections.Generic;

namespace EvilBaschdi.Core
{
    /// <inheritdoc cref="ICachedValueFor{TIn,TOut}" />
    /// <summary>
    ///     Abstract base class for value caching
    ///     (Cached value)
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    // ReSharper disable once UnusedType.Global
    public abstract class CachedValueFor<TIn, TOut> : ICachedValueFor<TIn, TOut>
    {
        private readonly bool _cacheDefaultValue = true;
        private readonly Dictionary<TIn, TOut> _valueDictionary = new();

        /// <summary />
        protected CachedValueFor()
        {
        }


        /// <summary />
        /// <param name="cacheDefaultValue"></param>
        protected CachedValueFor(bool cacheDefaultValue)

        {
            _cacheDefaultValue = cacheDefaultValue;
        }

        /// <inheritdoc />
        /// <summary>(Cached Value)</summary>
        public TOut ValueFor(TIn value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_valueDictionary.ContainsKey(value))
            {
                return _valueDictionary[value];
            }

            var nonCachedValue = NonCachedValueFor(value);


            if (_cacheDefaultValue || !Equals(nonCachedValue, default(TOut)))
            {
                _valueDictionary[value] = nonCachedValue;
            }

            return nonCachedValue;
        }


        /// <summary>
        ///     Resets the Cache
        /// </summary>
        public void ResetCache()
        {
            _valueDictionary.Clear();
        }

        /// <summary />
        /// <param name="value"></param>
        /// <returns />
        protected abstract TOut NonCachedValueFor(TIn value);
    }
}