using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace USBMediaController
{
    public class Container_ControllerConfig
    {
        public List<Container_ControllerConfig> list = new List<Container_ControllerConfig>();

        Container_SingleCommand[] page1 = new Container_SingleCommand[4];
        Container_SingleCommand[] page2 = new Container_SingleCommand[4];
        Container_SingleCommand[] page3 = new Container_SingleCommand[4];
        Container_SingleCommand[] page4 = new Container_SingleCommand[4];
        string label;
        string selectedLabel;

        #region CONSTRUCTORS
        public Container_ControllerConfig(Container_SingleCommand[] page1, Container_SingleCommand[] page2, Container_SingleCommand[] page3, Container_SingleCommand[] page4, string label)
        {
            this.page1 = page1;
            this.page2 = page2;
            this.page3 = page3;
            this.page4 = page4;
            this.label = label;
        }

        public Container_ControllerConfig()
        {
        }
        #endregion

        #region GET SET

        public string getCommandByID(string command, string listLabel)
        {
            int id=0;
            for (int clk = 0; clk < list.Count; clk++) if (list[clk].getLabel() == listLabel) id = clk;
            for (int clk = 0; clk < page1.Length; clk++)
            {
                if (list[id].page1[clk].getField() == command) return list[id].page1[clk].getCommand();
                if (list[id].page2[clk].getField() == command) return list[id].page2[clk].getCommand();
                if (list[id].page3[clk].getField() == command) return list[id].page3[clk].getCommand();
                if (list[id].page4[clk].getField() == command) return list[id].page4[clk].getCommand();
            }
            return "";
        }
        public Container_SingleCommand getPage1(int num) { return page1[num]; }
        public Container_SingleCommand getPage2(int num) { return page2[num]; }
        public Container_SingleCommand getPage3(int num) { return page3[num]; }
        public Container_SingleCommand getPage4(int num) { return page4[num]; }

        public Container_SingleCommand[] getPage1() { return page1; }
        public Container_SingleCommand[] getPage2() { return page2; }
        public Container_SingleCommand[] getPage3() { return page3; }
        public Container_SingleCommand[] getPage4() { return page4; }
        public string getLabel() { return label; }
        public string getSelectedLabel() { return selectedLabel; }

        public void setPage1(int num, Container_SingleCommand opt) { page1[num] = opt; }
        public void setPage2(int num, Container_SingleCommand opt) { page2[num] = opt; }
        public void setPage3(int num, Container_SingleCommand opt) { page3[num] = opt; }
        public void setPage4(int num, Container_SingleCommand opt) { page4[num] = opt; }
        public void setLabel(string val) { label = val; }
        public void setSelectedLabel(string val) { selectedLabel = val; }

        #endregion


    }
}
