﻿// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sea.Core.Publishers;
using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Select publisher form.
    /// </summary>
    public partial class SelectPublisherForm : Form
    {
        /// <summary>
        /// Publishers.
        /// </summary>
        private PublishersList Publishers;

        /// <summary>
        /// Selected publisher.
        /// </summary>
        public Publisher Publisher = null;

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAcceped = false;

        /// <summary>
        /// Set controls enable.
        /// </summary>
        private void SetControlsEnable()
        {
            bool is_sel = PublishersLB.SelectedIndex > -1;

            AcceptB.Enabled = is_sel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectPublisherForm()
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
            IsAcceped = true;
            Publisher = Publishers[PublishersLB.SelectedIndex];
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
            IsAcceped = false;
            Close();
        }

        /// <summary>
        /// Show form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void SelectPublisherForm_Shown(object sender, EventArgs e)
        {
            Publishers = PublishersList.XmlDeserialize(Parameters.PublishersXMLFullFilename);

            if (Publishers != null)
            {
                Publishers.ToListBox(PublishersLB);
            }

            SetControlsEnable();
        }
    }
}
