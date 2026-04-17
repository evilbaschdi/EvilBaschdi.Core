using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Tests.Extensions;

public class IntExtensionsTests
{
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(IntExtensions).GetConstructors());
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(IntExtensions).GetMethods().Where(method => !method.IsAbstract));
    }

    [Theory]
    [NSubstituteOmitAutoPropertiesTrueInlineAutoData(1, "one")]
    [NSubstituteOmitAutoPropertiesTrueInlineAutoData(10, "ten")]
    [NSubstituteOmitAutoPropertiesTrueInlineAutoData(2021, "two thousand twenty-one")]
    [NSubstituteOmitAutoPropertiesTrueInlineAutoData(-200, "minus two hundred")]
    [NSubstituteOmitAutoPropertiesTrueInlineAutoData(-66613, "minus sixty-six thousand six hundred thirteen")]
    [NSubstituteOmitAutoPropertiesTrueInlineAutoData(7653210, "seven million six hundred fifty-three thousand two hundred ten")]
    public void Value_ForProvidedInt_ReturnsString(
        int input,
        string output
    )
    {
        // Arrange

        // Act
        var result = input.ToWords();

        // Assert
        result.Should().Be(output);
    }
}