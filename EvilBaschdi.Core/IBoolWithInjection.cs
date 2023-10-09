namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="bool" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IBoolWithInjection<in TIn> : IValueFor<TIn, bool>
{
}