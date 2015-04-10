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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.NewB = new System.Windows.Forms.Button();
            this.EditB = new System.Windows.Forms.Button();
            this.DeleteB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(480, 433);
            this.listBox1.TabIndex = 0;
            // 
            // NewB
            // 
            this.NewB.Location = new System.Drawing.Point(132, 451);
            this.NewB.Name = "NewB";
            this.NewB.Size = new System.Drawing.Size(75, 23);
            this.NewB.TabIndex = 1;
            this.NewB.Text = "New";
            this.NewB.UseVisualStyleBackColor = true;
            // 
            // EditB
            // 
            this.EditB.Location = new System.Drawing.Point(213, 451);
            this.EditB.Name = "EditB";
            this.EditB.Size = new System.Drawing.Size(75, 23);
            this.EditB.TabIndex = 2;
            this.EditB.Text = "Edit";
            this.EditB.UseVisualStyleBackColor = true;
            // 
            // DeleteB
            // 
            this.DeleteB.Location = new System.Drawing.Point(294, 451);
            this.DeleteB.Name = "DeleteB";
            this.DeleteB.Size = new System.Drawing.Size(75, 23);
            this.DeleteB.TabIndex = 3;
            this.DeleteB.Text = "Delete";
            this.DeleteB.UseVisualStyleBackColor = true;
            // 
            // EditAuthorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 481);
            this.Controls.Add(this.DeleteB);
            this.Controls.Add(this.EditB);
            this.Controls.Add(this.NewB);
            this.Controls.Add(this.listBox1);
            this.MaximumSize = new System.Drawing.Size(520, 519);
            this.MinimumSize = new System.Drawing.Size(520, 519);
            this.Name = "EditAuthorsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit authors";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button NewB;
        private System.Windows.Forms.Button EditB;
        private System.Windows.Forms.Button DeleteB;
    }
}