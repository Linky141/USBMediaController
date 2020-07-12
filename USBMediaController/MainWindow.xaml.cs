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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;

namespace USBMediaController
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //------------------------------------------------------------------------------------
        #region VARIABLES

        Containter_ConnectionInfo connectionInfo = new Containter_ConnectionInfo();
        Container_ControllerConfig controllerConfig = new Container_ControllerConfig();
        string recivedData;
        string commandTmp="";
        string uartReciverBuffer = "";

        private delegate void UpdateUiTextDelegate(string text);
        #endregion

        //------------------------------------------------------------------------------------
        #region CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();
            pbarSendUartData.Maximum = 16;
            pbarSendUartData.Minimum = 0;
            pbarSendUartData.Value = 0;
            pbarSendUartData.Visibility = Visibility.Hidden;

            if(File.Exists(@"C:\USBMediaControllerv2\icon.ico")) tray_main.Icon = new System.Drawing.Icon(@"C:\USBMediaControllerv2\icon.ico");
            else ConsoleWrite("#Error Load Icon");

            if (!LoadCommunicationConfig())
            {
                ConsoleWrite("#Error Load Connection Config");
                ShowSysTrayInfo("USBMediaController", "Error Load Connection Config", BalloonIcon.Error);
            }
            else
            {
                btn_connect.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
            if (!LoadControllerConfig())
            {
                ConsoleWrite("#Error Load Controller Config");
                ShowSysTrayInfo("USBMediaController", "Error Load Controller Config", BalloonIcon.Error);
            }
            if (!LoadControllerCurrentSelected())
            {
                ConsoleWrite("#Error Load Controller Current Selected Profile Config");
                ShowSysTrayInfo("USBMediaController", "Error Load Controller Current Selected Profile Config", BalloonIcon.Error);
            }
            CheckFirstRun();
            SetAllInfoControls();
            InitializeDevice();
            //CenterWindowOnScreen();
            this.Hide();
        }

        #endregion

        //------------------------------------------------------------------------------------
        #region METHODS

        /*private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }*/

        private void SendUART(string data, int count)
        {
            if (connectionInfo.serial.IsOpen)
            {
                try
                {
                    byte[] hexstring = Encoding.ASCII.GetBytes(data);
                    foreach (byte hexval in hexstring)
                    {
                        byte[] _hexval = new byte[] { hexval };
                        connectionInfo.serial.Write(_hexval, 0, count);
                        Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleWrite("#Failed send: " + data + "(" + ex.Message + ")");
                }
            }
            else
            {
            }
        }

        private async void InitializeDevice()
        {
            btn_sendData.Visibility = Visibility.Hidden;
            btn_sendData.Opacity = 0;
            pbarSendUartData.Visibility = Visibility.Visible;
            pbarSendUartData.Value = 0;
            pbarSendUartData.Opacity = 1;
            await Task.Run(() =>
            {
                int id = 0;
               
                for (int clk = 0; clk < controllerConfig.list.Count; clk++) if (controllerConfig.list[clk].getLabel() == controllerConfig.getSelectedLabel()) id = clk;
                for (int clk = 0; clk < 4; clk++)
                {
                    SendUART(controllerConfig.list[id].getPage1(clk).getDescription(), 1);
                    SendUART("\n", 1);
                    tbx_console.Dispatcher.Invoke(() => ConsoleWrite("<--: " + controllerConfig.list[id].getPage1(clk).getDescription()));
                    Thread.Sleep(200);
                    pbarSendUartData.Dispatcher.Invoke(() => pbarSendUartData.Value++);
                    
                }
                for (int clk = 0; clk < 4; clk++)
                {
                    SendUART(controllerConfig.list[id].getPage2(clk).getDescription(), 1);
                    SendUART("\n", 1);
                    tbx_console.Dispatcher.Invoke(() => ConsoleWrite("<--: " + controllerConfig.list[id].getPage2(clk).getDescription()));
                    Thread.Sleep(200);
                    pbarSendUartData.Dispatcher.Invoke(() => pbarSendUartData.Value++);
                  
                }
                for (int clk = 0; clk < 4; clk++)
                {
                    SendUART(controllerConfig.list[id].getPage3(clk).getDescription(), 1);
                    SendUART("\n", 1);
                    tbx_console.Dispatcher.Invoke(() => ConsoleWrite("<--: " + controllerConfig.list[id].getPage3(clk).getDescription()));
                    Thread.Sleep(200);
                    pbarSendUartData.Dispatcher.Invoke(() => pbarSendUartData.Value++);
                  
                }
                for (int clk = 0; clk < 4; clk++)
                {
                    SendUART(controllerConfig.list[id].getPage4(clk).getDescription(), 1);
                    SendUART("\n", 1);
                    tbx_console.Dispatcher.Invoke(() => ConsoleWrite("<--: " + controllerConfig.list[id].getPage4(clk).getDescription()));
                    Thread.Sleep(200);
                    pbarSendUartData.Dispatcher.Invoke(() => pbarSendUartData.Value++);
      
                }

                for (int clk = 0; clk < 10; clk++) { pbarSendUartData.Dispatcher.Invoke(() => pbarSendUartData.Opacity -= 0.1); Thread.Sleep(50); }
                btn_sendData.Dispatcher.Invoke(() => btn_sendData.Visibility = Visibility.Visible);
                for (int clk = 0; clk < 10; clk++) { btn_sendData.Dispatcher.Invoke(() => btn_sendData.Opacity += 0.1); Thread.Sleep(50); }
                Thread.Sleep(200);
                pbarSendUartData.Dispatcher.Invoke(() => pbarSendUartData.Visibility = Visibility.Hidden);
                
            });

        }

        private string getConnectionStatus()
        {
            if (connectionInfo.serial.IsOpen) return "Connected at port " + connectionInfo.getPortName();
            else return "Disconnected";
        }

        private void SetConnectionInfoFields()
        {
            lbl_connectionStatus.Content = getConnectionStatus();
            lbl_trayInfoConnection.Content = getConnectionStatus();
        }

        private void SetProfileInfoFields()
        {
            lbl_selectedProfile.Content = controllerConfig.getSelectedLabel();
            lbl_trayInfoProfile.Content = "Profile: " + controllerConfig.getSelectedLabel();
        }

        private void SetAllInfoControls()
        {
            SetConnectionInfoFields();
            SetProfileInfoFields();
        }

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        public void CheckInputCommand(string command)
        {
            commandTmp += command;
            if (commandTmp.Length >= 4)
            {
                string commandSource = commandTmp.Substring(0, 4);
                commandTmp = "";
                //ConsoleWrite("$"+commandSource);

                // comands list: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes?redirectedfrom=MSDN
                if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Mute")
                {
                    keybd_event((byte)0xAD, 0, 0, 0);
                    keybd_event((byte)0xAD, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Volume DOWN") 
                {
                    keybd_event((byte)0xAE, 0, 0, 0);
                    keybd_event((byte)0xAE, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Volume UP")
                {
                    keybd_event((byte)0xAF, 0, 0, 0);
                    keybd_event((byte)0xAF, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Play")
                {
                    keybd_event((byte)0xB3, 0, 0, 0);
                    keybd_event((byte)0xB3, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Next track")
                {
                    keybd_event((byte)0xB0, 0, 0, 0);
                    keybd_event((byte)0xB0, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Last track")
                {
                    keybd_event((byte)0xB1, 0, 0, 0);
                    keybd_event((byte)0xB1, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Task manager")
                {
                    keybd_event((byte)0x11, 0, 0, 0);
                    keybd_event((byte)0x10, 0, 0, 0);
                    keybd_event((byte)0x1B, 0, 0, 0);
                    keybd_event((byte)0x11, 0, KEYEVENTF_KEYUP | 0, 0);
                    keybd_event((byte)0x10, 0, KEYEVENTF_KEYUP | 0, 0);
                    keybd_event((byte)0x1B, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Close")
                {
                    keybd_event((byte)0x12, 0, 0, 0);
                    keybd_event((byte)0x73, 0, 0, 0);
                    keybd_event((byte)0x12, 0, KEYEVENTF_KEYUP | 0, 0);
                    keybd_event((byte)0x73, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Run")
                {
                    keybd_event((byte)0x5B, 0, 0, 0);
                    keybd_event((byte)0x52, 0, 0, 0);
                    keybd_event((byte)0x5B, 0, KEYEVENTF_KEYUP | 0, 0);
                    keybd_event((byte)0x52, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()) == "Magnifier")
                {
                    keybd_event((byte)0x5B, 0, 0, 0);
                    keybd_event((byte)0xBB, 0, 0, 0);
                    keybd_event((byte)0x5B, 0, KEYEVENTF_KEYUP | 0, 0);
                    keybd_event((byte)0xBB, 0, KEYEVENTF_KEYUP | 0, 0);
                }
                else if (commandSource == "RTW!") //Ready To Write
                {
                    InitializeDevice();
                }
                //else if (commandSource == "GAD!") //Get Actual Data
                //{
                //    ConsoleWrite(">> Sending GAD Data!");
                //    Thread.Sleep(10);
                //    SendUART("Test1", 1);
                //    Thread.Sleep(10);
                //    SendUART("Test2", 1);
                //    Thread.Sleep(10);
                //    SendUART("Test3", 1);
                //    Thread.Sleep(10);
                //    SendUART("Test4", 1);
                //}
                else
                {
                    System.Diagnostics.Process.Start("CMD.exe", @"C:\Windows\System32\cmd.exe /k " + controllerConfig.getCommandByID(commandSource, controllerConfig.getSelectedLabel()));
                }
                    Thread.Sleep(10);
            }
        }


        private void CheckFirstRun()
        {
            if(controllerConfig.list.Count == 0)
            {
                Container_ControllerConfig tmp = new Container_ControllerConfig();
                tmp.setLabel("Default");
                controllerConfig.setSelectedLabel("Default");

                tmp.setPage1(0, new Container_SingleCommand("---", "---", "aa11"));
                tmp.setPage1(1, new Container_SingleCommand("---", "---", "aa22"));
                tmp.setPage1(2, new Container_SingleCommand("---", "---", "aa33"));
                tmp.setPage1(3, new Container_SingleCommand("---", "---", "aa44"));
                tmp.setPage2(0, new Container_SingleCommand("---", "---", "bb11"));
                tmp.setPage2(1, new Container_SingleCommand("---", "---", "bb22"));
                tmp.setPage2(2, new Container_SingleCommand("---", "---", "bb33"));
                tmp.setPage2(3, new Container_SingleCommand("---", "---", "bb44"));
                tmp.setPage3(0, new Container_SingleCommand("---", "---", "cc11"));
                tmp.setPage3(1, new Container_SingleCommand("---", "---", "cc22"));
                tmp.setPage3(2, new Container_SingleCommand("---", "---", "cc33"));
                tmp.setPage3(3, new Container_SingleCommand("---", "---", "cc44"));
                tmp.setPage4(0, new Container_SingleCommand("---", "---", "dd11"));
                tmp.setPage4(1, new Container_SingleCommand("---", "---", "dd22"));
                tmp.setPage4(2, new Container_SingleCommand("---", "---", "dd33"));
                tmp.setPage4(3, new Container_SingleCommand("---", "---", "dd44"));

                controllerConfig.list.Add(tmp);
            }
        }


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
            uartReciverBuffer += text;
            if(uartReciverBuffer.Length >4)
            {
                CheckInputCommand(uartReciverBuffer);
                ConsoleWrite(uartReciverBuffer);
                uartReciverBuffer = "";
            }
        }

        private void ConsoleWrite(string val)
        {
            tbx_console.Text += val + "\n";


            tbx_console.SelectionStart = tbx_console.Text.Length;
            tbx_console.ScrollToEnd();
        }

        #endregion

        //------------------------------------------------------------------------------------
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


        private void SaveControllerConfig()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) Directory.CreateDirectory(@"C:\USBMediaControllerv2\");
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\USBMediaControllerv2\ConfigController.bin", FileMode.Create)))
            {
                for(int clk = 0; clk < controllerConfig.list.Count; clk++)
                {
                    binWriter.Write(controllerConfig.list[clk].getLabel());

                    binWriter.Write(controllerConfig.list[clk].getPage1(0).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage1(0).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage1(1).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage1(1).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage1(2).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage1(2).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage1(3).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage1(3).getDescription());

                    binWriter.Write(controllerConfig.list[clk].getPage2(0).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage2(0).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage2(1).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage2(1).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage2(2).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage2(2).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage2(3).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage2(3).getDescription());

                    binWriter.Write(controllerConfig.list[clk].getPage3(0).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage3(0).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage3(1).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage3(1).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage3(2).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage3(2).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage3(3).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage3(3).getDescription());

                    binWriter.Write(controllerConfig.list[clk].getPage4(0).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage4(0).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage4(1).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage4(1).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage4(2).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage4(2).getDescription());
                    binWriter.Write(controllerConfig.list[clk].getPage4(3).getCommand());
                    binWriter.Write(controllerConfig.list[clk].getPage4(3).getDescription());
                }
            }
        }

        private bool LoadControllerConfig()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) return false;
            if (!File.Exists(@"C:\USBMediaControllerv2\ConfigController.bin")) return false;
            using (BinaryReader binReader = new BinaryReader(File.Open(@"C:\USBMediaControllerv2\ConfigController.bin", FileMode.Open)))
            {
                int size = LoadControllerListSize();

                for (int clk = 0; clk < size; clk++)
                {
                    Container_ControllerConfig tmp = new Container_ControllerConfig();
                    tmp.setLabel(binReader.ReadString());

                    tmp.setPage1(0, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "aa11"));
                    tmp.setPage1(1, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "aa22"));
                    tmp.setPage1(2, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "aa33"));
                    tmp.setPage1(3, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "aa44"));

                    tmp.setPage2(0, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "bb11"));
                    tmp.setPage2(1, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "bb22"));
                    tmp.setPage2(2, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "bb33"));
                    tmp.setPage2(3, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "bb44"));

                    tmp.setPage3(0, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "cc11"));
                    tmp.setPage3(1, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "cc22"));
                    tmp.setPage3(2, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "cc33"));
                    tmp.setPage3(3, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "cc44"));

                    tmp.setPage4(0, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "dd11"));
                    tmp.setPage4(1, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "dd22"));
                    tmp.setPage4(2, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "dd33"));
                    tmp.setPage4(3, new Container_SingleCommand(binReader.ReadString(), binReader.ReadString(), "dd44"));

                    controllerConfig.list.Add(tmp);
                }
            }
            return true;
        }

        private void SaveControllerCurrentSelected()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) Directory.CreateDirectory(@"C:\USBMediaControllerv2\");
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\USBMediaControllerv2\ConfigConnectionCurrent.bin", FileMode.Create)))
            {
                binWriter.Write(controllerConfig.getSelectedLabel());
            }
        }

        private bool LoadControllerCurrentSelected()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) return false;
            if (!File.Exists(@"C:\USBMediaControllerv2\ConfigConnectionCurrent.bin")) return false;
            using (BinaryReader binReader = new BinaryReader(File.Open(@"C:\USBMediaControllerv2\ConfigConnectionCurrent.bin", FileMode.Open)))
            {
                controllerConfig.setSelectedLabel(binReader.ReadString());
            }
            return true;
        }

        private void SaveControllerListSize()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) Directory.CreateDirectory(@"C:\USBMediaControllerv2\");
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\USBMediaControllerv2\ConfigConnectionSize.bin", FileMode.Create)))
            {
                binWriter.Write(controllerConfig.list.Count);
            }
        }

        private int LoadControllerListSize()
        {
            if (!Directory.Exists(@"C:\USBMediaControllerv2\")) return 0;
            if (!File.Exists(@"C:\USBMediaControllerv2\ConfigConnectionSize.bin")) return 0;
            int size = 0;
            using (BinaryReader binReader = new BinaryReader(File.Open(@"C:\USBMediaControllerv2\ConfigConnectionSize.bin", FileMode.Open)))
            {
                size = binReader.ReadInt32();
            }
            return size;
        }


        #endregion

        //------------------------------------------------------------------------------------
        #region SLOTS

        private void btn_sendData_Click(object sender, RoutedEventArgs e)
        {
            InitializeDevice();
        }

        private void btn_ConnectionSettings_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSettings connectionSettings = new ConnectionSettings(connectionInfo);
            connectionSettings.ShowDialog();
            if (connectionSettings.AcceptChanges())
            {
                connectionInfo = connectionSettings.getConnectionInfo();
                SaveCommunicationConfig();
            }
            SetAllInfoControls();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            try
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
                SetAllInfoControls();
            }
            catch (Exception exception)
            {
                ShowSysTrayInfo("USBMediaController", exception.Message, BalloonIcon.Error);
                ConsoleWrite("#ERROR: " + exception.Message);
            }
        }

        private void btn_CommandSettings_Click(object sender, RoutedEventArgs e)
        {
            ControllerConfig window = new ControllerConfig(controllerConfig);
            window.ShowDialog();
            if (window.Apply())
            {
                controllerConfig = window.GetConfig();
                SaveControllerConfig();
                SaveControllerCurrentSelected();
                SaveControllerListSize();
            }
            SetAllInfoControls();
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
            if(e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void btn_clearConsole_Click(object sender, RoutedEventArgs e)
        {
            tbx_console.Clear();
        }



        #endregion

    }
}
