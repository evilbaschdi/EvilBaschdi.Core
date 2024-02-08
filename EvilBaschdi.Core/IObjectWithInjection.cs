namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="object" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IObjectWithInjection<in TIn> : IValueFor<TIn, object>
{
}