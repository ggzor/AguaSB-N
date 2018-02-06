using System.Windows;

namespace AguaSB.Views.Conversores.Reactive
{
    public static class BoolToVisibility
    {
        public static readonly BoolToVisibilityConverter Converter = new BoolToVisibilityConverter();

        public static Visibility Convert(bool value) => (Visibility)Converter.Convert(value, typeof(Visibility), null, null);
    }
}
