// Copyright Joy Developing.

using System.Windows;

using GraphMaster.Tools;
using Lib.GUI.WPF;

namespace GraphMaster.Windows
{
    /// <summary>
    /// AreaParametersWindow.xaml logic.
    /// </summary>
    public partial class AreaParametersWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AreaParametersWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loading.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AreaMoveCoefficientTB.Text = Parameters.AreaMoveCoefficient.ToString();
            AreaZoomCoefficientTB.Text = Parameters.AreaZoomCoefficient.ToString();
        }

        /// <summary>
        /// Accept changes.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            Parameters.AreaMoveCoefficient = IO.GetDouble(AreaMoveCoefficientTB);
            Parameters.AreaZoomCoefficient = IO.GetDouble(AreaZoomCoefficientTB);
            Close();
        }

        /// <summary>
        /// Cancel changes.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
