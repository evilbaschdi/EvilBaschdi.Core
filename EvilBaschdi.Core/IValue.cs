namespace EvilBaschdi.Core
{
    /// <summary>
    ///     Generic Interface construct to encapsulate Classes without Interfaces
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<out T>
    {
        /// <summary>Value</summary>
        // ReSharper disable UnusedMemberInSuper.Global
        T Value { get; }
        // ReSharper restore UnusedMemberInSuper.Global
    }
}