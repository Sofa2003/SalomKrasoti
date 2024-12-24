using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SalomKrasoti
{
    public class FirstCharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                // Проверяем первый символ
                if (str[0] == '0')
                {
                    return Brushes.Red; // Возвращаем красный цвет, если первый символ '0'
                }
                return Brushes.Black; // Возвращаем черный цвет для других значений
            }
            return Brushes.Black; // Или какое-то другое значение по умолчанию
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
