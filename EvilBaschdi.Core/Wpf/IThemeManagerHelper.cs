using System.Windows.Media;

namespace EvilBaschdi.Core.Wpf
{
    public interface IThemeManagerHelper
    {
        void CreateAppStyleBy(Color color, string accentName);
        void RegisterSysteColorTheme();
    }
}