using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace VoilaMobileApp.Src.Converters
{
    public class CreditCardNumberValueConverter : IValueConverter
    {
        public CreditCardNumberValueConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var builder = new StringBuilder(Regex.Replace(value.ToString(), @"\D", ""));

            foreach (var i in Enumerable.Range(0, builder.Length / 4).Reverse())
                builder.Insert(4 * i + 4, " ");

            return builder.ToString().Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");

        }
    }
}

