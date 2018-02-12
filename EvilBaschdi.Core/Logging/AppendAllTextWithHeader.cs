using System;
using System.IO;
using System.Text;

namespace EvilBaschdi.Core.Logging
{
    /// <inheritdoc />
    /// <summary>
    ///     Does a File.AppendAllText by adding a headline to the file.
    /// </summary>
    public class AppendAllTextWithHeadline : IAppendAllTextWithHeadline
    {
        /// <inheritdoc />
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="headline"></param>
        public void RunFor(string path, string contents, string headline)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (contents == null)
            {
                throw new ArgumentNullException(nameof(contents));
            }

            if (headline == null)
            {
                throw new ArgumentNullException(nameof(headline));
            }

            if (!File.Exists(path))
            {
                File.AppendAllText(path, $"{headline}{Environment.NewLine}");
            }

            File.AppendAllText(path, contents);
        }

        /// <inheritdoc />
        /// <param name="path"></param>
        /// <param name="stringBuilder"></param>
        /// <param name="headline"></param>
        public void RunFor(string path, StringBuilder stringBuilder, string headline)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            if (headline == null)
            {
                throw new ArgumentNullException(nameof(headline));
            }

            RunFor(path, stringBuilder.ToString(), headline);
        }
    }
}