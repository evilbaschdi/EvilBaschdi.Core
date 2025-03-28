﻿using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global

namespace EvilBaschdi.Core.Extensions;

/// <summary>
///     Get if Windows Version is vista or higher.
/// </summary>
// ReSharper disable once UnusedType.Global
public static class VersionHelper
{
    /// <summary>
    ///     Gets the real OS Version.
    ///     Application has to contain a app.manifest supporting windows 10.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static string GetWindowsClientVersion
    {
        get
        {
            var major = Environment.OSVersion.Version.Major;
            var minor = Environment.OSVersion.Version.Minor;
            var build = Environment.OSVersion.Version.Build;

            // ReSharper disable once SwitchStatementMissingSomeCases
            return major switch
            {
                10 when minor == 0 && build is >= 10240 and < 22000 => "Win10",
                10 when minor == 0 && build >= 22000 => "Win11",
                _ => "Can not find windows version.",
            };
        }
    }

    /// <summary>
    ///     OS is FreeBSD
    /// </summary>
    public static bool IsFreeBsd => RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);

    /// <summary>
    ///     OS is Linux
    /// </summary>
    public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    /// <summary>
    ///     OS is OSX
    /// </summary>
    public static bool IsOsX => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    /// <summary>
    ///     OS is Windows
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    /// <summary>
    ///     OS is Windows 10.
    /// </summary>
    /// <returns></returns>
    public static bool IsWindows10 => IsWindows && GetWindowsClientVersion.StartsWith("Win10");

    /// <summary>
    ///     OS is Windows 11.
    /// </summary>
    /// <returns></returns>
    public static bool IsWindows11 => IsWindows && GetWindowsClientVersion.StartsWith("Win11");
}