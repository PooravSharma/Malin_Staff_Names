namespace Malin_Staff_Names
{
    partial class Malin_Staff_Names
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox_viewOnly = new System.Windows.Forms.ListBox();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox_filteredView = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_viewOnly
            // 
            this.listBox_viewOnly.FormattingEnabled = true;
            this.listBox_viewOnly.Location = new System.Drawing.Point(28, 16);
            this.listBox_viewOnly.Name = "listBox_viewOnly";
            this.listBox_viewOnly.Size = new System.Drawing.Size(172, 225);
            this.listBox_viewOnly.TabIndex = 0;
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(274, 22);
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.Size = new System.Drawing.Size(100, 20);
            this.textBox_ID.TabIndex = 1;
            this.textBox_ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ID_KeyPress);
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(274, 66);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(100, 20);
            this.textBox_Name.TabIndex = 2;
            this.textBox_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Name_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // listBox_filteredView
            // 
            this.listBox_filteredView.FormattingEnabled = true;
            this.listBox_filteredView.Location = new System.Drawing.Point(396, 16);
            this.listBox_filteredView.Name = "listBox_filteredView";
            this.listBox_filteredView.Size = new System.Drawing.Size(172, 225);
            this.listBox_filteredView.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(215, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 112);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Alt + A: Open Admin form ";
            // 
            // Malin_Staff_Names
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 258);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox_filteredView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_ID);
            this.Controls.Add(this.listBox_viewOnly);
            this.Name = "Malin_Staff_Names";
            this.Text = "Malin_Staff_Names";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Malin_Staff_Names_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_viewOnly;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox_filteredView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}

