namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="double" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IDoubleWithInjection<in TIn> : IValueFor<TIn, double>;