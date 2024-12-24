using Microsoft.Win32;
using SalomKrasoti.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PageEdit.xaml
    /// </summary>
    public partial class PageEdit : Page
    {
        Service sr = new Service();
        private Service currentService;
        private string selectedImagePath;
        public string seldnull = "";
        public PageEdit(Service service)
        {
            InitializeComponent();
            currentService = service;
            

            if (service != null)
            {
                tbID.Visibility = Visibility.Visible;
                tbID.Text = service.ID.ToString();
                // Заполнение полей данными услуги
                tbName.Text = service.Title;
                tbCost.Text = service.Cost.ToString();
                tbSecond.Text = service.DurationInSeconds.ToString();
                tbdescription.Text = service.Description;
                tbDiscount.Text = service.Discount.ToString();

                // Загрузка изображения
                var imagePath = System.IO.Path.Combine("Услуги салона красоты", service.MainImagePath);
                ImageService.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                selectedImagePath = service.MainImagePath; // Сохраняем путь изображения
            }
            else
            {
                lblID.Visibility = Visibility.Hidden;
                // Если это новая услуга, скрываем поле ID
                tbID.Visibility = Visibility.Collapsed;
            }
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(tbName.Text))
                errors.Add("Услуга не может быть пустой");

            if (!int.TryParse(tbCost.Text, out int cost) || cost <= 0)
                errors.Add("Цена должна быть положительным числом");

            if (!int.TryParse(tbSecond.Text, out int duration) || duration <= 0)
                errors.Add("Длительность должна быть положительным числом");

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение, если есть ошибки
            }
            var existingService = helper.GetContext().Service
            .FirstOrDefault(s => s.Title.Equals(tbName.Text, StringComparison.OrdinalIgnoreCase));

            if (existingService != null && currentService == null)
            {
                MessageBox.Show("Услуга с таким названием уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение, если услуга уже существует
            }

            // Если текущая услуга существует, обновляем ее, иначе создаем новую
            if (currentService != null)
            {
                currentService.Title = tbName.Text;
                currentService.Cost = cost;
                currentService.DurationInSeconds = duration;
                currentService.Description = tbdescription.Text;
                currentService.Discount = double.TryParse(tbDiscount.Text, out double discount) ? discount : 0;

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    if (!currentService.MainImagePath.StartsWith("/Услуги салона красоты/"))
                    {
                        // Если не начинается, добавляем префикс
                        currentService.MainImagePath = "/Услуги салона красоты/" + selectedImagePath;
                        MessageBox.Show("Типо нет: " + currentService.MainImagePath);
                    }
                    else
                    {
                        // Если уже начинается с нужной строки, проверяем, не дублируем ли мы путь
                        string imageFileName = System.IO.Path.GetFileName(selectedImagePath);
                        string existingFileName = System.IO.Path.GetFileName(currentService.MainImagePath);

                        if (existingFileName != imageFileName)
                        {
                            // Добавляем только если имена файлов различаются
                            currentService.MainImagePath += selectedImagePath; // Или другой способ обработки
                        }
                        else
                        {
                            //MessageBox.Show("Изображение уже добавлено: " + currentService.MainImagePath);
                        }
                    }
                }
                // Обновляем существующую запись
                helper.GetContext().SaveChanges();
                MessageBox.Show("Услуга успешно отредактирована");

            }
            else
            {
                // Создаем новую услугу
                Service newService = new Service
                {
                    Title = tbName.Text,
                    Cost = cost,
                    DurationInSeconds = duration,
                    Description = tbdescription.Text,
                    Discount = double.TryParse(tbDiscount.Text, out double newDiscount) ? newDiscount : 0,
                    MainImagePath = !string.IsNullOrEmpty(selectedImagePath) ? "/Услуги салона красоты/" + selectedImagePath : null
                };
                
                helper.GetContext().Service.Add(newService);
                helper.GetContext().SaveChanges();
                MessageBox.Show("Услуга успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new PageGlav(1));
            }

            
            
        }



        private void btnBase_OnClickDownloadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Фильтр по типам изображений

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Проверяем размер файла (не более 2 МБ)
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length > 2 * 1024 * 1024) // 2 МБ
                    {
                        MessageBox.Show("Размер изображения не должен превышать 2 МБ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Загружаем изображение в ImageControl
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filePath);
                    bitmap.EndInit();
                    bitmap.Freeze(); // Замораживаем изображение для многопоточности
                    this.ImageService.Source = bitmap;

                    // Сохраняем имя файла для дальнейшего использования
                    selectedImagePath = System.IO.Path.GetFileName(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnvixod_Click(object sender, RoutedEventArgs e)
        {
            PageGlav pageGlav = new PageGlav(1);
            NavigationService.Navigate(pageGlav);
        }
    }
}
