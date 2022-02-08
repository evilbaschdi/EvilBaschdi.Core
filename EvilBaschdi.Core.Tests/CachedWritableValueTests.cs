namespace EvilBaschdi.Core.Tests;

// ReSharper disable once UnusedType.Global
public class CachedWritableValueTests
{
    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    //{
    //    assertion.Verify(typeof(DummyCachedWritableValueString).GetConstructors());
    //}

    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Constructor_Returns(DummyCachedWritableValueString sut)
    //{
    //    Assert.IsAssignableFrom<IValue<string>>(sut);
    //    Assert.IsAssignableFrom<IWritableValue<string>>(sut);
    //    Assert.IsAssignableFrom<CachedValue<string>>(sut);
    //}

    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    //{
    //    assertion.Verify(typeof(DummyCachedWritableValueString).GetMethods().Where(method => !method.IsAbstract && method.Name != "set_Value"));
    //}

    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Value_ForGivenValue_CallsReset_ReturnsNotValue(
    //    DummyCachedWritableValueString sut
    //)
    //{
    //    // Arrange
    //    sut.Value = "Test2";

    //    // Act
    //    var result1 = sut.Value;
    //    sut.ResetCache();
    //    //var result2 = sut.Value;

    //    // Assert
    //    result1.Should().Be("Test2");
    //    //result1.Should().NotBeSameAs(result2);
    //    result1.Should().Be(default(string));
    //}

    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Value_ForGivenValue_SetsValue_ReturnsValue(
    //    DummyCachedWritableValueString sut
    //)
    //{
    //    // Arrange
    //    sut.Value = "Test";

    //    // Act
    //    var result1 = sut.Value;
    //    sut.Value = "Test2";
    //    var result2 = sut.Value;

    //    // Assert
    //    result1.Should().Be("Test");
    //    result1.Should().NotBeSameAs(result2);
    //    result2.Should().Be("Test2");
    //}

    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Value_ForGivenValue_ReturnsCachedValue(
    //    DummyCachedWritableValueString sut
    //)
    //{
    //    // Arrange
    //    sut.Value = "Test2";

    //    // Act
    //    var result1 = sut.Value;
    //    var result2 = sut.Value;

    //    // Assert
    //    result1.Should().BeSameAs(result2);
    //    result1.Should().Be(result2);
    //    result1.Should().Be("Test2");
    //    result2.Should().Be("Test2");
    //}

    //public class DummyCachedWritableValueString : CachedWritableValue<string>
    //{
    //    private string _value;

    //    protected override string NonCachedValue => "InitValue";

    //    protected override void SaveValue(string value)
    //    {
    //        _value= value;
    //    }
    //}
}