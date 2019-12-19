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
        Batch Batch;

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
            Batch = null;
            Cortex = null;
        }

        /// <summary>
        /// Click Go button event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void GoB_Click(object sender, RoutedEventArgs e)
        {
            if (Batch == null)
            {
                Batch = new Xor();
            }

            if (Cortex == null)
            {
                Cortex = Cortex.CreateMultilayerCortex(new int[] { 2, 3, 2 });
            }

            int iters = Lib.GUI.WPF.IO.GetInt(ItersCountTB);

            for (int i = 0; i < iters; i++)
            {
                Trainer.Train(Cortex, Batch);
            }

            OutputLB.Items.Add(String.Format("Iter : cost = {0}, right = {1}",
                                             Batch.TotalCost(Cortex),
                                             Batch.RightAnswersPart(Cortex)));
            OutputLB.SelectedIndex = OutputLB.Items.Count - 1;
            OutputLB.ScrollIntoView(OutputLB.SelectedItem);
        }
    }
}
