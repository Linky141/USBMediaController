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
using System.Windows.Shapes;

namespace USBMediaController
{
    /// <summary>
    /// Logika interakcji dla klasy ControlerConfigAddItem.xaml
    /// </summary>
    public partial class ControlerConfigAddItem : Window
    {
        private bool apply = false;
        private string name;
        public ControlerConfigAddItem()
        {
            InitializeComponent();
            tbx_name.Focus();
        }

        public bool Apply() { return apply; }
        public string getName() { return name; }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_name.Text != "")
            {
                apply = true;
                name = tbx_name.Text;
                this.Close();
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            apply = false;
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void tbx_name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                object tmoObj = null;
                RoutedEventArgs tmpROA = null;
                btn_add_Click(tmoObj, tmpROA);
            }
        }
    }
}
