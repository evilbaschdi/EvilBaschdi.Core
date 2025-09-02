using EvilBaschdi.Core.Security;

namespace EvilBaschdi.Core.Tests.Security;

public class EncryptionTests
{
    [Theory]
    [InlineData("Hello World", "SuperSecretKey")]
    [InlineData("Some other string", "AnotherSuperSecretKey")]
    public void EncryptAndDecrypt_ShouldReturnOriginalString(string clearText, string encryptionKey)
    {
        // Arrange
        var sut = new Encryption();

        // Act
        var encrypted = sut.EncryptString(clearText, encryptionKey);
        var decrypted = sut.DecryptString(encrypted, encryptionKey);

        // Assert
        decrypted.Should().Be(clearText);
    }
}