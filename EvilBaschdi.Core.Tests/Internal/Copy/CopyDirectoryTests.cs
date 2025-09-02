using EvilBaschdi.Core.Internal.Copy;

namespace EvilBaschdi.Core.Tests.Internal.Copy;

public class CopyDirectoryTests
{
    [Fact]
    public async Task RunForAsync_ShouldCallCopyDirectoryWithFiles()
    {
        // Arrange
        var copyDirectoryWithFiles = Substitute.For<ICopyDirectoryWithFiles>();
        var sut = new CopyDirectory(copyDirectoryWithFiles);
        var sourcePath = "source";
        var destinationPath = "destination";

        // Act
        await sut.RunForAsync(sourcePath, destinationPath);

        // Assert
        await copyDirectoryWithFiles.Received(1).RunForAsync(Arg.Is<DirectoryInfo>(d => d.Name == sourcePath), Arg.Is<DirectoryInfo>(d => d.Name == destinationPath));
    }
}