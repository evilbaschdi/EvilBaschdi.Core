using System;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly MetroWindow _mainWindow;

        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="mainWindow"></param>
        public DialogService(MetroWindow mainWindow)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }
            _mainWindow = mainWindow;
        }

        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<MessageDialogResult> ShowMessage(string title, string message)
        {
            var options = new MetroDialogSettings
                          {
                              ColorScheme = MetroDialogColorScheme.Accented
                          };

            _mainWindow.MetroDialogOptions = options;
            return await _mainWindow.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, options);
        }

        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="dialogStyle"></param>
        /// <returns></returns>
        public async Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle dialogStyle)
        {
            _mainWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            return await _mainWindow.ShowMessageAsync(title, message, dialogStyle, _mainWindow.MetroDialogOptions);
        }
    }
}