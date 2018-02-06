using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Views.Utilerias
{
    public static class PasswordBoxUtil
    {
        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof(bool), typeof(PasswordBoxUtil),
                new PropertyMetadata(false, (s, a) => TrySet((PasswordBox)s, (bool)a.NewValue)));

        public static void SetSelectAllOnFocus(PasswordBox elem, bool val) => elem.SetValue(SelectAllOnFocusProperty, val);
        public static bool GetSelectAllOnFocus(PasswordBox elem) => (bool)elem.GetValue(SelectAllOnFocusProperty);

        private static void TrySet(PasswordBox textBox, bool value)
        {
            if (value)
            {
                textBox.GotKeyboardFocus += async (s, a) =>
                {
                    await Task.Delay(20).ConfigureAwait(true); // Quite ugly but required to make it work
                    textBox.SelectAll();
                };
            }
        }
    }
}
