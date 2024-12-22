using SalomKrasoti.Models;
using System;
using System.Collections;
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
using static SalomKrasoti.MainWindow;

namespace SalomKrasoti.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageGlav.xaml
    /// </summary>
    public partial class PageGlav : Page
    {
        private List<Service> listspisokservice;
        private string searchQuery = "";
        public PageGlav(/*Frame frame*/)
        {
            InitializeComponent();
            listspisokservice = helper.GetContext().Service.ToList();
            SortBox.Items.Add("Все");
            SortBox.Items.Add("0 - 5%");
            SortBox.Items.Add("5 - 15%");
            SortBox.Items.Add("15 - 30%");
            SortBox.Items.Add("30 - 70%");
            SortBox.Items.Add("70 - 100%");
            SortBox.SelectedIndex = 0;
            DiscountBox.Items.Add("Без сортировки");
            DiscountBox.Items.Add("По возрастанию");
            DiscountBox.Items.Add("По убыванию");
            DiscountBox.SelectedIndex = 0;

            Load();
        }
        public void Load()
        {
            List<Service> services = new List<Service>();
            var serviceQuery = listspisokservice.AsQueryable();
            serviceGrid.ItemsSource = serviceQuery.ToList();
            int totalCount = listspisokservice.Count();

            foreach (var service in serviceQuery)
            {
                if (string.IsNullOrEmpty(service.MainImagePath))
                {
                    service.MainImagePath = "Услуги салона красоты/96.png"; // Путь к изображению по умолчанию
                }
                serviceGrid.ItemsSource = serviceQuery.ToList();
            }
            // Фильтрация по скидке
            switch (SortBox.SelectedItem.ToString())
            {
                case "0 - 5%":
                    serviceQuery = serviceQuery.Where(s => s.Discount == null || (s.Discount >= null && s.Discount < 5));
                    break;
                case "5 - 15%":
                    serviceQuery = serviceQuery.Where(s => s.Discount >= 5 && s.Discount < 15);
                    break;
                case "15 - 30%":
                    serviceQuery = serviceQuery.Where(s => s.Discount >= 15 && s.Discount < 30);
                    break;
                case "30 - 70%":
                    serviceQuery = serviceQuery.Where(s => s.Discount >= 30 && s.Discount < 70);
                    break;
                case "70 - 100%":
                    serviceQuery = serviceQuery.Where(s => s.Discount >= 70 && s.Discount < 100);
                    break;
                case "Все":
                default:
                    break; 
            }
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                serviceQuery = serviceQuery.Where(Service =>
                Service.Title.Contains(searchQuery));
            }
            string selectedDiscount = DiscountBox.SelectedItem?.ToString(); // Получаем выбранный элемент и проверяем на null

            switch (selectedDiscount)
            {
                case "По возрастанию":
                    serviceQuery = serviceQuery.OrderBy(s => s.Cost); // Сортировка по возрастанию
                    break;
                case "По убыванию":
                    serviceQuery = serviceQuery.OrderByDescending(s => s.Cost); // Сортировка по убыванию
                    break;
                case "Без сортировки":
                default:
                    break;
            }
       
            var filteredServices = serviceQuery.ToList();
            serviceGrid.ItemsSource = filteredServices;

            // Обновляем текст с количеством элементов
            RecordCountLabel.Content =  $"{filteredServices.Count} из {totalCount}";

        }

        

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void DiscountBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchQuery = SearchBox.Text;
            Load();
        }

        private void btnadnim_Click(object sender, RoutedEventArgs e)
        {
            Frame _frame = new Frame();
            Admin admin = new Admin(_frame);
            admin.ShowDialog();
        }
    }
}
