using System.Security.Cryptography;
using System.Text;

namespace EvilBaschdi.Core.Security;

/// <inheritdoc />
/// <summary>
///     encrypt and decrypt strings
/// </summary>
// ReSharper disable once UnusedType.Global
public class Encryption : IEncryption
{
    /// <inheritdoc />
    public string EncryptString([NotNull] string clearText, [NotNull] string encryptionKey)
    {
        ArgumentNullException.ThrowIfNull(clearText);
        ArgumentNullException.ThrowIfNull(encryptionKey);

        var clearBytes = Encoding.Unicode.GetBytes(clearText);

        var encryptionKeySpan = encryptionKey.AsSpan();
        var salt = new ReadOnlySpan<byte>("EvilBaschdi.Core.Security"u8.ToArray());

        byte[] derivedKey16 = Rfc2898DeriveBytes.Pbkdf2(encryptionKeySpan, salt, 1000, HashAlgorithmName.SHA1, 16);
        byte[] derivedKey32 = Rfc2898DeriveBytes.Pbkdf2(encryptionKeySpan, salt, 1000, HashAlgorithmName.SHA1, 32);

        var encryptedData = EncryptString(clearBytes, derivedKey32, derivedKey16);

        return Convert.ToBase64String(encryptedData);
    }

    /// <inheritdoc />
    public string DecryptString([NotNull] string cipherText, [NotNull] string encryptionKey)
    {
        ArgumentNullException.ThrowIfNull(cipherText);
        ArgumentNullException.ThrowIfNull(encryptionKey);

        var cipherBytes = Convert.FromBase64String(cipherText);

        var encryptionKeySpan = encryptionKey.AsSpan();
        var salt = new ReadOnlySpan<byte>("EvilBaschdi.Core.Security"u8.ToArray());

        byte[] derivedKey16 = Rfc2898DeriveBytes.Pbkdf2(encryptionKeySpan, salt, 1000, HashAlgorithmName.SHA1, 16);
        byte[] derivedKey32 = Rfc2898DeriveBytes.Pbkdf2(encryptionKeySpan, salt, 1000, HashAlgorithmName.SHA1, 32);

        var decryptedData = DecryptString(cipherBytes, derivedKey32, derivedKey16);
        return Encoding.Unicode.GetString(decryptedData);
    }

    /// <summary>
    ///     Encrypts the string.
    /// </summary>
    /// <param name="clearText">The clear text.</param>
    /// <param name="key">The key.</param>
    /// <param name="iv">The IV.</param>
    /// <returns></returns>
    private static byte[] EncryptString([NotNull] byte[] clearText, [NotNull] byte[] key, [NotNull] byte[] iv)
    {
        ArgumentNullException.ThrowIfNull(clearText);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(iv);

        using var memoryStream = new MemoryStream();
        // ReSharper disable once IdentifierTypo
        using var aes = Aes.Create();
        aes.Key = key ?? throw new ArgumentNullException(nameof(key));
        aes.IV = iv ?? throw new ArgumentNullException(nameof(iv));
        var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(clearText, 0, clearText.Length);
        cryptoStream.Close();
        var encryptedData = memoryStream.ToArray();
        return encryptedData;
    }

    /// <summary>
    ///     Decrypts the string.
    /// </summary>
    /// <param name="cipherData">The cipher data.</param>
    /// <param name="key">The key.</param>
    /// <param name="iv">The IV.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    private static byte[] DecryptString([NotNull] byte[] cipherData, [NotNull] byte[] key, [NotNull] byte[] iv)
    {
        ArgumentNullException.ThrowIfNull(cipherData);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(iv);

        using var memoryStream = new MemoryStream();
        // ReSharper disable once IdentifierTypo
        using var aes = Aes.Create();
        aes.Key = key ?? throw new ArgumentNullException(nameof(key));
        aes.IV = iv ?? throw new ArgumentNullException(nameof(iv));
        var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(cipherData, 0, cipherData.Length);
        cryptoStream.Close();
        var decryptedData = memoryStream.ToArray();
        return decryptedData;
    }
}