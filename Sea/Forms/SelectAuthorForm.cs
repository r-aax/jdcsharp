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
        private AuthorsList Authors;

        /// <summary>
        /// Author.
        /// </summary>
        public Author Author = null;

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Set controls enabled.
        /// </summary>
        private void SetControlsEnable()
        {
            bool is_sel = AuthorsLB.SelectedIndex > -1;

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
            Author = Authors[AuthorsLB.SelectedIndex];
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
            Authors = AuthorsList.XmlDeserialize(Parameters.AuthorsXMLFullFilename);

            if (Authors != null)
            {
                Authors.ToListBox(AuthorsLB);
            }

            SetControlsEnable();
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
