// Author: Alexey Rybakov

using System;
using System.Windows.Forms;
using System.Diagnostics;

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

        /// <summary>
        /// Open edit books form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DataBooksMI_Click(object sender, EventArgs e)
        {
            EditBooksForm form = new EditBooksForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Open edit categories form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DataCategoriesMI_Click(object sender, EventArgs e)
        {
            EditCategoriesTreeForm form = new EditCategoriesTreeForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Open edit authors form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DataAuthorsMI_Click(object sender, EventArgs e)
        {
            EditAuthorsForm form = new EditAuthorsForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Open edit publishers form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameteres</param>
        private void DataPublishersMI_Click(object sender, EventArgs e)
        {
            EditPublishersForm form = new EditPublishersForm();

            form.ShowDialog();
        }
    }
}
