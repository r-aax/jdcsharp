// Author: Alexey Rybakov

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using Sea.Core;
using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Sea, BD representation.
        /// </summary>
        private Core.Sea Sea;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Sea = new Core.Sea();
            ShowLastAction("data is loaded");
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

            // Deserialize all.
            Sea.Deserialize();

            // Backup.
            // We do backup while data base is not too big.
            Sea.Archive(Parameters.StoragePathBackupArchive);
        }

        /// <summary>
        /// Open edit books form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DataBooksMI_Click(object sender, EventArgs e)
        {
            EditBooksForm form = new EditBooksForm(Sea.Books);

            form.ShowDialog();
            Sea.FixBooks(form.IsAccepted);
        }

        /// <summary>
        /// Open edit categories form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DataCategoriesMI_Click(object sender, EventArgs e)
        {
            EditCategoriesTreeForm form = new EditCategoriesTreeForm(Sea.CategoryRoot);

            form.ShowDialog();
            Sea.FixCategories(form.IsAccepted);
        }

        /// <summary>
        /// Open edit authors form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DataAuthorsMI_Click(object sender, EventArgs e)
        {
            EditAuthorsForm form = new EditAuthorsForm(Sea.Authors);

            form.ShowDialog();
            Sea.FixAuthors(form.IsAccepted);
        }

        /// <summary>
        /// Open edit publishers form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameteres</param>
        private void DataPublishersMI_Click(object sender, EventArgs e)
        {
            EditPublishersForm form = new EditPublishersForm(Sea.Publishers);

            form.ShowDialog();
            Sea.FixPublishers(form.IsAccepted);
        }

        /// <summary>
        /// Show last action.
        /// </summary>
        /// <param name="action">action description</param>
        private void ShowLastAction(string action)
        {
            LastActionSSL.Text = "Last action: " + action;
        }

        /// <summary>
        /// Archive all data.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ToolArchiveMI_Click(object sender, EventArgs e)
        {
            Sea.Archive(Parameters.StoragePathArchive);
        }

        /// <summary>
        /// Dearchive all data.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ToolsDearchiveMI_Click(object sender, EventArgs e)
        {
            if (Sea.Dearchive(Parameters.StoragePathArchive))
            {
                ShowLastAction("dearchivation is completed");
            }
        }

        /// <summary>
        /// Restore data from last backup.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ToolsRestoreMI_Click(object sender, EventArgs e)
        {
            if (Sea.Dearchive(Parameters.StoragePathBackupArchive))
            {
                ShowLastAction("data restore is completed");
            }
        }

        /// <summary>
        /// Delete extra categories.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void deleteExtraCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sea.Books.DeleteExtraCategories();
        }

        /// <summary>
        /// Delete extra book files.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void deleteExtraBookFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Start search.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void SearchB_Click(object sender, EventArgs e)
        {
            // Now we ignore all filters, just copy all books and display them.
            Sea.SearchBooks();
            ShowLastAction(Sea.SBooks.Count.ToString() + " books found");

            {
                ;
            }
        }

        /// <summary>
        /// Clean filters.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CleanB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }
    }
}
