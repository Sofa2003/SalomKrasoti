﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SalomKrasoti.Models;

namespace SalomKrasoti
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Content = new Pages.PageGlav(0);
        }
        private void frame_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }
        public class helper
        {
            public static SalonKrasotiEntities ent;
            public static SalonKrasotiEntities GetContext()
            {
                if (ent == null)
                {
                    ent = new SalonKrasotiEntities();
                }
                return ent;
            }
        }
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
}
