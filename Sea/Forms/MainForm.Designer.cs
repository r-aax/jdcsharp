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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StoragePathSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataCategoriesMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DataAuthorsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DataPublishersMI = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StoragePathSSL});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(819, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StoragePathSSL
            // 
            this.StoragePathSSL.Name = "StoragePathSSL";
            this.StoragePathSSL.Size = new System.Drawing.Size(229, 17);
            this.StoragePathSSL.Text = "Путь к хранилищу: <путь к хранилищу>";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.данныеToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(819, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataCategoriesMI,
            this.DataAuthorsMI,
            this.DataPublishersMI});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.данныеToolStripMenuItem.Text = "Data";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 486);
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
    }
}

