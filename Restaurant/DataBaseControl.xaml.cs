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
        public DataBaseControl(ref RestaurantContext db)
        {
            InitializeComponent();
            database = db;
            System.Windows.Threading.DispatcherTimer messageTimer = new System.Windows.Threading.DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 8);
            BindWaiterComboBox();
        }

        private void messageTimer_Tick(object sender, EventArgs e)  //Not called need to be fixed
        {
            WaiterStatus.Content = "Waiting...";
            TableStatus.Content = "Waiting...";
        }

        public void BindWaiterComboBox()
        {
            var query = from b in database.Waiters
                        select b;

            ComboWaiter.ItemsSource = query.ToList();
            ComboWaiter.DisplayMemberPath = "FirstName";
            ComboWaiter.SelectedValuePath = "WaiterId";
        }

        public void BindTableComboBox()
        {
            var query = from b in database.Tables
                        select b;

            ComboTable.ItemsSource = query.ToList();
            ComboTable.DisplayMemberPath = "TableId";
            ComboTable.SelectedValuePath = "TableId";
        }

        public void BindMealComboBox()
        {
            var query = from b in database.Meals
                        select b;

            ComboMeal.ItemsSource = query.ToList();
            ComboMeal.DisplayMemberPath = "Meal_name";
            ComboMeal.SelectedValuePath = "MealId";
        }

        public void BindWaiterStats()
        {
            var query = from b in database.Waiters
                        select b;

            WaiterGrid.ItemsSource = query.ToList();
        }


        private void ChangeControl(object sender, RoutedEventArgs e)
        {
            this.Content = new MainControl(ref database);
        }

        private void LoadWaiter(object sender, ContextMenuEventArgs e)
        {
            BindWaiterComboBox();
        }

        private void UpdateBox(object sender, SelectionChangedEventArgs e)
        {
            if (ComboWaiter.SelectedValue != null)
            {
                int index = (int)ComboWaiter.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Waiters
                                where b.WaiterId == index
                                select b;

                    foreach (var item in query)
                    {
                        FirstNameModify.Text = item.FirstName;
                        LastNameModify.Text = item.LastName;
                    }
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
                BindTableComboBox();
            }
            if (Meal.IsSelected)
            {
                BindMealComboBox();
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
                if (col.Header.ToString() == "Bills") {
                    col.Visibility = Visibility.Collapsed;
                }
            }
            WaiterGrid.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void AddWaiter(object sender, RoutedEventArgs e)
        {
            var waiter = new Waiter
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text
            };

            database.Waiters.Add(waiter);
            database.SaveChanges();
            WaiterStatus.Content = "Waiter created";
            BindWaiterComboBox();
        }

        private void DeleteWaiter(object sender, RoutedEventArgs e)
        {
            int index = (int)ComboWaiter.SelectedValue;

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

        private void UpdateWaiter(object sender, RoutedEventArgs e)
        {
            if (ComboWaiter.SelectedValue != null)
            {
                int index = (int)ComboWaiter.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Waiters
                                where b.WaiterId == index
                                select b;

                    foreach (var item in query)
                    {
                        item.FirstName = FirstNameModify.Text;
                        item.LastName = LastNameModify.Text;
                    }
                    database.SaveChanges();
                    WaiterStatus.Content = "Waiter updated";
                }
            }
        }

        private void AddTable(object sender, RoutedEventArgs e)
        {
            int chair = 1;
            bool empty = false;

            if (Int32.TryParse(SeatNbr.Text, out chair))
            {
                if (StatusTableBox.SelectedIndex == 0 || StatusTableBox.SelectedIndex == 1)
                {
                    if (StatusTableBox.SelectedIndex == 0)
                    {
                        empty = true;
                    }
                    else if (StatusTableBox.SelectedIndex == 1)
                    {
                        empty = false;
                    }

                    var table = new Restaurant.Database.Table
                    {
                        Chair_number = chair,
                        isEmpty = empty,
                    };

                    database.Tables.Add(table);
                    database.SaveChanges();
                    TableStatus.Content = "Table created";
                    BindTableComboBox();
                }
                else
                {
                    TableStatus.Content = "Please choose a status";
                }
            }
            else
            {
                TableStatus.Content = "Please choose a valid number of chair";
            }
        }

        private void DeleteTable(object sender, RoutedEventArgs e)
        {
            if (ComboTable.SelectedValue != null)
            {
                int index = (int)ComboTable.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Tables
                                where b.TableId == index
                                select b;

                    foreach (var item in query)
                    {
                        database.Tables.Remove(item);
                    }

                    database.SaveChanges();
                    TableStatus.Content = "Table deleted";
                    BindWaiterComboBox();
                }
            }
            else
            {
                TableStatus.Content = "No table selected";
            }
        }

        private void UpdateTable(object sender, RoutedEventArgs e)
        {
            int chair = 1;
            bool empty = false;

            if (ComboTable.SelectedValue != null)
            {
                int index = (int)ComboTable.SelectedValue;

                if (Int32.TryParse(SeatNbrModify.Text, out chair))
                {
                    if (StatusTableBoxModify.SelectedIndex == 0 || StatusTableBoxModify.SelectedIndex == 1)
                    {
                        if (StatusTableBoxModify.SelectedIndex == 0)
                        {
                            empty = true;
                        }
                        else if (StatusTableBoxModify.SelectedIndex == 1)
                        {
                            empty = false;
                        }

                        if (index > 0)
                        {
                            var query = from b in database.Tables
                                        where b.TableId == index
                                        select b;
                                
                            foreach (var item in query)
                            {
                                item.Chair_number = chair;
                                item.isEmpty = empty;
                            }

                            database.SaveChanges();
                            TableStatus.Content = "Table updated";
                            BindTableComboBox();
                        }
                    }
                    else
                    {
                        TableStatus.Content = "Please choose a status";
                    }
                }
                else
                {
                    TableStatus.Content = "Please choose a valid number of chair";
                }
            }
            else
            {
                TableStatus.Content = "No table selected";
            }
        }

        private void UpdateTableBox(object sender, SelectionChangedEventArgs e)
        {
            if (ComboTable.SelectedValue != null)
            {
                int index = (int)ComboTable.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Tables
                                where b.TableId == index
                                select b;

                    foreach (var item in query)
                    {
                        SeatNbrModify.Text = item.Chair_number.ToString();
                        StatusTableBoxModify.SelectedIndex = item.isEmpty == true ? 0 : 1;
                    }
                }
            }
        }

        private void AddMeal(object sender, RoutedEventArgs e)
        {
            double price = 1;

            if (Double.TryParse(MealPrice.Text, out price))
            {
                var meal = new Meal
                {
                    Price = price,
                    Meal_name = MealName.Text
                };

                database.Meals.Add(meal);
                database.SaveChanges();
                MealStatus.Content = "Meal created";
                BindMealComboBox();
            }
            else
            {
                MealStatus.Content = "Please choose a valid price";
            }
        }

        private void UpdateMeal(object sender, RoutedEventArgs e)
        {
            if (ComboMeal.SelectedValue != null)
            {
                double price = 1;

                if (Double.TryParse(MealPriceModify.Text, out price))
                {
                    int index = (int)ComboMeal.SelectedValue;

                    if (index > 0)
                    {
                        var query = from b in database.Meals
                                    where b.MealId == index
                                    select b;

                        foreach (var item in query)
                        {
                            item.Price = price;
                            item.Meal_name = MealNameModify.Text;
                        }
                        database.SaveChanges();
                        BindMealComboBox();
                        MealStatus.Content = "Meal updated";
                    }
                }
                else
                {
                    MealStatus.Content = "Please choose a valid price";
                }
            }
            else
            {
                MealStatus.Content = "No meal selected";
            }
        }

        private void DeleteMeal(object sender, RoutedEventArgs e)
        {
            if (ComboMeal.SelectedValue != null)
            {
                int index = (int)ComboMeal.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Meals
                                where b.MealId == index
                                select b;

                    foreach (var item in query)
                    {
                        database.Meals.Remove(item);
                    }

                    database.SaveChanges();
                    BindMealComboBox();
                    MealStatus.Content = "Meal removed";
                }
            }
            else
            {
                MealStatus.Content = "No meal selected";
            }
        }

        private void UpdateMealBox(object sender, SelectionChangedEventArgs e)
        {
            if (ComboMeal.SelectedValue != null)
            {
                int index = (int)ComboMeal.SelectedValue;

                if (index > 0)
                {
                    var query = from b in database.Meals
                                where b.MealId == index
                                select b;

                    foreach (var item in query)
                    {
                        MealNameModify.Text = item.Meal_name;
                        MealPriceModify.Text = item.Price.ToString();
                    }
                }
            }
        }
    }
}
