namespace EvilBaschdi.Core.Model;

/// <summary>
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class FileListFromPathFilter
{
    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable PropertyCanBeMadeInitOnly.Global
    public List<string> FilterExtensionsNotToEqual { get; set; } = new();

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global

    public List<string> FilterExtensionsToEqual { get; set; } = new();

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<string> FilterFileNamesNotToEqual { get; set; } = new();

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<string> FilterFileNamesToEqual { get; set; } = new();

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once UnusedMember.Global
    public List<string> FilterFilePathsNotToEqual { get; set; } = new();

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once UnusedMember.Global
    public List<string> FilterFilePathsToEqual { get; set; } = new();
    // ReSharper restore PropertyCanBeMadeInitOnly.Global
    // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global
}