using System.Windows.Media.Imaging;

namespace EvilBaschdi.Core.Model
{
    /// <summary>
    /// </summary>
    public class AboutWindowConfiguration
    {
        /// <summary>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// </summary>
        public BitmapImage LogoSource { get; set; }
    }
}