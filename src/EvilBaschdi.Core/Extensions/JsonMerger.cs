using Newtonsoft.Json.Linq;

namespace EvilBaschdi.Core.Extensions;

/// <summary>
///     Provides static methods for merging JSON objects using custom rules for handling nested objects, arrays, and
///     primitive values.
/// </summary>
/// <remarks>
///     This class is intended for scenarios where JSON data from multiple sources needs to be combined into
///     a single object, with specific logic for resolving conflicts and merging structures.
///     Unlike <see cref="JObject.Merge(object)" />, this implementation performs deep equality checks when
///     merging arrays to avoid duplicate entries with identical content.
/// </remarks>
public static class JsonMerger
{
    /// <summary>
    ///     Merges two JSON objects, with <paramref name="preObject" /> values taking precedence.
    /// </summary>
    /// <param name="targetObject">The target object to merge into.</param>
    /// <param name="preObject">The source object whose values take precedence.</param>
    public static JObject CustomMerge([NotNull] JObject targetObject, [NotNull] JObject preObject)
    {
        ArgumentNullException.ThrowIfNull(targetObject);

        ArgumentNullException.ThrowIfNull(preObject);

        foreach (var property in preObject.Properties())
        {
            var key = property.Name;
            var preValue = property.Value;
            var targetValue = targetObject[key];

            targetObject[key] = (targetValue, preValue) switch
            {
                // Both are objects: recursively merge
                (JObject targetObj, JObject preObj) => MergeObjects(targetObj, preObj),

                // Both are arrays: union with deep equality
                (JArray targetArr, JArray preArr) => MergeArrays(targetArr, preArr),

                // All other cases: preValue wins
                _ => preValue
            };
        }

        return targetObject;
    }

    private static JObject MergeObjects(JObject target, JObject source)
    {
        CustomMerge(target, source);
        return target;
    }

    private static JArray MergeArrays(JArray target, JArray source)
    {
        foreach (var item in source)
        {
            if (!target.Any(x => JToken.DeepEquals(x, item)))
            {
                target.Add(item);
            }
        }

        return target;
    }
}