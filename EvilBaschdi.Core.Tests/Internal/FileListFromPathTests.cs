using System.Collections.Generic;
using System.Linq;
using AutoFixture.Idioms;
using EvilBaschdi.Core.Internal;
using EvilBaschdi.Core.Model;
using EvilBaschdi.Testing;
using FluentAssertions;
using Xunit;

namespace EvilBaschdi.Core.Tests.Internal
{
    public class FileListFromPathTests
    {
        [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
        public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(FileListFromPath).GetConstructors());
        }

        [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
        public void Constructor_ReturnsInterfaceName(FileListFromPath sut)
        {
            sut.Should().BeAssignableTo<IFileListFromPath>();
        }

        [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
        public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(FileListFromPath).GetMethods().Where(method => !method.IsAbstract));
        }

        [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
        public void ValueFor_WithFilters_Result(
            FileListFromPath sut)
        {
            // Arrange
            var filePathFilter = new FileListFromPathFilter
                                 {
                                     FilterExtensionsToEqual = new List<string>
                                                               {
                                                                   "txt"
                                                               }
                                 };

            // Act
            var result = sut.ValueFor(@"C:\temp", filePathFilter);

            // Assert
            result.Should().HaveCount(5);
        }

        [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
        public void GetSubdirectoriesContainingOnlyFiles_WithoutFilter_Result(
            FileListFromPath sut)
        {
            // Arrange
            const string userDir = @"C:\Symbols";

            // Act
            var result = sut.GetSubdirectoriesContainingOnlyFiles(userDir);

            // Assert
            result.Should().HaveCount(18);
        }
    }
}