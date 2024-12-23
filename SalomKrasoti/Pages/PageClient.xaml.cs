using SalomKrasoti.Models;
using System;
using System.Collections.Generic;
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
            listclient = helper.GetContext().Client.ToList();
            try 
            { 

            }
            catch (Exception ex) { }
            tbServiceName.Text = service.Title;
            tbServiceDuration.Text = ($"{ service.DurationInSeconds} минут").ToString();

            // Заполнение ComboBox клиентами
            cbClients.ItemsSource = listclient;
            

            // Установка свойства для отображения в ComboBox
            cbClients.DisplayMemberPath = "FullName"; // Указываем путь к свойству FullName
            cbClients.SelectedValuePath = "ID"; // Указываем путь к ID (если нужно)
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
            
        }

        private void btnvixod_Click(object sender, RoutedEventArgs e)
        {
            PageGlav pageGlav = new PageGlav(); 
            NavigationService.Navigate(pageGlav);   
        }
    }
}
