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

            var excludeExtensionList = new List<string>
                                       {
                                           "ics",
                                           "sam"
                                       };

            var excludeFileNameList = new List<string>
                                      {
                                          "listfilesbydate_log_"
                                      };

            var includeFilePaths = new List<string>
                                   {
                                       "mach2_0.3.0.0_x64"
                                   };

            var excludeFilePaths = new List<string>
                                   {
                                       "sdk"
                                   };

            var filePathFilter = new FileListFromPathFilter
                                 {
                                     FilterExtensionsNotToEqual = excludeExtensionList,
                                     FilterFileNamesNotToEqual = excludeFileNameList,
                                     FilterFilePathsToEqual = includeFilePaths,
                                     FilterFilePathsNotToEqual = excludeFilePaths
                                 };

            // Act
            var result = sut.ValueFor(@"C:\temp", filePathFilter);

            // Assert
            result.Should().HaveCount(8);
        }

        [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
        public void GetSubdirectoriesContainingOnlyFiles_WithoutFilter_Result(
            FileListFromPath sut)
        {
            // Arrange

            // Act
            var result = sut.GetSubdirectoriesContainingOnlyFiles(@"C:\temp");

            // Assert
            result.Should().HaveCount(2);
        }
    }
}