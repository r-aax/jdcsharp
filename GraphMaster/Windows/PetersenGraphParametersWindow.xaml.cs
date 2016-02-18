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
    /// Логика взаимодействия для PetersenGraphParametersWindow.xaml
    /// </summary>
    public partial class PetersenGraphParametersWindow : Window
    {
        /// <summary>
        /// Half order
        /// </summary>
        public int HalfOrder;

        /// <summary>
        /// Inner chord
        /// </summary>
        public int InnerChord;

        /// <summary>
        /// Accepted flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="half_order">half order</param>
        /// <param name="inner_chord">inner chord</param>
        public PetersenGraphParametersWindow(int half_order, int inner_chord)
        {
            InitializeComponent();

            HalfOrder = half_order;
            InnerChord = inner_chord;
            HalfOrderTB.Text = HalfOrder.ToString();
            InnerChordTB.Text = InnerChord.ToString();
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            HalfOrder = Lib.Utils.Convert.GetInt(HalfOrderTB.Text);
            InnerChord = Lib.Utils.Convert.GetInt(InnerChordTB.Text);
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
