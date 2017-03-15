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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.EditionTB = new System.Windows.Forms.TextBox();
            this.YearTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TypeCB = new System.Windows.Forms.ComboBox();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.IdTB = new System.Windows.Forms.TextBox();
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.BookFileTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.KeywordsTB = new System.Windows.Forms.TextBox();
            this.AddAuthorB = new System.Windows.Forms.Button();
            this.DeleteAuthorB = new System.Windows.Forms.Button();
            this.ChangeCategoriesB = new System.Windows.Forms.Button();
            this.AuthorsLB = new System.Windows.Forms.ListBox();
            this.CategoriesLB = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
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
            this.label5.Location = new System.Drawing.Point(33, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Year";
            // 
            // EditionTB
            // 
            this.EditionTB.Location = new System.Drawing.Point(730, 116);
            this.EditionTB.Name = "EditionTB";
            this.EditionTB.Size = new System.Drawing.Size(125, 20);
            this.EditionTB.TabIndex = 9;
            // 
            // YearTB
            // 
            this.YearTB.Location = new System.Drawing.Point(68, 116);
            this.YearTB.Name = "YearTB";
            this.YearTB.Size = new System.Drawing.Size(125, 20);
            this.YearTB.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Keywords";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 42);
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
            this.label1.Location = new System.Drawing.Point(46, 16);
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
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(68, 38);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(787, 20);
            this.NameTB.TabIndex = 1;
            // 
            // IdTB
            // 
            this.IdTB.Enabled = false;
            this.IdTB.Location = new System.Drawing.Point(68, 12);
            this.IdTB.Name = "IdTB";
            this.IdTB.Size = new System.Drawing.Size(125, 20);
            this.IdTB.TabIndex = 0;
            // 
            // AcceptB
            // 
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(382, 374);
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
            this.CancelB.Location = new System.Drawing.Point(463, 374);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(75, 23);
            this.CancelB.TabIndex = 2;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // BookFileTB
            // 
            this.BookFileTB.Location = new System.Drawing.Point(68, 90);
            this.BookFileTB.Name = "BookFileTB";
            this.BookFileTB.Size = new System.Drawing.Size(787, 20);
            this.BookFileTB.TabIndex = 24;
            this.BookFileTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BookFileTB_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "File";
            // 
            // KeywordsTB
            // 
            this.KeywordsTB.Location = new System.Drawing.Point(68, 64);
            this.KeywordsTB.Name = "KeywordsTB";
            this.KeywordsTB.Size = new System.Drawing.Size(787, 20);
            this.KeywordsTB.TabIndex = 26;
            // 
            // AddAuthorB
            // 
            this.AddAuthorB.Location = new System.Drawing.Point(154, 157);
            this.AddAuthorB.Name = "AddAuthorB";
            this.AddAuthorB.Size = new System.Drawing.Size(100, 23);
            this.AddAuthorB.TabIndex = 18;
            this.AddAuthorB.Text = "Add author";
            this.AddAuthorB.UseVisualStyleBackColor = true;
            this.AddAuthorB.Click += new System.EventHandler(this.AddAuthorB_Click);
            // 
            // DeleteAuthorB
            // 
            this.DeleteAuthorB.Location = new System.Drawing.Point(260, 157);
            this.DeleteAuthorB.Name = "DeleteAuthorB";
            this.DeleteAuthorB.Size = new System.Drawing.Size(100, 23);
            this.DeleteAuthorB.TabIndex = 19;
            this.DeleteAuthorB.Text = "Delete author";
            this.DeleteAuthorB.UseVisualStyleBackColor = true;
            this.DeleteAuthorB.Click += new System.EventHandler(this.DeleteAuthorB_Click);
            // 
            // ChangeCategoriesB
            // 
            this.ChangeCategoriesB.Location = new System.Drawing.Point(568, 157);
            this.ChangeCategoriesB.Name = "ChangeCategoriesB";
            this.ChangeCategoriesB.Size = new System.Drawing.Size(156, 23);
            this.ChangeCategoriesB.TabIndex = 1;
            this.ChangeCategoriesB.Text = "Change categories";
            this.ChangeCategoriesB.UseVisualStyleBackColor = true;
            this.ChangeCategoriesB.Click += new System.EventHandler(this.ChangeCategoriesB_Click);
            // 
            // AuthorsLB
            // 
            this.AuthorsLB.FormattingEnabled = true;
            this.AuthorsLB.Location = new System.Drawing.Point(68, 186);
            this.AuthorsLB.Name = "AuthorsLB";
            this.AuthorsLB.Size = new System.Drawing.Size(389, 173);
            this.AuthorsLB.TabIndex = 27;
            // 
            // CategoriesLB
            // 
            this.CategoriesLB.FormattingEnabled = true;
            this.CategoriesLB.Location = new System.Drawing.Point(463, 186);
            this.CategoriesLB.Name = "CategoriesLB";
            this.CategoriesLB.Size = new System.Drawing.Size(392, 173);
            this.CategoriesLB.TabIndex = 28;
            // 
            // EditBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 407);
            this.ControlBox = false;
            this.Controls.Add(this.CategoriesLB);
            this.Controls.Add(this.AuthorsLB);
            this.Controls.Add(this.ChangeCategoriesB);
            this.Controls.Add(this.KeywordsTB);
            this.Controls.Add(this.DeleteAuthorB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AddAuthorB);
            this.Controls.Add(this.BookFileTB);
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.IdTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TypeCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EditionTB;
        private System.Windows.Forms.TextBox YearTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TypeCB;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.TextBox IdTB;
        private System.Windows.Forms.TextBox BookFileTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox KeywordsTB;
        private System.Windows.Forms.Button ChangeCategoriesB;
        private System.Windows.Forms.Button DeleteAuthorB;
        private System.Windows.Forms.Button AddAuthorB;
        private System.Windows.Forms.ListBox AuthorsLB;
        private System.Windows.Forms.ListBox CategoriesLB;
    }
}