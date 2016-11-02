// Author: Alexey Rybakov

using System;
using System.Windows.Forms;

using Sea.Tools;
using Sea.Core.Publishers;

namespace Sea.Forms
{
    /// <summary>
    /// Edit publishers form.
    /// </summary>
    public partial class EditPublishersForm : Form
    {
        /// <summary>
        /// Publishers.
        /// </summary>
        private PublishersList Publishers;

        /// <summary>
        /// Accept button flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Set controls enable.
        /// </summary>
        private void SetControlsEnable()
        {
            bool is_sel = PublishersLB.SelectedIndex > -1;

            EditB.Enabled = is_sel;
            DeleteB.Enabled = is_sel;
        }

        /// <summary>
        /// Constructor.
        /// <param name="publishers">publishers list</param>
        /// </summary>
        public EditPublishersForm(PublishersList publishers)
        {
            InitializeComponent();

            Publishers = publishers;
        }

        /// <summary>
        /// Show edit publishers form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditPublishersForm_Shown(object sender, EventArgs e)
        {
            if (Publishers.IsEmpty)
            {
                // Write that we create new publishers list.
                Text = "Create new publishers list (no publishers file is found)";
            }

            Publishers.Sort();
            Publishers.ToListBox(PublishersLB);
            SetControlsEnable();
        }

        /// <summary>
        /// New publisher.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewB_Click(object sender, EventArgs e)
        {
            EditPublisherForm form = new EditPublisherForm("Create new publisher");

            form.Publisher = null;
            form.ShowDialog();

            if (form.IsAccepted)
            {
                Publisher publisher = form.Publisher;

                if (publisher.Name == "")
                {
                    MessageBox.Show("Empty publisher name");

                    return;
                }

                Publishers.Add(publisher);
                Publishers.Sort();
                Publishers.ToListBox(PublishersLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Edit publishers.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditB_Click(object sender, EventArgs e)
        {
            int i = PublishersLB.SelectedIndex;

            if (i > -1)
            {
                EditPublisherForm form = new EditPublisherForm("Edit publisher");

                form.Publisher = Publishers[i];
                form.ShowDialog();

                if (form.IsAccepted)
                {
                    Publisher publisher = form.Publisher;

                    if (publisher.Name == "")
                    {
                        MessageBox.Show("Empty publisher name");

                        return;
                    }

                    Publishers[i] = publisher;
                    Publishers.Sort();
                    Publishers.ToListBox(PublishersLB);
                }
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Delete publishers.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DeleteB_Click(object sender, EventArgs e)
        {
            int i = PublishersLB.SelectedIndex;

            if (i > -1)
            {
                Publishers.RemoveAt(i);
                Publishers.ToListBox(PublishersLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// Accept button.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel button.
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
        private void PublishersLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnable();
        }
    }
}
