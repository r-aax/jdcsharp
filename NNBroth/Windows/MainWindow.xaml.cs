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

using Lib.IO;
using Lib.GUI;
using Lib.Utils.Time;

using NNBroth.Evolution;
using NNBroth.Tests;

namespace NNBroth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Test.
        /// </summary>
        Test Test;

        /// <summary>
        /// Cortex.
        /// </summary>
        Cortex Cortex;

        /// <summary>
        /// Create form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Test = null;
            Cortex = null;
        }

        /// <summary>
        /// Click Go button event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void GoB_Click(object sender, RoutedEventArgs e)
        {
            if (Test == null)
            {
                Test = new Xor();
            }

            if (Cortex == null)
            {
                Cortex = Cortex.CreateMultilayerCortex(new int[] { 2, 3, 2 });
            }

            DateTime beg = DateTime.Now;
            DateTime end = DateTime.Now;
            TimeSpan cur = end.Subtract(beg);

        }
    }
}
