using EvilBaschdi.Core.Internal.Copy;

namespace EvilBaschdi.Core.Tests.Internal.Copy;

public sealed class CopyDirectoryWithFilesTests : IDisposable
{
    private readonly string _sourceDirectory;
    private readonly string _targetDirectory;

    public CopyDirectoryWithFilesTests()
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
    public async Task RunForAsync_ShouldCopyDirectoryWithFiles()
    {
        // Arrange
        var sut = new CopyDirectoryWithFiles();
        var sourceDir = new DirectoryInfo(_sourceDirectory);
        var targetDir = new DirectoryInfo(_targetDirectory);
        File.WriteAllText(Path.Combine(_sourceDirectory, "file.txt"), "test");
        var subDir = Directory.CreateDirectory(Path.Combine(_sourceDirectory, "subdir"));
        File.WriteAllText(Path.Combine(subDir.FullName, "file2.txt"), "test2");

        // Act
        await sut.RunForAsync(sourceDir, targetDir);

        // Assert
        Directory.Exists(_targetDirectory).Should().BeTrue();
        File.Exists(Path.Combine(_targetDirectory, "file.txt")).Should().BeTrue();
        Directory.Exists(Path.Combine(_targetDirectory, "subdir")).Should().BeTrue();
        File.Exists(Path.Combine(_targetDirectory, "subdir", "file2.txt")).Should().BeTrue();
    }
}