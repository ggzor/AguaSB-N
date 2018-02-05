using System;
using System.Globalization;
using System.Windows.Data;

namespace AguaSB.Views.Conversores
{
    public class ArithmeticConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(parameter as string))
                throw new ArgumentNullException(nameof(parameter));

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
