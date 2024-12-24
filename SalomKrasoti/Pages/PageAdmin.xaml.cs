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

namespace SalomKrasoti.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string code = TextBoxAdmin.Text;
            if (code == "0000")
            {
                MessageBox.Show("Вы успешно переходите на режим администратора");
                NavigationService.Navigate(new PageGlav(1));
            }
            else
            {
                MessageBox.Show("Неверный код.");
            }

        }
    }
}
