using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Application
{
    /// <inheritdoc />
    public class AboutWindowContent : IAboutWindowContent
    {
        private readonly Assembly _assembly;
        private readonly BitmapImage _logoSource;

        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="logoSource"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AboutWindowContent(Assembly assembly, BitmapImage logoSource)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            _logoSource = logoSource ?? throw new ArgumentNullException(nameof(logoSource));
        }

        /// <inheritdoc />
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