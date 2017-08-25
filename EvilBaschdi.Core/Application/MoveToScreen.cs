using System;
using System.Linq;
using System.Windows.Forms;
using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Application
{
    /// <inheritdoc />
    public class MoveToScreen : IMoveToScreen
    {
        /// <inheritdoc />
        /// <param name="metroWindow"></param>
        /// <param name="deviceName"></param>
        public void RunFor(MetroWindow metroWindow, string deviceName)
        {
            if (metroWindow == null)
            {
                throw new ArgumentNullException(nameof(metroWindow));
            }
            if (deviceName == null)
            {
                throw new ArgumentNullException(nameof(deviceName));
            }
            var targetScreen = Screen.AllScreens.FirstOrDefault(screen => screen.DeviceName == deviceName);

            if (targetScreen != null)
            {
                var workingArea = targetScreen.WorkingArea;

                metroWindow.Left = workingArea.Left + (workingArea.Width - metroWindow.Width) / 2;
                metroWindow.Top = workingArea.Top + (workingArea.Height - metroWindow.Height) / 2;
            }
        }
    }
}