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

namespace SalomKrasoti.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private Frame _frame;
        public Admin(Frame frame)
        {

            InitializeComponent();
            _frame = frame;
           
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string code = TextBoxAdmin.Text; // Получаем текст из TextBox

            if (code == "0000") // Проверяем код
            {
                _frame.Navigate(new PageGlav());
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный код.");
            }
            
        }
    }
    
}
