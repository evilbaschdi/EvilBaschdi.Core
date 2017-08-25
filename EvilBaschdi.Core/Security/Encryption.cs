using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EvilBaschdi.Core.Security
{
    /// <inheritdoc />
    /// <summary>
    ///     encrypt and decrypt strings
    /// </summary>
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
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
        private byte[] EncryptString(byte[] clearText, byte[] key, byte[] iv)
        {
            if (clearText == null)
            {
                throw new ArgumentNullException(nameof(clearText));
            }

            var memoryStream = new MemoryStream();
            var rijndael = Rijndael.Create();
            rijndael.Key = key ?? throw new ArgumentNullException(nameof(key));
            rijndael.IV = iv ?? throw new ArgumentNullException(nameof(iv));
            var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
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
        private byte[] DecryptString(byte[] cipherData, byte[] key, byte[] iv)
        {
            if (cipherData == null)
            {
                throw new ArgumentNullException(nameof(cipherData));
            }

            var memoryStream = new MemoryStream();
            var rijndael = Rijndael.Create();
            rijndael.Key = key ?? throw new ArgumentNullException(nameof(key));
            rijndael.IV = iv ?? throw new ArgumentNullException(nameof(iv));
            var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();
            var decryptedData = memoryStream.ToArray();
            return decryptedData;
        }
    }
}