using System.Windows.Media.Imaging;

namespace EvilBaschdi.Core.Model
{
    public class AboutWindowConfiguration
    {
        public string Title { get; set; }
        public string ProductName { get; set; }
        public string Copyright { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }

        public BitmapImage LogoSource { get; set; }
    }
}