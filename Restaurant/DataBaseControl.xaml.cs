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
using Restaurant.Database;

namespace Restaurant
{
    /// <summary>
    /// Logique d'interaction pour DataBaseControl.xaml
    /// </summary>
    public partial class DataBaseControl : UserControl
    {
        private RestaurantContext database;
        public DataBaseControl()
        {
            InitializeComponent();
            database = new RestaurantContext();
            BindWaiterComboBox();
        }

        public void BindWaiterComboBox()
        {
            var query = from b in database.Waiters
                        select b;

            ComboTest.ItemsSource = query.ToList();
            ComboTest.DisplayMemberPath = "FirstName";
            ComboTest.SelectedValuePath = "WaiterId";
        }

        public void BindWaiterStats()
        {
            var query = from b in database.Waiters
                        select b;

            WaiterGrid.ItemsSource = query;
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
            BindWaiterComboBox();
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
                BindWaiterComboBox();
            }
        }

        private void ChangeControl(object sender, RoutedEventArgs e)
        {
            this.Content = new MainControl();
        }

        private void LoadWaiter(object sender, ContextMenuEventArgs e)
        {
            BindWaiterComboBox();
        }

        private void UpdateBox(object sender, SelectionChangedEventArgs e)
        {
            if (ComboTest.SelectedValue != null)
            {
                int index = (int)ComboTest.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Waiters
                                where b.WaiterId == index
                                select b;

                    foreach (var item in query)
                    {
                        FirstName.Text = item.FirstName;
                        LastName.Text = item.LastName;
                    }
                }
            }
        }

        private void UpdateWaiter(object sender, RoutedEventArgs e)
        {
            if (ComboTest.SelectedValue != null)
            {
                int index = (int)ComboTest.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Waiters
                                where b.WaiterId == index
                                select b;

                    foreach (var item in query)
                    {
                        item.FirstName = FirstName.Text;
                        item.LastName = LastName.Text;
                    }

                    database.SaveChanges();
                }
            }
        }

        private void LoadStat(object sender, ContextMenuEventArgs e)
        {
            BindWaiterStats();
        }
    }
}
