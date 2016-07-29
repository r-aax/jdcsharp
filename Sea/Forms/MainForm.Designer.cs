﻿namespace Sea.Forms
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StoragePathSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.LastActionSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataBooksMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DataCategoriesMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DataAuthorsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DataPublishersMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsArchiveMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolArchiveMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsDearchiveMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsRestoreMI = new System.Windows.Forms.ToolStripMenuItem();
            this.correctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteExtraCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteExtraBookFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.SearchB = new System.Windows.Forms.Button();
            this.CleanB = new System.Windows.Forms.Button();
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
            this.DataAuthorsMI,
            this.DataPublishersMI});
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
            // DataPublishersMI
            // 
            this.DataPublishersMI.Name = "DataPublishersMI";
            this.DataPublishersMI.Size = new System.Drawing.Size(130, 22);
            this.DataPublishersMI.Text = "Publishers";
            this.DataPublishersMI.Click += new System.EventHandler(this.DataPublishersMI_Click);
            // 
            // ToolsArchiveMI
            // 
            this.ToolsArchiveMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolArchiveMI,
            this.ToolsDearchiveMI,
            this.ToolsRestoreMI});
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
            // ToolsRestoreMI
            // 
            this.ToolsRestoreMI.Name = "ToolsRestoreMI";
            this.ToolsRestoreMI.Size = new System.Drawing.Size(126, 22);
            this.ToolsRestoreMI.Text = "Restore";
            this.ToolsRestoreMI.Click += new System.EventHandler(this.ToolsRestoreMI_Click);
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
            this.label3.Location = new System.Drawing.Point(11, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Name";
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(52, 43);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(797, 20);
            this.NameTB.TabIndex = 7;
            // 
            // SearchB
            // 
            this.SearchB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchB.ForeColor = System.Drawing.Color.OliveDrab;
            this.SearchB.Location = new System.Drawing.Point(693, 88);
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
            this.CleanB.Location = new System.Drawing.Point(774, 88);
            this.CleanB.Name = "CleanB";
            this.CleanB.Size = new System.Drawing.Size(75, 23);
            this.CleanB.TabIndex = 10;
            this.CleanB.Text = "Clean";
            this.CleanB.UseVisualStyleBackColor = true;
            this.CleanB.Click += new System.EventHandler(this.CleanB_Click);
            // 
            // BooksDGV
            // 
            this.BooksDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BooksDGV.Location = new System.Drawing.Point(14, 136);
            this.BooksDGV.Name = "BooksDGV";
            this.BooksDGV.Size = new System.Drawing.Size(835, 325);
            this.BooksDGV.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 486);
            this.Controls.Add(this.BooksDGV);
            this.Controls.Add(this.CleanB);
            this.Controls.Add(this.SearchB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NameTB);
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
        private System.Windows.Forms.ToolStripMenuItem DataPublishersMI;
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
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.Button SearchB;
        private System.Windows.Forms.Button CleanB;
        private System.Windows.Forms.DataGridView BooksDGV;
    }
}

