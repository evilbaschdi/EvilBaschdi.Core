using System.Collections.Concurrent;
using EvilBaschdi.Core.Extensions;

namespace EvilBaschdi.Core.Tests.Extensions;

public class ConcurrentBagExtensionsTests
{
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(ConcurrentBagExtensions).GetConstructors());
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void AddRange_ForProvidedConcurrentBag_AddsEachItemInListToConcurrentBag(
        ConcurrentBag<string> dummyConcurrentBag1,
        ConcurrentBag<string> dummyConcurrentBag2,
        string dummyString1,
        string dummyString2)
    {
        // Arrange
        dummyConcurrentBag1.Add(dummyString1);
        dummyConcurrentBag2.Add(dummyString2);
        dummyConcurrentBag1.AddRange(dummyConcurrentBag2);

        // Act

        // Assert
        dummyConcurrentBag1.Should().HaveCount(2);
        dummyConcurrentBag1.Should().Contain(dummyString1);
        dummyConcurrentBag1.Should().Contain(dummyString2);
    }
}