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
using Lib.DataStruct;
using Lib.GUI.WindowsForms;
using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Edit publishers form.
    /// </summary>
    public partial class EditPublishersForm : Form
    {
        /// <summary>
        /// List of publishers.
        /// </summary>
        private StringsList PublishersList;

        private void SetControlsEnable()
        {
            bool is_sel = PublishersLB.SelectedIndex > -1;

            EditB.Enabled = is_sel;
            DeleteB.Enabled = is_sel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditPublishersForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show edit publishers form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditPublishersForm_Shown(object sender, EventArgs e)
        {
            PublishersList = StringsList.XmlDeserialize(Parameters.PublishersXMLFullFilename);

            if (PublishersList == null)
            {
                // Write that we create new publishers list.
                Text = "Create new publishers list (no publishers file is found)";

                PublishersList = new StringsList();
            }
            else
            {
                PublishersList.FillListBox(PublishersLB);
            }

            SetControlsEnable();
        }

        /// <summary>
        /// New publisher.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewB_Click(object sender, EventArgs e)
        {
            EditStringForm form = new EditStringForm("", "Create new publisher");

            form.ShowDialog();

            if (form.IsAccepted)
            {
                if (form.Result == "")
                {
                    MessageBox.Show("Empty publisher name");

                    return;
                }

                PublishersList.Add(form.Result);
                PublishersList.Items.Sort();
                PublishersList.FillListBox(PublishersLB);
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
                String publisher = PublishersList[i];

                EditStringForm form = new EditStringForm(publisher, "Edit publisher");

                form.ShowDialog();

                if (form.Result == "")
                {
                    MessageBox.Show("Empty publisher name");

                    return;
                }

                PublishersList[i] = form.Result;
                PublishersList.Items.Sort();
                PublishersList.FillListBox(PublishersLB);
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
                PublishersList.Items.RemoveAt(i);
                PublishersList.FillListBox(PublishersLB);
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
            PublishersList.XmlSerialize(Parameters.PublishersXMLFullFilename);
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
