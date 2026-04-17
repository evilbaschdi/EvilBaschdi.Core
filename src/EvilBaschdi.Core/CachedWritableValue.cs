namespace EvilBaschdi.Core;

/// <inheritdoc cref="ICachedWritableValue{T}" />
/// <inheritdoc cref="CachedValue{T}" />
// ReSharper disable once UnusedType.Global
public abstract class CachedWritableValue<T> : CachedValue<T>, ICachedWritableValue<T>
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