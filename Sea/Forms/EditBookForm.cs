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
using Sea.Core.Authors;

namespace Sea.Forms
{
    /// <summary>
    /// Edit book form.
    /// </summary>
    public partial class EditBookForm : Form
    {
        /// <summary>
        /// New book.
        /// </summary>
        public Book Book = null;

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAccepted;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditBookForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Accept changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            // Save new book.
            Book = new Book();

            // Fill fields.
            Book.Name = NameTB.Text;

            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            // Just close the form.
            IsAccepted = false;
            Close();
        }

        /// <summary>
        /// Add author button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void AddAuthorB_Click(object sender, EventArgs e)
        {
            SelectAuthorForm form = new SelectAuthorForm();

            form.ShowDialog();

            if (form.IsAccepted)
            {
                Book.AuthorsList.Add(form.Author);
            }

            Book.AuthorsList.ToListBox(AuthorsLB);
        }

        /// <summary>
        /// Delete author button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void DeleteAuthorB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Add publisher button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void AddPublisherB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Delete publisher button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void DeletePublisherB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Add category button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AddCategoryB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Delete category button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DeleteCategoryB_Click(object sender, EventArgs e)
        {
            Debug.Assert(false);
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditBookForm_Shown(object sender, EventArgs e)
        {
        }
    }
}
