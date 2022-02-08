namespace EvilBaschdi.Core;

/// <summary>
/// </summary>
/// <typeparam name="TIn"></typeparam>
// ReSharper disable once UnusedType.Global
public interface IRunFor<in TIn>
{
    /// <summary>
    /// </summary>
    void RunFor(TIn value);
}