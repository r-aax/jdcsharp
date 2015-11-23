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
        private BooksList Books;

        /// <summary>
        /// Accept button flag.
        /// </summary>
        public bool IsAccepted = false;

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
        /// <param name="books">books list</param>
        /// </summary>
        public EditBooksForm(BooksList books)
        {
            InitializeComponent();

            Books = books;
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditBooksForm_Shown(object sender, EventArgs e)
        {
            if (Books.IsEmpty)
            {
                // No books.
                Text = "Create new books list (no books file is found)";
            }

            Books.ToListBox(BooksLB);
            SetControlsEnable();
        }

        /// <summary>
        /// New book button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewB_Click(object sender, EventArgs e)
        {
            EditBookForm form = new EditBookForm("Create new book");

            form.ShowDialog();

            if (form.IsAccepted)
            {
                Books.Add(form.Book);
                Books.Sort();
                Books.ToListBox(BooksLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Edit book button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditB_Click(object sender, EventArgs e)
        {
            int i = BooksLB.SelectedIndex;

            if (i > -1)
            {
                Book book = Books[i].Clone() as Book;

                EditBookForm form = new EditBookForm("Edit book");

                form.Book = book;
                form.ShowDialog();

                Books[i] = form.Book.Clone() as Book;
                Books.Sort();
                Books.ToListBox(BooksLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Delete book button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DeleteB_Click(object sender, EventArgs e)
        {
            int i = BooksLB.SelectedIndex;

            if (i > -1)
            {
                Books.Items.RemoveAt(i);
                Books.ToListBox(BooksLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            // No changes.
            IsAccepted = false;
            Close();
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
