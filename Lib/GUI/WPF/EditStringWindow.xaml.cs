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
    /// Logic for EditStringWindow.xaml
    /// </summary>
    public partial class EditStringWindow : Window
    {
        /// <summary>
        /// String.
        /// </summary>
        public string Result;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditStringWindow(string ini, string label)
        {
            InitializeComponent();
            Result = ini;
            TextTB.Text = ini;
            Title = label;
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            Result = TextTB.Text;
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
