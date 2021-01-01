namespace EvilBaschdi.Core
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstract class for value caching
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // ReSharper disable once UnusedType.Global
    public abstract class CachedValue<T> : ICachedValue<T>
    {
        private readonly bool _cacheTypeDefaultValue = true;

        private bool _isCached;
        private T _value;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cacheTypeDefaultValue"></param>
        protected CachedValue(bool cacheTypeDefaultValue)
        {
            _cacheTypeDefaultValue = cacheTypeDefaultValue;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        protected CachedValue()
        {
        }

        /// <summary>
        ///     Non cached value
        /// </summary>
        protected abstract T NonCachedValue { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Cached value
        /// </summary>
        public T Value
        {
            get
            {
                // ReSharper disable once InvertIf
                if (!_isCached || _isCached && !_cacheTypeDefaultValue && Equals(default(T), _value))
                {
                    _value = NonCachedValue;
                    _isCached = true;
                }

                return _value;
            }
            internal set => _value = value;
        }

        /// <summary>
        ///     Resets the Cached Value
        /// </summary>
        public void ResetCache()
        {
            _isCached = false;
            _value = default;
        }
    }
}