using EvilBaschdi.Core.Internal;

namespace EvilBaschdi.Core.Tests.Internal;

public class ValidateValueTests
{
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ValidateValue).GetConstructors());
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_ReturnsInterfaceName(ValidateValue sut)
    {
        sut.Should().BeAssignableTo<IValidateValue>();
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ValidateValue).GetMethods().Where(method => !method.IsAbstract));
    }
}