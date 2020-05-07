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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls.Primitives;

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


        #region CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();


            //https://www.codeproject.com/Articles/36468/WPF-NotifyIcon-2

            tray_main.Icon = new System.Drawing.Icon(@"C:\Users\Tomasz Bielas\Documents\Programowanie\VisualStudio\USBMediaController\USBMediaController\icon.ico");
            if (!LoadCommunicationConfig())
            {
                ConsoleWrite("#Error Load Connection Config");
                ShowSysTrayInfo("USBMediaController", "Error Load Connection Config", BalloonIcon.Error);
            }
            else
            {
                btn_connect.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            this.Hide();
        }

        #endregion


        #region METHODS

        private void ShowSysTrayInfo(string title, string text, BalloonIcon icon)
        {
            tray_main.ShowBalloonTip(title, text, icon);
        }

        private void Recieve(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            recivedData = connectionInfo.serial.ReadExisting();
            Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextDelegate(UseRecivedData), recivedData);
        }
        private void UseRecivedData(string text)
        {
            tbx_console.Text += text;
            tbx_console.Focus();
            tbx_console.CaretIndex = tbx_console.Text.Length;
            tbx_console.ScrollToEnd();
        }

        private void ConsoleWrite(string val)
        {
            tbx_console.Text += val + "\n";
        }

        #endregion


        #region SAVE OPERATION METHODS

        private void SaveCommunicationConfig()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) Directory.CreateDirectory(@"C:\USBMediaControllerv2\");
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\USBMediaControllerv2\ConfigConnection.bin", FileMode.Create)))
            {
                binWriter.Write(connectionInfo.getPortName());
                binWriter.Write(connectionInfo.getBaudRate());
                binWriter.Write(connectionInfo.getHandshake());
                binWriter.Write(connectionInfo.getParity());
                binWriter.Write(connectionInfo.getDataBits());
                binWriter.Write(connectionInfo.getStopBits());
                binWriter.Write(connectionInfo.getReadTimeout());
                binWriter.Write(connectionInfo.getWriteTimeout());
            }


        }

        private bool LoadCommunicationConfig()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) return false;
            if (!File.Exists(@"C:\USBMediaControllerv2\ConfigConnection.bin")) return false;
            using (BinaryReader binReader = new BinaryReader(File.Open(@"C:\USBMediaControllerv2\ConfigConnection.bin", FileMode.Open)))
            {
                connectionInfo.setPortName(binReader.ReadString());
                connectionInfo.setBaudRate(binReader.ReadInt32());
                connectionInfo.setHandshake(binReader.ReadString());
                connectionInfo.setParity(binReader.ReadString());
                connectionInfo.setDataBits(binReader.ReadInt32());
                connectionInfo.setStopBits(binReader.ReadString());
                connectionInfo.setReadTimeout(binReader.ReadInt32());
                connectionInfo.setWriteTimeout(binReader.ReadInt32());
            }
            connectionInfo.SetSerial();

            return true;
        }

        #endregion


        #region SLOTS
        private void btn_ConnectionSettings_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSettings connectionSettings = new ConnectionSettings(connectionInfo);
            connectionSettings.ShowDialog();
            if (connectionSettings.AcceptChanges())
            {
                connectionInfo = connectionSettings.getConnectionInfo();
                SaveCommunicationConfig();
            }
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            if (connectionInfo.serial.IsOpen)
            {
                connectionInfo.serial.Close();
                if (!connectionInfo.serial.IsOpen)
                {
                    btn_connect.Content = "Connect";
                    ShowSysTrayInfo("USBMediaController", "Disconnected", BalloonIcon.Info);
                    ConsoleWrite("#Disconnected");
                }
            }
            else
            {
                connectionInfo.serial.Open();
                if (connectionInfo.serial.IsOpen)
                {
                    connectionInfo.serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recieve);
                    btn_connect.Content = "Disconnect";
                    ShowSysTrayInfo("USBMediaController", "Connected at port " + connectionInfo.getPortName(), BalloonIcon.Info);
                    ConsoleWrite("#Connected at port " + connectionInfo.getPortName());
                }
            }
        }

        private void btn_CommandSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_trayClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_minimalise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_hideToTray_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btn_trayShow_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion


    }
}
