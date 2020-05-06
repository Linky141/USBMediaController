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
    /// Logika interakcji dla klasy ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : Window
    {
        public ConnectionSettings()
        {
            InitializeComponent();
            FillCombobox();
        }

        /*
         * deklaracja zmiennych
         */

        bool acceptChanges = false;
        Containter_ConnectionInfo connectionInfo = new Containter_ConnectionInfo();
        /*
         * gettery i settery
         */

        public bool AcceptChanges() { return acceptChanges; }
        public Containter_ConnectionInfo getConnectionInfo() { return connectionInfo; }






        /*
         * Metody obsługi akcji
         */
        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            acceptChanges = true;
            connectionInfo.setPortName(cbx_portName.Text);
            connectionInfo.setBaudRate(Int32.Parse(cbx_baudrate.Text));
            connectionInfo.setHandshake((System.IO.Ports.Handshake)cbx_handshake.SelectedItem);
            connectionInfo.setParity((System.IO.Ports.Parity)cbx_parity.SelectedItem);
            connectionInfo.setDataBits(Int32.Parse(cbx_dataBits.Text));
            connectionInfo.setStopBits((System.IO.Ports.StopBits)cbx_stopBits.SelectedItem);
            connectionInfo.setReadTimeout(Int32.Parse(tbx_readTimeout.Text));
            connectionInfo.setWriteTimeout(Int32.Parse(tbx_writeTimeout.Text));
            connectionInfo.SetSerial();

            this.Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            acceptChanges = false;
            this.Close();
        }

        private void FillCombobox()
        {
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.None);
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.RequestToSend);
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.RequestToSendXOnXOff);
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.XOnXOff);

            cbx_parity.Items.Add(System.IO.Ports.Parity.Even);
            cbx_parity.Items.Add(System.IO.Ports.Parity.Mark);
            cbx_parity.Items.Add(System.IO.Ports.Parity.None);
            cbx_parity.Items.Add(System.IO.Ports.Parity.Odd);
            cbx_parity.Items.Add(System.IO.Ports.Parity.Space);

            for (int clk = 0; clk < 11; clk++) cbx_dataBits.Items.Add(clk);

            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.None);
            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.One);
            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.OnePointFive);
            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.Two);

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
