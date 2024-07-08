using System.Diagnostics;

namespace EvilBaschdi.Core.AppHelpers;

/// <inheritdoc cref="IValueFor{TIn,TOut}" />
/// <inheritdoc cref="IRunFor{TIn}" />
public interface IProcessByPath : IValueFor<string, Process>, IRunFor<string>;