using System.Linq;
using AutoFixture.Idioms;
using EvilBaschdi.Core.Extensions;
using EvilBaschdi.Testing;
using FluentAssertions;
using Xunit;

namespace EvilBaschdi.Core.Tests.Extensions
{
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
        [NSubstituteOmitAutoPropertiesTrueInlineAutoData(2021, "two thousand and twenty-one")]
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
}