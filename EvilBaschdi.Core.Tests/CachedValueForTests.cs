namespace EvilBaschdi.Core.Tests;

public class CachedValueForTests
{
    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(CachedValueForTestClass).GetConstructors());
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_ReturnsI(CachedValueForTestClass sut)
    {
        sut.Should().BeAssignableTo<ICachedValueFor<string, string>>();
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(CachedValueForTestClass).GetMethods().Where(method => !method.IsAbstract));
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void ValueFor_DictionaryDoesNotContainKey_AddedToDictionary_ReturnsCachedValue(
        string key,
        CachedValueForTestClass sut)
    {
        // Arrange

        // Act
        var result = sut.ValueFor(key);

        // Assert
        result.Should().Be($"result = {key}");
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void ValueFor_DictionaryContainsKey_ReturnsSameValue(
        string key,
        CachedValueForTestClass sut)
    {
        // Arrange

        // Act
        var result1 = sut.ValueFor(key);
        var result2 = sut.ValueFor(key);

        // Assert
        result2.Should().BeSameAs(result1);
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void ValueFor_DictionaryContainsKey_ResetsCache_ReturnsNotSameButEqualValue(
        string key,
        CachedValueForTestClass sut)
    {
        // Arrange

        // Act
        var result1 = sut.ValueFor(key);
        sut.ResetCache();
        var result2 = sut.ValueFor(key);

        // Assert
        result2.Should().Be(result1);
        result2.Should().NotBeSameAs(result1);
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void ValueFor_ReturnsDefaultValue_CachesDefaultValue(
        string dummyValue)
    {
        // Arrange
        var sut = new CachedValueForTestClassReturningDefaultOfGuid(true);

        // Act
        var result1 = sut.ValueFor(dummyValue);
        var result2 = sut.ValueFor(dummyValue);

        // Assert            
        sut.CallCounter.Should().Be(1);
        result2.Should().Be(result1);
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueAutoData]
    public void ValueFor_ReturnsDefaultValue_DoesNotCacheDefaultValue(
        string dummyValue)
    {
        // Arrange
        var sut = new CachedValueForTestClassReturningDefaultOfGuid(false);

        // Act
        var result1 = sut.ValueFor(dummyValue);
        var result2 = sut.ValueFor(dummyValue);

        // Assert            
#pragma warning disable xUnit2005 // Do not use identity check on value type
#pragma warning disable MFA001 // Replace Xunit assertion with Fluent Assertions equivalent
#pragma warning disable FluentAssertions0703 // Simplify Assertion
        Assert.NotSame(result2, result1);
#pragma warning restore FluentAssertions0703 // Simplify Assertion
#pragma warning restore MFA001 // Replace Xunit assertion with Fluent Assertions equivalent
#pragma warning restore xUnit2005 // Do not use identity check on value type
        sut.CallCounter.Should().Be(2);
        result2.Should().Be(result1);
    }

    public class CachedValueForTestClass : CachedValueFor<string, string>
    {
        protected override string NonCachedValueFor(string value)
        {
            return $"result = {value}";
        }
    }

    private class CachedValueForTestClassReturningDefaultOfGuid(
        bool cacheDefaultValues) : CachedValueFor<string, Guid>(cacheDefaultValues)
    {
        public int CallCounter;

        protected override Guid NonCachedValueFor(string value)
        {
            CallCounter++;
            return Guid.Empty;
        }
    }
}