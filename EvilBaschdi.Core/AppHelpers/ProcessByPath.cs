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

        var process = new Process
                      {
                          StartInfo =
                          {
                              FileName = value,
                              UseShellExecute = true
                          }
                      };

        return process;
    }

    /// <inheritdoc />
    public void RunFor([NotNull] string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        ValueFor(value).Start();
    }
}