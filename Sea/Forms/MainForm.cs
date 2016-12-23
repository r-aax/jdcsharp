// Author: Alexey Rybakov

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing;

using Lib.Utils;

using Sea.Core;
using Sea.Core.Books;
using Sea.Core.Authors;
using Sea.Tools;
using Sea.Core.Categories;

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
        /// Categories for filter.
        /// </summary>
        private CategoriesList FilterCategories;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Sea = new Core.Sea();
            FilterCategories = new CategoriesList();
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
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archive (*.zip)|*.zip";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Sea.Archive(sfd.FileName);
                ShowLastAction("archivation to " + sfd.FileName + " is completed");
            }
        }

        /// <summary>
        /// Dearchive all data.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ToolsDearchiveMI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archive (*.zip)|*.zip";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (Sea.Dearchive(ofd.FileName))
                {
                    ShowLastAction("dearchivation from " + ofd.FileName + " is completed");
                }
            }
        }

        /// <summary>
        /// Backup data.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ToolsBackupMI_Click(object sender, EventArgs e)
        {
            Sea.Archive(Parameters.StoragePathBackupArchive);
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
        /// Convert data to new format.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ToolConvertMI_Click(object sender, EventArgs e)
        {
            (new AuthorsListSpecial(Sea.Authors)).XmlSerialize(Parameters.AuthorsXMLFullFilename + ".converted");
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
            Sea.SearchBooks(NameTB.Text.ToLower(),
                            AuthorTB.Text.ToLower(),
                            PublisherTB.Text.ToLower(),
                            FilterCategories,
                            YearFromTB.Text,
                            YearToTB.Text);
            ShowLastAction(Sea.SBooks.Count.ToString() + " books found");
            FillBooksDataGridView(BooksDGV, Sea.SBooks);
            BooksDGV.Sort(BooksDGV.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        /// <summary>
        /// Clean filters.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CleanB_Click(object sender, EventArgs e)
        {
            // Clean search forms.
            NameTB.Clear();
            AuthorTB.Clear();
            PublisherTB.Clear();
            YearFromTB.Clear();
            YearToTB.Clear();
            FilterCategories.Clear();
            FilterCategories.ToListBox(CategoriesLB);
        }

        /// <summary>
        /// Fill data grid with books.
        /// </summary>
        /// <param name="g">data grid view</param>
        /// <param name="b">books list</param>
        private void FillBooksDataGridView(DataGridView g, BooksList b)
        {
            // Set grid.
            g.RowCount = b.Count;
            g.ColumnCount = 3;
            g.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            g.Columns[0].HeaderText = "Description";
            g.Columns[1].HeaderText = "Id";
            g.Columns[2].HeaderText = "Info";

            // Set font.
            Font font = new Font(g.Font.Name, g.Font.Size, FontStyle.Bold);
            foreach (DataGridViewColumn col in g.Columns)
            {
                col.HeaderCell.Style.Font = font;
            }

            // Set data to grid.
            for (int i = 0; i < b.Count; i++)
            {
                DataGridViewRow row = g.Rows[i];
                row.Cells[0].Value = b[i].FullName(BookFullNamePrintStyle.Wide);
                row.Cells[1].Style.Alignment = DataGridViewContentAlignment.TopRight;
                row.Cells[1].Value = b[i].Id.ToString();
                row.Cells[2].Style.Alignment = DataGridViewContentAlignment.TopRight;
                row.Cells[2].Value = b[i].TypeString;
            }
        }

        /// <summary>
        /// Click on cell.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void BooksDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("cell click : " + e.RowIndex + ", " + e.ColumnIndex);
        }

        /// <summary>
        /// Click on categories list.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CategoriesLB_Click(object sender, EventArgs e)
        {
            SelectCategoriesForm form = new SelectCategoriesForm();

            form.Categories = (FilterCategories.Clone() as CategoriesList).Items;
            form.ShowDialog();

            if (form.IsAccepted)
            {
                FilterCategories.Items = form.Categories;
                FilterCategories.ToListBox(CategoriesLB);
            }
        }

        /// <summary>
        /// Double-click on table cell.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void BooksDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int col_ind = e.ColumnIndex;
            int row_ind = e.RowIndex;
            string id_string = BooksDGV.Rows[row_ind].Cells[1].Value as string;
            int id = Lib.Utils.Convert.GetInt(id_string);
            Book b = Sea.SBooks.FindById(id);

            // Start process.
            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = Parameters.DataPath;
            info.FileName = Parameters.DataPath + "/" + b.File;
            Process.Start(info);
        }
    }
}
