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

namespace GridMaster.Windows
{
    /// <summary>
    /// Interaction logic for CahngesWindow.xaml
    /// </summary>
    public partial class ChangesWindow : Window
    {
        public ChangesWindow()
        {
            InitializeComponent();

            ShowChanges();
        }

        /// <summary>
        /// Add string to lines.
        /// </summary>
        /// <param name="str">string</param>
        private void Ch(string str)
        {
            LinesLB.Items.Add(str);
        }

        /// <summary>
        /// Show changes.
        /// </summary>
        private void ShowChanges()
        {
            Ch("Changes:");
            Ch("2017-05-03: min: Information about functionality changes has been added.");
            Ch("            maj: The indentifiers of adjacent interfaces don't need to be the same.");
            Ch("2018-08-30: maj: New poolicy for PERI_C linked border conditions.");
            Ch("                 PERI_C linked border conditions is a pair of border conditions that are");
            Ch("                 of the same size and that are placed on the same block, opposite to each other.");
            Ch("2019-06-03: maj: Blocks deleting functionality is added.");
        }
    }
}
