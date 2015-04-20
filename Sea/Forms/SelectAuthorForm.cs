using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sea.Core.Authors;
using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Select author form.
    /// </summary>
    public partial class SelectAuthorForm : Form
    {
        /// <summary>
        /// List of authors.
        /// </summary>
        private AuthorsList AuthorsList;

        /// <summary>
        /// Author.
        /// </summary>
        public Author Author = null;

        /// <summary>
        /// Accep flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Set controls enabled.
        /// </summary>
        private void SetControlsEnable()
        {
            bool is_sel = AuthorsLB.SelectedIndex >= -1;

            AcceptB.Enabled = is_sel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectAuthorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            IsAccepted = true;
            Author = AuthorsList[AuthorsLB.SelectedIndex];
            Close();
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            // Nothing happened.
            IsAccepted = false;
            Close();
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void SelectAuthorForm_Shown(object sender, EventArgs e)
        {
            AuthorsList = AuthorsList.XmlDeserialize(Parameters.AuthorsXMLFullFilename);

            if (AuthorsList != null)
            {
                AuthorsList.ToListBox(AuthorsLB);
            }

            SetControlsEnable();
        }
    }
}
