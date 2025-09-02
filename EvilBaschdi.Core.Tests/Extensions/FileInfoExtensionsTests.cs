using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Tests.Extensions;

public sealed class FileInfoExtensionsTests : IDisposable
{
    private readonly string _tempDirectory;

    public FileInfoExtensionsTests()
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
    public void GetProperFilePathCapitalization_ShouldReturnCorrectCapitalization()
    {
        // Arrange
        var fileName = "TestFile.txt";
        var lowerCaseFile = Path.Combine(_tempDirectory, fileName.ToLower());
        File.WriteAllText(lowerCaseFile, "test");
        var file = new FileInfo(lowerCaseFile);

        // Act
        var result = file.GetProperFilePathCapitalization();

        // Assert
        result.Should().EndWith(fileName.ToLower());
    }

    [Fact]
    public void IsFileLocked_ShouldReturnTrue_WhenFileIsLocked()
    {
        // Arrange
        var file = new FileInfo(Path.Combine(_tempDirectory, "locked.txt"));
        using (file.Create())
        {
            // Act
            var result = file.IsFileLocked();

            // Assert
            result.Should().BeTrue();
        }
    }

    [Fact]
    public void IsFileLocked_ShouldReturnFalse_WhenFileIsNotLocked()
    {
        // Arrange
        var file = new FileInfo(Path.Combine(_tempDirectory, "unlocked.txt"));
        file.Create().Close();

        // Act
        var result = file.IsFileLocked();

        // Assert
        result.Should().BeFalse();
    }
}