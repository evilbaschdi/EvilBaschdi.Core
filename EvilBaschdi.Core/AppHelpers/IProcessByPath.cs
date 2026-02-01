using System.Diagnostics;

namespace EvilBaschdi.Core.AppHelpers;

/// <summary>
///     Provides creation and execution helpers for <see cref="Process" /> instances
///     based on an executable or file system path.
///     Implementations are responsible for configuring and starting the process.
/// </summary>
public interface IProcessByPath :
    IValueFor<string, Process>,
    IValueFor2<string, string, Process>,
    IRunFor<string>,
    IRunFor2<string, string>;