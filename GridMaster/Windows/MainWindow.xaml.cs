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
using System.Windows.Forms;

using Lib.MathMod.Grid;
using Lib.MathMod.Grid.Load;

namespace GridMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Structured grid.
        /// </summary>
        private StructuredGrid Grid;

        /// <summary>
        /// Init components.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Create empty grid.
            Grid = new StructuredGrid();
        }

        /// <summary>
        /// Set brief grid statistic.
        /// </summary>
        /// <param name="stat">statistic string</param>
        public void UpdateBriefGridStatistic()
        {
            BriefGridStatisticTB.Text = Grid.BriefStatistic();
        }

        /// <summary>
        /// Set last action.
        /// </summary>
        /// <param name="last_action">last action string</param>
        public void UpdateLastAction(string last_action)
        {
            LastActionTB.Text = last_action;
        }
        
        /// <summary>
        /// Load grid.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void GridLoadMI_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string filename, extension;

            ofd.Filter = "PFG (*.pfg)|*.pfg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofd.FileName;
                extension = System.IO.Path.GetExtension(filename);

                if ((extension == ".pfg") || (extension == ".PFG"))
                {
                    string extension_ibc = ((extension == ".pfg") ? ".ibc" : ".IBC");
                    string filename_ibc = filename.Replace(extension, extension_ibc);

                    GridLoaderPFG.Load(Grid, filename, filename_ibc, false);
                    UpdateLastAction("Grid " + filename + " is loaded.");
                    UpdateBriefGridStatistic();
                }
                else
                {
                    System.Windows.MessageBox.Show("unknown grid extension");
                }
            }
        }
    }
}
