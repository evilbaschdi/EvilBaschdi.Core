using System.Diagnostics.CodeAnalysis;

namespace EvilBaschdi.Core.Extensions;

/// <summary>
///     Extension methods for dictionary formatting.
/// </summary>
[SuppressMessage("ReSharper", "GrammarMistakeInComment")]
public static class DictionaryExtensions
{
    extension<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> input)
    {
        /// <summary>
        ///     Formats dictionary entries into a single joined string with customizable separators.
        /// </summary>
        /// <param name="entrySeparator">
        ///     The separator placed between each key-value entry (e.g., Environment.NewLine or ", ").
        /// </param>
        /// <param name="keyValuePairSeparator">
        ///     The separator placed between the key and its value (e.g. is ": ").
        /// </param>
        /// <returns>A formatted string containing all key-value pairs.</returns>
        public string ToJoinedString(string entrySeparator, string keyValuePairSeparator)
        {
            ArgumentNullException.ThrowIfNull(input);
            ArgumentNullException.ThrowIfNull(entrySeparator);
            ArgumentNullException.ThrowIfNull(keyValuePairSeparator);

            return string.Join(entrySeparator, input.Select(kvp => $"{kvp.Key}{keyValuePairSeparator}{kvp.Value}"));
        }
    }
}