namespace Sea.Forms
{
    partial class EditCategoriesTreeForm
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
            this.CategoriesTreeTV = new System.Windows.Forms.TreeView();
            this.NewCategoryB = new System.Windows.Forms.Button();
            this.MoveUpB = new System.Windows.Forms.Button();
            this.MoveDownB = new System.Windows.Forms.Button();
            this.RenameB = new System.Windows.Forms.Button();
            this.HoldB = new System.Windows.Forms.Button();
            this.ReplaceAfterB = new System.Windows.Forms.Button();
            this.ReplaceUnderB = new System.Windows.Forms.Button();
            this.AddBeforeB = new System.Windows.Forms.Button();
            this.AddAfterB = new System.Windows.Forms.Button();
            this.AddUnderB = new System.Windows.Forms.Button();
            this.DeleteB = new System.Windows.Forms.Button();
            this.ReplaceBeforeB = new System.Windows.Forms.Button();
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.MainMenuMS = new System.Windows.Forms.MenuStrip();
            this.обновитьСчетчикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FunctionsShowCountersMI = new System.Windows.Forms.ToolStripMenuItem();
            this.FunctionsHideCountersMI = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // CategoriesTreeTV
            // 
            this.CategoriesTreeTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoriesTreeTV.BackColor = System.Drawing.Color.SeaShell;
            this.CategoriesTreeTV.Location = new System.Drawing.Point(12, 27);
            this.CategoriesTreeTV.Name = "CategoriesTreeTV";
            this.CategoriesTreeTV.Size = new System.Drawing.Size(533, 550);
            this.CategoriesTreeTV.TabIndex = 0;
            this.CategoriesTreeTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CategoriesTreeTV_AfterSelect);
            // 
            // NewCategoryB
            // 
            this.NewCategoryB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewCategoryB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewCategoryB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NewCategoryB.Location = new System.Drawing.Point(551, 27);
            this.NewCategoryB.Name = "NewCategoryB";
            this.NewCategoryB.Size = new System.Drawing.Size(125, 23);
            this.NewCategoryB.TabIndex = 1;
            this.NewCategoryB.Text = "New category";
            this.NewCategoryB.UseVisualStyleBackColor = true;
            this.NewCategoryB.Click += new System.EventHandler(this.NewCategoryB_Click);
            // 
            // MoveUpB
            // 
            this.MoveUpB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveUpB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MoveUpB.Location = new System.Drawing.Point(551, 193);
            this.MoveUpB.Name = "MoveUpB";
            this.MoveUpB.Size = new System.Drawing.Size(125, 23);
            this.MoveUpB.TabIndex = 2;
            this.MoveUpB.Text = "Move up";
            this.MoveUpB.UseVisualStyleBackColor = true;
            this.MoveUpB.Click += new System.EventHandler(this.MoveUpB_Click);
            // 
            // MoveDownB
            // 
            this.MoveDownB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveDownB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MoveDownB.Location = new System.Drawing.Point(551, 222);
            this.MoveDownB.Name = "MoveDownB";
            this.MoveDownB.Size = new System.Drawing.Size(125, 23);
            this.MoveDownB.TabIndex = 3;
            this.MoveDownB.Text = "Move down";
            this.MoveDownB.UseVisualStyleBackColor = true;
            this.MoveDownB.Click += new System.EventHandler(this.MoveDownB_Click);
            // 
            // RenameB
            // 
            this.RenameB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RenameB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RenameB.Location = new System.Drawing.Point(551, 442);
            this.RenameB.Name = "RenameB";
            this.RenameB.Size = new System.Drawing.Size(125, 23);
            this.RenameB.TabIndex = 4;
            this.RenameB.Text = "Rename";
            this.RenameB.UseVisualStyleBackColor = true;
            this.RenameB.Click += new System.EventHandler(this.RenameB_Click);
            // 
            // HoldB
            // 
            this.HoldB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HoldB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HoldB.ForeColor = System.Drawing.Color.Chocolate;
            this.HoldB.Location = new System.Drawing.Point(551, 276);
            this.HoldB.Name = "HoldB";
            this.HoldB.Size = new System.Drawing.Size(125, 23);
            this.HoldB.TabIndex = 5;
            this.HoldB.Text = "Capture";
            this.HoldB.UseVisualStyleBackColor = true;
            this.HoldB.Click += new System.EventHandler(this.HoldB_Click);
            // 
            // ReplaceAfterB
            // 
            this.ReplaceAfterB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceAfterB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReplaceAfterB.Location = new System.Drawing.Point(551, 359);
            this.ReplaceAfterB.Name = "ReplaceAfterB";
            this.ReplaceAfterB.Size = new System.Drawing.Size(125, 23);
            this.ReplaceAfterB.TabIndex = 6;
            this.ReplaceAfterB.Text = "Replace after";
            this.ReplaceAfterB.UseVisualStyleBackColor = true;
            this.ReplaceAfterB.Click += new System.EventHandler(this.ReplaceAfterB_Click);
            // 
            // ReplaceUnderB
            // 
            this.ReplaceUnderB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceUnderB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReplaceUnderB.Location = new System.Drawing.Point(551, 388);
            this.ReplaceUnderB.Name = "ReplaceUnderB";
            this.ReplaceUnderB.Size = new System.Drawing.Size(125, 23);
            this.ReplaceUnderB.TabIndex = 7;
            this.ReplaceUnderB.Text = "Replace under";
            this.ReplaceUnderB.UseVisualStyleBackColor = true;
            this.ReplaceUnderB.Click += new System.EventHandler(this.ReplaceUnderB_Click);
            // 
            // AddBeforeB
            // 
            this.AddBeforeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBeforeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddBeforeB.Location = new System.Drawing.Point(551, 81);
            this.AddBeforeB.Name = "AddBeforeB";
            this.AddBeforeB.Size = new System.Drawing.Size(125, 23);
            this.AddBeforeB.TabIndex = 8;
            this.AddBeforeB.Text = "Add before";
            this.AddBeforeB.UseVisualStyleBackColor = true;
            this.AddBeforeB.Click += new System.EventHandler(this.AddBeforeB_Click);
            // 
            // AddAfterB
            // 
            this.AddAfterB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddAfterB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddAfterB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddAfterB.Location = new System.Drawing.Point(551, 110);
            this.AddAfterB.Name = "AddAfterB";
            this.AddAfterB.Size = new System.Drawing.Size(125, 23);
            this.AddAfterB.TabIndex = 9;
            this.AddAfterB.Text = "Add after";
            this.AddAfterB.UseVisualStyleBackColor = true;
            this.AddAfterB.Click += new System.EventHandler(this.AddAfterB_Click);
            // 
            // AddUnderB
            // 
            this.AddUnderB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddUnderB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddUnderB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddUnderB.Location = new System.Drawing.Point(551, 139);
            this.AddUnderB.Name = "AddUnderB";
            this.AddUnderB.Size = new System.Drawing.Size(125, 23);
            this.AddUnderB.TabIndex = 10;
            this.AddUnderB.Text = "Add under";
            this.AddUnderB.UseVisualStyleBackColor = true;
            this.AddUnderB.Click += new System.EventHandler(this.AddUnderB_Click);
            // 
            // DeleteB
            // 
            this.DeleteB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteB.Location = new System.Drawing.Point(551, 471);
            this.DeleteB.Name = "DeleteB";
            this.DeleteB.Size = new System.Drawing.Size(125, 23);
            this.DeleteB.TabIndex = 11;
            this.DeleteB.Text = "Delete";
            this.DeleteB.UseVisualStyleBackColor = true;
            this.DeleteB.Click += new System.EventHandler(this.DeleteB_Click);
            // 
            // ReplaceBeforeB
            // 
            this.ReplaceBeforeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceBeforeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReplaceBeforeB.Location = new System.Drawing.Point(551, 330);
            this.ReplaceBeforeB.Name = "ReplaceBeforeB";
            this.ReplaceBeforeB.Size = new System.Drawing.Size(125, 23);
            this.ReplaceBeforeB.TabIndex = 12;
            this.ReplaceBeforeB.Text = "Replace before";
            this.ReplaceBeforeB.UseVisualStyleBackColor = true;
            this.ReplaceBeforeB.Click += new System.EventHandler(this.ReplaceBeforeB_Click);
            // 
            // AcceptB
            // 
            this.AcceptB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(551, 525);
            this.AcceptB.Name = "AcceptB";
            this.AcceptB.Size = new System.Drawing.Size(125, 23);
            this.AcceptB.TabIndex = 14;
            this.AcceptB.Text = "Accept";
            this.AcceptB.UseVisualStyleBackColor = true;
            this.AcceptB.Click += new System.EventHandler(this.AcceptB_Click);
            // 
            // CancelB
            // 
            this.CancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelB.ForeColor = System.Drawing.Color.IndianRed;
            this.CancelB.Location = new System.Drawing.Point(551, 554);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(125, 23);
            this.CancelB.TabIndex = 15;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // MainMenuMS
            // 
            this.MainMenuMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьСчетчикиToolStripMenuItem});
            this.MainMenuMS.Location = new System.Drawing.Point(0, 0);
            this.MainMenuMS.Name = "MainMenuMS";
            this.MainMenuMS.Size = new System.Drawing.Size(684, 24);
            this.MainMenuMS.TabIndex = 16;
            this.MainMenuMS.Text = "menuStrip1";
            // 
            // обновитьСчетчикиToolStripMenuItem
            // 
            this.обновитьСчетчикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FunctionsShowCountersMI,
            this.FunctionsHideCountersMI});
            this.обновитьСчетчикиToolStripMenuItem.Name = "обновитьСчетчикиToolStripMenuItem";
            this.обновитьСчетчикиToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.обновитьСчетчикиToolStripMenuItem.Text = "Функции";
            // 
            // FunctionsShowCountersMI
            // 
            this.FunctionsShowCountersMI.Name = "FunctionsShowCountersMI";
            this.FunctionsShowCountersMI.Size = new System.Drawing.Size(178, 22);
            this.FunctionsShowCountersMI.Text = "Показать счетчики";
            this.FunctionsShowCountersMI.Click += new System.EventHandler(this.FunctionsRefreshCountersMI_Click);
            // 
            // FunctionsHideCountersMI
            // 
            this.FunctionsHideCountersMI.Name = "FunctionsHideCountersMI";
            this.FunctionsHideCountersMI.Size = new System.Drawing.Size(178, 22);
            this.FunctionsHideCountersMI.Text = "Скрыть счетчики";
            this.FunctionsHideCountersMI.Click += new System.EventHandler(this.FunctionsHideCountersMI_Click);
            // 
            // EditCategoriesTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(684, 585);
            this.ControlBox = false;
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.ReplaceBeforeB);
            this.Controls.Add(this.DeleteB);
            this.Controls.Add(this.AddUnderB);
            this.Controls.Add(this.AddAfterB);
            this.Controls.Add(this.AddBeforeB);
            this.Controls.Add(this.ReplaceUnderB);
            this.Controls.Add(this.ReplaceAfterB);
            this.Controls.Add(this.HoldB);
            this.Controls.Add(this.RenameB);
            this.Controls.Add(this.MoveDownB);
            this.Controls.Add(this.MoveUpB);
            this.Controls.Add(this.NewCategoryB);
            this.Controls.Add(this.CategoriesTreeTV);
            this.Controls.Add(this.MainMenuMS);
            this.MainMenuStrip = this.MainMenuMS;
            this.MinimumSize = new System.Drawing.Size(692, 599);
            this.Name = "EditCategoriesTreeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit categories tree";
            this.Shown += new System.EventHandler(this.EditCategoriesTreeForm_Shown);
            this.MainMenuMS.ResumeLayout(false);
            this.MainMenuMS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView CategoriesTreeTV;
        private System.Windows.Forms.Button NewCategoryB;
        private System.Windows.Forms.Button MoveUpB;
        private System.Windows.Forms.Button MoveDownB;
        private System.Windows.Forms.Button RenameB;
        private System.Windows.Forms.Button HoldB;
        private System.Windows.Forms.Button ReplaceAfterB;
        private System.Windows.Forms.Button ReplaceUnderB;
        private System.Windows.Forms.Button AddBeforeB;
        private System.Windows.Forms.Button AddAfterB;
        private System.Windows.Forms.Button AddUnderB;
        private System.Windows.Forms.Button DeleteB;
        private System.Windows.Forms.Button ReplaceBeforeB;
        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.MenuStrip MainMenuMS;
        private System.Windows.Forms.ToolStripMenuItem обновитьСчетчикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FunctionsShowCountersMI;
        private System.Windows.Forms.ToolStripMenuItem FunctionsHideCountersMI;
    }
}