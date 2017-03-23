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
using Lib.MathMod.Grid.Partitioning;
using Lib.DataStruct;
using Lib.IO;

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
        private HistogramExt Hist = null;

        /// <summary>
        /// Log.
        /// </summary>
        private StringsList Log = null;

        /// <summary>
        /// Init components.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Create empty grid.
            Grid = new StructuredGrid();
            LoadPFGProps = null;
            Log = new StringsList();

            // Initial statistics.
            UpdateBriefGridStatistic();
        }

        /// <summary>
        /// Init histogram extended.
        /// </summary>
        /// <param name="partitions_count">count of partitions</param>
        private void InitHistogramExt(int partitions_count)
        {
            Hist = new HistogramExt(partitions_count);

            for (int i = 0; i < Grid.BlocksCount; i++)
            {
                Lib.MathMod.Grid.Block b = Grid.Blocks[i];
                Hist.W[b.PartitionNumber].Add(b.CellsCount);
            }

            Hist.FormV();
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
            Log.Add(last_action);
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

            ofd.Filter = "PFG (*.PFG, *.pfg)|*.pfg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File f = new File(ofd);

                if ((f.Ext == ".pfg") || (f.Ext == ".PFG"))
                {
                    File pfg = f;
                    File ibc = new File(pfg);
                    ibc.ChangeExtensionCaseSensitive(".ibc");
                    LoadPFGProps = new GridLoadSavePFGProperties();
                    LoadPFGProps.IsExtensionUppercase = pfg.IsUpperExt;

                    string last_action = "Grid " + pfg.Name + " (and *" + ibc.Ext + ") ";

                    if (GridLoaderSaverPFG.Load(Grid, pfg.Name, ibc.Name, GridLoadSaveIBlankMI.IsChecked))
                    {
                        UpdateLastAction(last_action + "is loaded.");

                        // If grid is loaded we should try to load REP border conditions.

                        File peri = new File(pfg);
                        peri.ChangeExtensionCaseSensitive(".peri");

                        if (GridLoaderSaverPFG.LoadPERI(Grid, peri.Name))
                        {
                            Log.Add(peri.Name + " is loaded.");
                        }
                    }
                    else
                    {
                        UpdateLastAction(last_action + "loading error.");
                        Grid = new StructuredGrid();
                    }

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
                File pfg = new File(sfd);
                File ibc = new File(pfg);
                ibc.ChangeExtensionCaseSensitive(".ibc");

                GridLoaderSaverPFG.Save(Grid, pfg.Name, ibc.Name, GridLoadSaveIBlankMI.IsChecked);
                UpdateLastAction("Grid " + pfg.Name + " (and *" + ibc.Ext + ") is saved.");

                // If there are border conditions links then save them.
                if (Grid.BCondsLinksCount > 0)
                {
                    File peri = new File(pfg);
                    peri.ChangeExtensionCaseSensitive(".peri");

                    if (GridLoaderSaverPFG.SavePERI(Grid, peri.Name))
                    {
                        Log.Add(peri.Name + " is saved.");
                    }
                }
            }
        }

        /// <summary>
        /// Export blocks distribution to *dis/*DIS file.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>

        private void GridExportBlocksDistribution_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = LoadPFGProps.IsExtensionUppercase ? "DIS (*.DIS)|*.DIS" : "DIS (*.dis)|*.dis";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GridLoaderSaverPFG.ExportBlocksDistribution(Grid, sfd.FileName);
                UpdateLastAction("Grid blocks distributions is exported to " + sfd.FileName);
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
        /// Call grid blocks distribution information.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void InfoBlocksDistrMI_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow w = new InfoWindow("Blocks distribution");
            w.AddBlocksDistribution(Grid);
            w.Show();
        }

        /// <summary>
        /// Start cut half max block.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CutHalfMaxBlockB_Click(object sender, RoutedEventArgs e)
        {
            int iters = Int32.Parse(CutHalfMaxBlockItersTB.Text);
            int i = 0;

            for (; i < iters; i++)
            {
                if (GridCutter.CutHalfMaxBlock(Grid) == null)
                {
                    break;
                }
            }

            UpdateBriefGridStatistic();
            string diag = (GridCutter.CutRejectedString == null)
                          ? "common termination"
                          : GridCutter.CutRejectedString;
            UpdateLastAction(String.Format("Cut: {0} blocks have been cutted ({1}).", i, diag));
        }

        /// <summary>
        /// Start greedy uniform blocks distribution with cut half max blocks.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GUBlocksDistrB_Click(object sender, RoutedEventArgs e)
        {
            int partitions = Int32.Parse(GUBlocksDistrPartitionsTB.Text);
            int iters = Int32.Parse(GUBlocksDistrItersTB.Text);
            double dev = Double.Parse(GUBlocksDistrDeviationTB.Text) / 100.0;
            int total_ites = 0;
            double cur_dev = 0.0;
            string diag = null;

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
            InitHistogramExt(partitions);
        }

        /// <summary>
        /// Minimal cuts count blocks distribution.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void MCCBlocksDistrB_Click(object sender, RoutedEventArgs e)
        {
            int partitions = Int32.Parse(MCCBlocksDistrPartitionsTB.Text);
            double min_cut = Double.Parse(MCCBlocksDistrMinCutTB.Text) / 100.0;

            // Partition.
            MinimalCutsPartitioner partitioner = new MinimalCutsPartitioner(Grid);
            int blocks_before = Grid.BlocksCount;
            partitioner.Partition(partitions, min_cut);
            int blocks_after = Grid.BlocksCount;

            // Upfdate information.
            UpdateBriefGridStatistic();
            InitHistogramExt(partitions);
            UpdateLastAction(String.Format("MCC distr: {0} cuts, {1}% deviation",
                             blocks_after - blocks_before, Hist.Dev));
        }

        /// <summary>
        /// Show information click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void InfoLogMI_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow w = new InfoWindow("Log");
            Log.FillListBox(w.LinesLB);
            w.ShowDialog();
        }

        /// <summary>
        /// Save picture click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ToolsSavePictireMI_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            string filename = null;

            sfd.Filter = "Pictures (*.png)|*.png";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = sfd.FileName;

                Transform transform = DrawAreaC.LayoutTransform;
                DrawAreaC.LayoutTransform = null;
                Size size = new Size(DrawAreaC.ActualWidth, DrawAreaC.ActualHeight);
                DrawAreaC.Measure(size);
                DrawAreaC.Arrange(new System.Windows.Rect(size));
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)size.Width,
                                                                   (int)size.Height,
                                                                   96d,
                                                                   96d,
                                                                   PixelFormats.Pbgra32);
                bitmap.Render(DrawAreaC);

                using (System.IO.FileStream out_stream = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();

                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(out_stream);
                }

                DrawAreaC.LayoutTransform = transform;
            }
        }

        /// <summary>
        /// Change margin value.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OptionsMarginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                GridCutter.MinMargin = Int32.Parse(OptionsMarginTB.Text);
            }
            catch (Exception)
            {
                GridCutter.MinMargin = 1;
            }
        }
    }
}
