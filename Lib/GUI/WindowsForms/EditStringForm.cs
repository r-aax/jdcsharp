// Author: Alexey Rybakov

using System;
using System.Windows.Forms;

namespace Lib.GUI.WindowsForms
{
    /// <summary>
    /// Input string form.
    /// </summary>
    public partial class EditStringForm : Form
    {
        /// <summary>
        /// Result.
        /// </summary>
        public string Result;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditStringForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with additional parameters.
        /// </summary>
        /// <param name="initial_string">initial string</param>
        /// <param name="label">form label</param>
        public EditStringForm(string initial_string, string label)
            : this()
        {
            TextTB.Text = initial_string;
            Text = label;
            Result = initial_string;
        }

        /// <summary>
        /// Accept string.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            Result = TextTB.Text;
            Close();
        }

        /// <summary>
        /// Cancel string input.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            // Initial string is result.
            Close();
        }

        /// <summary>
        /// Shown form event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void InputStringForm_Shown(object sender, EventArgs e)
        {
            TextTB.Focus();
        }
    }
}
