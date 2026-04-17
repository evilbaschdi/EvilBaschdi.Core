using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("Hello World", "world", "Earth", StringComparison.OrdinalIgnoreCase, "Hello Earth")]
    [InlineData("Hello World", "world", "Earth", StringComparison.Ordinal, "Hello World")]
    [InlineData("Hello World", "World", "Earth", StringComparison.Ordinal, "Hello Earth")]
    public void Replace_ShouldWorkAsExpected(string source, string oldValue, string newValue, StringComparison comparisonType, string expected)
    {
        // Act
        var result = source.Replace(oldValue, newValue, comparisonType);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World", 5, "Hello ")]
    [InlineData("Hello", 5, "Hello")]
    [InlineData("Hello", 10, "Hello")]
    public void RemoveRight_ShouldWorkAsExpected(string value, int count, string expected)
    {
        // Act
        var result = value.RemoveRight(count);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World", 6, "World")]
    [InlineData("Hello", 5, "Hello")]
    [InlineData("Hello", 10, "Hello")]
    public void RemoveLeft_ShouldWorkAsExpected(string value, int count, string expected)
    {
        // Act
        var result = value.RemoveLeft(count);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void DecodeString_ShouldDecodeAsExpected()
    {
        // Arrange
        const string value = "Heizölrückstoßabdämpfung";

        // Act
        var result = value.DecodeString();

        // Assert
        result.Should().Be(value);
    }
}