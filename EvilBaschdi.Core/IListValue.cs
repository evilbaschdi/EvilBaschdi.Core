using System.Collections.Generic;

namespace EvilBaschdi.Core;

/// <summary>
///     Generic Interface construct to encapsulate Classes without Interfaces
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IListValue<T>
{
    /// <summary>Value</summary>
    // ReSharper disable UnusedMemberInSuper.Global
    List<T> Value { get; }
    // ReSharper restore UnusedMemberInSuper.Global
}