using System.Collections.Generic;

namespace EvilBaschdi.Core;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public interface IValueOfIEnumerable<out T> : IValue<IEnumerable<T>>
{
}