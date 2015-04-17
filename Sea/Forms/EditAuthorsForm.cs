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
        private AuthorsList AuthorsList;

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
        public EditAuthorsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show edit authors form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditAuthorsForm_Shown(object sender, EventArgs e)
        {
            AuthorsList = AuthorsList.XmlDeserialize(Parameters.AuthorsXMLFullFilename);

            if (AuthorsList == null)
            {
                AuthorsList = new AuthorsList();

                // Write that we create new authors list.
                Text = "Create new authors list (no authors file is found)";
            }

            AuthorsList.ToListBox(AuthorsLB);
            SetControlsEnable();
        }

        /// <summary>
        /// New author click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewB_Click(object sender, EventArgs e)
        {
            EditAuthorForm form = new EditAuthorForm("", "", "", "Create new author");

            form.ShowDialog();

            if (form.IsAccepted)
            {
                if (form.FirstName == "")
                {
                    MessageBox.Show("Author must have first name");

                    return;
                }

                if (form.LastName == "")
                {
                    MessageBox.Show("Author must have last name");

                    return;
                }

                AuthorsList.Items.Add(new Author(form.FirstName, form.SecondName, form.LastName));
                AuthorsList.Sort();
                AuthorsList.ToListBox(AuthorsLB);
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
                Author author = AuthorsList[i];

                EditAuthorForm form = new EditAuthorForm(author.FirstName,
                                                         author.SecondName,
                                                         author.LastName,
                                                         "Edit author");

                form.ShowDialog();

                if (form.FirstName == "")
                {
                    MessageBox.Show("Author must have first name");

                    return;
                }

                if (form.LastName == "")
                {
                    MessageBox.Show("Author must have last name");

                    return;
                }

                AuthorsList[i].FirstName = form.FirstName;
                AuthorsList[i].SecondName = form.SecondName;
                AuthorsList[i].LastName = form.LastName;
                AuthorsList.Sort();
                AuthorsList.ToListBox(AuthorsLB);
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
                AuthorsList.Items.RemoveAt(i);
                AuthorsList.ToListBox(AuthorsLB);
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
            AuthorsList.XmlSerialize(Parameters.AuthorsXMLFullFilename);
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
