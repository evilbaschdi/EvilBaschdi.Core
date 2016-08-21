using System;
using System.IO;
using System.Text;

namespace EvilBaschdi.Core.Logging
{
    /// <summary>
    ///     Does a File.AppendAllText by adding a headline to the file.
    /// </summary>
    public class AppendAllTextWithHeadline : IAppendAllTextWithHeadline
    {
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="headline"></param>
        public void For(string path, string contents, string headline)
        {
            if (!File.Exists(path))
            {
                File.AppendAllText(path, $"{headline}{Environment.NewLine}");
            }
            File.AppendAllText(path, contents);
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stringBuilder"></param>
        /// <param name="headline"></param>
        public void For(string path, StringBuilder stringBuilder, string headline)
        {
            For(path, stringBuilder.ToString(), headline);
        }
    }
}