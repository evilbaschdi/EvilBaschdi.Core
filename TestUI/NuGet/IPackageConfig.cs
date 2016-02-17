namespace EvilBaschdi.TestUI.NuGet
{
    public interface IPackageConfig
    {
        PackageCollection Read(string path);
        string Version(string id, PackageCollection collection);
        string TargetFramework(string id, PackageCollection collection);

        PackageCollection SetVersion(string id, string version, PackageCollection collection);

        void Write(string path, PackageCollection collection);
    }
}