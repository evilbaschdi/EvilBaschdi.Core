namespace EvilBaschdi.Core
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstract class for value caching
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CachedValue<T> : ICachedValue<T>
    {
        private T _value;
        private bool _isCached;

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
    }
}