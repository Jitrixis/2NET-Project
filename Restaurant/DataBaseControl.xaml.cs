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
            System.Windows.Threading.DispatcherTimer messageTimer = new System.Windows.Threading.DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 8);
            BindWaiterComboBox();
        }

        private void messageTimer_Tick(object sender, EventArgs e)
        {
            if (WaiterStatus.Content != "Waiting...")
            {
                WaiterStatus.Content = "Waiting...";
            }
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

            //WaiterGrid.ItemsSource = LoadCollectionData();
            WaiterGrid.ItemsSource = query.ToList();
        }

        private List<Waiter> LoadCollectionData()
        {
            List<Waiter> waiters = new List<Waiter>();

            var query = from b in database.Waiters
                        select b;

            foreach (var item in query)
            {
                waiters.Add(new Waiter { FirstName=item.FirstName, LastName = item.LastName});
            }
            return waiters;
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
            WaiterStatus.Content = "Waiter created";
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
                WaiterStatus.Content = "Waiter deleted";
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
                    WaiterStatus.Content = "Waiter updated";
                }
            }
        }

        private void LoadTab(object sender, SelectionChangedEventArgs e)
        {
            if (Waiter.IsSelected)
            {
                BindWaiterComboBox();
            }
            if (Table.IsSelected)
            {

            }
            if (Meal.IsSelected)
            {

            }
            if (Stats.IsSelected)
            {
                BindWaiterStats();
            }
        }

        private void UpdateDebug(object sender, RoutedEventArgs e)
        {
            foreach (DataGridColumn col in WaiterGrid.Columns)
            {
                if (col.Header == "Bills") {
                    col.Visibility = Visibility.Collapsed;
                }
            }
            //WaiterGrid.Columns[0].Visibility = Visibility.Collapsed;
            WaiterGrid.Columns[3].Visibility = Visibility.Collapsed;
            //WaiterGrid.Columns[4].Visibility = Visibility.Collapsed;
        }
    }
}
