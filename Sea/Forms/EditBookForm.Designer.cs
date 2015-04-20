namespace Sea.Forms
{
    partial class EditBookForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DeletePublisherB = new System.Windows.Forms.Button();
            this.AddPublisherB = new System.Windows.Forms.Button();
            this.DeleteAuthorB = new System.Windows.Forms.Button();
            this.AddAuthorB = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.AuthorsLB = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DeleteCategoryB = new System.Windows.Forms.Button();
            this.AddCategoryB = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(887, 495);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(887, 495);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DeletePublisherB);
            this.tabPage1.Controls.Add(this.AddPublisherB);
            this.tabPage1.Controls.Add(this.DeleteAuthorB);
            this.tabPage1.Controls.Add(this.AddAuthorB);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.listBox3);
            this.tabPage1.Controls.Add(this.AuthorsLB);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textBox6);
            this.tabPage1.Controls.Add(this.textBox5);
            this.tabPage1.Controls.Add(this.textBox4);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.NameTB);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(879, 469);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DeletePublisherB
            // 
            this.DeletePublisherB.Location = new System.Drawing.Point(635, 437);
            this.DeletePublisherB.Name = "DeletePublisherB";
            this.DeletePublisherB.Size = new System.Drawing.Size(100, 23);
            this.DeletePublisherB.TabIndex = 21;
            this.DeletePublisherB.Text = "Delete publisher";
            this.DeletePublisherB.UseVisualStyleBackColor = true;
            this.DeletePublisherB.Click += new System.EventHandler(this.DeletePublisherB_Click);
            // 
            // AddPublisherB
            // 
            this.AddPublisherB.Location = new System.Drawing.Point(529, 437);
            this.AddPublisherB.Name = "AddPublisherB";
            this.AddPublisherB.Size = new System.Drawing.Size(100, 23);
            this.AddPublisherB.TabIndex = 20;
            this.AddPublisherB.Text = "Add publisher";
            this.AddPublisherB.UseVisualStyleBackColor = true;
            this.AddPublisherB.Click += new System.EventHandler(this.AddPublisherB_Click);
            // 
            // DeleteAuthorB
            // 
            this.DeleteAuthorB.Location = new System.Drawing.Point(275, 437);
            this.DeleteAuthorB.Name = "DeleteAuthorB";
            this.DeleteAuthorB.Size = new System.Drawing.Size(100, 23);
            this.DeleteAuthorB.TabIndex = 19;
            this.DeleteAuthorB.Text = "Delete author";
            this.DeleteAuthorB.UseVisualStyleBackColor = true;
            this.DeleteAuthorB.Click += new System.EventHandler(this.DeleteAuthorB_Click);
            // 
            // AddAuthorB
            // 
            this.AddAuthorB.Location = new System.Drawing.Point(169, 437);
            this.AddAuthorB.Name = "AddAuthorB";
            this.AddAuthorB.Size = new System.Drawing.Size(100, 23);
            this.AddAuthorB.TabIndex = 18;
            this.AddAuthorB.Text = "Add author";
            this.AddAuthorB.UseVisualStyleBackColor = true;
            this.AddAuthorB.Click += new System.EventHandler(this.AddAuthorB_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(455, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Publishers list";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(116, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Authors list";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(437, 180);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(372, 251);
            this.listBox3.TabIndex = 15;
            // 
            // AuthorsLB
            // 
            this.AuthorsLB.FormattingEnabled = true;
            this.AuthorsLB.Location = new System.Drawing.Point(92, 180);
            this.AuthorsLB.Name = "AuthorsLB";
            this.AuthorsLB.Size = new System.Drawing.Size(339, 251);
            this.AuthorsLB.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(659, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(348, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Edition";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Year";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(709, 104);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(393, 104);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(92, 104);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(647, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Id";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(684, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(125, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(92, 78);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(717, 20);
            this.textBox3.TabIndex = 2;
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(92, 52);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(717, 20);
            this.NameTB.TabIndex = 1;
            this.NameTB.Text = "Test book";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 20);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DeleteCategoryB);
            this.tabPage2.Controls.Add(this.AddCategoryB);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(879, 469);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Categories list";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DeleteCategoryB
            // 
            this.DeleteCategoryB.Location = new System.Drawing.Point(437, 432);
            this.DeleteCategoryB.Name = "DeleteCategoryB";
            this.DeleteCategoryB.Size = new System.Drawing.Size(100, 23);
            this.DeleteCategoryB.TabIndex = 2;
            this.DeleteCategoryB.Text = "Delete category";
            this.DeleteCategoryB.UseVisualStyleBackColor = true;
            this.DeleteCategoryB.Click += new System.EventHandler(this.DeleteCategoryB_Click);
            // 
            // AddCategoryB
            // 
            this.AddCategoryB.Location = new System.Drawing.Point(331, 432);
            this.AddCategoryB.Name = "AddCategoryB";
            this.AddCategoryB.Size = new System.Drawing.Size(100, 23);
            this.AddCategoryB.TabIndex = 1;
            this.AddCategoryB.Text = "Add category";
            this.AddCategoryB.UseVisualStyleBackColor = true;
            this.AddCategoryB.Click += new System.EventHandler(this.AddCategoryB_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(865, 420);
            this.listBox1.TabIndex = 0;
            // 
            // AcceptB
            // 
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(360, 501);
            this.AcceptB.Name = "AcceptB";
            this.AcceptB.Size = new System.Drawing.Size(75, 23);
            this.AcceptB.TabIndex = 1;
            this.AcceptB.Text = "Accept";
            this.AcceptB.UseVisualStyleBackColor = true;
            this.AcceptB.Click += new System.EventHandler(this.AcceptB_Click);
            // 
            // CancelB
            // 
            this.CancelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelB.ForeColor = System.Drawing.Color.IndianRed;
            this.CancelB.Location = new System.Drawing.Point(441, 501);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(75, 23);
            this.CancelB.TabIndex = 2;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // EditBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 533);
            this.ControlBox = false;
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.panel1);
            this.Name = "EditBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit book";
            this.Shown += new System.EventHandler(this.EditBookForm_Shown);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button DeleteCategoryB;
        private System.Windows.Forms.Button AddCategoryB;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox AuthorsLB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button DeletePublisherB;
        private System.Windows.Forms.Button AddPublisherB;
        private System.Windows.Forms.Button DeleteAuthorB;
        private System.Windows.Forms.Button AddAuthorB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;

    }
}