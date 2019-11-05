using System;
using System.Linq;
using AutoFixture.Idioms;
using EvilBaschdi.Testing;
using Xunit;

namespace EvilBaschdi.Core.Tests
{
    public class CachedValueForTests
    {
        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void Constructor_HasNullGuards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(CachedValueForTestClass).GetConstructors());
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void Constructor_ReturnsI(CachedValueForTestClass sut)
        {
            Assert.IsAssignableFrom<ICachedValueFor<string, string>>(sut);
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void Methods_HaveNullGuards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(CachedValueForTestClass).GetMethods().Where(method => !method.IsAbstract));
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void ValueFor_DictionaryDoesNotContainKey_AddedToDictionary_ReturnsCachedValue(
            string key,
            CachedValueForTestClass sut)
        {
            // Arrange

            // Act
            var result = sut.ValueFor(key);

            // Assert
            Assert.Equal($"result = {key}", result);
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void ValueFor_DictionaryContainsKey_ReturnsSameValue(
            string key,
            CachedValueForTestClass sut)
        {
            // Arrange

            // Act
            var result1 = sut.ValueFor(key);
            var result2 = sut.ValueFor(key);

            // Assert
            Assert.Same(result2, result1);
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void ValueFor_DictionaryContainsKey_ResetsCache_ReturnsNotSameButEqualValue(
            string key,
            CachedValueForTestClass sut)
        {
            // Arrange

            // Act
            var result1 = sut.ValueFor(key);
            sut.ResetCache();
            var result2 = sut.ValueFor(key);

            // Assert
            Assert.NotSame(result2, result1);
            Assert.Equal(result2, result1);
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void ValueFor_ReturnsDefaultValue_CachesDefaultValue(
            string dummyValue)
        {
            // Arrange
            var sut = new CachedValueForTestClassReturningDefaultOfGuid(true);

            // Act
            var result1 = sut.ValueFor(dummyValue);
            var result2 = sut.ValueFor(dummyValue);


            // Assert            
            Assert.Equal(1, sut.CallCounter);
            Assert.Equal(result2, result1);
        }

        [Theory]
        [NSubstituteOmitAutoPropertiesTrueAutoData]
        public void ValueFor_ReturnsDefaultValue_DoesNotCacheDefaultValue(
            string dummyValue)
        {
            // Arrange
            var sut = new CachedValueForTestClassReturningDefaultOfGuid(false);

            // Act
            var result1 = sut.ValueFor(dummyValue);
            var result2 = sut.ValueFor(dummyValue);


            // Assert            
#pragma warning disable xUnit2005 // Do not use identity check on value type
            Assert.NotSame(result2, result1);
#pragma warning restore xUnit2005 // Do not use identity check on value type
            Assert.Equal(2, sut.CallCounter);
            Assert.Equal(result2, result1);
        }

        public class CachedValueForTestClass : CachedValueFor<string, string>
        {
            protected override string NonCachedValueFor(string value)
            {
                return $"result = {value}";
            }
        }

        public class CachedValueForTestClassReturningDefaultOfGuid : CachedValueFor<string, Guid>
        {
            public int CallCounter;

            public CachedValueForTestClassReturningDefaultOfGuid(bool cacheDefaultValues)
                : base(cacheDefaultValues)
            {
            }

            protected override Guid NonCachedValueFor(string value)
            {
                CallCounter++;
                return Guid.Empty;
            }
        }
    }
}