using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EvilBaschdi.TestUI.NuGet
{
    public class PackageConfig : IPackageConfig
    {
        public PackageCollection Read(string path)
        {
            var serializer = new XmlSerializer(typeof(PackageCollection));
            var reader = new StreamReader(path);
            var packageCollection = (PackageCollection) serializer.Deserialize(reader);
            reader.Close();
            return packageCollection;
        }

        public string Version(string id, PackageCollection collection)
        {
            return collection.Packages.All(package => package.Id != id) ? string.Empty : collection.Packages.First(package => package.Id == id).Version;
        }

        public string TargetFramework(string id, PackageCollection collection)
        {
            return collection.Packages.All(package => package.Id != id) ? string.Empty : collection.Packages.First(package => package.Id == id).TargetFramework;
        }

        public PackageCollection SetVersion(string id, string version, PackageCollection collection)
        {
            foreach (var package in collection.Packages.Where(pkg => pkg.Id == id))
            {
                package.Version = version;
            }
            return collection;
        }

        public void Write(string path, PackageCollection collection)
        {
            File.Delete(path);
            var xmlWriterSettings = new XmlWriterSettings
                                    {
                                        Indent = true,
                                        OmitXmlDeclaration = false,
                                        Encoding = Encoding.UTF8
                                    };
            var serializer = new XmlSerializer(typeof(PackageCollection));
            var xmlWriter = XmlWriter.Create(path, xmlWriterSettings);
            var ns = new XmlSerializerNamespaces();

            ns.Add("", "");
            serializer.Serialize(xmlWriter, collection, ns);
        }
    }
}