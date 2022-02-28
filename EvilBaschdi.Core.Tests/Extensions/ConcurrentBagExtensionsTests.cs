using System.Collections.Concurrent;
using AutoFixture.Idioms;
using EvilBaschdi.Core.Extensions;
using EvilBaschdi.Testing;
using FluentAssertions;
using Xunit;

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
        dummyConcurrentBag1.Count.Should().Be(2);
        dummyConcurrentBag1.Should().Contain(dummyString1);
        dummyConcurrentBag1.Should().Contain(dummyString2);
    }
}