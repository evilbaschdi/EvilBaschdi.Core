namespace EvilBaschdi.Core
{
    /// <inheritdoc />
    /// <summary>
    ///     Interface for classes that return a cached value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICachedValue<out T> : IValue<T>
    {
        /// <summary>
        ///     Reset Cache
        /// </summary>
        // ReSharper disable once UnusedMemberInSuper.Global
        void ResetCache();
    }
}