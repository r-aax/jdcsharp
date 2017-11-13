﻿using System;
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

using GridMaster.Tools;

namespace GridMaster.Windows
{
    /// <summary>
    /// Interaction logic for AdditionalParametersWindow.xaml
    /// </summary>
    public partial class AdditionalParametersWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AdditionalParametersWindow()
        {
            InitializeComponent();
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
        /// OK button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OK_B_Click(object sender, RoutedEventArgs e)
        {
            AdditionalParameters.EpsForBCondsMatchParallelMove = Double.Parse(EpsBCondsMatchParallelMove.Text);
            AdditionalParameters.EpsForBCondsMatchRotation = Double.Parse(EpsBCondsMatchRotation.Text);
            Close();
        }
    }
}
