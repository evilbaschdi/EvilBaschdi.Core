using EvilBaschdi.Core.Internal;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Tests.Internal;

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

    //[Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    //public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    //{
    //    assertion.Verify(typeof(FileListFromPath).GetMethods().Where(method => !method.IsAbstract));
    //}

/*
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void ValueFor_WithFilters_Result(
        FileListFromPath sut)
    {
        // Arrange
        var filePathFilter = new FileListFromPathFilter
                             {
                                 FilterExtensionsToEqual = ["txt"]
                             };

        // Act
        var result = sut.ValueFor(@"C:\temp", filePathFilter);

        // Assert
        result.Should().HaveCount(5);
    }
    */

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void GetSubdirectoriesContainingOnlyFiles_WithoutFilter_Result(
        FileListFromPath sut)
    {
        // Arrange
        const string dir = @"C:\Windows";

        // Act
        var result = sut.GetSubdirectoriesContainingOnlyFiles(dir);

        // Assert
        result.Should().NotBeNull();
    }
}