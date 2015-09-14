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
using Sea.Core.Categories;

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
        /// Set controls enable.
        /// </summary>
        private void SetControlsEnable()
        {
            // Can delete author if it is selected.
            DeleteAuthorB.Enabled = AuthorsLB.SelectedIndex > -1;

            // Can delete publisher if it is selected.
            DeletePublisherB.Enabled = PublishersLB.SelectedIndex > -1;

            // For different types of books controls enables may differ.
            BookType book_type = IntToBookType(TypeCB.SelectedIndex);
            bool is_magazine = book_type == BookType.Magazine;
            bool is_article = book_type == BookType.Article;
            ArticleSourceTB.Enabled = is_article;
            NumberTB.Enabled = is_article || is_magazine;

            // Clean not enable text boxes.

            if (!ArticleSourceTB.Enabled)
            {
                ArticleSourceTB.Text = "";
            }

            if (!NumberTB.Enabled)
            {
                NumberTB.Text = "";
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditBookForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="label">label of form</param>
        public EditBookForm(string label)
            : this()
        {
            Text = label;
        }

        /// <summary>
        /// Convert integer to book type.
        /// </summary>
        /// <param name="i">book type number</param>
        /// <returns>book type</returns>
        private BookType IntToBookType(int i)
        {
            switch (i)
            {
                case 0:
                    return BookType.Book;

                case 1:
                    return BookType.Magazine;

                case 2:
                    return BookType.Article;

                case 3:
                    return BookType.Other;

                default:
                    Debug.Assert(false, "Unknown book type.");
                    return BookType.Other;
            }
        }

        /// <summary>
        /// Convert book type to integer.
        /// </summary>
        /// <param name="type">book type</param>
        /// <returns>book type number</returns>
        private int BookTypeToInt(BookType type)
        {
            switch (type)
            {
                case BookType.Book:
                    return 0;

                case BookType.Magazine:
                    return 1;

                case BookType.Article:
                    return 2;

                case BookType.Other:
                    return 3;

                default:
                    Debug.Assert(false, "Unknown book type.");
                    return 3;
            }
        }

        /// <summary>
        /// Accept changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            // Save new book.
            try
            {
                Book.Name = NameTB.Text;
                Book.Type = IntToBookType(TypeCB.SelectedIndex);
                Book.ArticleSource = ArticleSourceTB.Text;
                Book.Number = (NumberTB.Text == "") ? 0 : Convert.ToInt32(NumberTB.Text);
                Book.Edition = (EditionTB.Text == "") ? 1 : Convert.ToInt32(EditionTB.Text);
                Book.Year = (YearTB.Text == "") ? 0 : Convert.ToInt32(YearTB.Text);

                IsAccepted = true;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Not valid book data.");
            }

            SetControlsEnable();
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
                Book.Authors.Add(form.Author);
                Book.Authors.Sort();
            }

            Book.Authors.ToListBox(AuthorsLB);
            SetControlsEnable();
        }

        /// <summary>
        /// Delete author button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void DeleteAuthorB_Click(object sender, EventArgs e)
        {
            Book.Authors.RemoveAt(AuthorsLB.SelectedIndex);
            Book.Authors.ToListBox(AuthorsLB);
            SetControlsEnable();
        }

        /// <summary>
        /// Add publisher button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void AddPublisherB_Click(object sender, EventArgs e)
        {
            SelectPublisherForm form = new SelectPublisherForm();

            form.ShowDialog();

            if (form.IsAccepted)
            {
                Book.Publishers.Add(form.Publisher);
            }

            Book.Publishers.ToListBox(PublishersLB);
            SetControlsEnable();
        }

        /// <summary>
        /// Delete publisher button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void DeletePublisherB_Click(object sender, EventArgs e)
        {
            Book.Publishers.RemoveAt(PublishersLB.SelectedIndex);
            Book.Publishers.ToListBox(PublishersLB);
            SetControlsEnable();
        }

        /// <summary>
        /// Change categories button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ChangeCategoriesB_Click(object sender, EventArgs e)
        {
            SelectCategoriesForm form = new SelectCategoriesForm();

            form.Categories = (Book.Categories.Clone() as CategoriesList).Items;
            form.ShowDialog();

            if (form.IsAccepted)
            {
                Book.Categories.Items = form.Categories;
                Book.Categories.ToListBox(CategoriesLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditBookForm_Shown(object sender, EventArgs e)
        {
            if (Book != null)
            {
                // Show book.

                IdTB.Text = Book.Id.ToString();
                NameTB.Text = Book.Name;
                TypeCB.SelectedIndex = BookTypeToInt(Book.Type);
                ArticleSourceTB.Text = Book.ArticleSource;
                NumberTB.Text = Book.Number.ToString();

                // Year 0 is no year.
                YearTB.Text = (Book.Year != 0) ? Book.Year.ToString() : "";

                // Edition 1 it is edition by default (we do not need to show it).
                EditionTB.Text = (Book.Edition != 1) ? Book.Edition.ToString() : "";

                // Lists.
                Book.Authors.ToListBox(AuthorsLB);
                Book.Publishers.ToListBox(PublishersLB);
                Book.Categories.ToListBox(CategoriesLB);

                // Controls.
                SetControlsEnable();
            }
            else
            {
                Book = new Book();
                TypeCB.SelectedIndex = (int)BookType.Book;
            }
        }

        /// <summary>
        /// Authors selected index change.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AuthorsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }

        /// <summary>
        /// Publishers selected index change.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void PublishersLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }

        /// <summary>
        /// Categories selected index change.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CategoriesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }

        /// <summary>
        /// Change type of book click.
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="e">parameters</param>
        private void TypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }
    }
}
