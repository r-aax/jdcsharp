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
using Lib.MathMod.Grid.Cut;
using Lib.Maths.Geometry;
using Lib.Draw.WPF;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using GridMaster.Tools;
using Lib.Maths;
using Lib.Utils;

namespace GridMaster.Windows
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
        /// Rect drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Logical area of painting.
        /// </summary>
        private static readonly Rect2D RelDrawRect = new Rect2D(100.0, 100.0);

        /// <summary>
        /// Histogram.
        /// </summary>
        private Histogram Hist = null;

        /// <summary>
        /// Init components.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Create empty grid.
            Grid = new StructuredGrid();
            LoadPFGProps = null;

            // Initial statistics.
            UpdateBriefGridStatistic();
        }

        /// <summary>
        /// Init histogram.
        /// </summary>
        /// <param name="partitions_count">count of partitions</param>
        private void InitHistogram(int partitions_count)
        {
            double[] partition_weights = Grid.PartitionsWeights(partitions_count);
            Hist = new Histogram(partition_weights.Length);
            Hist.V = partition_weights;
            Paint();
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
        /// Paint.
        /// </summary>
        private void Paint()
        {
            // If draw master is not created then create it.
            if (Drawer == null)
            {
                Drawer = new RectDrawer(RelDrawRect, DrawAreaC, false, true);
            }

            // Drawing.
            Drawer.BeginDraw();
            PaintInner();
            Drawer.EndDraw();
        }

        /// <summary>
        /// Inner paint.
        /// </summary>
        private void PaintInner()
        {
            if (Hist != null)
            {
                Hist.Draw(Drawer);
            }
        }

        /// <summary>
        /// Event when window is loaded.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Paint();
        }

        /// <summary>
        /// Change size.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Paint();
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

        /// <summary>
        /// Click on button cut single block.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        private void CutSingleBlockB_Click(object sender, RoutedEventArgs e)
        {
            int bid = Int32.Parse(CutSingleBlockBlockIdTB.Text);
            Dir d = Dir.Dirs[CutSingleBlockDirectionCB.SelectedIndex];
            int pos = Int32.Parse(CutSingleBlockPositionTB.Text);        
            Lib.MathMod.Grid.Block b = ((bid >= 0) && (bid < Grid.BlocksCount)) ? Grid.Blocks[bid] : null;

            GridCutter.MinMargin = 1;
            GridCutter.Cut(b, d, pos);

            // Update information.
            UpdateBriefGridStatistic();
            if (GridCutter.CutRejectedString == null)
            {
                UpdateLastAction(String.Format("Cut: block id {0}, direction {1}, position {2} is cutted.",
                                               bid, d, pos));
            }
            else
            {
                UpdateLastAction(String.Format("Cut: the cutting is impossible ({0})", GridCutter.CutRejectedString));
            }
        }

        /// <summary>
        /// Call grid description information.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void InfoGridMI_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow w = new InfoWindow("Grid description");
            w.AddGridInfo(Grid);
            w.Show();        
        }

        /// <summary>
        /// Start cut half max block.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CutHalfMaxBlockB_Click(object sender, RoutedEventArgs e)
        {
            int margin = Int32.Parse(CutHalfMaxBlockMarginTB.Text);
            int iters = Int32.Parse(CutHalfMaxBlockItersTB.Text);
            int min_margin_old = GridCutter.MinMargin;
            int i = 0;

            GridCutter.MinMargin = margin;
            for (; i < iters; i++)
            {
                if (GridCutter.CutHalfMaxBlock(Grid) == null)
                {
                    break;
                }
            }

            GridCutter.MinMargin = min_margin_old;

            UpdateBriefGridStatistic();
            string diag = (GridCutter.CutRejectedString == null)
                          ? "common termination"
                          : GridCutter.CutRejectedString;
            UpdateLastAction(String.Format("Cut: {0} blocks have been cutted ({1}).", i, diag));
        }

        /// <summary>
        /// Draw blocks distribution histogram.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DrawBlocksDistributionHistMI_Click(object sender, RoutedEventArgs e)
        {
            Hist = new Histogram(5);
            Hist.V[0] = 10.0;
            Hist.V[1] = 50.0;
            Hist.V[2] = 30.0;
            Hist.V[3] = 90.0;
            Hist.V[4] = 20.0;
            Paint();
        }

        /// <summary>
        /// Start greedy uniform blocks distribution with cut half max blocks.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GUBlocksDistrB_Click(object sender, RoutedEventArgs e)
        {
            int partitions = Int32.Parse(GUBlocksDistrPartitionsTB.Text);
            int margin = Int32.Parse(GUBlocksDistrMarginTB.Text);
            int iters = Int32.Parse(GUBlocksDistrItersTB.Text);
            double dev = Double.Parse(GUBlocksDistrDeviationTB.Text) / 100.0;
            int total_ites = 0;
            double cur_dev = 0.0;
            string diag = null;

            GridCutter.MinMargin = margin;

            double[] weights;
            double[] partitions_weights;
            int[] weights_to_partitions;

            do
            {
                // Distribution parameters.
                int bc = Grid.BlocksCount;
                weights = new double[bc];
                partitions_weights = new double[partitions];
                weights_to_partitions = new int[bc];

                // Init weights.
                for (int i = 0; i < bc; i++)
                {
                    weights[i] = Grid.Blocks[i].CellsCount;
                }

                // Distribute.
                WeightsDistribution.GreedyDistribution(weights, partitions,
                                                       partitions_weights, weights_to_partitions);
                cur_dev = Arrays.RelOverDeviationOfPositives(partitions_weights);

                // Check post conditions.
                if (cur_dev <= dev)
                {
                    diag = "deviation is reached";

                    break;
                }
                else if (total_ites >= iters)
                {
                    diag = "max iters count is reached";

                    break;
                }

                // Cut next.
                GridCutter.CutHalfMaxBlock(Grid);
                if (GridCutter.CutRejectedString == null)
                {
                    total_ites++;
                }
                else
                {
                    diag = GridCutter.CutRejectedString;

                    break;
                }
            }
            while (true);

            Grid.SetBlocksPartitionsNumbers(weights_to_partitions);
            UpdateBriefGridStatistic();
            UpdateLastAction(String.Format("GU distr: {0} iters, {1}% deviation ({2}).",
                                           total_ites, cur_dev * 100.0, diag));
            InitHistogram(partitions);
        }
    }
}
