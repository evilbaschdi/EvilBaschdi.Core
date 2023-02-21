using EvilBaschdi.Core.AppHelpers;

namespace EvilBaschdi.Core.Tests.AppHelpers;

public class ProcessByPathTests
{
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ProcessByPath).GetConstructors());
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_ReturnsInterfaceName(ProcessByPath sut)
    {
        sut.Should().BeAssignableTo<IProcessByPath>();
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ProcessByPath).GetMethods().Where(method => !method.IsAbstract));
    }
}