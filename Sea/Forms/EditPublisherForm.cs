// Author: Alexey Rybakov

using System;
using System.Windows.Forms;

using Sea.Core.Publishers;

namespace Sea.Forms
{
    /// <summary>
    /// Edit publisher form.
    /// </summary>
    public partial class EditPublisherForm : Form
    {
        /// <summary>
        /// Publisher.
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAccepted;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditPublisherForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="label">label of form</param>
        public EditPublisherForm(string label)
            : this()
        {
            Text = label;
        }

        /// <summary>
        /// Show form event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditPublisherForm_Shown(object sender, EventArgs e)
        {
            if (Publisher != null)
            {
                NameTB.Text = Publisher.Name;
            }
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            Publisher = new Publisher(NameTB.Text);
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
            IsAccepted = false;
            Close();
        }
    }
}
