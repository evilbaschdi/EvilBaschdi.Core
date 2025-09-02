using EvilBaschdi.Core.Internal;

namespace EvilBaschdi.Core.Tests.Internal;

public sealed class MoveDirectoryTests : IDisposable
{
    private readonly string _sourceDirectory;
    private readonly string _targetDirectory;

    public MoveDirectoryTests()
    {
        _sourceDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _targetDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_sourceDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_sourceDirectory))
        {
            Directory.Delete(_sourceDirectory, true);
        }

        if (Directory.Exists(_targetDirectory))
        {
            Directory.Delete(_targetDirectory, true);
        }
    }

    [Fact]
    public void RunFor_ShouldMoveDirectory_WhenTargetDoesNotExist()
    {
        // Arrange
        var sut = new MoveDirectory();
        File.WriteAllText(Path.Combine(_sourceDirectory, "file.txt"), "test");

        // Act
        sut.RunFor(_sourceDirectory, _targetDirectory);

        // Assert
        Directory.Exists(_sourceDirectory).Should().BeFalse();
        Directory.Exists(_targetDirectory).Should().BeTrue();
        File.Exists(Path.Combine(_targetDirectory, "file.txt")).Should().BeTrue();
    }

    [Fact]
    public void RunFor_ShouldMoveDirectoryContent_WhenTargetDoesExist()
    {
        // Arrange
        var sut = new MoveDirectory();
        Directory.CreateDirectory(_targetDirectory);
        File.WriteAllText(Path.Combine(_sourceDirectory, "file.txt"), "test");
        var subDir = Directory.CreateDirectory(Path.Combine(_sourceDirectory, "subdir"));
        File.WriteAllText(Path.Combine(subDir.FullName, "file2.txt"), "test2");

        // Act
        sut.RunFor(_sourceDirectory, _targetDirectory);

        // Assert
        Directory.Exists(_sourceDirectory).Should().BeFalse();
        Directory.Exists(_targetDirectory).Should().BeTrue();
        File.Exists(Path.Combine(_targetDirectory, "file.txt")).Should().BeTrue();
        Directory.Exists(Path.Combine(_targetDirectory, "subdir")).Should().BeTrue();
        File.Exists(Path.Combine(_targetDirectory, "subdir", "file2.txt")).Should().BeTrue();
    }
}