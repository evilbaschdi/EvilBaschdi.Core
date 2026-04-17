using System.Text;
using EvilBaschdi.Core.Logging;

namespace EvilBaschdi.Core.Tests.Logging;

public sealed class AppendAllTextWithHeadlineTests : IDisposable
{
    private readonly string _tempFile;

    public AppendAllTextWithHeadlineTests()
    {
        _tempFile = Path.GetTempFileName();
    }

    public void Dispose()
    {
        if (File.Exists(_tempFile))
        {
            File.Delete(_tempFile);
        }
    }

    [Fact]
    public void RunFor_String_ShouldCreateFileWithHeadlineAndContent_WhenFileDoesNotExist()
    {
        // Arrange
        var sut = new AppendAllTextWithHeadline();
        var headline = "Test Headline";
        var content = "Test Content";
        var expected = $"{headline}{Environment.NewLine}{content}";
        File.Delete(_tempFile);

        // Act
        sut.RunFor(_tempFile, content, headline);
        var result = File.ReadAllText(_tempFile);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RunFor_String_ShouldAppendContent_WhenFileDoesExist()
    {
        // Arrange
        var sut = new AppendAllTextWithHeadline();
        var headline = "Test Headline";
        var initialContent = "Initial Content";
        var appendedContent = "Appended Content";
        var expected = $"{headline}{Environment.NewLine}{initialContent}{appendedContent}";
        File.WriteAllText(_tempFile, $"{headline}{Environment.NewLine}{initialContent}");

        // Act
        sut.RunFor(_tempFile, appendedContent, headline);
        var result = File.ReadAllText(_tempFile);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RunFor_StringBuilder_ShouldCreateFileWithHeadlineAndContent_WhenFileDoesNotExist()
    {
        // Arrange
        var sut = new AppendAllTextWithHeadline();
        var headline = "Test Headline";
        var content = new StringBuilder("Test Content");
        var expected = $"{headline}{Environment.NewLine}{content}";
        File.Delete(_tempFile);

        // Act
        sut.RunFor(_tempFile, content, headline);
        var result = File.ReadAllText(_tempFile);

        // Assert
        result.Should().Be(expected);
    }
}