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
            frame.Content = new Pages.PageGlav();
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

    }
}
