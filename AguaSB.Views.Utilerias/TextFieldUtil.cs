using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace AguaSB.Views.Utilerias
{
    public static class TextFieldUtil
    {
        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof(bool), typeof(TextFieldUtil),
                new PropertyMetadata(false, (s, a) => TrySet((TextBoxBase)s, (bool)a.NewValue)));

        public static void SetSelectAllOnFocus(TextBoxBase elem, bool val) => elem.SetValue(SelectAllOnFocusProperty, val);
        public static bool GetSelectAllOnFocus(TextBoxBase elem) => (bool)elem.GetValue(SelectAllOnFocusProperty);

        private static void TrySet(TextBoxBase textBox, bool value)
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
