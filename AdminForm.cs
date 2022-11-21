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
        Dictionary<int, String> staffInfo = Malin_Staff_Names.MasterFile;
        Boolean changeMade = false;
        int staffID; 
        string staffName;
        public AdminForm()
        {
            InitializeComponent();
        }

        public AdminForm(int Id, string Name)
        {
            InitializeComponent();
            textBox_adminID.Text =Id.ToString();
            textBox_adminName.Text =Name;

        }

        #region KeyBoard ShortCut
        private void AdminForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.C)
            {                
                CreateInfo();

            }
            if (e.Alt && e.KeyCode == Keys.U)
            {
               
                UpdateInfo();

            }
            if (e.Alt && e.KeyCode == Keys.D)
            {
                DeleteInfo();

            }
            if (e.Alt && e.KeyCode == Keys.Z)
            {
                UndoChange();
            }
            if (e.Alt && e.KeyCode == Keys.L)
            {
                if (changeMade==true)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to SAVE the changes you have made?", "Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        SaveDictonary();
                        Malin_Staff_Names malin_Staff_Names = new Malin_Staff_Names();
                        malin_Staff_Names.FillDictonary();
                    }
                }
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
                int newId = random.Next(770000000, 779999999);

                while (staffInfo.ContainsKey(newId))
                {
                    newId = random.Next(77000000, 77999999);
                }
                
                string newName = textBox_adminName.Text;
                DialogResult result = MessageBox.Show("Are you sure you want to create a NEW staff information with the ID = " + newId + " and Name = " + textBox_adminName.Text, "Creating New", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    PlaceHolder(newId, textBox_adminName.Text);
                    staffInfo.Add(newId, newName);
                   
                }
                textBox_adminID.Clear();
                textBox_adminName.Clear();
                changeMade = true;

                /* foreach (var info in staffInfo.Where(info => info.Key.ToString().Contains(newId.ToString())))
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



            }
            else
            {
                MessageBox.Show("Some thing when wrong!!!", "ERROR: Creating Staff Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UpdateInfo()
        {
            if (!string.IsNullOrEmpty(textBox_adminName.Text))
            {
                DialogResult result = MessageBox.Show("Are you sure you want to CHANGE the Name of the staff to " + textBox_adminName.Text, "Updating Staff Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    PlaceHolder(int.Parse(textBox_adminID.Text), textBox_adminName.Text);
                    staffInfo[int.Parse(textBox_adminID.Text)] = textBox_adminName.Text;
                }

                textBox_adminName.Clear();
                textBox_adminID.Clear();
                changeMade = true;
            }
            else
            {
                MessageBox.Show("Some thing when wrong!!!", "ERROR: Updating Staff Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DeleteInfo()
        {
            if (!string.IsNullOrEmpty(textBox_adminName.Text) && staffInfo.ContainsKey(int.Parse(textBox_adminID.Text)))
            {
                DialogResult result = MessageBox.Show("Are you sure you want to DELETE Staff information with the ID = " + textBox_adminID.Text + " and Name = " + textBox_adminName.Text, "Deleting Staff Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    PlaceHolder(int.Parse(textBox_adminID.Text), textBox_adminName.Text);
                    staffInfo.Remove(int.Parse(textBox_adminID.Text));
                    textBox_adminName.Clear();
                      textBox_adminID.Clear();
                }
                
                changeMade = true;
            }
            else
            {
                MessageBox.Show("Some thing when wrong!!!", "ERROR: Deleting Staff Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UndoChange()
        {
            changeMade = false;
            FillTextBox();

        }
        private void SaveDictonary()
        {
            string fileName = "MalinStaffNamesV2.csv";
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(file))
                {
                    foreach (var info in staffInfo)
                    {

                        writer.WriteLine(info.Key.ToString()+","+  info.Value);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion

        private void textBox_adminName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Malin_Staff_Names.TextBoxNameFilter(textBox_adminName, e);
        }
        private void PlaceHolder(int Id, string Name)
        {
            staffID = Id;
            staffName = Name;   
        }
        private void FillTextBox()
        {
            textBox_adminID.Text = staffID.ToString();
            textBox_adminName.Text = staffName;
        }
    }
}
