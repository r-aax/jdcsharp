// Author: Alexey Rybakov

using System;
using System.Windows.Forms;

using Sea.Core.Authors;

namespace Sea.Forms
{
    /// <summary>
    /// Edit author form.
    /// </summary>
    public partial class EditAuthorForm : Form
    {
        /// <summary>
        /// Author.
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAccepted;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditAuthorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="label">label of form</param>
        public EditAuthorForm(string label)
            : this()
        {
            Text = label;
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditAuthorForm_Shown(object sender, EventArgs e)
        {
            if (Author != null)
            {
                IdTB.Text = Author.Id.ToString();
                RusFirstNameTB.Text = Author.RusFirstName;
                RusSecondNameTB.Text = Author.RusSecondName;
                RusLastNameTB.Text = Author.RusLastName;
                EngFirstNameTB.Text = Author.EngFirstName;
                EngSecondNameTB.Text = Author.EngSecondName;
                EngLastNameTB.Text = Author.EngLastName;
            }
        }

        /// <summary>
        /// Accept edit.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            if (Author == null)
            {
                // New author.
                Author = new Author(RusFirstNameTB.Text, RusSecondNameTB.Text, RusLastNameTB.Text,
                                    EngFirstNameTB.Text, EngSecondNameTB.Text, EngLastNameTB.Text);
            }
            else
            {
                // Old author, save identifier.
                int id = Author.Id;
                Author = new Author(RusFirstNameTB.Text, RusSecondNameTB.Text, RusLastNameTB.Text,
                                    EngFirstNameTB.Text, EngSecondNameTB.Text, EngLastNameTB.Text);
                Author.Id = id;
            }

            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel edit.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            IsAccepted = false;
            Close();
        }
    }
}
