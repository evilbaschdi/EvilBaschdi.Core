namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    ///     Generic Interface construct to encapsulate Classes without Interfaces
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<T>
    {
        /// <summary>Value</summary>
        T Value { get; }
    }
}