﻿using System.Security.Cryptography;
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
    public string EncryptString(string clearText, string encryptionKey)
    {
        if (clearText == null)
        {
            throw new ArgumentNullException(nameof(clearText));
        }

        if (encryptionKey == null)
        {
            throw new ArgumentNullException(nameof(encryptionKey));
        }

        var clearBytes = Encoding.Unicode.GetBytes(clearText);
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey,
            // ReSharper disable once UseUtf8StringLiteral
#pragma warning disable IDE0230
            new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 },
#pragma warning restore IDE0230
            1000,
            HashAlgorithmName.SHA1);
        var encryptedData = EncryptString(clearBytes, rfc2898DeriveBytes.GetBytes(32),
            rfc2898DeriveBytes.GetBytes(16));
        return Convert.ToBase64String(encryptedData);
    }

    /// <inheritdoc />
    public string DecryptString(string cipherText, string encryptionKey)
    {
        if (cipherText == null)
        {
            throw new ArgumentNullException(nameof(cipherText));
        }

        if (encryptionKey == null)
        {
            throw new ArgumentNullException(nameof(encryptionKey));
        }

        var cipherBytes = Convert.FromBase64String(cipherText);
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey,
            // ReSharper disable once UseUtf8StringLiteral
#pragma warning disable IDE0230
            new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 },
#pragma warning restore IDE0230
            1000,
            HashAlgorithmName.SHA1);
        var decryptedData = DecryptString(cipherBytes, rfc2898DeriveBytes.GetBytes(32),
            rfc2898DeriveBytes.GetBytes(16));
        return Encoding.Unicode.GetString(decryptedData);
    }

    /// <summary>
    ///     Encrypts the string.
    /// </summary>
    /// <param name="clearText">The clear text.</param>
    /// <param name="key">The key.</param>
    /// <param name="iv">The IV.</param>
    /// <returns></returns>
    private static byte[] EncryptString(byte[] clearText, byte[] key, byte[] iv)
    {
        if (clearText == null)
        {
            throw new ArgumentNullException(nameof(clearText));
        }

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
    private static byte[] DecryptString(byte[] cipherData, byte[] key, byte[] iv)
    {
        if (cipherData == null)
        {
            throw new ArgumentNullException(nameof(cipherData));
        }

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