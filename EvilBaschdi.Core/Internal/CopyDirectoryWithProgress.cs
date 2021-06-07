using System;
using System.IO;
using System.Threading.Tasks;
using EvilBaschdi.Core.Extensions;
using JetBrains.Annotations;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class CopyDirectoryWithProgress : ICopyDirectoryWithProgress
    {
        private readonly ICopyDirectoryWithFilesWithProgress _copyDirectoryWithFilesWithProgress;
        private readonly ICopyProgress _copyProgress;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="copyDirectoryWithFilesWithProgress"></param>
        /// <param name="copyProgress"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CopyDirectoryWithProgress([NotNull] ICopyDirectoryWithFilesWithProgress copyDirectoryWithFilesWithProgress, [NotNull] ICopyProgress copyProgress)
        {
            _copyDirectoryWithFilesWithProgress = copyDirectoryWithFilesWithProgress ?? throw new ArgumentNullException(nameof(copyDirectoryWithFilesWithProgress));
            _copyProgress = copyProgress ?? throw new ArgumentNullException(nameof(copyProgress));
        }

        /// <inheritdoc />
        public async Task RunForAsync([NotNull] string source, [NotNull] string target, [NotNull] IProgress<double> progress)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (progress == null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            var diSource = new DirectoryInfo(source);
            var diTarget = new DirectoryInfo(target);

            _copyProgress.TotalSize = diSource.GetDirectorySize();

            await _copyDirectoryWithFilesWithProgress.RunForAsync(diSource, diTarget, progress);
        }
    }
}