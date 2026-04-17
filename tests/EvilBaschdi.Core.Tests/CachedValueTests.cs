namespace EvilBaschdi.Core.Tests;

public class CachedValueTests
{
    private class TestCachedValue : CachedValue<string>
    {
        private int _callCount;
        public int CallCount => _callCount;

        protected override string NonCachedValue
        {
            get
            {
                _callCount++;
                return "test";
            }
        }
    }

    [Fact]
    public void Value_WhenCalledOnce_ReturnsValue()
    {
        // Arrange
        var sut = new TestCachedValue();

        // Act
        var result = sut.Value;

        // Assert
        result.Should().Be("test");
        sut.CallCount.Should().Be(1);
    }

    [Fact]
    public void Value_WhenCalledMultipleTimes_ReturnsCachedValue()
    {
        // Arrange
        var sut = new TestCachedValue();

        // Act
        var result1 = sut.Value;
        var result2 = sut.Value;
        var result3 = sut.Value;

        // Assert
        result1.Should().Be("test");
        result2.Should().Be("test");
        result3.Should().Be("test");
        sut.CallCount.Should().Be(1);
    }

    [Fact]
    public void ResetCache_WhenCalled_ResetsValue()
    {
        // Arrange
        var sut = new TestCachedValue();
        _ = sut.Value;

        // Act
        sut.ResetCache();
        var result = sut.Value;

        // Assert
        sut.CallCount.Should().Be(2);
        result.Should().Be("test");
    }
}