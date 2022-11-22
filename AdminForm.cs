using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        // 5.1.	Create the Admin Form with the following settings: Control Box = false and KeyPreview = True, then add two textboxes. The textbox for the Staff ID should be read-only for Update and Delete purposes.
        //5.8.	Add suitable error trapping and user feedback to ensure a fully functional User Experience. Make all methods private and ensure the Dictionary is updated.  
        Dictionary<int, String> staffInfo = Malin_Staff_Names.MasterFile;
        Boolean changeMade = false;
        int staffID;
        string staffName;
        public AdminForm()
        {
            InitializeComponent();
        }

        // 5.2.	Create a method that will receive the Staff ID from the general form and then populate textboxes with the related data. 
        public AdminForm(int Id, string Name)
        {
            InitializeComponent();
            textBox_adminID.Text =Id.ToString();
            textBox_adminName.Text =Name;

        }

        #region KeyBoard ShortCut
        private void AdminForm_KeyDown(object sender, KeyEventArgs e)
        {
            // 5.3.	Create a method that will create a new Staff ID and input the staff name from the related text box. The Staff ID must be unique starting with 77xxxxxxx while the staff name may be duplicated. The new staff member must be added to the Dictionary data structure.
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

            //5.6.	Create a method that will save changes to the csv file, this method should be called before the Admin Form closes.
            // 5.7.	Create a method that will close the Admin Form when the Alt + L keys are pressed.
            if (e.Alt && e.KeyCode == Keys.L)
            {
                if (changeMade==true)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to SAVE the changes you have made?", "Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        SaveDictonary();
                        Malin_Staff_Names malin_Staff_Names = new Malin_Staff_Names();

                    }
                }
                this.Close();
            }
        }
        #endregion

        #region Method 
        // 5.3.	Create a method that will create a new Staff ID and input the staff name from the related text box. The Staff ID must be unique starting with 77xxxxxxx while the staff name may be duplicated. The new staff member must be added to the Dictionary data structure.
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
                MessageBox.Show("Name textbox is empty!!!", "ERROR: Creating Staff Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5.4.	Create a method that will Update the name of the current Staff ID.
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
                MessageBox.Show("Name textbox is empty!!!", "ERROR: Updating Staff Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //5.5.	Create a method that will Remove the current Staff ID and clear the text boxes.
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

        //Undo changes made in the admin form 
        public void UndoChange()
        {
            changeMade = false;
            FillTextBox();

        }

        // 5.6.	Create a method that will save changes to the csv file, this method should be called before the Admin Form closes.
        private void SaveDictonary()
        {   //Stopwatch stopWatch = new Stopwatch();
            string fileName = "MalinStaffNamesV2.csv";
            try
            {

                FileStream file = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(file))
                { //stopWatch.Start();
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
            /*stopWatch.Stop();
            MessageBox.Show("Number of ticks taken for the .CSV file to Save = " +stopWatch.ElapsedTicks.ToString()+" ticks", "Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
*/
        }

        #endregion

        #region TextBox
        private void textBox_adminName_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxAdminNameFilter(textBox_adminName, e);
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
        private void TextBoxAdminNameFilter(TextBox t, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && t.Text.IndexOf(" ") !=-1)
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}
