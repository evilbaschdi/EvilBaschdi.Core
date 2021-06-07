using System;
using System.Threading.Tasks;
using EvilBaschdi.Core.Internal;

namespace EvilBaschdi.Core.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ICopyProgress copyProgress = new CopyProgress();
            ICopyDirectoryWithFilesWithProgress copyDirectoryWithFilesWithProgress = new CopyDirectoryWithFilesWithProgress(copyProgress);
            ICopyDirectoryWithProgress copyDirectoryWithProgress = new CopyDirectoryWithProgress(copyDirectoryWithFilesWithProgress, copyProgress);

            //await copyDirectory.RunForAsync(@"C:\temp\copy_source", @"C:\temp\copy_target");

            var t = 0d;
            IProgress<double> progress = new Progress<double>(increment =>
                                                              {
                                                                  t += increment;
                                                                  //Console.WriteLine($"t: {t}");
                                                                  Console.WriteLine($"increment: {increment}");
                                                              });


            await copyDirectoryWithProgress.RunForAsync(@"C:\Windows10Upgrade", @"C:\temp\copy_target", progress);


            //var directoryInfo = new DirectoryInfo(@"C:\temp\copy_source");

            //Console.WriteLine(directoryInfo.GetDirectorySize());
            Console.ReadLine();
        }
    }
}