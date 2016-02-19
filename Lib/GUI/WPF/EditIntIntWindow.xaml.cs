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
    /// Logic for EditIntIntWindow.xaml
    /// </summary>
    public partial class EditIntIntWindow : Window
    {
        /// <summary>
        /// Int1 value.
        /// </summary>
        public int Int1V;

        /// <summary>
        /// Int2 value.
        /// </summary>
        public int Int2V;

        /// <summary>
        /// Accepted flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="int1_v">int1_v</param>
        /// <param name="int2_v">int2_v</param>
        /// <param name="window_label">label for window</param>
        /// <param name="int1_label">label for int1 value</param>
        /// <param name="int2_label">label for int2 value</param>
        public EditIntIntWindow(int int1_v, int int2_v,
                                string window_label, string int1_label, string int2_label)
        {
            InitializeComponent();

            Int1V = int1_v;
            Int2V = int2_v;
            Int1TB.Text = Int1V.ToString();
            Int2TB.Text = Int2V.ToString();

            Title = window_label;
            Int1L.Content = int1_label;
            Int2L.Content = int2_label;
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            Int1V = Lib.Utils.Convert.GetInt(Int1TB.Text);
            Int2V = Lib.Utils.Convert.GetInt(Int2TB.Text);
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
