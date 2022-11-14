using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        static Dictionary<int, string> MasterFile = new Dictionary<int, string>();

        #region Method 
        private void FillDictonary()
        {
            if (File.Exists("MalinStaffNamesV2.csv"))
            {
                using (StreamReader reader = new StreamReader("MalinStaffNamesV2.csv"))
                {
                    string[] readLines = File.ReadAllLines(@"MalinStaffNamesV2.csv");
                    foreach (var line in readLines)
                    {
                        string[] lineData = line.Split(',');
                        MasterFile.Add(int.Parse(lineData[0]), lineData[1]);
                    }
                   
                }
                
                //FillBox(listBox_viewOnly);
            }
            else
                MessageBox.Show("File did not load");
        }
        private void FillBox(ListBox l)
        {
            l.Items.Clear();
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
        private void TextBoxNameFilter(TextBox t, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && t.Text.IndexOf(" ") !=-1)
            {
                e.Handled = true;
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
        #endregion

        private void Malin_Staff_Names_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.I)
            {
               /* textBox_ID.Clear();
                textBox_Name.Clear();
                textBox_ID.Select();*/
                FillBox(listBox_viewOnly);
            }
        }
    }
}
