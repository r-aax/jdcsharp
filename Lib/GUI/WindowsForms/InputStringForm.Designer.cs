namespace Lib.GUI.WindowsForms
{
    partial class InputStringForm
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
            this.AcceptB = new System.Windows.Forms.Button();
            this.CancelB = new System.Windows.Forms.Button();
            this.TextTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AcceptB
            // 
            this.AcceptB.Location = new System.Drawing.Point(200, 38);
            this.AcceptB.Name = "AcceptB";
            this.AcceptB.Size = new System.Drawing.Size(75, 23);
            this.AcceptB.TabIndex = 0;
            this.AcceptB.Text = "Принять";
            this.AcceptB.UseVisualStyleBackColor = true;
            this.AcceptB.Click += new System.EventHandler(this.AcceptB_Click);
            // 
            // CancelB
            // 
            this.CancelB.Location = new System.Drawing.Point(281, 38);
            this.CancelB.Name = "CancelB";
            this.CancelB.Size = new System.Drawing.Size(75, 23);
            this.CancelB.TabIndex = 1;
            this.CancelB.Text = "Отменить";
            this.CancelB.UseVisualStyleBackColor = true;
            this.CancelB.Click += new System.EventHandler(this.CancelB_Click);
            // 
            // TextTB
            // 
            this.TextTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTB.Location = new System.Drawing.Point(12, 12);
            this.TextTB.Name = "TextTB";
            this.TextTB.Size = new System.Drawing.Size(526, 20);
            this.TextTB.TabIndex = 2;
            // 
            // InputStringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 70);
            this.Controls.Add(this.TextTB);
            this.Controls.Add(this.CancelB);
            this.Controls.Add(this.AcceptB);
            this.MaximumSize = new System.Drawing.Size(558, 104);
            this.MinimumSize = new System.Drawing.Size(558, 104);
            this.Name = "InputStringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Shown += new System.EventHandler(this.InputStringForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptB;
        private System.Windows.Forms.Button CancelB;
        private System.Windows.Forms.TextBox TextTB;
    }
}