using System;
using System.Windows.Media.Imaging;
using EvilBaschdi.Core.Application;

namespace EvilBaschdi.Core.Wpf.ViewModel
{
    /// <summary>
    /// </summary>
    public class AboutViewModel
    {
        private readonly IAboutWindowContent _aboutWindowContent;

        /// <summary>
        /// </summary>
        /// <param name="aboutWindowContent"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AboutViewModel(IAboutWindowContent aboutWindowContent)
        {
            _aboutWindowContent = aboutWindowContent ?? throw new ArgumentNullException(nameof(aboutWindowContent));
        }

        /// <summary>
        /// </summary>
        public string Title => _aboutWindowContent.Value.Title;

        /// <summary>
        /// </summary>
        public string ProductName => _aboutWindowContent.Value.ProductName;

        /// <summary>
        /// </summary>
        public string Copyright => $"{_aboutWindowContent.Value.Copyright} by {_aboutWindowContent.Value.Company}";

        /// <summary>
        /// </summary>
        public string Company => _aboutWindowContent.Value.Company;

        /// <summary>
        /// </summary>
        public string Description => _aboutWindowContent.Value.Description;

        /// <summary>
        /// </summary>

        public string Version => $"Version: {_aboutWindowContent.Value.Version}";

        /// <summary>
        /// </summary>

        public BitmapImage LogoSource => _aboutWindowContent.Value.LogoSource;
    }
}