namespace Sea.Forms
{
    partial class SelectCategoriesForm
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
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.DeselectAllB = new System.Windows.Forms.Button();
            this.SelectB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CategoriesTreeTV
            // 
            this.CategoriesTreeTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoriesTreeTV.BackColor = System.Drawing.Color.SeaShell;
            this.CategoriesTreeTV.Location = new System.Drawing.Point(12, 12);
            this.CategoriesTreeTV.Name = "CategoriesTreeTV";
            this.CategoriesTreeTV.Size = new System.Drawing.Size(658, 485);
            this.CategoriesTreeTV.TabIndex = 0;
            // 
            // AcceptB
            // 
            this.AcceptB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(414, 503);
            this.AcceptB.Name = "AcceptB";
            this.AcceptB.Size = new System.Drawing.Size(125, 23);
            this.AcceptB.TabIndex = 1;
            this.AcceptB.Text = "Accept";
            this.AcceptB.UseVisualStyleBackColor = true;
            this.AcceptB.Click += new System.EventHandler(this.AcceptB_Click);
            // 
            // CancelB
            // 
            this.CancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelB.ForeColor = System.Drawing.Color.IndianRed;
            this.CancelB.Location = new System.Drawing.Point(545, 503);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(125, 23);
            this.CancelB.TabIndex = 2;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // DeselectAllB
            // 
            this.DeselectAllB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeselectAllB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeselectAllB.Location = new System.Drawing.Point(152, 503);
            this.DeselectAllB.Name = "DeselectAllB";
            this.DeselectAllB.Size = new System.Drawing.Size(125, 23);
            this.DeselectAllB.TabIndex = 4;
            this.DeselectAllB.Text = "Unselect all";
            this.DeselectAllB.UseVisualStyleBackColor = true;
            this.DeselectAllB.Click += new System.EventHandler(this.DeselectAllB_Click);
            // 
            // SelectB
            // 
            this.SelectB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectB.Location = new System.Drawing.Point(12, 503);
            this.SelectB.Name = "SelectB";
            this.SelectB.Size = new System.Drawing.Size(134, 23);
            this.SelectB.TabIndex = 5;
            this.SelectB.Text = "Select / Unselect";
            this.SelectB.UseVisualStyleBackColor = true;
            this.SelectB.Click += new System.EventHandler(this.SelectB_Click);
            // 
            // SelectCategoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(682, 534);
            this.Controls.Add(this.SelectB);
            this.Controls.Add(this.DeselectAllB);
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.CategoriesTreeTV);
            this.MinimumSize = new System.Drawing.Size(690, 568);
            this.Name = "SelectCategoriesForm";
            this.Text = "Select categories";
            this.Shown += new System.EventHandler(this.SelectCategoriesForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView CategoriesTreeTV;
        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.Button DeselectAllB;
        private System.Windows.Forms.Button SelectB;
    }
}