﻿using System.Collections.Generic;
using EvilBaschdi.Core.Model;

namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc cref="IValueFor{TIn, TOut}" />
    /// <inheritdoc cref="IValueFor2{TIn1, TIn2, TOut}" />
    public interface IFileListFromPath : IValueFor2<string, FileListFromPathFilter, List<string>>, IValueFor<string, List<string>>
    {
        /// <summary>
        ///     Gets a list of accessible directories that contain files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMemberInSuper.Global
        IEnumerable<string> GetSubdirectoriesContainingOnlyFiles(string path);
    }
}