using System.Diagnostics;

namespace EvilBaschdi.Core.AppHelpers;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class ProcessByPath : IProcessByPath
{
    /// <inheritdoc />
    public Process ValueFor([NotNull] string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new()
               {
                   StartInfo =
                   {
                       FileName = value,
                       UseShellExecute = true
                   }
               };
    }

    /// <inheritdoc />
    public void RunFor([NotNull] string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        ValueFor(value).Start();
    }
}