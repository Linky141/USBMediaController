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
using System.Windows.Threading;
using System.IO.Ports;
using Hardcodet.Wpf.TaskbarNotification;
using MaterialDesignThemes.Wpf;
using System.Windows.Media.Animation;

namespace USBMediaController
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VARIABLES

        Containter_ConnectionInfo connectionInfo = new Containter_ConnectionInfo();
        string recivedData;


        private delegate void UpdateUiTextDelegate(string text);
        #endregion


        public MainWindow()
        {
            InitializeComponent();


            //https://www.codeproject.com/Articles/36468/WPF-NotifyIcon-2

            tray_main.Icon = new System.Drawing.Icon(@"C:\Users\Tomasz Bielas\Documents\Programowanie\VisualStudio\USBMediaController\USBMediaController\icon.ico");
            
        }


        private void ShowSysTrayInfo(string title, string text, BalloonIcon icon)
        {
            tray_main.ShowBalloonTip(title, text, icon);
        }

        private void Recieve(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            recivedData = connectionInfo.serial.ReadExisting();
            Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextDelegate(WriteData), recivedData);
        }
        private void WriteData(string text)
        {
            tbx_console.Text += text;
            tbx_console.Focus();
            tbx_console.CaretIndex = tbx_console.Text.Length;
            tbx_console.ScrollToEnd();
        }


        private void btn_ConnectionSettings_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSettings connectionSettings = new ConnectionSettings();
            connectionSettings.ShowDialog();
            if (connectionSettings.AcceptChanges()) connectionInfo = connectionSettings.getConnectionInfo();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            if (connectionInfo.serial.IsOpen)
            {
                connectionInfo.serial.Close();
                btn_connect.Content = "Rozłącz";
                ShowSysTrayInfo("Połączenie", "rozłączono", BalloonIcon.Info);
            }
            else
            {
                connectionInfo.serial.Open();
                connectionInfo.serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recieve);
                btn_connect.Content = "Rozłącz";
                ShowSysTrayInfo("Połączenie", "połączono", BalloonIcon.Info);
            }
        }

        private void btn_CommandSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_trayClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
