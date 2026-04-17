namespace EvilBaschdi.Core.Internal.Copy;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class CopyProgress : ICopyProgress
{
    /// <inheritdoc />
    public double TotalSize { get; set; } = 0d;

    /// <inheritdoc />
    public IProgress<double> Progress { get; set; }

    /// <inheritdoc />
    public double TempSize { get; set; } = 0d;
}