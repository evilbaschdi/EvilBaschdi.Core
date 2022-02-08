using System;
using System.Threading.Tasks;
using EvilBaschdi.Core.Internal;

namespace EvilBaschdi.Core.TestConsole;

// ReSharper disable once ArrangeTypeModifiers
// ReSharper disable once ClassNeverInstantiated.Global
class Program
{
    // ReSharper disable once UnusedParameter.Local
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        ICopyProgress copyProgress = new CopyProgress();
        ICopyDirectoryWithFilesWithProgress copyDirectoryWithFilesWithProgress = new CopyDirectoryWithFilesWithProgress(copyProgress);
        ICopyDirectoryWithProgress copyDirectoryWithProgress = new CopyDirectoryWithProgress(copyDirectoryWithFilesWithProgress, copyProgress);

        //await copyDirectory.RunForAsync(@"C:\temp\copy_source", @"C:\temp\copy_target");

        var t = 0d;

        copyProgress.Progress = new Progress<double>(increment =>
                                                     {
                                                         t += increment;
                                                         //Console.WriteLine($"t: {t}");
                                                         Console.WriteLine($"increment: {increment}");
                                                     });

        await copyDirectoryWithProgress.RunForAsync(@"C:\Windows10Upgrade", @"C:\temp\copy_target");

        //var directoryInfo = new DirectoryInfo(@"C:\temp\copy_source");

        Console.WriteLine(t);
        Console.ReadLine();
    }
}