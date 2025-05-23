﻿namespace EvilBaschdi.Core;

/// <summary>
///     <see cref="Guid" /> ValueFor(TIn value)
/// </summary>
// ReSharper disable once UnusedType.Global
public interface IGuidFor<in TIn> : IValueFor<TIn, Guid>;