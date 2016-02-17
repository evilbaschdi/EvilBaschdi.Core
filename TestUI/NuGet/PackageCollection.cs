using System;
using System.Xml.Serialization;

namespace EvilBaschdi.TestUI.NuGet
{
    [Serializable]
    [XmlRoot("packages")]
    public class PackageCollection
    {
        [XmlElement("package")]
        public Package[] Packages { get; set; }
    }
}