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

        private string portName;
        private int baudRate;
        private System.IO.Ports.Handshake handshake;
        private System.IO.Ports.Parity parity;
        private int dataBits = 8;
        private System.IO.Ports.StopBits stopBits;
        private int readTimeout = 200;
        private int writeTimeout = 50;

        /*
         * GETTERS, SETTERS, CONSTRUCTOR
         */

        public Containter_ConnectionInfo() { }
        public Containter_ConnectionInfo(System.IO.Ports.SerialPort serial) { this.serial = serial; }
        public Containter_ConnectionInfo(string portName, int baudRate, System.IO.Ports.Handshake handshake, System.IO.Ports.Parity parity, int dataBits, System.IO.Ports.StopBits stopBits, int readTimeout, int writeTimeout) {
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
        public System.IO.Ports.Handshake getHandshake() { return handshake; }
        public void setHandshake(System.IO.Ports.Handshake val) { handshake = val; }
        public System.IO.Ports.Parity getParity() { return parity; }
        public void setParity(System.IO.Ports.Parity val) { parity = val; }
        public int getDataBits() { return dataBits; }
        public void setDataBits(int val) { dataBits = val; }
        public System.IO.Ports.StopBits getStopBits() { return stopBits; }
        public void setStopBits(System.IO.Ports.StopBits val) { stopBits = val; }
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
            serial.Handshake = handshake;
            serial.Parity = parity;
            serial.DataBits = dataBits;
            serial.StopBits = stopBits;
            serial.ReadTimeout = readTimeout;
            serial.WriteTimeout = writeTimeout;
        }



    }
}
