using System.Windows;
using System.Windows.Input;

namespace AguaSB.Views.Utilerias
{
    public static class FocusNext
    {
        public static readonly DependencyProperty EnableFocusNextProperty =
            DependencyProperty.RegisterAttached("EnableFocusNext", typeof(bool), typeof(FocusNext),
                new PropertyMetadata(false, (s, a) => TryEnable((UIElement)s, (bool)a.NewValue)));

        public static void SetEnableFocusNext(UIElement elem, bool val) => elem.SetValue(EnableFocusNextProperty, val);
        public static bool GetEnableFocusNext(UIElement elem) => (bool)elem.GetValue(EnableFocusNextProperty);

        public static readonly DependencyProperty FocusNextKeyProperty =
            DependencyProperty.RegisterAttached("FocusNextKey", typeof(Key), typeof(FocusNext), new PropertyMetadata(Key.Enter));

        public static void SetFocusNextKey(UIElement elem, Key val) => elem.SetValue(FocusNextKeyProperty, val);
        public static Key GetFocusNextKey(UIElement elem) => (Key)elem.GetValue(FocusNextKeyProperty);

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target", typeof(UIElement), typeof(FocusNext),
                new PropertyMetadata(null, (s, a) => TryEnable((UIElement)s, a.NewValue != null)));

        public static void SetTarget(UIElement elem, UIElement val) => elem.SetValue(TargetProperty, val);
        public static UIElement GetTarget(UIElement elem) => (UIElement)elem.GetValue(TargetProperty);

        private static void TryEnable(UIElement elem, bool enable)
        {
            if (enable)
            {
                elem.PreviewKeyDown += (src, args) =>
                {
                    if (args.Key == Key.Enter)
                    {
                        Apply(elem);
                    }
                };
            }
        }

        public static void Apply(UIElement elem)
        {
            if (GetTarget(elem) is UIElement target)
            {
                if (target is IFocusable focusable)
                    focusable.Focus();
                else
                    target.Focus();
            }
            else
            {
                var request = new TraversalRequest(FocusNavigationDirection.Next);
                elem.MoveFocus(request);
            }
        }
    }
}
