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
    public List<string> FilterExtensionsNotToEqual { get; set; } = [];

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<string> FilterExtensionsToEqual { get; set; } = [];

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<string> FilterFileNamesNotToEqual { get; set; } = [];

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<string> FilterFileNamesToEqual { get; set; } = [];

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once UnusedMember.Global
    public List<string> FilterFilePathsNotToEqual { get; set; } = [];

    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once UnusedMember.Global
    public List<string> FilterFilePathsToEqual { get; set; } = [];
    // ReSharper restore PropertyCanBeMadeInitOnly.Global
    // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global
}