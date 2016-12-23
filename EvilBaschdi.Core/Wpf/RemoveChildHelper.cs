using System.Windows;
using System.Windows.Controls;

namespace EvilBaschdi.Core.Wpf
{
    /// <summary>
    /// </summary>
    public static class RemoveChildHelper
    {
        /// <summary>
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void RemoveChild(this DependencyObject parent, UIElement child)
        {
            var panel = parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(child);
                return;
            }

            var decorator = parent as Decorator;
            if (decorator != null)
            {
                if (Equals(decorator.Child, child))
                {
                    decorator.Child = null;
                }
                return;
            }

            var contentPresenter = parent as ContentPresenter;
            if (contentPresenter != null)
            {
                if (Equals(contentPresenter.Content, child))
                {
                    contentPresenter.Content = null;
                }
                return;
            }

            var contentControl = parent as ContentControl;
            if (contentControl != null)
            {
                if (Equals(contentControl.Content, child))
                {
                    contentControl.Content = null;
                }
            }

            // maybe more
        }
    }
}