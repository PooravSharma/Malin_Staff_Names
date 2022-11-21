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
        public static SortedDictionary<int, string> MasterFile = new SortedDictionary<int, string>();
       
        #region Method 
        public void FillDictonary()
        {
            Stopwatch stopWatch = new Stopwatch();
            MasterFile.Clear();
            if (File.Exists("MalinStaffNamesV2.csv"))
            {
                using (StreamReader reader = new StreamReader("MalinStaffNamesV2.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        stopWatch.Start();
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
                    stopWatch.Stop();
                    MessageBox.Show("Number of ticks taken for the .CSV file to Open = " +stopWatch.ElapsedTicks.ToString()+" ticks", "Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                FillBox();
            }
            else
                MessageBox.Show("File did not load");
        }
        private void FillBox()
        {
            listBox_viewOnly.Items.Clear();
            foreach (var info in MasterFile)
            {
                listBox_viewOnly.Items.Add(info.Key + "     " + info.Value);
            }
        }
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
        public static void TextBoxNameFilter(TextBox t, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && t.Text.IndexOf(" ") !=-1)
            {
                e.Handled = true;
            }
        }
        private void SearchID(TextBox t)
        {
            listBox_filteredView.Items.Clear();
         
            foreach (var info in MasterFile.Where(info => info.Key.ToString().Contains(t.Text)))
            {
                listBox_filteredView.Items.Add(info.Key + "\t" + info.Value);
            }
        }
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
            if (e.Alt && e.KeyCode == Keys.I)
            {
                textBox_ID.Clear();
                textBox_Name.Clear();
                listBox_filteredView.Items.Clear();
                textBox_ID.Select();

            }
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
            if (listBox_filteredView.SelectedIndex == 0 && e.KeyCode == Keys.Enter) 
            {
                string line = listBox_filteredView.SelectedItem.ToString();
                string[] line_Words =   line.Split('\t');
              
                textBox_ID.Text = line_Words[0];

                textBox_Name.Text = line_Words[1];
                listBox_filteredView.Items.Clear();

                
            }
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
