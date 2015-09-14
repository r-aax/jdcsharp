// Author: Alexey Rybakov

using System;
using System.Windows.Forms;

using Sea.Tools;
using Sea.Core.Authors;

namespace Sea.Forms
{
    /// <summary>
    /// Edit authors form.
    /// </summary>
    public partial class EditAuthorsForm : Form
    {
        /// <summary>
        /// List of authors.
        /// </summary>
        private AuthorsList Authors;

        /// <summary>
        /// Set enable properties for controls.
        /// </summary>
        private void SetControlsEnable()
        {
            bool is_sel = AuthorsLB.SelectedIndex > -1;

            EditB.Enabled = is_sel;
            DeleteB.Enabled = is_sel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditAuthorsForm(AuthorsList authors)
        {
            InitializeComponent();

            Authors = authors;
        }

        /// <summary>
        /// Show edit authors form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditAuthorsForm_Shown(object sender, EventArgs e)
        {
            if (Authors.IsEmpty)
            {
                // Write that we create new authors list.
                Text = "Create new authors list (no authors file is found)";
            }

            Authors.ToListBox(AuthorsLB);
            SetControlsEnable();
        }

        /// <summary>
        /// New author click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewB_Click(object sender, EventArgs e)
        {
            EditAuthorForm form = new EditAuthorForm("Create new author");

            form.Author = null;
            form.ShowDialog();

            if (form.IsAccepted)
            {
                Author author = form.Author;

                if (author.FirstName == "")
                {
                    MessageBox.Show("Author must have first name");

                    return;
                }

                if (author.LastName == "")
                {
                    MessageBox.Show("Author must have last name");

                    return;
                }

                Authors.Add(author);
                Authors.Sort();
                Authors.ToListBox(AuthorsLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Edit author click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void EditB_Click(object sender, EventArgs e)
        {
            int i = AuthorsLB.SelectedIndex;

            if (i > -1)
            {
                EditAuthorForm form = new EditAuthorForm("Edit author");

                form.Author = Authors[i];
                form.ShowDialog();

                if (form.IsAccepted)
                {
                    Author author = form.Author;

                    if (author.FirstName == "")
                    {
                        MessageBox.Show("Author must have first name");

                        return;
                    }

                    if (author.LastName == "")
                    {
                        MessageBox.Show("Author must have last name");

                        return;
                    }

                    Authors[i] = author;
                    Authors.Sort();
                    Authors.ToListBox(AuthorsLB);
                }
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Delete author click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameter</param>
        private void DeleteB_Click(object sender, EventArgs e)
        {
            int i = AuthorsLB.SelectedIndex;

            if (i > -1)
            {
                Authors.RemoveAt(i);
                Authors.ToListBox(AuthorsLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Accept changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            Authors.XmlSerialize(Parameters.AuthorsXMLFullFilename);
            Close();
        }

        /// <summary>
        /// Cancel changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            // No changes.
            Close();
        }

        /// <summary>
        /// Change selected index.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AuthorsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }
    }
}
