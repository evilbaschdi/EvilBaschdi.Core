using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<MessageDialogResult> ShowMessage(string title, string message);

        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="dialogStyle"></param>
        /// <returns></returns>
        Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle dialogStyle);
    }
}