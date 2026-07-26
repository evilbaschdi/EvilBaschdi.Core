using System.Runtime.InteropServices;
using EvilBaschdi.Core.Enums;

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
    ///     Application has to contain an app.manifest supporting windows 10.
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

    // Lazy-loaded property so the check only runs once
    /// <summary>
    ///     Gets the current platform.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public static PlatformKind CurrentPlatform => _currentPlatform ??= GetCurrentPlatform();

    private static PlatformKind? _currentPlatform;

    private static PlatformKind GetCurrentPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return PlatformKind.Windows;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return PlatformKind.Linux;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return PlatformKind.OSX;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            return PlatformKind.FreeBSD;
        }

        return PlatformKind.Unknown;
    }

    /// <summary>
    ///     OS is FreeBSD
    /// </summary>
    public static bool IsFreeBsd => CurrentPlatform == PlatformKind.FreeBSD;

    /// <summary>
    ///     OS is Linux
    /// </summary>
    public static bool IsLinux => CurrentPlatform == PlatformKind.Linux;

    /// <summary>
    ///     OS is OSX
    /// </summary>
    public static bool IsOSX => CurrentPlatform == PlatformKind.OSX;

    /// <summary>
    ///     OS is Windows
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsWindows => CurrentPlatform == PlatformKind.Windows;

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