using System.Diagnostics;

namespace EvilBaschdi.Core.AppHelpers;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class ProcessByPath : IProcessByPath
{
    /// <inheritdoc />
    public Process ValueFor(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        return new()
               {
                   StartInfo = new()
                               {
                                   FileName = path,
                                   UseShellExecute = true
                               }
               };
    }

    /// <inheritdoc />
    public Process ValueFor(string path, string workingDirectory)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(workingDirectory);

        var process = ValueFor(path);
        process.StartInfo.WorkingDirectory = workingDirectory;
        return process;
    }

    /// <inheritdoc />
    public void RunFor(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        using var process = ValueFor(path);
        _ = process.Start();
    }

    /// <inheritdoc />
    public void RunFor(string path, string workingDirectory)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(workingDirectory);

        using var process = ValueFor(path, workingDirectory);
        _ = process.Start();
    }
}