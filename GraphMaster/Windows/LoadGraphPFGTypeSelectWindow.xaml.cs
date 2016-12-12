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
    /// Interaction logic for LoadGraphPFGTypeSelectWindow.xaml
    /// </summary>
    public partial class LoadGraphPFGTypeSelectWindow : Window
    {
        /// <summary>
        /// Accept.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Load graph PFG type select window.
        /// </summary>
        public LoadGraphPFGTypeSelectWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Skeleton load mode.
        /// </summary>
        public bool IsSkeleton
        {
            get
            {
                return (bool)SkeletonRB.IsChecked;
            }
        }

        /// <summary>
        /// Blocks adjacency mode.
        /// </summary>
        public bool IsBlocksAdjacency
        {
            get
            {
                return (bool)BlocksAdjacencyRB.IsChecked;
            }
        }
    }
}
