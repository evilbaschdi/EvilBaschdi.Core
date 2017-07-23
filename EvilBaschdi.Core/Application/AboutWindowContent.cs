using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Application
{
    public class AboutWindowContent : IAboutWindowContent
    {
        private readonly Assembly _assembly;
        private readonly BitmapImage _logoSource;

        public AboutWindowContent(Assembly assembly, BitmapImage logoSource)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            if (logoSource == null)
            {
                throw new ArgumentNullException(nameof(logoSource));
            }
            _assembly = assembly;
            _logoSource = logoSource;
        }

        public AboutWindowConfiguration Value => new AboutWindowConfiguration
                                                 {
                                                     Title = _assembly.GetCustomAttributes<AssemblyTitleAttribute>().First().Title,
                                                     ProductName = _assembly.GetCustomAttributes<AssemblyProductAttribute>().First().Product,
                                                     Copyright = _assembly.GetCustomAttributes<AssemblyCopyrightAttribute>().First().Copyright,
                                                     Company = _assembly.GetCustomAttributes<AssemblyCompanyAttribute>().First().Company,
                                                     Description = _assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().First().Description,
                                                     Version = _assembly.GetName().Version.ToString(),
                                                     LogoSource = _logoSource
                                                 };
    }
}