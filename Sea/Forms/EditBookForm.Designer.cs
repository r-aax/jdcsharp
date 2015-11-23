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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DeletePublisherB = new System.Windows.Forms.Button();
            this.DeleteAuthorB = new System.Windows.Forms.Button();
            this.AddPublisherB = new System.Windows.Forms.Button();
            this.AddAuthorB = new System.Windows.Forms.Button();
            this.PublishersLB = new System.Windows.Forms.ListBox();
            this.AuthorsLB = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ChangeCategoriesB = new System.Windows.Forms.Button();
            this.CategoriesLB = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.NumberTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.EditionTB = new System.Windows.Forms.TextBox();
            this.YearTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TypeCB = new System.Windows.Forms.ComboBox();
            this.ArticleSourceTB = new System.Windows.Forms.TextBox();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.IdTB = new System.Windows.Forms.TextBox();
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.BookFileTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 151);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(863, 323);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DeletePublisherB);
            this.tabPage1.Controls.Add(this.DeleteAuthorB);
            this.tabPage1.Controls.Add(this.AddPublisherB);
            this.tabPage1.Controls.Add(this.AddAuthorB);
            this.tabPage1.Controls.Add(this.PublishersLB);
            this.tabPage1.Controls.Add(this.AuthorsLB);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(855, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DeletePublisherB
            // 
            this.DeletePublisherB.Location = new System.Drawing.Point(648, 263);
            this.DeletePublisherB.Name = "DeletePublisherB";
            this.DeletePublisherB.Size = new System.Drawing.Size(100, 23);
            this.DeletePublisherB.TabIndex = 21;
            this.DeletePublisherB.Text = "Delete publisher";
            this.DeletePublisherB.UseVisualStyleBackColor = true;
            this.DeletePublisherB.Click += new System.EventHandler(this.DeletePublisherB_Click);
            // 
            // DeleteAuthorB
            // 
            this.DeleteAuthorB.Location = new System.Drawing.Point(220, 263);
            this.DeleteAuthorB.Name = "DeleteAuthorB";
            this.DeleteAuthorB.Size = new System.Drawing.Size(100, 23);
            this.DeleteAuthorB.TabIndex = 19;
            this.DeleteAuthorB.Text = "Delete author";
            this.DeleteAuthorB.UseVisualStyleBackColor = true;
            this.DeleteAuthorB.Click += new System.EventHandler(this.DeleteAuthorB_Click);
            // 
            // AddPublisherB
            // 
            this.AddPublisherB.Location = new System.Drawing.Point(542, 263);
            this.AddPublisherB.Name = "AddPublisherB";
            this.AddPublisherB.Size = new System.Drawing.Size(100, 23);
            this.AddPublisherB.TabIndex = 20;
            this.AddPublisherB.Text = "Add publisher";
            this.AddPublisherB.UseVisualStyleBackColor = true;
            this.AddPublisherB.Click += new System.EventHandler(this.AddPublisherB_Click);
            // 
            // AddAuthorB
            // 
            this.AddAuthorB.Location = new System.Drawing.Point(114, 263);
            this.AddAuthorB.Name = "AddAuthorB";
            this.AddAuthorB.Size = new System.Drawing.Size(100, 23);
            this.AddAuthorB.TabIndex = 18;
            this.AddAuthorB.Text = "Add author";
            this.AddAuthorB.UseVisualStyleBackColor = true;
            this.AddAuthorB.Click += new System.EventHandler(this.AddAuthorB_Click);
            // 
            // PublishersLB
            // 
            this.PublishersLB.FormattingEnabled = true;
            this.PublishersLB.Location = new System.Drawing.Point(437, 6);
            this.PublishersLB.Name = "PublishersLB";
            this.PublishersLB.Size = new System.Drawing.Size(412, 251);
            this.PublishersLB.TabIndex = 15;
            this.PublishersLB.SelectedIndexChanged += new System.EventHandler(this.PublishersLB_SelectedIndexChanged);
            // 
            // AuthorsLB
            // 
            this.AuthorsLB.FormattingEnabled = true;
            this.AuthorsLB.Location = new System.Drawing.Point(8, 6);
            this.AuthorsLB.Name = "AuthorsLB";
            this.AuthorsLB.Size = new System.Drawing.Size(423, 251);
            this.AuthorsLB.TabIndex = 14;
            this.AuthorsLB.SelectedIndexChanged += new System.EventHandler(this.AuthorsLB_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ChangeCategoriesB);
            this.tabPage2.Controls.Add(this.CategoriesLB);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(855, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Categories list";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ChangeCategoriesB
            // 
            this.ChangeCategoriesB.Location = new System.Drawing.Point(356, 263);
            this.ChangeCategoriesB.Name = "ChangeCategoriesB";
            this.ChangeCategoriesB.Size = new System.Drawing.Size(156, 23);
            this.ChangeCategoriesB.TabIndex = 1;
            this.ChangeCategoriesB.Text = "Change categories";
            this.ChangeCategoriesB.UseVisualStyleBackColor = true;
            this.ChangeCategoriesB.Click += new System.EventHandler(this.ChangeCategoriesB_Click);
            // 
            // CategoriesLB
            // 
            this.CategoriesLB.FormattingEnabled = true;
            this.CategoriesLB.Location = new System.Drawing.Point(6, 6);
            this.CategoriesLB.Name = "CategoriesLB";
            this.CategoriesLB.Size = new System.Drawing.Size(865, 251);
            this.CategoriesLB.TabIndex = 0;
            this.CategoriesLB.SelectedIndexChanged += new System.EventHandler(this.CategoriesLB_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Number";
            // 
            // NumberTB
            // 
            this.NumberTB.Location = new System.Drawing.Point(389, 115);
            this.NumberTB.Name = "NumberTB";
            this.NumberTB.Size = new System.Drawing.Size(100, 20);
            this.NumberTB.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(685, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Edition";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Year";
            // 
            // EditionTB
            // 
            this.EditionTB.Location = new System.Drawing.Point(730, 115);
            this.EditionTB.Name = "EditionTB";
            this.EditionTB.Size = new System.Drawing.Size(125, 20);
            this.EditionTB.TabIndex = 9;
            // 
            // YearTB
            // 
            this.YearTB.Location = new System.Drawing.Point(58, 115);
            this.YearTB.Name = "YearTB";
            this.YearTB.Size = new System.Drawing.Size(125, 20);
            this.YearTB.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(693, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Id";
            // 
            // TypeCB
            // 
            this.TypeCB.FormattingEnabled = true;
            this.TypeCB.Items.AddRange(new object[] {
            "Book",
            "Magazine",
            "Article",
            "Other"});
            this.TypeCB.Location = new System.Drawing.Point(730, 9);
            this.TypeCB.Name = "TypeCB";
            this.TypeCB.Size = new System.Drawing.Size(125, 21);
            this.TypeCB.TabIndex = 3;
            this.TypeCB.SelectedIndexChanged += new System.EventHandler(this.TypeCB_SelectedIndexChanged);
            // 
            // ArticleSourceTB
            // 
            this.ArticleSourceTB.Location = new System.Drawing.Point(58, 64);
            this.ArticleSourceTB.Name = "ArticleSourceTB";
            this.ArticleSourceTB.Size = new System.Drawing.Size(797, 20);
            this.ArticleSourceTB.TabIndex = 2;
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(58, 38);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(797, 20);
            this.NameTB.TabIndex = 1;
            // 
            // IdTB
            // 
            this.IdTB.Enabled = false;
            this.IdTB.Location = new System.Drawing.Point(58, 12);
            this.IdTB.Name = "IdTB";
            this.IdTB.Size = new System.Drawing.Size(125, 20);
            this.IdTB.TabIndex = 0;
            // 
            // AcceptB
            // 
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(372, 480);
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
            this.CancelB.Location = new System.Drawing.Point(453, 480);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(75, 23);
            this.CancelB.TabIndex = 2;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // BookFileTB
            // 
            this.BookFileTB.Location = new System.Drawing.Point(58, 89);
            this.BookFileTB.Name = "BookFileTB";
            this.BookFileTB.Size = new System.Drawing.Size(797, 20);
            this.BookFileTB.TabIndex = 24;
            this.BookFileTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BookFileTB_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "File";
            // 
            // EditBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 513);
            this.ControlBox = false;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BookFileTB);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.NumberTB);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.IdTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TypeCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ArticleSourceTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.YearTB);
            this.Controls.Add(this.NameTB);
            this.Controls.Add(this.EditionTB);
            this.Controls.Add(this.label6);
            this.Name = "EditBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit book";
            this.Shown += new System.EventHandler(this.EditBookForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ChangeCategoriesB;
        private System.Windows.Forms.ListBox CategoriesLB;
        private System.Windows.Forms.ListBox PublishersLB;
        private System.Windows.Forms.ListBox AuthorsLB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EditionTB;
        private System.Windows.Forms.TextBox YearTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TypeCB;
        private System.Windows.Forms.TextBox ArticleSourceTB;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.TextBox IdTB;
        private System.Windows.Forms.Button DeletePublisherB;
        private System.Windows.Forms.Button AddPublisherB;
        private System.Windows.Forms.Button DeleteAuthorB;
        private System.Windows.Forms.Button AddAuthorB;
        private System.Windows.Forms.TextBox NumberTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox BookFileTB;
        private System.Windows.Forms.Label label8;

    }
}