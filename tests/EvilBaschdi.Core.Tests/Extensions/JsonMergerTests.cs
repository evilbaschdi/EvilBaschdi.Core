using EvilBaschdi.Core.Extensions;
using Newtonsoft.Json.Linq;

namespace EvilBaschdi.Core.Tests.Extensions;

public class JsonMergerTests
{
    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(JsonMerger).GetConstructors());
    }

    [Theory, NSubstituteOmitAutoPropertiesTrueAutoData]
    public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(JsonMerger).GetMethods().Where(method => !method.IsAbstract));
    }

    [Fact]
    public void CustomMerge_ForProvidedObjects_MergesTheirContent()
    {
        // Arrange
        var dummyPreJObject = JObject.Parse("""
                                            {
                                                    "name": "Alice",
                                                    "age": 30,
                                                    "address": {
                                                            "city": "Wonderland",
                                                            "zip": "12345"
                                                        },
                                                    "hobbies": ["reading", "chess"]
                                                }
                                            """);

        var dummyTargetJObject = JObject.Parse("""
                                               {
                                                       "name": "Bob",
                                                       "age": 25,
                                                       "address": {
                                                               "city": "Builderland",
                                                               "street": "456 Construction Ave"
                                                           },
                                                       "hobbies": ["chess", "cycling"]
                                                   }
                                               """);

        // Act
        var result = JsonMerger.CustomMerge(dummyPreJObject, dummyTargetJObject);

        // Assert
        var expectedJObject = JObject.Parse("""
                                            {
                                                    "name": "Bob",
                                                    "age": 25,
                                                    "address": {
                                                            "city": "Builderland",
                                                            "zip": "12345",
                                                            "street": "456 Construction Ave"
                                                        },
                                                    "hobbies": ["reading", "chess", "cycling"]
                                                }
                                            """);
        result.Should().BeEquivalentTo(expectedJObject);
    }
}