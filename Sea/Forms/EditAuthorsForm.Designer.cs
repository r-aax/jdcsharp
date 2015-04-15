namespace Sea.Forms
{
    partial class EditAuthorsForm
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
            this.AuthorsLB = new System.Windows.Forms.ListBox();
            this.NewB = new System.Windows.Forms.Button();
            this.EditB = new System.Windows.Forms.Button();
            this.DeleteB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.AcceptB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AuthorsLB
            // 
            this.AuthorsLB.FormattingEnabled = true;
            this.AuthorsLB.Location = new System.Drawing.Point(12, 12);
            this.AuthorsLB.Name = "AuthorsLB";
            this.AuthorsLB.Size = new System.Drawing.Size(480, 446);
            this.AuthorsLB.TabIndex = 0;
            this.AuthorsLB.SelectedIndexChanged += new System.EventHandler(this.AuthorsLB_SelectedIndexChanged);
            // 
            // NewB
            // 
            this.NewB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewB.Location = new System.Drawing.Point(12, 464);
            this.NewB.Name = "NewB";
            this.NewB.Size = new System.Drawing.Size(75, 23);
            this.NewB.TabIndex = 1;
            this.NewB.Text = "New";
            this.NewB.UseVisualStyleBackColor = true;
            this.NewB.Click += new System.EventHandler(this.NewB_Click);
            // 
            // EditB
            // 
            this.EditB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditB.Location = new System.Drawing.Point(93, 464);
            this.EditB.Name = "EditB";
            this.EditB.Size = new System.Drawing.Size(75, 23);
            this.EditB.TabIndex = 2;
            this.EditB.Text = "Edit";
            this.EditB.UseVisualStyleBackColor = true;
            this.EditB.Click += new System.EventHandler(this.EditB_Click);
            // 
            // DeleteB
            // 
            this.DeleteB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteB.Location = new System.Drawing.Point(174, 464);
            this.DeleteB.Name = "DeleteB";
            this.DeleteB.Size = new System.Drawing.Size(75, 23);
            this.DeleteB.TabIndex = 3;
            this.DeleteB.Text = "Delete";
            this.DeleteB.UseVisualStyleBackColor = true;
            this.DeleteB.Click += new System.EventHandler(this.DeleteB_Click);
            // 
            // CancelB
            // 
            this.CancelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelB.ForeColor = System.Drawing.Color.IndianRed;
            this.CancelB.Location = new System.Drawing.Point(417, 464);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(75, 23);
            this.CancelB.TabIndex = 4;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // AcceptB
            // 
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(336, 464);
            this.AcceptB.Name = "AcceptB";
            this.AcceptB.Size = new System.Drawing.Size(75, 23);
            this.AcceptB.TabIndex = 5;
            this.AcceptB.Text = "Accept";
            this.AcceptB.UseVisualStyleBackColor = true;
            this.AcceptB.Click += new System.EventHandler(this.AcceptB_Click);
            // 
            // EditAuthorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 495);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.DeleteB);
            this.Controls.Add(this.EditB);
            this.Controls.Add(this.NewB);
            this.Controls.Add(this.AuthorsLB);
            this.MaximumSize = new System.Drawing.Size(520, 533);
            this.MinimumSize = new System.Drawing.Size(520, 533);
            this.Name = "EditAuthorsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit authors";
            this.Shown += new System.EventHandler(this.EditAuthorsForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AuthorsLB;
        private System.Windows.Forms.Button NewB;
        private System.Windows.Forms.Button EditB;
        private System.Windows.Forms.Button DeleteB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.Button AcceptB;
    }
}