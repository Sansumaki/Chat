using System.Windows;
using System.Windows.Controls;

namespace Chat.Utilities
{
    public static class Extensions
    {
        public static void ScrollIntoView(this ItemsControl control, object item)
        {
            if (!(control.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement framework)) { return; }
            framework.BringIntoView();
        }

        public static void ScrollIntoView(this ItemsControl control)
        {
            var count = control.Items.Count;
            if (count == 0) { return; }
            var item = control.Items[count - 1];
            control.ScrollIntoView(item);
        }
    }
}
