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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        private RestaurantContext database;
        public MainControl()
        {
            InitializeComponent();
            database = new RestaurantContext();
            LoadTables();
        }

        public MainControl(ref RestaurantContext db)
        {
            InitializeComponent();
            database = db;
            LoadTables();
        }

        private void GoToDatabase(object sender, RoutedEventArgs e)
        {
            {
                this.Content = new DataBaseControl(ref database);
                
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadTables(){
            var query = from b in database.Tables
                        select b;

            foreach (var item in query)
            {
                //create a button
                MahApps.Metro.Controls.Tile TableTile = new MahApps.Metro.Controls.Tile {
                    Title = string.Format("Table {0}", item.TableId) ,
                    Content = string.Format("{0} {1}", item.Chair_number, item.Chair_number > 1 ? "people" : "peoples"),
                    Tag = item.TableId
                };
                /*System.Windows.Controls.Button Tablebutton = new Button();
                Tablebutton.Content = string.Format("Table {0}\n-{1} {2}-", item.TableId, item.Chair_number, item.Chair_number > 1?"people":"peoples");
                Tablebutton.Height = 100;
                Tablebutton.Width = 100;
                Tablebutton.Margin = new System.Windows.Thickness(5, 5, 5, 5);*/
                if (item.isEmpty)
                {
                    //Tablebutton.Background = Brushes.Green;
                    TableTile.Background = Brushes.DarkGreen;
                }
                else
                {
                    //Tablebutton.Background = Brushes.Red;
                    TableTile.Background = Brushes.Gray;
                }
                //Tablebutton.Click += new RoutedEventHandler(Tablebutton_Click);
                //Tablebutton.
                TableTile.Click += new RoutedEventHandler(Tablebutton_Click);
                Wrap.Children.Add(TableTile);
            }
        }

        private void Tablebutton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string number_str = button.Tag.ToString();

            int id = -1;

            if (Int32.TryParse(number_str, out id))
            {
                this.Content = new BillControl(id, ref database);
            }
            // identify which button was clicked and perform necessary actions
        }
    }
}
