using System;
using System.Windows.Media.Imaging;
using EvilBaschdi.Core.Application;

namespace EvilBaschdi.Core.Wpf.ViewModel
{
    public class AboutViewModel
    {
        private readonly IAboutWindowContent _aboutWindowContent;

        public AboutViewModel(IAboutWindowContent aboutWindowContent)
        {
            if (aboutWindowContent == null)
            {
                throw new ArgumentNullException(nameof(aboutWindowContent));
            }
            _aboutWindowContent = aboutWindowContent;
        }


        public string Title => _aboutWindowContent.Value.Title;
        public string ProductName => _aboutWindowContent.Value.ProductName;
        public string Copyright => $"{_aboutWindowContent.Value.Copyright} by {_aboutWindowContent.Value.Company}";
        public string Company => _aboutWindowContent.Value.Company;

        public string Description => _aboutWindowContent.Value.Description;

        public string Version => $"Version: {_aboutWindowContent.Value.Version}";

        public BitmapImage LogoSource => _aboutWindowContent.Value.LogoSource;
    }
}