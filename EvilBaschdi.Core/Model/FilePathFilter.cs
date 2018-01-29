using System.Collections.Generic;

namespace EvilBaschdi.Core.Model
{
    /// <summary>
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FilePathFilter
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="filterExtensionsToEqual"></param>
        /// <param name="filterExtensionsNotToEqual"></param>
        /// <param name="filterFileNamesToEqual"></param>
        /// <param name="filterFileNamesNotToEqual"></param>
        /// <param name="filterFilePathsToEqual"></param>
        /// <param name="filterFilePathsNotToEqual"></param>
        public FilePathFilter(List<string> filterExtensionsToEqual, List<string> filterExtensionsNotToEqual, List<string> filterFileNamesToEqual,
                              List<string> filterFileNamesNotToEqual, List<string> filterFilePathsToEqual, List<string> filterFilePathsNotToEqual)
        {
            FilterExtensionsToEqual = filterExtensionsToEqual;
            FilterExtensionsNotToEqual = filterExtensionsNotToEqual;
            FilterFileNamesToEqual = filterFileNamesToEqual;
            FilterFileNamesNotToEqual = filterFileNamesNotToEqual;
            FilterFilePathsToEqual = filterFilePathsToEqual;
            FilterFilePathsNotToEqual = filterFilePathsNotToEqual;
        }

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