﻿using SalomKrasoti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PageClient.xaml
    /// </summary>
    public partial class PageClient : Page
    {
        public string iag = "";
        public string selectedClientFullName = "";
        private List<Client> listclient;
        private Service service;
        public PageClient(Service service)
        {
            InitializeComponent();
            this.service = service; // Сохраняем ссылку на выбранную услугу
            listclient = helper.GetContext().Client.ToList();

            tbServiceName.Text = service.Title;
            tbServiceDuration.Text = ($"{service.DurationInSeconds} минут").ToString();

            // Заполнение ComboBox клиентами
            cbClients.ItemsSource = listclient;
            cbClients.DisplayMemberPath = "FullName"; // Указываем путь к свойству FullName
            cbClients.SelectedValuePath = "ID"; // Указываем путь к ID (если нужно)
            tbStartTime.PreviewTextInput += tbStartTime_PreviewTextInput;
            dpServiceDate.PreviewTextInput += dpServiceDate_PreviewTextInput;
        }

        public void Load()
        {

        }

        private void SelectedCombox(object sender, SelectionChangedEventArgs e)
        {
            if (cbClients.SelectedItem is Client selectedClient)
            {
                selectedClientFullName = $"{selectedClient.LastName} {selectedClient.FirstName} {selectedClient.Patronymic}";
                Load();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {   
            if (cbClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            // Валидация ввода времени
            if (!TimeSpan.TryParse(tbStartTime.Text, out TimeSpan startTime))
            {
                MessageBox.Show("Введите корректное время в формате ЧЧ:ММ.");
                return;
            }
           

            // Валидация ввода даты
            if (dpServiceDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату оказания услуги.");
                return;
            }

            DateTime serviceDate = dpServiceDate.SelectedDate.Value;

            // Рассчитываем время окончания услуги
            DateTime endTime = serviceDate.Add(startTime).AddMinutes(service.DurationInSeconds);

            // Создаем новую запись в базе данных
            var record = new ClientService
            {
                ClientID = ((Client)cbClients.SelectedItem).ID,
                ServiceID = service.ID,
                StartTime = serviceDate.Add(startTime),
                EndTime = endTime
            };

            try
            {
                var context = helper.GetContext();
                context.ClientService.Add(record);
                context.SaveChanges();
                MessageBox.Show("Запись успешно добавлена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении записи: {ex.Message}");
            }
        }

        private void btnvixod_Click(object sender, RoutedEventArgs e)
        {
            PageGlav pageGlav = new PageGlav(1);
            NavigationService.Navigate(pageGlav);
        }

        private void tbStartTime_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dpServiceDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void tbStartTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex1 = new Regex("[^0-9:]");
            e.Handled = regex1.IsMatch(e.Text);
        }
    }
}
