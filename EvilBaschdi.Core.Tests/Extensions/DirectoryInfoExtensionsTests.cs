using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Tests.Extensions;

public sealed class DirectoryInfoExtensionsTests : IDisposable
{
    private readonly string _tempDirectory;

    public DirectoryInfoExtensionsTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDirectory))
        {
            Directory.Delete(_tempDirectory, true);
        }
    }

    [Fact]
    public void GetDirectorySize_ShouldReturnCorrectSize()
    {
        // Arrange
        var dir = new DirectoryInfo(_tempDirectory);
        File.WriteAllText(Path.Combine(_tempDirectory, "file1.txt"), "12345");
        var subDir = Directory.CreateDirectory(Path.Combine(_tempDirectory, "subdir"));
        File.WriteAllText(Path.Combine(subDir.FullName, "file2.txt"), "1234567890");

        // Act
        var size = dir.GetDirectorySize();

        // Assert
        size.Should().Be(15);
    }

    [Fact]
    public void GetProperDirectoryCapitalization_ShouldReturnCorrectCapitalization()
    {
        // Arrange
        var dirName = "TestDir";
        var lowerCaseDir = Path.Combine(_tempDirectory, dirName.ToLower());
        Directory.CreateDirectory(lowerCaseDir);
        var dir = new DirectoryInfo(lowerCaseDir);

        // Act
        var result = dir.GetProperDirectoryCapitalization();

        // Assert
        result.Should().EndWith(dirName.ToLower());
    }

    [Fact]
    public void RenameTo_ShouldRenameDirectory()
    {
        // Arrange
        var dir = new DirectoryInfo(_tempDirectory);
        var newName = Guid.NewGuid().ToString();

        // Act
        dir.RenameTo(newName);

        // Assert
        var newPath = Path.Combine(Path.GetTempPath(), newName);
        Directory.Exists(newPath).Should().BeTrue();
        Directory.Exists(_tempDirectory).Should().BeFalse();

        // Cleanup
        if (Directory.Exists(newPath))
        {
            Directory.Delete(newPath, true);
        }
    }
}