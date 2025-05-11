namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="string" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IStringFor<in TIn> : IValueFor<TIn, string>;