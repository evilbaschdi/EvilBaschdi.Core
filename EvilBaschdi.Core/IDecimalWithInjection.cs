﻿namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="decimal" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IDecimalWithInjection<in TIn> : IValueFor<TIn, decimal>
{
}