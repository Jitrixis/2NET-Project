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

        private void GoToDatabase(object sender, RoutedEventArgs e)
        {
            {
                this.Content = new DataBaseControl();
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
                System.Windows.Controls.Button Tablebutton = new Button();
                Tablebutton.Content = String.Format("Table {0}\n{1} {2}", item.TableId, item.Chair_number, item.Chair_number > 1?"people":"peoples");
                Tablebutton.Height = 100;
                Tablebutton.Width = 100;
                Tablebutton.Margin = new System.Windows.Thickness(5);
                if (item.isEmpty)
                {
                    Tablebutton.Background = Brushes.Green;
                }
                else
                {
                    Tablebutton.Background = Brushes.Red;
                }
                
                Wrap.Children.Add(Tablebutton);
            }
        }
    }
}
