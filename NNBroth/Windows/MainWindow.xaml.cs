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
        DoublesToInt Test;

        /// <summary>
        /// Generation.
        /// </summary>
        Generation Generation;

        /// <summary>
        /// Create form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Test = null;
            Generation = null;
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

            if (Generation == null)
            {
                Generation = new Generation();
                Generation.AddDefaultCreatures(10, Test.InputDimension, Test.OutputDimension);
            }

            DateTime beg = DateTime.Now;
            for (int i = 0; i < Lib.GUI.WPF.IO.GetInt(ItersCountTB); i++)
            {
                Generation.Selection(Test);
            }
            DateTime end = DateTime.Now;
            TimeSpan cur = end.Subtract(beg);

            OutputLB.Items.Add(Generation.ToString() + String.Format(" : time = {0}", cur));
        }
    }
}
