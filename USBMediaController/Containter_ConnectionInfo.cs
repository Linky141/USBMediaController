using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBMediaController
{
    public class Containter_ConnectionInfo
    {
        /*
         * declarations
         */

        public System.IO.Ports.SerialPort serial = new System.IO.Ports.SerialPort();

        private string portName = "COM1";
        private int baudRate = 9600;
        private string handshake = "None";
        private string parity = "None";
        private int dataBits = 8;
        private string stopBits = "One";
        private int readTimeout = 200;
        private int writeTimeout = 50;

        /*
         * GETTERS, SETTERS, CONSTRUCTOR
         */

        public Containter_ConnectionInfo() { }
        public Containter_ConnectionInfo(System.IO.Ports.SerialPort serial) { this.serial = serial; }
        public Containter_ConnectionInfo(string portName, int baudRate, string handshake, string parity, int dataBits, string stopBits, int readTimeout, int writeTimeout) {
            this.portName = portName;
            this.baudRate = baudRate;
            this.handshake = handshake;
            this.parity = parity;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
            this.readTimeout = readTimeout;
            this.writeTimeout = writeTimeout;
        }

        public int getBaudRate() { return baudRate; }
        public void setBaudRate(int val) { baudRate = val; }
        public string getPortName() { return portName; }
        public void setPortName(string val) { portName = val; }
        public string getHandshake() { return handshake; }
        public void setHandshake(string val) { handshake = val; }
        public string getParity() { return parity; }
        public void setParity(string val) { parity = val; }
        public int getDataBits() { return dataBits; }
        public void setDataBits(int val) { dataBits = val; }
        public string getStopBits() { return stopBits; }
        public void setStopBits(string val) { stopBits = val; }
        public int getReadTimeout() { return readTimeout; }
        public void setReadTimeout(int val) { readTimeout = val; }
        public int getWriteTimeout() { return writeTimeout; }
        public void setWriteTimeout(int val) { writeTimeout = val; }





        /*
         * OTHER METHODS
         */

        public void SetSerial()
        {
            serial.PortName = portName;
            serial.BaudRate = baudRate;
            if(handshake.Equals("None")) serial.Handshake = System.IO.Ports.Handshake.None;
            else if (handshake.Equals("RequestToSend")) serial.Handshake = System.IO.Ports.Handshake.RequestToSend;
            else if (handshake.Equals("RequestToSendXOnXOff")) serial.Handshake = System.IO.Ports.Handshake.RequestToSendXOnXOff;
            else if (handshake.Equals("XOnXOff")) serial.Handshake = System.IO.Ports.Handshake.XOnXOff;
            if (parity.Equals("Even")) serial.Parity = System.IO.Ports.Parity.Even;
            else if (parity.Equals("Mark")) serial.Parity = System.IO.Ports.Parity.Mark;
            else if (parity.Equals("None")) serial.Parity = System.IO.Ports.Parity.None;
            else if (parity.Equals("Odd")) serial.Parity = System.IO.Ports.Parity.Odd;
            else if (parity.Equals("Space")) serial.Parity = System.IO.Ports.Parity.Space;
            serial.DataBits = dataBits;
            if (stopBits.Equals("None")) serial.StopBits = System.IO.Ports.StopBits.None;
            else if (stopBits.Equals("One")) serial.StopBits = System.IO.Ports.StopBits.One;
            else if (stopBits.Equals("OnePointFive")) serial.StopBits = System.IO.Ports.StopBits.OnePointFive;
            else if (stopBits.Equals("Two")) serial.StopBits = System.IO.Ports.StopBits.Two;
            serial.ReadTimeout = readTimeout;
            serial.WriteTimeout = writeTimeout;
        }



    }
}
