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
    /// Логика взаимодействия для OrderAndChordsWindow.xaml
    /// </summary>
    public partial class OrderAndChordsWindow : Window
    {
        /// <summary>
        /// Order
        /// </summary>
        public int Order;

        /// <summary>
        /// Chords
        /// </summary>
        public int[] Chords;

        /// <summary>
        /// Accepted flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="order">order</param>
        /// <param name="chords">chords</param>
        public OrderAndChordsWindow(int order, int[] chords)
        {
            InitializeComponent();

            Order = order;
            Chords = chords.Clone() as int[];
            OrderTB.Text = Order.ToString();
            ChordsTB.Text = "";

            for (int i = 0; i < Chords.Length; i++)
            {
                if (ChordsTB.Text == "")
                {
                    ChordsTB.Text = Chords[0].ToString();
                }
                else
                {
                    ChordsTB.Text = ChordsTB.Text + " " + Chords[i].ToString();
                }
            }
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            Order = Lib.Utils.Convert.GetInt(OrderTB.Text);
            string[] chords = ChordsTB.Text.Split();
            Chords = new int[chords.Length];

            for (int i = 0; i < chords.Length; i++)
            {
                Chords[i] = Lib.Utils.Convert.GetInt(chords[i]);
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
