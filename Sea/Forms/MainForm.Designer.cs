namespace Sea.Forms
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StoragePathSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.LastActionSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataBooksMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DataCategoriesMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DataAuthorsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsArchiveMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolArchiveMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsDearchiveMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsBackupMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsRestoreMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolConvertMI = new System.Windows.Forms.ToolStripMenuItem();
            this.correctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteExtraCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteExtraBookFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.NameKeywordsTB = new System.Windows.Forms.TextBox();
            this.SearchB = new System.Windows.Forms.Button();
            this.CleanB = new System.Windows.Forms.Button();
            this.AuthorTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CategoriesLB = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.YearFromTB = new System.Windows.Forms.TextBox();
            this.YearToTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BooksDGV = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BooksDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StoragePathSSL,
            this.LastActionSSL});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(866, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StoragePathSSL
            // 
            this.StoragePathSSL.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.StoragePathSSL.Name = "StoragePathSSL";
            this.StoragePathSSL.Size = new System.Drawing.Size(162, 17);
            this.StoragePathSSL.Text = "Storage path: <storage path>";
            // 
            // LastActionSSL
            // 
            this.LastActionSSL.ForeColor = System.Drawing.Color.SteelBlue;
            this.LastActionSSL.Name = "LastActionSSL";
            this.LastActionSSL.Size = new System.Drawing.Size(67, 17);
            this.LastActionSSL.Text = "Last action:";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.данныеToolStripMenuItem,
            this.ToolsArchiveMI,
            this.correctionToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(866, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataBooksMI,
            this.toolStripSeparator1,
            this.DataCategoriesMI,
            this.DataAuthorsMI});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.данныеToolStripMenuItem.Text = "Data";
            // 
            // DataBooksMI
            // 
            this.DataBooksMI.Name = "DataBooksMI";
            this.DataBooksMI.Size = new System.Drawing.Size(130, 22);
            this.DataBooksMI.Text = "Books";
            this.DataBooksMI.Click += new System.EventHandler(this.DataBooksMI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
            // 
            // DataCategoriesMI
            // 
            this.DataCategoriesMI.Name = "DataCategoriesMI";
            this.DataCategoriesMI.Size = new System.Drawing.Size(130, 22);
            this.DataCategoriesMI.Text = "Categories";
            this.DataCategoriesMI.Click += new System.EventHandler(this.DataCategoriesMI_Click);
            // 
            // DataAuthorsMI
            // 
            this.DataAuthorsMI.Name = "DataAuthorsMI";
            this.DataAuthorsMI.Size = new System.Drawing.Size(130, 22);
            this.DataAuthorsMI.Text = "Authors";
            this.DataAuthorsMI.Click += new System.EventHandler(this.DataAuthorsMI_Click);
            // 
            // ToolsArchiveMI
            // 
            this.ToolsArchiveMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolArchiveMI,
            this.ToolsDearchiveMI,
            this.toolStripMenuItem1,
            this.ToolsBackupMI,
            this.ToolsRestoreMI,
            this.toolStripSeparator2,
            this.ToolConvertMI});
            this.ToolsArchiveMI.Name = "ToolsArchiveMI";
            this.ToolsArchiveMI.Size = new System.Drawing.Size(47, 20);
            this.ToolsArchiveMI.Text = "Tools";
            // 
            // ToolArchiveMI
            // 
            this.ToolArchiveMI.Name = "ToolArchiveMI";
            this.ToolArchiveMI.Size = new System.Drawing.Size(126, 22);
            this.ToolArchiveMI.Text = "Archive";
            this.ToolArchiveMI.Click += new System.EventHandler(this.ToolArchiveMI_Click);
            // 
            // ToolsDearchiveMI
            // 
            this.ToolsDearchiveMI.Name = "ToolsDearchiveMI";
            this.ToolsDearchiveMI.Size = new System.Drawing.Size(126, 22);
            this.ToolsDearchiveMI.Text = "Dearchive";
            this.ToolsDearchiveMI.Click += new System.EventHandler(this.ToolsDearchiveMI_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 6);
            // 
            // ToolsBackupMI
            // 
            this.ToolsBackupMI.Name = "ToolsBackupMI";
            this.ToolsBackupMI.Size = new System.Drawing.Size(126, 22);
            this.ToolsBackupMI.Text = "Backup";
            this.ToolsBackupMI.Click += new System.EventHandler(this.ToolsBackupMI_Click);
            // 
            // ToolsRestoreMI
            // 
            this.ToolsRestoreMI.Name = "ToolsRestoreMI";
            this.ToolsRestoreMI.Size = new System.Drawing.Size(126, 22);
            this.ToolsRestoreMI.Text = "Restore";
            this.ToolsRestoreMI.Click += new System.EventHandler(this.ToolsRestoreMI_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // ToolConvertMI
            // 
            this.ToolConvertMI.Name = "ToolConvertMI";
            this.ToolConvertMI.Size = new System.Drawing.Size(126, 22);
            this.ToolConvertMI.Text = "Convert";
            this.ToolConvertMI.Click += new System.EventHandler(this.ToolConvertMI_Click);
            // 
            // correctionToolStripMenuItem
            // 
            this.correctionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteExtraCategoriesToolStripMenuItem,
            this.deleteExtraBookFilesToolStripMenuItem});
            this.correctionToolStripMenuItem.Name = "correctionToolStripMenuItem";
            this.correctionToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.correctionToolStripMenuItem.Text = "Correction";
            // 
            // deleteExtraCategoriesToolStripMenuItem
            // 
            this.deleteExtraCategoriesToolStripMenuItem.Name = "deleteExtraCategoriesToolStripMenuItem";
            this.deleteExtraCategoriesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteExtraCategoriesToolStripMenuItem.Text = "Delete Extra Categories";
            this.deleteExtraCategoriesToolStripMenuItem.Click += new System.EventHandler(this.deleteExtraCategoriesToolStripMenuItem_Click);
            // 
            // deleteExtraBookFilesToolStripMenuItem
            // 
            this.deleteExtraBookFilesToolStripMenuItem.Name = "deleteExtraBookFilesToolStripMenuItem";
            this.deleteExtraBookFilesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteExtraBookFilesToolStripMenuItem.Text = "Delete Extra Book Files";
            this.deleteExtraBookFilesToolStripMenuItem.Click += new System.EventHandler(this.deleteExtraBookFilesToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Name / Keywords";
            // 
            // NameKeywordsTB
            // 
            this.NameKeywordsTB.Location = new System.Drawing.Point(110, 27);
            this.NameKeywordsTB.Name = "NameKeywordsTB";
            this.NameKeywordsTB.Size = new System.Drawing.Size(220, 20);
            this.NameKeywordsTB.TabIndex = 7;
            // 
            // SearchB
            // 
            this.SearchB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchB.ForeColor = System.Drawing.Color.OliveDrab;
            this.SearchB.Location = new System.Drawing.Point(687, 27);
            this.SearchB.Name = "SearchB";
            this.SearchB.Size = new System.Drawing.Size(75, 23);
            this.SearchB.TabIndex = 9;
            this.SearchB.Text = "Search";
            this.SearchB.UseVisualStyleBackColor = true;
            this.SearchB.Click += new System.EventHandler(this.SearchB_Click);
            // 
            // CleanB
            // 
            this.CleanB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CleanB.ForeColor = System.Drawing.Color.OliveDrab;
            this.CleanB.Location = new System.Drawing.Point(687, 73);
            this.CleanB.Name = "CleanB";
            this.CleanB.Size = new System.Drawing.Size(75, 23);
            this.CleanB.TabIndex = 10;
            this.CleanB.Text = "Clean";
            this.CleanB.UseVisualStyleBackColor = true;
            this.CleanB.Click += new System.EventHandler(this.CleanB_Click);
            // 
            // AuthorTB
            // 
            this.AuthorTB.Location = new System.Drawing.Point(110, 53);
            this.AuthorTB.Name = "AuthorTB";
            this.AuthorTB.Size = new System.Drawing.Size(220, 20);
            this.AuthorTB.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Author";
            // 
            // CategoriesLB
            // 
            this.CategoriesLB.FormattingEnabled = true;
            this.CategoriesLB.Location = new System.Drawing.Point(434, 27);
            this.CategoriesLB.Name = "CategoriesLB";
            this.CategoriesLB.Size = new System.Drawing.Size(220, 69);
            this.CategoriesLB.TabIndex = 16;
            this.CategoriesLB.Click += new System.EventHandler(this.CategoriesLB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Categories";
            // 
            // YearFromTB
            // 
            this.YearFromTB.Location = new System.Drawing.Point(110, 79);
            this.YearFromTB.Name = "YearFromTB";
            this.YearFromTB.Size = new System.Drawing.Size(70, 20);
            this.YearFromTB.TabIndex = 18;
            // 
            // YearToTB
            // 
            this.YearToTB.Location = new System.Drawing.Point(186, 79);
            this.YearToTB.Name = "YearToTB";
            this.YearToTB.Size = new System.Drawing.Size(70, 20);
            this.YearToTB.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Years";
            // 
            // BooksDGV
            // 
            this.BooksDGV.AllowUserToAddRows = false;
            this.BooksDGV.AllowUserToDeleteRows = false;
            this.BooksDGV.AllowUserToResizeColumns = false;
            this.BooksDGV.AllowUserToResizeRows = false;
            this.BooksDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BooksDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.BooksDGV.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.BooksDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BooksDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.BooksDGV.GridColor = System.Drawing.Color.Gainsboro;
            this.BooksDGV.Location = new System.Drawing.Point(0, 105);
            this.BooksDGV.Name = "BooksDGV";
            this.BooksDGV.Size = new System.Drawing.Size(866, 359);
            this.BooksDGV.TabIndex = 21;
            this.BooksDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BooksDGV_CellDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(866, 486);
            this.Controls.Add(this.BooksDGV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.YearToTB);
            this.Controls.Add(this.YearFromTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CategoriesLB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AuthorTB);
            this.Controls.Add(this.CleanB);
            this.Controls.Add(this.SearchB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NameKeywordsTB);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sea";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BooksDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StoragePathSSL;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataAuthorsMI;
        private System.Windows.Forms.ToolStripMenuItem DataCategoriesMI;
        private System.Windows.Forms.ToolStripMenuItem DataBooksMI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel LastActionSSL;
        private System.Windows.Forms.ToolStripMenuItem ToolsArchiveMI;
        private System.Windows.Forms.ToolStripMenuItem ToolArchiveMI;
        private System.Windows.Forms.ToolStripMenuItem ToolsDearchiveMI;
        private System.Windows.Forms.ToolStripMenuItem ToolsRestoreMI;
        private System.Windows.Forms.ToolStripMenuItem correctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteExtraCategoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteExtraBookFilesToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NameKeywordsTB;
        private System.Windows.Forms.Button SearchB;
        private System.Windows.Forms.Button CleanB;
        private System.Windows.Forms.TextBox AuthorTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox CategoriesLB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox YearFromTB;
        private System.Windows.Forms.TextBox YearToTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolsBackupMI;
        private System.Windows.Forms.DataGridView BooksDGV;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolConvertMI;
    }
}

