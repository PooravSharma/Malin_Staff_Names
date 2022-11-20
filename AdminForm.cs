using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Malin_Staff_Names
{
    public partial class AdminForm : Form
    {

        public AdminForm()
        {
            InitializeComponent();

        }
        Dictionary<int, String> staffInfo;
        public AdminForm(int Id, string Name, Dictionary<int, string> MasterClass)
        {
            InitializeComponent();
            textBox_adminID.Text =Id.ToString();
            textBox_adminName.Text =Name;
            staffInfo = MasterClass;

        }

        #region KeyBoard ShortCut
        private void AdminForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.C)
            {
                textBox_adminID.Clear();
                textBox_adminName.Clear();
                textBox_adminName.Select();
                CreateInfo();

            }
            if (e.Alt && e.KeyCode == Keys.U)
            {
                textBox_adminName.Clear();
                textBox_adminID.Clear();
                textBox_adminName.Select();
                UpdateInfo();

            }
            if (e.Alt && e.KeyCode == Keys.D)
            {


            }
            if (e.Alt && e.KeyCode == Keys.Z)
            {


            }
            if (e.Alt && e.KeyCode == Keys.L)
            {
                this.Close();
            }
        }
        #endregion

        #region Method 

        public void CreateInfo()
        {
            if (!string.IsNullOrEmpty(textBox_adminName.Text))
            {
                System.Random random = new System.Random();
                int newId = 77000000+ random.Next(000000, 999999);
                foreach (var info in staffInfo.Where(info => info.Key.ToString().Contains(newId.ToString())))
                {
                    newId= 0;
                    newId = 77000000+ random.Next(000000, 999999);
                }


                /*  while (staffInfo.ContainsKey(newId))
                  {
                      newId= 0;
                      newId = 77000000+ random.Next(000000, 999999);
                  }*/


                /* Boolean loopInfo = true;
                 int newId = 77000000+ random.Next(000000, 999999);
                 while (loopInfo)
                 {
                     if (staffInfo.TryGetValue(newId, out string Name))
                     {
                         newId= 0;
                         newId = 77000000+ random.Next(000000, 999999);

                     }
                     else 
                     { 
                         loopInfo = false;                     
                     }
                 }*/

                textBox_adminID.Text = newId.ToString();
                string newName = textBox_adminName.Text;
                DialogResult result = MessageBox.Show("Creating New", "Are you sure you want to create a NEW staff information with the ID = " + textBox_adminID.Text + " and Name = " + textBox_adminName.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    staffInfo.Add(newId, newName);
                    textBox_adminID.Clear();
                    textBox_adminName.Clear();
                }


            }
        }
        public void UpdateInfo()
        {
            if (!string.IsNullOrEmpty(textBox_adminName.Text))
            {
                DialogResult result = MessageBox.Show("Updating Staff Information", "Are you sure you want to CHANGE the Name of the staff to " + textBox_adminName.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    staffInfo[int.Parse(textBox_adminID.Text)] = textBox_adminName.Text;
                }
                textBox_adminName.Clear();
                textBox_adminID.Clear();

            }
        }
        public void DeleteInfo()
        {
            if (!string.IsNullOrEmpty(textBox_adminName.Text))
            {
                DialogResult result = MessageBox.Show("Deleting Staff Information", "Are you sure you want to DELETE Staff information with the ID = " + textBox_adminID.Text + " and Name = " + textBox_adminName.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    staffInfo.Remove(int.Parse(textBox_adminID.Text));
                }
                textBox_adminName.Clear();
                textBox_adminID.Clear();

            }
        }
        public void UndoChange()
        {

        }
        private void SaveDictonary()
        {
            string fileName = "MalinStaffNamesV2.csv";
            SaveFileDialog SaveText = new SaveFileDialog();
            DialogResult sr = SaveText.ShowDialog();
            SaveText.Filter = "Excel Files | *.csv";
            SaveText.DefaultExt = "csv";
            if (sr == DialogResult.OK)
            {
                fileName = SaveText.FileName;

            }
            if (sr == DialogResult.Cancel)
            {
                SaveText.FileName = fileName;
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var info in staffInfo)
                    {
                        
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }




        }
        #endregion
    }
}
