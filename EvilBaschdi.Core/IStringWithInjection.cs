namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="string" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IStringWithInjection<in TIn> : IValueFor<TIn, string>
{
}