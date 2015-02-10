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
    /// Logique d'interaction pour BillControl.xaml
    /// </summary>
    public partial class BillControl : UserControl
    {
        private RestaurantContext database;

        private int index;

        public BillControl()
        {
            InitializeComponent();
        }

        public BillControl(int index, ref RestaurantContext db)
        {
            InitializeComponent();
            database = db;
            this.index = index;
            
            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            List<ComboBox> Boxes = new List<ComboBox>();
            bool empty = false;
            var waiter = from b in database.Waiters
                        select b;

            var meal = from b in database.Meals
                         select b; 

            ComboWaiter.ItemsSource = waiter.ToList();
            ComboWaiter.DisplayMemberPath = "FirstName";
            ComboWaiter.SelectedValuePath = "WaiterId";
            ComboWaiter.SelectedIndex = 0;

            var table = from b in database.Tables
                        where b.TableId == index
                        select b;

            var table_liste = table.ToList();

            empty = table_liste[0].isEmpty;
            int table_id = table_liste[0].TableId;

            if (empty)
            {
                empty = true;
                BillPayment.IsEnabled = false;
                CancelButton.IsEnabled = false;
                ComboStatus.SelectedIndex = 0;
                ComboStatus.IsEnabled = false;

                for (int i = 0; i < table_liste[0].Chair_number; ++i)
                {
                    Boxes.Add(new ComboBox());

                    Boxes[Boxes.Count - 1].ItemsSource = meal.ToList();
                    Boxes[Boxes.Count - 1].DisplayMemberPath = "Meal_name";
                    Boxes[Boxes.Count - 1].SelectedValuePath = "MealId";
                    Boxes[Boxes.Count - 1].Width = 175;
                    Boxes[Boxes.Count - 1].Margin = new System.Windows.Thickness(5);

                    WrapMeals.Children.Add(Boxes[Boxes.Count - 1]);
                }
            }
            else
            {
                TableSetup.IsEnabled = false;
                ComboStatus.SelectedIndex = 1;
                ComboStatus.IsEnabled = false;
                
                var bill = from b in database.Bills
                           where b.TableId == table_id
                           select b;

                Bill last_bill = new Bill();

                foreach (var meal_list in bill)
                {
                    last_bill = meal_list;
                }

                for (int i = 0; i < last_bill.string_Meals.Count; ++i)
                {
                    Boxes.Add(new ComboBox());
                    Boxes[Boxes.Count - 1].Items.Add(new ComboBoxItem { Content = last_bill.string_Meals[i].Description });
                    Boxes[Boxes.Count - 1].Width = 175;
                    Boxes[Boxes.Count - 1].Margin = new System.Windows.Thickness(5);
                    Boxes[Boxes.Count - 1].SelectedIndex = 0;
                    Boxes[Boxes.Count - 1].IsEnabled = false;

                    WrapMeals.Children.Add(Boxes[Boxes.Count - 1]);
                }

                ComboWaiter.SelectedValue = last_bill.WaiterId;
                ComboWaiter.IsEnabled = false;
                TotalBill.Content = last_bill.BillPrice.ToString() + " $";
            }
        }

        private void ChangeControl(object sender, RoutedEventArgs e)
        {
            this.Content = new MainControl(ref database);
        }

        private void SetupTable(object sender, RoutedEventArgs e)
        {
            int w_index = Int32.Parse(ComboWaiter.SelectedValue.ToString());        //Recuperation du waiter
            double total_price = 0.0;
            var waiter = from b in database.Waiters
                         where b.WaiterId == w_index
                         select b;

            var list_waiter = waiter.ToList();

            List<MealString> meal_list = new List<MealString>();                                //Recup des repas pour la facture

            foreach (ComboBox child in WrapMeals.Children)      
            {
                int m_index = Int32.Parse(child.SelectedValue.ToString());

                var meal = from b in database.Meals
                           where b.MealId == m_index
                           select b;

                var list_meal = meal.ToList();
                if (list_meal.Count > 0)
                {
                    meal_list.Add(new MealString { Description = list_meal[0].Meal_name});
                    total_price += list_meal[0].Price;
                }
            }

            var table = from b in database.Tables       //Ajout de la facture a la table
                       where b.TableId == this.index
                       select b;

            var list_table = table.ToList();

            Bill bill = new Restaurant.Database.Bill                       //Creation et remplissage de la bill
            {
                WaiterId = list_waiter[0].WaiterId,
                string_Meals = meal_list,
                TableId = list_table[0].TableId,
                BillPrice = total_price
            };

            database.Bills.Add(bill);

            foreach (var item in table.ToList())
            {
                item.isEmpty = false;
            }
            database.SaveChanges();
            this.Content = new BillControl(index, ref database);
        }

        private void PayBill(object sender, RoutedEventArgs e)
        {
            var table = from b in database.Tables       //Ajout de la facture a la table
                        where b.TableId == this.index
                        select b;

            var list_table = table.ToList();
            list_table[0].isEmpty = true;
            database.SaveChanges();

            this.Content = new MainControl(ref database);
        }

        private void Cancel_Bill(object sender, RoutedEventArgs e)
        {
            var bill = from b in database.Bills
                       where b.TableId == index
                       select b;

            Bill last_bill = new Bill();

            foreach (var meal_list in bill)
            {
                last_bill = meal_list;
            }

            var table = from b in database.Tables
                        where b.TableId == this.index
                        select b;

            var list_table = table.ToList();
            list_table[0].isEmpty = true;

            database.Bills.Remove(last_bill);
            database.SaveChanges();
            this.Content = new BillControl(index, ref database);
        }
    }
}
