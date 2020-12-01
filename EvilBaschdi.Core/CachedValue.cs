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
        private bool _isCached;
        private T _value;

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
                if (_isCached)
                {
                    return _value;
                }

                _value = NonCachedValue;
                _isCached = true;
                return _value;
            }
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