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
    /// Logika interakcji dla klasy ControllerConfig.xaml
    /// </summary>
    public partial class ControllerConfig : Window
    {
        //------------------------------------------------------------------------------------
        #region VARIABLE

        private Container_ControllerConfig container = new Container_ControllerConfig();
        private bool apply = false;

        #endregion

        //------------------------------------------------------------------------------------
        #region CONSTRUCTOR
        public ControllerConfig()
        {
            InitializeComponent();
            FillComboBox();
            RefreshProfileComboBox();
            //CenterWindowOnScreen();
        }

        public ControllerConfig(Container_ControllerConfig container)
        {
            InitializeComponent();
            FillComboBox();
            this.container = container;
            RefreshProfileComboBox();
            //CenterWindowOnScreen();
        }

        #endregion

        //------------------------------------------------------------------------------------
        #region METHODS

       /* private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
*/
        private bool CheckCheckBoxExistingValue(ComboBox cbx, string val) {
            if (cbx.Items.Contains(val)) return true;
            else return false;
            }

        private void RefreshProfileComboBox()
        {
            cbx_profile.Items.Clear();
            for (int clk = 0; clk < container.list.Count; clk++) cbx_profile.Items.Add(container.list[clk].getLabel());
            cbx_profile.SelectedItem = container.getSelectedLabel();

            for (int clk = 0; clk < container.list.Count; clk++)
            {
                if (container.list[clk].getLabel() == container.getSelectedLabel())
                {

                    //Page 1
                    if (CheckCheckBoxExistingValue(cbx_f1p1Action, container.list[clk].getPage1(0).getCommand()))
                    {
                        cbx_f1p1Action.SelectedItem = container.list[clk].getPage1(0).getCommand();
                        chk_f1p1Manual.IsChecked = false;
                        tbx_f1p1Action.IsEnabled = false;
                        cbx_f1p1Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f1p1Action.Text = container.list[clk].getPage1(0).getCommand();
                        chk_f1p1Manual.IsChecked = true;
                        tbx_f1p1Action.IsEnabled = true;
                        cbx_f1p1Action.IsEnabled = false;
                    }
                    tbx_f1p1Description.Text = container.list[clk].getPage1(0).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f2p1Action, container.list[clk].getPage1(1).getCommand()))
                    {
                        cbx_f2p1Action.SelectedItem = container.list[clk].getPage1(1).getCommand();
                        chk_f2p1Manual.IsChecked = false;
                        tbx_f2p1Action.IsEnabled = false;
                        cbx_f2p1Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f2p1Action.Text = container.list[clk].getPage1(1).getCommand();
                        chk_f2p1Manual.IsChecked = true;
                        tbx_f2p1Action.IsEnabled = true;
                        cbx_f2p1Action.IsEnabled = false;
                    }
                    tbx_f2p1Description.Text = container.list[clk].getPage1(1).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f3p1Action, container.list[clk].getPage1(2).getCommand()))
                    {
                        cbx_f3p1Action.SelectedItem = container.list[clk].getPage1(2).getCommand();
                        chk_f3p1Manual.IsChecked = false;
                        tbx_f3p1Action.IsEnabled = false;
                        cbx_f3p1Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f3p1Action.Text = container.list[clk].getPage1(2).getCommand();
                        chk_f3p1Manual.IsChecked = true;
                        tbx_f2p1Action.IsEnabled = true;
                        cbx_f2p1Action.IsEnabled = false;
                    }
                    tbx_f3p1Description.Text = container.list[clk].getPage1(2).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f4p1Action, container.list[clk].getPage1(3).getCommand()))
                    {
                        cbx_f4p1Action.SelectedItem = container.list[clk].getPage1(3).getCommand();
                        chk_f4p1Manual.IsChecked = false;
                        tbx_f4p1Action.IsEnabled = false;
                        cbx_f4p1Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f4p1Action.Text = container.list[clk].getPage1(3).getCommand();
                        chk_f4p1Manual.IsChecked = true;
                        tbx_f2p1Action.IsEnabled = true;
                        cbx_f2p1Action.IsEnabled = false;
                    }
                    tbx_f4p1Description.Text = container.list[clk].getPage1(3).getDescription();

                    //Page 2
                    if (CheckCheckBoxExistingValue(cbx_f1p2Action, container.list[clk].getPage2(0).getCommand()))
                    {
                        cbx_f1p2Action.SelectedItem = container.list[clk].getPage2(0).getCommand();
                        chk_f1p2Manual.IsChecked = false;
                        tbx_f1p2Action.IsEnabled = false;
                        cbx_f1p2Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f1p2Action.Text = container.list[clk].getPage2(0).getCommand();
                        chk_f1p2Manual.IsChecked = true;
                        tbx_f1p2Action.IsEnabled = true;
                        cbx_f1p2Action.IsEnabled = false;
                    }
                    tbx_f1p2Description.Text = container.list[clk].getPage2(0).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f2p2Action, container.list[clk].getPage2(1).getCommand()))
                    {
                        cbx_f2p2Action.SelectedItem = container.list[clk].getPage2(1).getCommand();
                        chk_f2p2Manual.IsChecked = false;
                        tbx_f2p2Action.IsEnabled = false;
                        cbx_f2p2Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f2p2Action.Text = container.list[clk].getPage2(1).getCommand();
                        chk_f2p2Manual.IsChecked = true;
                        tbx_f2p2Action.IsEnabled = true;
                        cbx_f2p2Action.IsEnabled = false;
                    }
                    tbx_f2p2Description.Text = container.list[clk].getPage2(1).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f3p2Action, container.list[clk].getPage2(2).getCommand()))
                    {
                        cbx_f3p2Action.SelectedItem = container.list[clk].getPage2(2).getCommand();
                        chk_f3p2Manual.IsChecked = false;
                        tbx_f3p2Action.IsEnabled = false;
                        cbx_f3p2Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f3p2Action.Text = container.list[clk].getPage2(2).getCommand();
                        chk_f3p2Manual.IsChecked = true;
                        tbx_f3p2Action.IsEnabled = true;
                        cbx_f3p2Action.IsEnabled = false;
                    }
                    tbx_f3p2Description.Text = container.list[clk].getPage2(2).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f4p2Action, container.list[clk].getPage2(3).getCommand()))
                    {
                        cbx_f4p2Action.SelectedItem = container.list[clk].getPage2(3).getCommand();
                        chk_f4p2Manual.IsChecked = false;
                        tbx_f4p2Action.IsEnabled = false;
                        cbx_f4p2Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f4p2Action.Text = container.list[clk].getPage2(3).getCommand();
                        chk_f4p2Manual.IsChecked = true;
                        tbx_f4p2Action.IsEnabled = true;
                        cbx_f4p2Action.IsEnabled = false;
                    }
                    tbx_f4p2Description.Text = container.list[clk].getPage2(3).getDescription();


                    //Page 3
                    if (CheckCheckBoxExistingValue(cbx_f1p3Action, container.list[clk].getPage3(0).getCommand()))
                    {
                        cbx_f1p3Action.SelectedItem = container.list[clk].getPage3(0).getCommand();
                        chk_f1p3Manual.IsChecked = false;
                        tbx_f1p3Action.IsEnabled = false;
                        cbx_f1p3Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f1p3Action.Text = container.list[clk].getPage3(0).getCommand();
                        chk_f1p3Manual.IsChecked = true;
                        tbx_f1p3Action.IsEnabled = true;
                        cbx_f1p3Action.IsEnabled = false;
                    }
                    tbx_f1p3Description.Text = container.list[clk].getPage3(0).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f2p3Action, container.list[clk].getPage3(1).getCommand()))
                    {
                        cbx_f2p3Action.SelectedItem = container.list[clk].getPage3(1).getCommand();
                        chk_f2p3Manual.IsChecked = false;
                        tbx_f2p3Action.IsEnabled = false;
                        cbx_f2p3Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f2p3Action.Text = container.list[clk].getPage3(1).getCommand();
                        chk_f2p3Manual.IsChecked = true;
                        tbx_f2p3Action.IsEnabled = true;
                        cbx_f2p3Action.IsEnabled = false;
                    }
                    tbx_f2p3Description.Text = container.list[clk].getPage3(1).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f3p3Action, container.list[clk].getPage3(2).getCommand()))
                    {
                        cbx_f3p3Action.SelectedItem = container.list[clk].getPage3(2).getCommand();
                        chk_f3p3Manual.IsChecked = false;
                        tbx_f3p3Action.IsEnabled = false;
                        cbx_f3p3Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f3p3Action.Text = container.list[clk].getPage3(2).getCommand();
                        chk_f3p3Manual.IsChecked = true;
                        tbx_f3p3Action.IsEnabled = true;
                        cbx_f3p3Action.IsEnabled = false;
                    }
                    tbx_f3p3Description.Text = container.list[clk].getPage3(2).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f4p3Action, container.list[clk].getPage3(3).getCommand()))
                    {
                        cbx_f4p3Action.SelectedItem = container.list[clk].getPage3(3).getCommand();
                        chk_f4p3Manual.IsChecked = false;
                        tbx_f4p3Action.IsEnabled = false;
                        cbx_f4p3Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f4p3Action.Text = container.list[clk].getPage3(3).getCommand();
                        chk_f4p3Manual.IsChecked = true;
                        tbx_f4p3Action.IsEnabled = true;
                        cbx_f4p3Action.IsEnabled = false;
                    }
                    tbx_f4p3Description.Text = container.list[clk].getPage3(3).getDescription();

                    //Page 4
                    if (CheckCheckBoxExistingValue(cbx_f1p4Action, container.list[clk].getPage4(0).getCommand()))
                    {
                        cbx_f1p4Action.SelectedItem = container.list[clk].getPage4(0).getCommand();
                        chk_f1p4Manual.IsChecked = false;
                        tbx_f1p4Action.IsEnabled = false;
                        cbx_f1p4Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f1p4Action.Text = container.list[clk].getPage4(0).getCommand();
                        chk_f1p4Manual.IsChecked = true;
                        tbx_f1p4Action.IsEnabled = true;
                        cbx_f1p4Action.IsEnabled = false;
                    }
                    tbx_f1p4Description.Text = container.list[clk].getPage4(0).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f2p4Action, container.list[clk].getPage4(1).getCommand()))
                    {
                        cbx_f2p4Action.SelectedItem = container.list[clk].getPage4(1).getCommand();
                        chk_f2p4Manual.IsChecked = false;
                        tbx_f2p4Action.IsEnabled = false;
                        cbx_f2p4Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f2p4Action.Text = container.list[clk].getPage4(1).getCommand();
                        chk_f2p4Manual.IsChecked = true;
                        tbx_f2p4Action.IsEnabled = true;
                        cbx_f2p4Action.IsEnabled = false;
                    }
                    tbx_f2p4Description.Text = container.list[clk].getPage4(1).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f3p4Action, container.list[clk].getPage4(2).getCommand()))
                    {
                        cbx_f3p4Action.SelectedItem = container.list[clk].getPage4(2).getCommand();
                        chk_f3p4Manual.IsChecked = false;
                        tbx_f3p4Action.IsEnabled = false;
                        cbx_f3p4Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f3p4Action.Text = container.list[clk].getPage4(2).getCommand();
                        chk_f3p4Manual.IsChecked = true;
                        tbx_f3p4Action.IsEnabled = true;
                        cbx_f3p4Action.IsEnabled = false;
                    }
                    tbx_f3p4Description.Text = container.list[clk].getPage4(2).getDescription();

                    if (CheckCheckBoxExistingValue(cbx_f4p4Action, container.list[clk].getPage4(3).getCommand()))
                    {
                        cbx_f4p4Action.SelectedItem = container.list[clk].getPage4(3).getCommand();
                        chk_f4p4Manual.IsChecked = false;
                        tbx_f4p4Action.IsEnabled = false;
                        cbx_f4p4Action.IsEnabled = true;
                    }
                    else
                    {
                        tbx_f4p4Action.Text = container.list[clk].getPage4(3).getCommand();
                        chk_f4p4Manual.IsChecked = true;
                        tbx_f4p4Action.IsEnabled = true;
                        cbx_f4p4Action.IsEnabled = false;
                    }
                    tbx_f4p4Description.Text = container.list[clk].getPage4(3).getDescription();
                }
            }
        }

        private void FillComboBox()
        {
            List<string> actions = new List<string>();
            actions.Add("Play");
            actions.Add("Next track");
            actions.Add("Last track");
            actions.Add("Stop");
            actions.Add("Volume UP");
            actions.Add("Volume DOWN");
            actions.Add("Mute");
            actions.Add("Task manager");
            actions.Add("Close");
            actions.Add("Run");
            actions.Add("Magnifier");
            actions.Add("NextDesktop");
            actions.Add("LastDesktop");

            for (int clk = 0; clk < actions.Count; clk++)
            {
                cbx_f1p1Action.Items.Add(actions[clk]);
                cbx_f2p1Action.Items.Add(actions[clk]);
                cbx_f3p1Action.Items.Add(actions[clk]);
                cbx_f4p1Action.Items.Add(actions[clk]);
                cbx_f1p2Action.Items.Add(actions[clk]);
                cbx_f2p2Action.Items.Add(actions[clk]);
                cbx_f3p2Action.Items.Add(actions[clk]);
                cbx_f4p2Action.Items.Add(actions[clk]);
                cbx_f1p3Action.Items.Add(actions[clk]);
                cbx_f2p3Action.Items.Add(actions[clk]);
                cbx_f3p3Action.Items.Add(actions[clk]);
                cbx_f4p3Action.Items.Add(actions[clk]);
                cbx_f1p4Action.Items.Add(actions[clk]);
                cbx_f2p4Action.Items.Add(actions[clk]);
                cbx_f3p4Action.Items.Add(actions[clk]);
                cbx_f4p4Action.Items.Add(actions[clk]);
            }

            cbx_f1p1Action.SelectedIndex = 0;
            cbx_f2p1Action.SelectedIndex = 0;
            cbx_f3p1Action.SelectedIndex = 0;
            cbx_f4p1Action.SelectedIndex = 0;
            cbx_f1p2Action.SelectedIndex = 0;
            cbx_f2p2Action.SelectedIndex = 0;
            cbx_f3p2Action.SelectedIndex = 0;
            cbx_f4p2Action.SelectedIndex = 0;
            cbx_f1p3Action.SelectedIndex = 0;
            cbx_f2p3Action.SelectedIndex = 0;
            cbx_f3p3Action.SelectedIndex = 0;
            cbx_f4p3Action.SelectedIndex = 0;
            cbx_f1p4Action.SelectedIndex = 0;
            cbx_f2p4Action.SelectedIndex = 0;
            cbx_f3p4Action.SelectedIndex = 0;
            cbx_f4p4Action.SelectedIndex = 0;
        }

        #endregion

        //------------------------------------------------------------------------------------
        #region SLOTS

        private void btn_profileLoad_Click(object sender, RoutedEventArgs e)
        {
            container.setSelectedLabel(cbx_profile.Text);
            RefreshProfileComboBox();
        }



        private void btn_profileSave_Click(object sender, RoutedEventArgs e)
        {
            ControlerConfigAddItem window = new ControlerConfigAddItem();
            window.ShowDialog();
            if (window.Apply()) {
                Container_ControllerConfig tmp = new Container_ControllerConfig();
                tmp.setLabel(window.getName());


                if (chk_f1p1Manual.IsChecked == true) tmp.setPage1(0, new Container_SingleCommand(tbx_f1p1Action.Text, tbx_f1p1Description.Text, "aa11"));
                else tmp.setPage1(0, new Container_SingleCommand(cbx_f1p1Action.SelectedItem.ToString(), tbx_f1p1Description.Text, "aa11"));

                if (chk_f2p1Manual.IsChecked == true) tmp.setPage1(1, new Container_SingleCommand(tbx_f2p1Action.Text, tbx_f2p1Description.Text, "aa22"));
                else tmp.setPage1(1, new Container_SingleCommand(cbx_f2p1Action.SelectedItem.ToString(), tbx_f2p1Description.Text, "aa22"));

                if (chk_f3p1Manual.IsChecked == true) tmp.setPage1(2, new Container_SingleCommand(tbx_f3p1Action.Text, tbx_f3p1Description.Text, "aa33"));
                else tmp.setPage1(2, new Container_SingleCommand(cbx_f3p1Action.SelectedItem.ToString(), tbx_f3p1Description.Text, "aa33"));

                if (chk_f4p1Manual.IsChecked == true) tmp.setPage1(3, new Container_SingleCommand(tbx_f4p1Action.Text, tbx_f4p1Description.Text, "aa44"));
                else tmp.setPage1(3, new Container_SingleCommand(cbx_f4p1Action.SelectedItem.ToString(), tbx_f4p1Description.Text, "aa44"));


                if (chk_f1p2Manual.IsChecked == true) tmp.setPage2(0, new Container_SingleCommand(tbx_f1p2Action.Text, tbx_f1p2Description.Text, "bb11"));
                else tmp.setPage2(0, new Container_SingleCommand(cbx_f1p2Action.SelectedItem.ToString(), tbx_f1p2Description.Text, "bb11"));

                if (chk_f2p2Manual.IsChecked == true) tmp.setPage2(1, new Container_SingleCommand(tbx_f2p2Action.Text, tbx_f2p2Description.Text, "bb22"));
                else tmp.setPage2(1, new Container_SingleCommand(cbx_f2p2Action.SelectedItem.ToString(), tbx_f2p2Description.Text, "bb22"));

                if (chk_f3p2Manual.IsChecked == true) tmp.setPage2(2, new Container_SingleCommand(tbx_f3p2Action.Text, tbx_f3p2Description.Text, "bb33"));
                else tmp.setPage2(2, new Container_SingleCommand(cbx_f3p2Action.SelectedItem.ToString(), tbx_f3p2Description.Text, "bb33"));

                if (chk_f4p2Manual.IsChecked == true) tmp.setPage2(3, new Container_SingleCommand(tbx_f4p2Action.Text, tbx_f4p2Description.Text, "bb44"));
                else tmp.setPage2(3, new Container_SingleCommand(cbx_f4p2Action.SelectedItem.ToString(), tbx_f4p2Description.Text, "bb44"));


                if (chk_f1p3Manual.IsChecked == true) tmp.setPage3(0, new Container_SingleCommand(tbx_f1p3Action.Text, tbx_f1p3Description.Text, "cc11"));
                else tmp.setPage3(0, new Container_SingleCommand(cbx_f1p3Action.SelectedItem.ToString(), tbx_f1p3Description.Text, "cc11"));

                if (chk_f2p3Manual.IsChecked == true) tmp.setPage3(1, new Container_SingleCommand(tbx_f2p3Action.Text, tbx_f2p3Description.Text, "cc22"));
                else tmp.setPage3(1, new Container_SingleCommand(cbx_f2p3Action.SelectedItem.ToString(), tbx_f2p3Description.Text, "cc22"));

                if (chk_f3p3Manual.IsChecked == true) tmp.setPage3(2, new Container_SingleCommand(tbx_f3p3Action.Text, tbx_f3p3Description.Text, "cc33"));
                else tmp.setPage3(2, new Container_SingleCommand(cbx_f3p3Action.SelectedItem.ToString(), tbx_f3p3Description.Text, "cc33"));

                if (chk_f4p3Manual.IsChecked == true) tmp.setPage3(3, new Container_SingleCommand(tbx_f4p3Action.Text, tbx_f4p3Description.Text, "cc44"));
                else tmp.setPage3(3, new Container_SingleCommand(cbx_f4p3Action.SelectedItem.ToString(), tbx_f4p3Description.Text, "cc44"));


                if (chk_f1p4Manual.IsChecked == true) tmp.setPage4(0, new Container_SingleCommand(tbx_f1p4Action.Text, tbx_f1p4Description.Text, "dd11"));
                else tmp.setPage4(0, new Container_SingleCommand(cbx_f1p4Action.SelectedItem.ToString(), tbx_f1p4Description.Text, "dd11"));

                if (chk_f2p4Manual.IsChecked == true) tmp.setPage4(1, new Container_SingleCommand(tbx_f2p4Action.Text, tbx_f2p4Description.Text, "dd22"));
                else tmp.setPage4(1, new Container_SingleCommand(cbx_f2p4Action.SelectedItem.ToString(), tbx_f2p4Description.Text, "dd22"));

                if (chk_f3p4Manual.IsChecked == true) tmp.setPage4(2, new Container_SingleCommand(tbx_f3p4Action.Text, tbx_f3p4Description.Text, "dd33"));
                else tmp.setPage4(2, new Container_SingleCommand(cbx_f3p4Action.SelectedItem.ToString(), tbx_f3p4Description.Text, "dd33"));

                if (chk_f4p4Manual.IsChecked == true) tmp.setPage4(3, new Container_SingleCommand(tbx_f4p4Action.Text, tbx_f4p4Description.Text, "dd44"));
                else tmp.setPage4(3, new Container_SingleCommand(cbx_f4p4Action.SelectedItem.ToString(), tbx_f4p4Description.Text, "dd44"));


                container.list.Add(tmp);
                container.setSelectedLabel(tmp.getLabel());
                RefreshProfileComboBox();
                cbx_profile.SelectedItem = tmp.getLabel();
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            apply = true;
            this.Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            apply = false;
            this.Close();
        }

        private void btn_profileDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = cbx_profile.SelectedIndex;
            container.list.RemoveAt(index);
            RefreshProfileComboBox();
            if (index <= container.list.Count-1) cbx_profile.SelectedIndex = index;
            else cbx_profile.SelectedIndex = index - 1;
            Object tmpObj=null;
            RoutedEventArgs tmpREA=null;
            btn_profileLoad_Click(tmpObj, tmpREA);
        }

        private void btn_profileOverwrite_Click(object sender, RoutedEventArgs e)
        {
            int index = cbx_profile.SelectedIndex;


                if (chk_f1p1Manual.IsChecked == true) container.list[index].setPage1(0, new Container_SingleCommand(tbx_f1p1Action.Text, tbx_f1p1Description.Text, "aa11"));
                else container.list[index].setPage1(0, new Container_SingleCommand(cbx_f1p1Action.SelectedItem.ToString(), tbx_f1p1Description.Text, "aa11"));

                if (chk_f2p1Manual.IsChecked == true) container.list[index].setPage1(1, new Container_SingleCommand(tbx_f2p1Action.Text, tbx_f2p1Description.Text, "aa22"));
                else container.list[index].setPage1(1, new Container_SingleCommand(cbx_f2p1Action.SelectedItem.ToString(), tbx_f2p1Description.Text, "aa22"));

                if (chk_f3p1Manual.IsChecked == true) container.list[index].setPage1(2, new Container_SingleCommand(tbx_f3p1Action.Text, tbx_f3p1Description.Text, "aa33"));
                else container.list[index].setPage1(2, new Container_SingleCommand(cbx_f3p1Action.SelectedItem.ToString(), tbx_f3p1Description.Text, "aa33"));

                if (chk_f4p1Manual.IsChecked == true) container.list[index].setPage1(3, new Container_SingleCommand(tbx_f4p1Action.Text, tbx_f4p1Description.Text, "aa44"));
                else container.list[index].setPage1(3, new Container_SingleCommand(cbx_f4p1Action.SelectedItem.ToString(), tbx_f4p1Description.Text, "aa44"));


                if (chk_f1p2Manual.IsChecked == true) container.list[index].setPage2(0, new Container_SingleCommand(tbx_f1p2Action.Text, tbx_f1p2Description.Text, "bb11"));
                else container.list[index].setPage2(0, new Container_SingleCommand(cbx_f1p2Action.SelectedItem.ToString(), tbx_f1p2Description.Text, "bb11"));

                if (chk_f2p2Manual.IsChecked == true) container.list[index].setPage2(1, new Container_SingleCommand(tbx_f2p2Action.Text, tbx_f2p2Description.Text, "bb22"));
                else container.list[index].setPage2(1, new Container_SingleCommand(cbx_f2p2Action.SelectedItem.ToString(), tbx_f2p2Description.Text, "bb22"));

                if (chk_f3p2Manual.IsChecked == true) container.list[index].setPage2(2, new Container_SingleCommand(tbx_f3p2Action.Text, tbx_f3p2Description.Text, "bb33"));
                else container.list[index].setPage2(2, new Container_SingleCommand(cbx_f3p2Action.SelectedItem.ToString(), tbx_f3p2Description.Text, "bb33"));

                if (chk_f4p2Manual.IsChecked == true) container.list[index].setPage2(3, new Container_SingleCommand(tbx_f4p2Action.Text, tbx_f4p2Description.Text, "bb44"));
                else container.list[index].setPage2(3, new Container_SingleCommand(cbx_f4p2Action.SelectedItem.ToString(), tbx_f4p2Description.Text, "bb44"));


                if (chk_f1p3Manual.IsChecked == true) container.list[index].setPage3(0, new Container_SingleCommand(tbx_f1p3Action.Text, tbx_f1p3Description.Text, "cc11"));
                else container.list[index].setPage3(0, new Container_SingleCommand(cbx_f1p3Action.SelectedItem.ToString(), tbx_f1p3Description.Text, "cc11"));

                if (chk_f2p3Manual.IsChecked == true) container.list[index].setPage3(1, new Container_SingleCommand(tbx_f2p3Action.Text, tbx_f2p3Description.Text, "cc22"));
                else container.list[index].setPage3(1, new Container_SingleCommand(cbx_f2p3Action.SelectedItem.ToString(), tbx_f2p3Description.Text, "cc22"));

                if (chk_f3p3Manual.IsChecked == true) container.list[index].setPage3(2, new Container_SingleCommand(tbx_f3p3Action.Text, tbx_f3p3Description.Text, "cc33"));
                else container.list[index].setPage3(2, new Container_SingleCommand(cbx_f3p3Action.SelectedItem.ToString(), tbx_f3p3Description.Text, "cc33"));

                if (chk_f4p3Manual.IsChecked == true) container.list[index].setPage3(3, new Container_SingleCommand(tbx_f4p3Action.Text, tbx_f4p3Description.Text, "cc44"));
                else container.list[index].setPage3(3, new Container_SingleCommand(cbx_f4p3Action.SelectedItem.ToString(), tbx_f4p3Description.Text, "cc44"));


                if (chk_f1p4Manual.IsChecked == true) container.list[index].setPage4(0, new Container_SingleCommand(tbx_f1p4Action.Text, tbx_f1p4Description.Text, "dd11"));
                else container.list[index].setPage4(0, new Container_SingleCommand(cbx_f1p4Action.SelectedItem.ToString(), tbx_f1p4Description.Text, "dd11"));

                if (chk_f2p4Manual.IsChecked == true) container.list[index].setPage4(1, new Container_SingleCommand(tbx_f2p4Action.Text, tbx_f2p4Description.Text, "dd22"));
                else container.list[index].setPage4(1, new Container_SingleCommand(cbx_f2p4Action.SelectedItem.ToString(), tbx_f2p4Description.Text, "dd22"));

                if (chk_f3p4Manual.IsChecked == true) container.list[index].setPage4(2, new Container_SingleCommand(tbx_f3p4Action.Text, tbx_f3p4Description.Text, "dd33"));
                else container.list[index].setPage4(2, new Container_SingleCommand(cbx_f3p4Action.SelectedItem.ToString(), tbx_f3p4Description.Text, "dd33"));

                if (chk_f4p4Manual.IsChecked == true) container.list[index].setPage4(3, new Container_SingleCommand(tbx_f4p4Action.Text, tbx_f4p4Description.Text, "dd44"));
                else container.list[index].setPage4(3, new Container_SingleCommand(cbx_f4p4Action.SelectedItem.ToString(), tbx_f4p4Description.Text, "dd44"));

                RefreshProfileComboBox();
        }

        private void chk_f1p1Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f1p1Action.IsEnabled = true;
            cbx_f1p1Action.IsEnabled = false;
        }

        private void chk_f1p1Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f1p1Action.IsEnabled = false;
            cbx_f1p1Action.IsEnabled = true;
        }

        private void chk_f2p1Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f2p1Action.IsEnabled = true;
            cbx_f2p1Action.IsEnabled = false;
        }

        private void chk_f2p1Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f2p1Action.IsEnabled = false;
            cbx_f2p1Action.IsEnabled = true;
        }

        private void chk_f3p1Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f3p1Action.IsEnabled = true;
            cbx_f3p1Action.IsEnabled = false;
        }

        private void chk_f3p1Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f3p1Action.IsEnabled = false;
            cbx_f3p1Action.IsEnabled = true;
        }

        private void chk_f4p1Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f4p1Action.IsEnabled = true;
            cbx_f4p1Action.IsEnabled = false;
        }

        private void chk_f4p1Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f4p1Action.IsEnabled = false;
            cbx_f4p1Action.IsEnabled = true;
        }

        private void chk_f1p2Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f1p2Action.IsEnabled = true;
            cbx_f1p2Action.IsEnabled = false;
        }

        private void chk_f1p2Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f1p2Action.IsEnabled = false;
            cbx_f1p2Action.IsEnabled = true;
        }

        private void chk_f2p2Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f2p2Action.IsEnabled = true;
            cbx_f2p2Action.IsEnabled = false;
        }

        private void chk_f2p2Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f2p2Action.IsEnabled = false;
            cbx_f2p2Action.IsEnabled = true;
        }

        private void chk_f3p2Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f3p2Action.IsEnabled = true;
            cbx_f3p2Action.IsEnabled = false;
        }

        private void chk_f3p2Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f3p2Action.IsEnabled = false;
            cbx_f3p2Action.IsEnabled = true;
        }

        private void chk_f4p2Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f4p2Action.IsEnabled = false;
            cbx_f4p2Action.IsEnabled = true;
        }

        private void chk_f4p2Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f4p2Action.IsEnabled = true;
            cbx_f4p2Action.IsEnabled = false;
        }

        private void chk_f1p3Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f1p3Action.IsEnabled = false;
            cbx_f1p3Action.IsEnabled = true;
        }

        private void chk_f1p3Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f1p3Action.IsEnabled = true;
            cbx_f1p3Action.IsEnabled = false;
        }

        private void chk_f2p3Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f2p3Action.IsEnabled = false;
            cbx_f2p3Action.IsEnabled = true;
        }

        private void chk_f2p3Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f2p3Action.IsEnabled = true;
            cbx_f2p3Action.IsEnabled = false;
        }

        private void chk_f3p3Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f3p3Action.IsEnabled = false;
            cbx_f3p3Action.IsEnabled = true;
        }

        private void chk_f3p3Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f3p3Action.IsEnabled = true;
            cbx_f3p3Action.IsEnabled = false;
        }

        private void chk_f4p3Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f4p3Action.IsEnabled = false;
            cbx_f4p3Action.IsEnabled = true;
        }

        private void chk_f4p3Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f4p3Action.IsEnabled = true;
            cbx_f4p3Action.IsEnabled = false;
        }

        private void chk_f1p4Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f1p4Action.IsEnabled = true;
            cbx_f1p4Action.IsEnabled = false;
        }

        private void chk_f1p4Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f1p4Action.IsEnabled = false;
            cbx_f1p4Action.IsEnabled = true;
        }

        private void chk_f2p4Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f2p4Action.IsEnabled = true;
            cbx_f2p4Action.IsEnabled = false;
        }

        private void chk_f2p4Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f2p4Action.IsEnabled = false;
            cbx_f2p4Action.IsEnabled = true;
        }

        private void chk_f3p4Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f3p4Action.IsEnabled = true;
            cbx_f3p4Action.IsEnabled = false;
        }

        private void chk_f3p4Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f3p4Action.IsEnabled = false;
            cbx_f3p4Action.IsEnabled = true;
        }

        private void chk_f4p4Manual_Checked(object sender, RoutedEventArgs e)
        {
            tbx_f4p4Action.IsEnabled = true;
            cbx_f4p4Action.IsEnabled = false;
        }

        private void chk_f4p4Manual_Unchecked(object sender, RoutedEventArgs e)
        {
            tbx_f4p4Action.IsEnabled = false;
            cbx_f4p4Action.IsEnabled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }


        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

            string comunicat = "Special characters:\n" +
                "%> - play symbol\n" +
                "%< - reversed play symbol\n" +
                "%| - pause symbol\n" +
                "%} - desktop symbol\n" +
                "%, - speaker symbol\n" +
                "%: - padlock symbol\n" +
                "%; - standby symbol";
            NotyficationWindow window = new NotyficationWindow(true, comunicat, "Close");
            window.ShowDialog();
        }

        #endregion

        //------------------------------------------------------------------------------------
        #region GET SET

        public bool Apply() { return apply; }

        public Container_ControllerConfig GetConfig() { return container; }








        #endregion


    }
}
