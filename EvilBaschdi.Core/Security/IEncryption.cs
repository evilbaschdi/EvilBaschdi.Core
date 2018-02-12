namespace EvilBaschdi.Core.Security
{
    /// <summary>
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        ///     Decrypts a string
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="encryptionKey">The password.</param>
        /// <returns></returns>
        string DecryptString(string cipherText, string encryptionKey);

        /// <summary>
        ///     Encrypts a string
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="encryptionKey">The password.</param>
        /// <returns></returns>
        string EncryptString(string clearText, string encryptionKey);
    }
}