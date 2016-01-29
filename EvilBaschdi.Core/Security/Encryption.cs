using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EvilBaschdi.Core.Security
{
    /// <summary>
    ///     encrypt and decrypt strings
    /// </summary>
    public class Encryption
    {
        /// <summary>
        ///     Encrypts the string.
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The IV.</param>
        /// <returns></returns>
        private static byte[] EncryptString(byte[] clearText, byte[] key, byte[] iv)
        {
            var memoryStream = new MemoryStream();
            var rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;
            var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(clearText, 0, clearText.Length);
            cryptoStream.Close();
            var encryptedData = memoryStream.ToArray();
            return encryptedData;
        }

        /// <summary>
        ///     Encrypts the string.
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="encryptionKey">The password.</param>
        /// <returns></returns>
        public static string EncryptString(string clearText, string encryptionKey)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            var encryptedData = EncryptString(clearBytes, rfc2898DeriveBytes.GetBytes(32),
                rfc2898DeriveBytes.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        ///     Decrypts the string.
        /// </summary>
        /// <param name="cipherData">The cipher data.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The IV.</param>
        /// <returns></returns>
        private static byte[] DecryptString(byte[] cipherData, byte[] key, byte[] iv)
        {
            var memoryStream = new MemoryStream();
            var rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;
            var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();
            var decryptedData = memoryStream.ToArray();
            return decryptedData;
        }

        /// <summary>
        ///     Decrypts the string.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="encryptionKey">The password.</param>
        /// <returns></returns>
        public static string DecryptString(string cipherText, string encryptionKey)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            var decryptedData = DecryptString(cipherBytes, rfc2898DeriveBytes.GetBytes(32),
                rfc2898DeriveBytes.GetBytes(16));
            return Encoding.Unicode.GetString(decryptedData);
        }
    }
}