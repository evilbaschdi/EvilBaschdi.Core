namespace EvilBaschdi.Core;

/// <inheritdoc />
public interface IWritableValue<T> : IValue<T>
{
    /// <inheritdoc cref="IValue{TOut}" />
    // ReSharper disable once UnusedMember.Global
    new T Value { get; set; }
}