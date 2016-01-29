using System;
using System.Xml.Serialization;

namespace EvilBaschdi.TestUI
{
    [Serializable]
    [XmlRoot("packages")]
    public class PackageCollection
    {
        [XmlElement("package")]
        public Package[] Packages { get; set; }
    }
}