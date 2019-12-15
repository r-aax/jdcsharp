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
        /// Create form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click Go button event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void GoB_Click(object sender, RoutedEventArgs e)
        {
            MNIST mnist = new MNIST("../../Tests/mnist/train-images.idx3-ubyte",
                                    "../../Tests/mnist/train-labels.idx1-ubyte");
            
            Creature creature = new Creature(mnist.InDimension, mnist.OutDimension);
            double[] in_data = mnist.GetTestCase(10);
            double[] out_data = creature.Sense(in_data);
            double[] right_out_data = mnist.GetTestCaseAnswer(10);

            OutputLB.Items.Add("HI");
            OutputLB.Items.Add(String.Format("      out_data : {0}", ToStringConverter.Convert(out_data)));
            OutputLB.Items.Add(String.Format("right_out_data : {0}", ToStringConverter.Convert(right_out_data)));
        }
    }
}
