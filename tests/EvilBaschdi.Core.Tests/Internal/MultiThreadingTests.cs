using System.Collections.Concurrent;
using EvilBaschdi.Core.Internal;

namespace EvilBaschdi.Core.Tests.Internal;

public class MultiThreadingTests
{
    [Fact]
    public void RunFor_ShouldExecuteActionForAllItems()
    {
        // Arrange
        var sut = new MultiThreading();
        var list = Enumerable.Range(0, 100).ToList();
        var concurrentBag = new ConcurrentBag<int>();

        // Act
        sut.RunFor(list, range =>
                         {
                             for (var i = range.Item1; i < range.Item2; i++)
                             {
                                 concurrentBag.Add(list[i]);
                             }
                         });

        // Assert
        concurrentBag.Should().HaveCount(list.Count);
        concurrentBag.Should().BeEquivalentTo(list);
    }
}