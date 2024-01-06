using System.Collections.Concurrent;

namespace EvilBaschdi.Core.Extensions;

/// <summary>
///     HelperClass to extend ConcurrentBag with "AddRange".
/// </summary>
// ReSharper disable once UnusedType.Global
public static class ConcurrentBagExtensions
{
    /// <summary>
    ///     Add Range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bag"></param>
    /// <param name="toAdd"></param>
    // ReSharper disable once UnusedMember.Global
    public static void AddRange<T>([NotNull] this ConcurrentBag<T> bag, [NotNull] IEnumerable<T> toAdd)
    {
        ArgumentNullException.ThrowIfNull(bag);

        ArgumentNullException.ThrowIfNull(toAdd);

        foreach (var element in toAdd)
        {
            bag.Add(element);
        }
    }
}