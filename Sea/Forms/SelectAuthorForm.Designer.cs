namespace Sea.Forms
{
    partial class SelectAuthorForm
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
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AuthorsLB
            // 
            this.AuthorsLB.FormattingEnabled = true;
            this.AuthorsLB.Location = new System.Drawing.Point(12, 12);
            this.AuthorsLB.Name = "AuthorsLB";
            this.AuthorsLB.Size = new System.Drawing.Size(480, 446);
            this.AuthorsLB.TabIndex = 0;
            // 
            // AcceptB
            // 
            this.AcceptB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptB.ForeColor = System.Drawing.Color.OliveDrab;
            this.AcceptB.Location = new System.Drawing.Point(177, 464);
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
            this.CancelB.Location = new System.Drawing.Point(258, 464);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(75, 23);
            this.CancelB.TabIndex = 2;
            this.CancelB.Text = "Cancel";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // SelectAuthorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 495);
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.AcceptB);
            this.Controls.Add(this.AuthorsLB);
            this.MaximumSize = new System.Drawing.Size(520, 533);
            this.MinimumSize = new System.Drawing.Size(520, 533);
            this.Name = "SelectAuthorForm";
            this.Text = "Author selection";
            this.Shown += new System.EventHandler(this.SelectAuthorForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AuthorsLB;
        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
    }
}