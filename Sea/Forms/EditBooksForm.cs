// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Sea.Core.Books;
using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Edit books form.
    /// </summary>
    public partial class EditBooksForm : Form
    {
        /// <summary>
        /// List of books.
        /// </summary>
        private BooksList BooksList;

        /// <summary>
        /// Set enables properties of buttons.
        /// </summary>
        private void SetControlsEnable()
        {
            bool is_sel = BooksLB.SelectedIndex > -1;

            EditB.Enabled = is_sel;
            DeleteB.Enabled = is_sel;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditBooksForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditBooksForm_Shown(object sender, EventArgs e)
        {
            BooksList = BooksList.XmlDeserialize(Parameters.BooksXMLFullFilename);

            if (BooksList == null)
            {
                BooksList = new BooksList();

                // No books.
                Text = "Create new books list (no books file is found)";
            }

            BooksList.ToListBox(BooksLB);
            SetControlsEnable();
        }

        /// <summary>
        /// New book button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewB_Click(object sender, EventArgs e)
        {
            EditBookForm form = new EditBookForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Edit book button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Delete book button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DeleteB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Change selected index.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void BooksLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }
    }
}
