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
using System.Windows.Shapes;

namespace GraphMaster.Windows
{
    /// <summary>
    /// Logic for Grid 2D sizes Grid2DSizesWindow.xaml
    /// </summary>
    public partial class Grid2DSizesWindow : Window
    {
        /// <summary>
        /// X size.
        /// </summary>
        public int XSize;

        /// <summary>
        /// Y size.
        /// </summary>
        public int YSize;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x_size">X size</param>
        /// <param name="y_size">Y size</param>
        public Grid2DSizesWindow(int x_size, int y_size)
        {
            InitializeComponent();

            XSize = x_size;
            YSize = y_size;
            XSizeTB.Text = XSize.ToString();
            YSizeTB.Text = YSize.ToString();
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            XSize = Lib.Utils.Convert.GetInt(XSizeTB.Text);
            YSize = Lib.Utils.Convert.GetInt(YSizeTB.Text);
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
