using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBMediaController
{
    public class Container_SingleCommand
    {
        private string field;
        private string command;
        private string description;

        public Container_SingleCommand()
        {
        }

        public Container_SingleCommand(string command, string description, string field)
        {
            this.command = command;
            this.description = description;
            this.field = field;
        }

        public string getCommand() { return command; }
        public void setCommand(string val) { command = val; }
        public string getDescription() { return description; }
        public void setDescription(string val) { description = val; }
        public string getField() { return field; }
        public void setField(string val) { field = val; }


    }

}
