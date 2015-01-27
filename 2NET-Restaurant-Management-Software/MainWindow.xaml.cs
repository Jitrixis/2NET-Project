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
using _2NET_Restaurant_Management_Software.Database;

namespace _2NET_Restaurant_Management_Software
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RestaurantContext database;

        public MainWindow()
        {
            InitializeComponent();
            database = new RestaurantContext();
            BindComboBox();
        }

        public void BindComboBox()
        {
            var query = from b in database.Waiters
                        select b;

            ComboTest.ItemsSource = query.ToList();
            ComboTest.DisplayMemberPath = "FirstName";
            ComboTest.SelectedValuePath = "WaiterId";
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var waiter = new Waiter
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                 
            };

            database.Waiters.Add(waiter);
            database.SaveChanges();
            Status.Content = "Waiter created";
            BindComboBox();
        }

        private void DeleteWaiter(object sender, RoutedEventArgs e)
        {
            int index = (int)ComboTest.SelectedValue;

            if (index > 0)
            {
                var query = from b in database.Waiters
                            where b.WaiterId == index
                            select b;

                foreach (var item in query)
                {
                    database.Waiters.Remove(item);
                }

                database.SaveChanges();
                BindComboBox();
            }
        }
    }

    
}
