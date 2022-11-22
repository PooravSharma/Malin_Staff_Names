using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malin_Staff_Names
{
    public partial class Malin_Staff_Names : Form
    {
        public Malin_Staff_Names()
        {
            InitializeComponent();
            FillDictonary();
        }
        // 4.1.	Create a Dictionary data structure with a TKey of type integer and a TValue of type string, name the new structure “MasterFile”.
        // 4.10.	Add suitable error trapping and user feedback to ensure a fully functional User Experience. Make all methods private and ensure the Dictionary is static and public.
        public static Dictionary<int, string> MasterFile = new Dictionary<int, string>();

        #region Method 

        // 4.2.	Create a method that will read the data from the .csv file into the Dictionary data structure when the form loads.
        public void FillDictonary()
        {
            //Stopwatch stopWatch = new Stopwatch();
            MasterFile.Clear();
            if (File.Exists("MalinStaffNamesV2.csv"))
            {
                using (StreamReader reader = new StreamReader("MalinStaffNamesV2.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        //  stopWatch.Start();
                        string line = reader.ReadLine();
                        string[] lineData = line.Split(',');
                        MasterFile.Add(int.Parse(lineData[0]), lineData[1]);
                    }

                    /*   string[] readLines = File.ReadAllLines(@"MalinStaffNamesV2.csv");
                       foreach (var line in readLines)
                       {
                           string[] lineData = line.Split(',');
                           MasterFile.Add(int.Parse(lineData[0]), lineData[1]);
                       }*/
                    /*stopWatch.Stop();
                    MessageBox.Show("Number of ticks taken for the .CSV file to Open = " +stopWatch.ElapsedTicks.ToString()+" ticks", "Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
*/
                }

                FillBox();
            }
            else
                MessageBox.Show("File did not load");
        }

        // 4.3.	Create a method to display the Dictionary data into a non-selectable display only listbox (ie read only).
        private void FillBox()
        {
            listBox_viewOnly.Items.Clear();
            foreach (var info in MasterFile)
            {
                listBox_viewOnly.Items.Add(info.Key + "     " + info.Value);
            }
        }

        // Filters the ID textbox so that the user can only input numbers
        private void TextBoxIDFilter(TextBox t, KeyPressEventArgs e)
        {

            char ch = e.KeyChar;
            if (ch == 46 && t.Text.IndexOf('.') !=-1)
            {
                e.Handled =true;
                return;
            }
            if (!Char.IsDigit(ch) && ch !=8 && ch != 46)
            {
                e.Handled = true;
            }
            t.MaxLength = 9;

        }

        // Filters the Name textbox so that the user can only input numbers
        private void TextBoxNameFilter(TextBox t, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && t.Text.IndexOf(" ") !=-1)
            {
                e.Handled = true;
            }
        }

        //4.5.	Create a method to filter the Staff ID data from the Dictionary into the second filtered and selectable list box. This method must use a textbox input and update as each number is entered. The listbox must reflect the filtered data in real time.
        private void SearchID(TextBox t)
        {
            listBox_filteredView.Items.Clear();

            foreach (var info in MasterFile.Where(info => info.Key.ToString().Contains(t.Text)))
            {
                listBox_filteredView.Items.Add(info.Key + "\t" + info.Value);
            }
        }

        // 4.4.	Create a method to filter the Staff Name data from the Dictionary into a second filtered and selectable listbox. This method must use a textbox input and update as each character is entered. The listbox must reflect the filtered data in real time.
        private void SearchName(TextBox t)
        {
            if (!string.IsNullOrEmpty(t.Text))
            {
                listBox_filteredView.Items.Clear();

                foreach (var info in MasterFile.Where(info => info.Value.ToString().ToLower().Contains(t.Text.ToLower())))
                {
                    listBox_filteredView.Items.Add(info.Key + "\t" + info.Value);

                }
            }
            else
            {
                listBox_filteredView.Items.Clear();
            }
        }

        #endregion

        // 4.10.	Add suitable error trapping and user feedback to ensure a fully functional User Experience. Make all methods private and ensure the Dictionary is static and public.
        #region TextBox

        private void textBox_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxIDFilter(textBox_ID, e);
        }

        private void textBox_Name_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBoxNameFilter(textBox_Name, e);
        }
        private void textBox_ID_TextChanged(object sender, EventArgs e)
        {
            SearchID(textBox_ID);
        }
        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            SearchName(textBox_Name);
        }
        #endregion

        #region Keyboard Shortcut
        private void Malin_Staff_Names_KeyDown(object sender, KeyEventArgs e)
        {
            // 4.7.	Create a method for the Staff ID textbox which will clear the contents and place the focus into the textbox. Utilise a keyboard shortcut.
            if (e.Alt && e.KeyCode == Keys.I)
            {
                textBox_ID.Clear();
                textBox_Name.Clear();
                listBox_filteredView.Items.Clear();
                textBox_ID.Select();

            }
            // 4.6.	Create a method for the Staff Name textbox which will clear the contents and place the focus into the Staff Name textbox. Utilise a keyboard shortcut.
            if (e.Alt && e.KeyCode == Keys.N)
            {
                textBox_Name.Clear();
                textBox_ID.Clear();
                listBox_filteredView.Items.Clear();
                textBox_Name.Select();

            }
            if (e.Alt && e.KeyCode == Keys.L)
            {
                Application.Exit();

            }
            // 4.8.	Create a method for the filtered and selectable listbox which will populate the two textboxes when a staff record is selected.
            if (listBox_filteredView.SelectedIndex == 0 && e.KeyCode == Keys.Enter)
            {
                string line = listBox_filteredView.SelectedItem.ToString();
                string[] line_Words = line.Split('\t');

                textBox_ID.Text = line_Words[0];

                textBox_Name.Text = line_Words[1];
                listBox_filteredView.Items.Clear();


            }
            // 4.9.	Create a method that will open the Admin Form when the Alt + A keys are pressed. Ensure the General Form sends the currently selected Staff ID and Staff Name to the Admin Form for Update and Delete purposes and is opened as modal. 
            if (!string.IsNullOrEmpty(textBox_ID.Text) && !string.IsNullOrEmpty(textBox_Name.Text) && e.Alt && e.KeyCode == Keys.A)
            {
                AdminForm admin = new AdminForm(int.Parse(textBox_ID.Text), textBox_Name.Text);
                admin.Show();
                textBox_ID.Clear();
                textBox_Name.Clear();

            }
            else if (e.Alt && e.KeyCode == Keys.A)
            {
                AdminForm admin = new AdminForm();
                admin.Show();
                textBox_ID.Clear();
                textBox_Name.Clear();
            }


        }
        #endregion


    }
}
