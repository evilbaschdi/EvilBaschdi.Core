using System.Linq;
using System.Windows.Forms;
using System.Windows.Interop;
using EvilBaschdi.Core.DotNetExtensions;
using MahApps.Metro.Controls;

namespace EvilBaschdi.Core.Application
{
    /// <summary>
    ///     Class that provides the count of current connected screens of the current device / session.
    /// </summary>
    public class ScreenCount : IScreenCount
    {
        /// <summary>
        ///     Count of current connected screens.
        /// </summary>
        public int Value => Screen.AllScreens.Length;
    }

    /// <summary>
    /// </summary>
    public interface ICurrentScreen : IValueFor<MetroWindow, string>
    {
    }

    /// <summary>
    /// </summary>
    public class CurrentScreen : ICurrentScreen
    {
        /// <summary>
        /// </summary>
        /// <param name="metroWindow"></param>
        /// <returns></returns>
        public string ValueFor(MetroWindow metroWindow)
        {
            var screen = Screen.FromHandle(new WindowInteropHelper(metroWindow).Handle);
            return screen.DeviceName;
        }
    }

    /// <summary>
    /// </summary>
    public interface IMoveToScreen : IRunFor2<MetroWindow, string>
    {
    }

    /// <summary>
    /// </summary>
    public class MoveToScreen : IMoveToScreen
    {
        /// <summary>
        /// </summary>
        /// <param name="metroWindow"></param>
        /// <param name="deviceName"></param>
        public void RunFor(MetroWindow metroWindow, string deviceName)
        {
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