using EvilBaschdi.Core.Internal;

namespace EvilBaschdi.Core.Tests.Internal;

public class ReadKeyFromConsoleTests
{
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ReadKeyFromConsole).GetConstructors());
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_ReturnsInterfaceName(ReadKeyFromConsole sut)
    {
        sut.Should().BeAssignableTo<IReadKeyFromConsole>();
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ReadKeyFromConsole).GetMethods().Where(method => !method.IsAbstract));
    }
}