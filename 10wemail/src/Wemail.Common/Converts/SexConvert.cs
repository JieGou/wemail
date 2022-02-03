using System;
using System.Globalization;
using System.Windows.Data;

namespace Wemail.Common.Converts
{
    public class SexConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            bool isParse = int.TryParse(value.ToString(), out int result);
            if (isParse)
            {
                string sex = result == 1 ? "女" : "男";
                return sex;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = (string)value;
            if (date == "女")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}