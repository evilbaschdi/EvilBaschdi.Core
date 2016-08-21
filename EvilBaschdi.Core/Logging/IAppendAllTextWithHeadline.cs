using System.Text;

namespace EvilBaschdi.Core.Logging
{
    /// <summary>
    ///     Does a File.AppendAllText by adding a headline to the file.
    /// </summary>
    public interface IAppendAllTextWithHeadline
    {
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="headline"></param>
        void For(string path, string contents, string headline);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stringBuilder"></param>
        /// <param name="headline"></param>
        void For(string path, StringBuilder stringBuilder, string headline);
    }
}