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

namespace Sea.Forms
{
    /// <summary>
    /// Edit author form.
    /// </summary>
    public partial class EditAuthorForm : Form
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName;

        /// <summary>
        /// Second name.
        /// </summary>
        public string SecondName;

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName;

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAccepted = true;

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
        /// <param name="ini_first_name">initial first name</param>
        /// <param name="ini_second_name">initial second name</param>
        /// <param name="ini_last_name">initial last name</param>
        /// <param name="label">label of form</param>
        public EditAuthorForm(string ini_first_name, string ini_second_name, string ini_last_name,
                              string label)
            : this()
        {
            FirstNameTB.Text = ini_first_name;
            SecondNameTB.Text = ini_second_name;
            LastNameTB.Text = ini_last_name;
            Text = label;
            FirstName = ini_first_name;
            SecondName = ini_second_name;
            LastName = ini_last_name;
        }

        /// <summary>
        /// Accept edit.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            FirstName = FirstNameTB.Text;
            SecondName = SecondNameTB.Text;
            LastName = LastNameTB.Text;
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
