using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lib.GUI.WPF
{
    /// <summary>
    /// Logic for EditIntWindow.xaml
    /// </summary>
    public partial class EditIntWindow : Window
    {
        /// <summary>
        /// Int.
        /// </summary>
        public int Result;

        /// <summary>
        /// Accepted flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ini">initialization value</param>
        /// <param name="label">description (form label)</param>
        public EditIntWindow(int ini, string label)
        {
            InitializeComponent();

            Result = ini;
            TextTB.Text = ini.ToString();
            Title = label;
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            Result = Lib.Utils.Convert.GetInt(TextTB.Text);
            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
