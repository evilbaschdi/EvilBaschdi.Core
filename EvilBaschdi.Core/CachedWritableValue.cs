namespace EvilBaschdi.Core
{
    /// <inheritdoc cref="IWritableValue{T}" />
    /// <inheritdoc cref="ICachedValue{T}" />
    public abstract class CachedWritableValue<T> : CachedValue<T>, IWritableValue<T>
    {
        /// <summary>
        /// </summary>
        public new T Value
        {
            get => base.Value;
            set
            {
                SaveValue(value);
                ResetCache();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        protected abstract void SaveValue(T value);
    }
}