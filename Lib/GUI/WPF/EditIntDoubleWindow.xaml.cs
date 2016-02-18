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
    /// Logic for EditIntDoubleWindow.xaml
    /// </summary>
    public partial class EditIntDoubleWindow : Window
    {
        /// <summary>
        /// Int value.
        /// </summary>
        public int IntV;

        /// <summary>
        /// Double value.
        /// </summary>
        public double DoubleV;

        /// <summary>
        /// Accepted flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="int_v">int_v</param>
        /// <param name="double_v">double_v</param>
        /// <param name="window_label">label for window</param>
        /// <param name="int_label">label for int value</param>
        /// <param name="double_label">label for double value</param>
        public EditIntDoubleWindow(int int_v, double double_v,
                                   string window_label, string int_label, string double_label)
        {
            InitializeComponent();

            IntV = int_v;
            DoubleV = double_v;
            IntTB.Text = IntV.ToString();
            DoubleTB.Text = Lib.Utils.Convert.GetString(DoubleV);

            Title = window_label;
            IntL.Content = int_label;
            DoubleL.Content = double_label;
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            IntV = Lib.Utils.Convert.GetInt(IntTB.Text);
            DoubleV = Lib.Utils.Convert.GetDouble(DoubleTB.Text);
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
