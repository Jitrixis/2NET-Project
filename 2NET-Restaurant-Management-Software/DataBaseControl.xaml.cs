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

namespace _2NET_Restaurant_Management_Software
{
    /// <summary>
    /// Logique d'interaction pour DataBaseControl.xaml
    /// </summary>
    public partial class DataBaseControl : UserControl
    {
        public DataBaseControl()
        {
            InitializeComponent();
        }

        private void ChangeControl(object sender, RoutedEventArgs e)
        {
            this.Content = new MainControl();
        }
    }
}
