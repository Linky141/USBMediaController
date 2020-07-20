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
    /// Logika interakcji dla klasy NotyficationWindow.xaml
    /// </summary>
    public partial class NotyficationWindow : Window
    {
        public NotyficationWindow(bool mode, string comunicat)
        {
            InitializeComponent();
            this.mode = mode;
            this.comunicat = comunicat;

            if(mode)
            {
                btnA.Visibility = Visibility.Hidden;
                btnB.Content = "Ok";
            }
            else
            {
                btnA.Content = "Apply";
                btnB.Content = "Cancel";
            }
            tbxInformation.Text = comunicat;
        }

        public NotyficationWindow(bool mode, string comunicat, string button)
        {
            InitializeComponent();
            this.mode = mode;
            this.comunicat = comunicat;

            if (mode)
            {
                btnA.Visibility = Visibility.Hidden;
                btnB.Content = button;
            }
            else
            {
                btnA.Content = button;
                btnB.Content = "Cancel";
            }
            tbxInformation.Text = comunicat;
        }

        public NotyficationWindow(bool mode, string comunicat, string btn1, string btn2)
        {
            InitializeComponent();
            this.mode = mode;
            this.comunicat = comunicat;

            if (mode)
            {
                btnA.Visibility = Visibility.Hidden;
                btnB.Content = btn1;
            }
            else
            {
                btnA.Content = btn1;
                btnB.Content = btn2;
            }
            tbxInformation.Text = comunicat;
        }

        bool mode = true;
        string comunicat = "";
        bool decision = false;

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            decision = true;
            this.Close();
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            decision = false;
            this.Close();
        }

        public bool Answer() { return decision; }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
    }
}
