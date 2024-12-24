using SalomKrasoti.Models;
using System;
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
using System.Windows.Threading;
using static SalomKrasoti.MainWindow;

namespace SalomKrasoti.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageUpcomingEnt.xaml
    /// </summary>
    public partial class PageUpcomingEnt : Page
    {
        private List<ClientService> listClServ;
        private DispatcherTimer timer;
        public PageUpcomingEnt()
        {
            InitializeComponent();
            listClServ = helper.GetContext().ClientService.ToList();
            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30); // Устанавливаем интервал 30 секунд
            timer.Tick += Timer_Tick; // Подписываемся на событие Tick
            timer.Start(); // Запускаем таймер
            Load();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Load();
        }
        public void Load()
        {

            var currentDate = DateTime.Now; // Текущая дата и время
            var twoDaysLater = currentDate.AddDays(2); // Дата через 2 дня

            var serviceQuery = listClServ.Where(service => service.StartTime >= currentDate && service.StartTime <= twoDaysLater)
                .OrderBy(service => service.StartTime)
                .AsQueryable();

            datagridClServ.ItemsSource = serviceQuery.ToList();
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

        private void btnVixod_Click(object sender, RoutedEventArgs e)
        {
            PageGlav pgv = new PageGlav(1);
            NavigationService.Navigate(pgv);

        }
    }
}
