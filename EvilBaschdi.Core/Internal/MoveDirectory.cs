using System;
using System.IO;
using System.Linq;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    public class MoveDirectory : IMoveDirectory
    {
        /// <inheritdoc />
        public void RunFor(string source, string target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (!Directory.Exists(target))
            {
                Directory.Move(source, target);
            }
            else
            {
                var sourcePath = source.TrimEnd('\\', ' ');
                var targetPath = target.TrimEnd('\\', ' ');
                var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories).GroupBy(Path.GetDirectoryName);
                foreach (var folder in files)
                {
                    var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                    Directory.CreateDirectory(targetFolder);
                    foreach (var file in folder)
                    {
                        if (file == null) continue;
                        var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                        if (File.Exists(targetFile))
                        {
                            File.Delete(targetFile);
                        }

                        File.Move(file, targetFile);
                    }
                }

                Directory.Delete(source, true);
            }
        }
    }
}