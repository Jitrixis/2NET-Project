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
using MahApps.Metro.Controls;
using Restaurant.Database;

namespace Restaurant
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public RestaurantContext database;
        public MainWindow()
        {
            InitializeComponent();
            database = new RestaurantContext();
            var init = from test in database.Tables
                       select test;
            this.Content = new MainControl(ref database);
        }
        private void GoToDatabase(object sender, RoutedEventArgs e)
        {
            {
                this.Content = new DataBaseControl(ref database);
            }
        }
    }
}
