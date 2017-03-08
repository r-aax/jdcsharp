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
        /// Properties of PFG load.
        /// </summary>
        private GridLoadSavePFGProperties LoadPFGProps;

        /// <summary>
        /// Init components.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Create empty grid.
            Grid = new StructuredGrid();
            LoadPFGProps = null;
        }

        /// <summary>
        /// Set brief grid statistic.
        /// </summary>
        /// <param name="stat">statistic string</param>
        public void UpdateBriefGridStatistic()
        {
            BriefGridStatisticTB.Text = Grid.ToString();
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

            ofd.Filter = "PFG (*.PFG, *.pfg)|*.pfg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofd.FileName;
                extension = System.IO.Path.GetExtension(filename);

                if ((extension == ".pfg") || (extension == ".PFG"))
                {
                    string extension_ibc;

                    LoadPFGProps = new GridLoadSavePFGProperties();

                    if (extension == ".pfg")
                    {
                        extension_ibc = ".ibc";
                        LoadPFGProps.IsExtensionUppercase = false;
                    }
                    else
                    {
                        extension_ibc = ".IBC";
                        LoadPFGProps.IsExtensionUppercase = true;
                    }

                    string filename_ibc = filename.Replace(extension, extension_ibc);

                    GridLoaderSaverPFG.Load(Grid, filename, filename_ibc,
                                            GridLoadSaveIBlankMI.IsChecked);
                    UpdateLastAction("Grid " + filename + " (and *" + extension_ibc + ") is loaded.");
                    UpdateBriefGridStatistic();
                }
                else
                {
                    System.Windows.MessageBox.Show("unknown grid extension");
                }
            }
        }

        /// <summary>
        /// Save grid.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void GridSaveMI_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = LoadPFGProps.IsExtensionUppercase ? "PFG (*.PFG)|*.PFG" : "PFG (*.pfg)|*.pfg";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = sfd.FileName;
                string extension = System.IO.Path.GetExtension(filename);
                string extension_ibc = LoadPFGProps.IsExtensionUppercase ? ".IBC" : ".ibc";
                string filename_ibc = filename.Replace(extension, extension_ibc);

                GridLoaderSaverPFG.Save(Grid, filename, filename_ibc,
                                        GridLoadSaveIBlankMI.IsChecked);
                UpdateLastAction("Grid " + filename + " (and *" + extension_ibc + ") is saved.");
            }
        }
    }
}
