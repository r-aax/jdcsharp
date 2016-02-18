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
    /// Logic for OrderAndIntsWindow.xaml
    /// </summary>
    public partial class OrderAndIntsWindow : Window
    {
        /// <summary>
        /// Order
        /// </summary>
        public int Order;

        /// <summary>
        /// Ints
        /// </summary>
        public int[] Ints;

        /// <summary>
        /// Accepted flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Init order and ints.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ints"></param>
        private void InitOrderAndInts(int order, int[] ints)
        {
            Order = order;
            Ints = ints.Clone() as int[];
            OrderTB.Text = Order.ToString();
            IntsTB.Text = "";

            for (int i = 0; i < Ints.Length; i++)
            {
                if (IntsTB.Text == "")
                {
                    IntsTB.Text = Ints[0].ToString();
                }
                else
                {
                    IntsTB.Text = IntsTB.Text + " " + Ints[i].ToString();
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="order">order</param>
        /// <param name="ints">ints list</param>
        public OrderAndIntsWindow(int order, int[] ints)
        {
            InitializeComponent();
            InitOrderAndInts(order, ints);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="order">order</param>
        /// <param name="ints">ints list</param>
        /// <param name="window_label">label for window</param>
        /// <param name="ints_label">label for ints textbox</param>
        public OrderAndIntsWindow(int order, int[] ints, string window_label, string ints_label)
        {
            InitializeComponent();
            InitOrderAndInts(order, ints);
            Title = window_label;
            IntsLabelL.Content = ints_label;
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            Order = Lib.Utils.Convert.GetInt(OrderTB.Text);
            string[] ints = IntsTB.Text.Split();
            Ints = new int[ints.Length];

            for (int i = 0; i < ints.Length; i++)
            {
                Ints[i] = Lib.Utils.Convert.GetInt(ints[i]);
            }
            
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
