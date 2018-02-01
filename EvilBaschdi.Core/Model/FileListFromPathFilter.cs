using System.Collections.Generic;

namespace EvilBaschdi.Core.Model
{
    /// <summary>
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileListFromPathFilter
    {
        /// <summary>
        /// </summary>
        public List<string> FilterExtensionsToEqual { get; set; }

        /// <summary>
        /// </summary>
        public List<string> FilterExtensionsNotToEqual { get; set; }

        /// <summary>
        /// </summary>
        public List<string> FilterFileNamesToEqual { get; set; }

        /// <summary>
        /// </summary>
        public List<string> FilterFileNamesNotToEqual { get; set; }

        /// <summary>
        /// </summary>
        public List<string> FilterFilePathsToEqual { get; set; }

        /// <summary>
        /// </summary>
        public List<string> FilterFilePathsNotToEqual { get; set; }
    }
}