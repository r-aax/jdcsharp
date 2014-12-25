// Copyright Joy Developing.

using System;
using System.Windows.Forms;

using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Storage path info.
            StoragePathSSL.Text = "Storage path: " + Parameters.StoragePath;
        }
    }
}
