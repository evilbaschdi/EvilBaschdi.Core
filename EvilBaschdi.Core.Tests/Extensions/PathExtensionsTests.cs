using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Tests.Extensions;

public sealed class PathExtensionsTests : IDisposable
{
    private readonly string _tempDirectory;

    public PathExtensionsTests()
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
    public void IsAccessible_ShouldReturnTrue_WhenDirectoryIsAccessible()
    {
        // Arrange
        var path = _tempDirectory;

        // Act
        var result = path.IsAccessible();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAccessible_ShouldReturnFalse_WhenDirectoryIsNotAccessible()
    {
        // Arrange
        var path = @"Z:\InvalidPath";

        // Act
        var result = path.IsAccessible();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void DirectoryInfo_ShouldReturnDirectoryInfo()
    {
        // Arrange
        var path = _tempDirectory;

        // Act
        var result = path.DirectoryInfo();

        // Assert
        result.Should().BeOfType<DirectoryInfo>();
        result.FullName.Should().Be(_tempDirectory);
    }

    [Fact]
    public void FileInfo_ShouldReturnFileInfo()
    {
        // Arrange
        var path = Path.Combine(_tempDirectory, "file.txt");
        File.Create(path).Close();

        // Act
        var result = path.FileInfo();

        // Assert
        result.Should().BeOfType<FileInfo>();
        result.FullName.Should().Be(path);
    }

    [Fact]
    public void GetExistingDirectories_ShouldReturnOnlyExistingDirectories()
    {
        // Arrange
        var existingDir = _tempDirectory;
        var nonExistingDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var dirs = new List<string> { existingDir, nonExistingDir };

        // Act
        var result = dirs.GetExistingDirectories();

        // Assert
        result.Should().HaveCount(1);
        result.Should().Contain(existingDir);
    }
}