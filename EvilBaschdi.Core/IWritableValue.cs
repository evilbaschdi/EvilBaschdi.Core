namespace EvilBaschdi.Core
{
    /// <inheritdoc />
    public interface IWritableValue<T> : IValue<T>
    {
        /// <inheritdoc cref="IValue{TOut}" />
        new T Value { get; set; }
    }
}