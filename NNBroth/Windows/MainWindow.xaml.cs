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
using Lib.Utils;
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
        /// Cortex.
        /// </summary>
        Cortex Cortex = null;

        /// <summary>
        /// Batch.
        /// </summary>
        Batch Batch = null;

        /// <summary>
        /// Create form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="str">string</param>
        private void Log(string str)
        {
            LogLB.Items.Add(str);

            // Scroll down.
            LogLB.SelectedIndex = LogLB.Items.Count - 1;
            LogLB.ScrollIntoView(LogLB.SelectedItem);
        }

        /// <summary>
        /// Create cortex.
        /// </summary>
        private void CreateCortex()
        {
            string layers_sizes_str = LayersSizesLB.Text;
            string[] layers_sizes_strs = layers_sizes_str.Split(new Char[] { '[', ']', ' ', ',' });
            List<int> layers_sizes = new List<int>();

            for (int i = 0; i < layers_sizes_strs.Length; i++)
            {
                if (layers_sizes_strs[i].Trim() != "")
                {
                    layers_sizes.Add(Lib.Utils.Convert.GetInt(layers_sizes_strs[i]));
                }
            }

            int[] layers_sizes_array = layers_sizes.ToArray();

            Log("Создание нейросети : " + Arrays.ConvertToString(layers_sizes_array));
            Cortex.DefaultLinkWeight = Lib.Utils.Convert.GetDouble(DefaultLinkWeightLB.Text);
            Cortex.DefaultNeuronBias = Lib.Utils.Convert.GetDouble(DefaultNeuronBiasLB.Text);
            Cortex = Cortex.CreateMultilayerCortex(layers_sizes_array);
        }

        /// <summary>
        /// Create batch.
        /// </summary>
        private void CreateBatch()
        {
            Batch raw_batch = null;

            if (BatchNameLB.Text == "Xor")
            {
                raw_batch = new Xor();                
            }
            else if (BatchNameLB.Text == "MNIST")
            {
                raw_batch = new MNIST("../../Tests/mnist/train-images.idx3-ubyte",
                                      "../../Tests/mnist/train-labels.idx1-ubyte");
            }
            else
            {
                throw new Exception("unknown batch name");
            }

            if (BatchSizeLB.Text == "full")
            {
                Batch = raw_batch;
            }
            else
            {
                Batch = raw_batch.RandomMiniBatch(Lib.Utils.Convert.GetInt(BatchSizeLB.Text));
            }

            Log("Тестовый набор : " + Batch.Name);
        }

        /// <summary>
        /// Click on run bitton.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void RunB_Click(object sender, RoutedEventArgs e)
        {
            if (Cortex == null)
            {
                CreateCortex();
            }

            if (Batch == null)
            {
                CreateBatch();
            }

            int iters = Lib.Utils.Convert.GetInt(LearningIterstionsLB.Text);

            // Learning rate.
            Trainer.DefaultLearningRate = Lib.Utils.Convert.GetDouble(LearningRateLB.Text);

            // Learning cycle.
            for (int i = 0; i < iters; i++)
            {
                Trainer.Train(Cortex, Batch);
            }

            string report = String.Format("iters {0}, cost = {1}, right = {2}",
                                          iters,
                                          Batch.TotalCost(Cortex),
                                          Batch.RightAnswersPart(Cortex));
            Log("Прогон : " + report);
            Log("Коэфф  : " + Cortex.WeightsBiasesIntervalsStr());
        }

        /// <summary>
        /// Click on delete cortex button.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DelB_Click(object sender, RoutedEventArgs e)
        {
            Cortex = null;
            Batch = null;
        }
    }
}
