namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="int" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IIntWithInjection<in TIn> : IValueFor<TIn, int>
{
}