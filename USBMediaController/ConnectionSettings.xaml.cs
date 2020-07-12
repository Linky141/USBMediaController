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
        public ConnectionSettings(Containter_ConnectionInfo connectionInfo)
        {
            this.connectionInfo = connectionInfo;
            InitializeComponent();
            FillCombobox();

            cbx_portName.SelectedValue = this.connectionInfo.getPortName();
            cbx_baudrate.SelectedValue = this.connectionInfo.getBaudRate().ToString();
            cbx_handshake.SelectedValue = this.connectionInfo.getHandshake().ToString();
            cbx_parity.SelectedValue = this.connectionInfo.getParity().ToString();
            cbx_dataBits.SelectedValue = this.connectionInfo.getDataBits();
            cbx_stopBits.SelectedValue = this.connectionInfo.getStopBits().ToString();
            tbx_readTimeout.Text = this.connectionInfo.getReadTimeout().ToString();
            tbx_writeTimeout.Text = this.connectionInfo.getWriteTimeout().ToString();
            //CenterWindowOnScreen();
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



   /*     private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }*/


        /*
         * Metody obsługi akcji
         */
        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            acceptChanges = true;
            connectionInfo.setPortName(cbx_portName.Text);
            connectionInfo.setBaudRate(Int32.Parse(cbx_baudrate.Text));
            connectionInfo.setHandshake(cbx_handshake.Text);
            connectionInfo.setParity(cbx_parity.Text);
            connectionInfo.setDataBits(Int32.Parse(cbx_dataBits.Text));
            connectionInfo.setStopBits(cbx_stopBits.Text);
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
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.None.ToString());
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.RequestToSend.ToString());
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.RequestToSendXOnXOff.ToString());
            cbx_handshake.Items.Add(System.IO.Ports.Handshake.XOnXOff.ToString());

            cbx_parity.Items.Add(System.IO.Ports.Parity.Even.ToString());
            cbx_parity.Items.Add(System.IO.Ports.Parity.Mark.ToString());
            cbx_parity.Items.Add(System.IO.Ports.Parity.None.ToString());
            cbx_parity.Items.Add(System.IO.Ports.Parity.Odd.ToString());
            cbx_parity.Items.Add(System.IO.Ports.Parity.Space.ToString());

            for (int clk = 0; clk < 11; clk++) cbx_dataBits.Items.Add(clk);

            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.None.ToString());
            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.One.ToString());
            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.OnePointFive.ToString());
            cbx_stopBits.Items.Add(System.IO.Ports.StopBits.Two.ToString());

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
    }
}
