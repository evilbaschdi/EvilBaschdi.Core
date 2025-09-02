using EvilBaschdi.Core.Internal.Copy;

namespace EvilBaschdi.Core.Tests.Internal.Copy;

public class CopyDirectoryWithProgressTests
{
    [Fact]
    public async Task RunForAsync_ShouldCallCopyDirectoryWithFilesWithProgressAndSetProgress()
    {
        // Arrange
        var copyDirectoryWithFilesWithProgress = Substitute.For<ICopyDirectoryWithFilesWithProgress>();
        var copyProgress = Substitute.For<ICopyProgress>();
        var sut = new CopyDirectoryWithProgress(copyDirectoryWithFilesWithProgress, copyProgress);
        var sourcePath = Path.Combine(Path.GetTempPath(), "source");
        var destinationPath = Path.Combine(Path.GetTempPath(), "destination");
        Directory.CreateDirectory(sourcePath);
        await File.WriteAllTextAsync(Path.Combine(sourcePath, "file.txt"), "test", TestContext.Current.CancellationToken);

        // Act
        await sut.RunForAsync(sourcePath, destinationPath);

        // Assert
        copyProgress.Received(1).TotalSize = 4;
        copyProgress.Received(1).TempSize = 0d;
        await copyDirectoryWithFilesWithProgress.Received(1).RunForAsync(Arg.Is<DirectoryInfo>(d => d.Name == "source"), Arg.Is<DirectoryInfo>(d => d.Name == "destination"));

        // Cleanup
        Directory.Delete(sourcePath, true);
    }
}