using System.Text;

namespace EvilBaschdi.Core.Logging;

/// <inheritdoc />
/// <summary>
///     Does a File.AppendAllText by adding a headline to the file.
/// </summary>
// ReSharper disable once UnusedType.Global
public class AppendAllTextWithHeadline : IAppendAllTextWithHeadline
{
    /// <inheritdoc />
    /// <param name="path"></param>
    /// <param name="contents"></param>
    /// <param name="headline"></param>
    public void RunFor(string path, string contents, string headline)
    {
        ArgumentNullException.ThrowIfNull(path);

        ArgumentNullException.ThrowIfNull(contents);

        ArgumentNullException.ThrowIfNull(headline);

        if (!File.Exists(path))
        {
            File.AppendAllText(path, $"{headline}{Environment.NewLine}");
        }

        File.AppendAllText(path, contents);
    }

    /// <inheritdoc />
    /// <param name="path"></param>
    /// <param name="stringBuilder"></param>
    /// <param name="headline"></param>
    public void RunFor(string path, StringBuilder stringBuilder, string headline)
    {
        ArgumentNullException.ThrowIfNull(path);

        ArgumentNullException.ThrowIfNull(stringBuilder);

        ArgumentNullException.ThrowIfNull(headline);

        RunFor(path, stringBuilder.ToString(), headline);
    }
}