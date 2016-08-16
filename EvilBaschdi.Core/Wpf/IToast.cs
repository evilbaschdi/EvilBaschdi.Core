namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    ///     Interface for toast message support.
    /// </summary>
    public interface IToast
    {
        /// <summary>
        ///     Show Message.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        void Show(string status, string message);
    }
}