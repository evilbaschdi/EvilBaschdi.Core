using System;
using System.Windows.Forms;
using System.Windows.Interop;
using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Application
{
    /// <inheritdoc />
    public class CurrentScreen : ICurrentScreen
    {
        /// <inheritdoc />
        /// <param name="metroWindow"></param>
        /// <returns></returns>
        public string ValueFor(MetroWindow metroWindow)
        {
            if (metroWindow == null)
            {
                throw new ArgumentNullException(nameof(metroWindow));
            }
            var screen = Screen.FromHandle(new WindowInteropHelper(metroWindow).Handle);
            return screen.DeviceName;
        }
    }
}