using System;
using System.IO;
using System.Reflection;

namespace EvilBaschdi.Core.Extensions
{
    /// <summary>
    ///     Extension class to display change dates of current application.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        ///     Gets LinkerTime from assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly" /> is <see langword="null" />.</exception>
        public static DateTime GetLinkerTime(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return GetLinkerTime(assembly, null);
        }

        /// <summary>
        ///     Gets LinkerTime from assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly" /> is <see langword="null" />.</exception>
        private static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var filePath = assembly.Location;
            const int cPeHeaderOffset = 60;
            const int cLinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                stream.Read(buffer, 0, 2048);
            }

            var offset = BitConverter.ToInt32(buffer, cPeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + cLinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }
    }
}