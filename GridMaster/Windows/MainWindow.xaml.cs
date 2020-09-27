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
using Lib.Maths;
using Lib.Utils;
using Lib.MathMod.Grid.Partitioning;
using Lib.DataStruct;
using Lib.IO;
using Lib.MathMod.Grid.DescartesObjects;
using Lib.MathMod.Grid.Delete;
using System.Diagnostics;

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
        /// Last cuts count.
        /// </summary>
        private int LastCuts = 0;

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
            Hist = new HistogramExt(partitions_count, Grid);
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
                Hist.Cells = Grid.CellsCount();
                Hist.IfaceCells = Grid.IfaceCellsCountMultiple();
                Hist.CrossCells = Grid.IfaceCellsCountCrossMultiple();
                Hist.Cuts = LastCuts;
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
                    GridLoadSavePFGProperties.IsExtensionUppercase = pfg.IsUpperExt;

                    string last_action = "Grid " + pfg.Name + " (and *" + ibc.Ext + ") ";

                    if (GridLoaderSaverPFG.Load(Grid, pfg.Name, ibc.Name,
                                                GridLoadSavePFGProperties.EpsPointsEqCheck,
                                                GridLoadSavePFGProperties.EpsForBCondsMatchParallelMove,
                                                GridLoadSavePFGProperties.EpsForBCondsMatchRotation))
                    {
                        UpdateLastAction(last_action + "is loaded.");
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
            sfd.Filter = GridLoadSavePFGProperties.IsExtensionUppercase
                         ? "PFG (*.PFG)|*.PFG"
                         : "PFG (*.pfg)|*.pfg";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File pfg = new File(sfd);
                File ibc = new File(pfg);
                ibc.ChangeExtensionCaseSensitive(".ibc");

                GridLoaderSaverPFG.Save(Grid, pfg.Name, ibc.Name);
                UpdateLastAction("Grid " + pfg.Name + " (and *" + ibc.Ext + ") is saved.");

                // No PERI file used now.
                // If there are border conditions links then save them.
                /*
                if (Grid.BCondsLinksCount > 0)
                {
                    File peri = new File(pfg);
                    peri.ChangeExtensionCaseSensitive(".peri");
                
                    if (GridLoaderSaverPFG.SavePERI(Grid, peri.Name))
                    {
                        Log.Add(peri.Name + " is saved.");
                    }
                }
                */
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
            sfd.Filter = GridLoadSavePFGProperties.IsExtensionUppercase
                         ? "DIS (*.DIS)|*.DIS" 
                         : "DIS (*.dis)|*.dis";

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

            // Partition.
            GreedyUniformPartitioner partitioner = new GreedyUniformPartitioner(Grid);
            int blocks_before = Grid.BlocksCount;
            string diag = partitioner.Partition(partitions, iters, dev);
            int blocks_after = Grid.BlocksCount;
            int cuts = blocks_after - blocks_before;

            // Update information.
            UpdateBriefGridStatistic();
            LastCuts = cuts;
            InitHistogramExt(partitions);
            UpdateLastAction(String.Format("GU distr: {0} cuts, {1}% deviation ({2}).",
                                           cuts, Hist.Dev, diag));
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
            int cuts = blocks_after - blocks_before;

            // Upfdate information.
            UpdateBriefGridStatistic();
            LastCuts = cuts;
            InitHistogramExt(partitions);
            UpdateLastAction(String.Format("MCC distr: {0} cuts, {1}% deviation", cuts, Hist.Dev));
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

        /// <summary>
        /// Check iblank data using.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GridLoadSaveIBlankMI_Checked(object sender, RoutedEventArgs e)
        {
            GridLoadSavePFGProperties.IsIBlank = true;
        }

        /// <summary>
        /// Uncheck iblank data using.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GridLoadSaveIBlankMI_Unchecked(object sender, RoutedEventArgs e)
        {
            GridLoadSavePFGProperties.IsIBlank = false;
        }

        /// <summary>
        /// Check use border conditions links property.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GridUseBCondsLinksMI_Checked(object sender, RoutedEventArgs e)
        {
            GridProperties.IsBcondsLinks = true;
        }

        /// <summary>
        /// Uncheck use border conditions links property.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GridUseBCondsLinksMI_Unchecked(object sender, RoutedEventArgs e)
        {
            GridProperties.IsBcondsLinks = false;
        }

        /// <summary>
        /// Additional parameters window open.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AddittionalParametersMI_Click(object sender, RoutedEventArgs e)
        {
            (new AdditionalParametersWindow()).ShowDialog();
        }

        /// <summary>
        /// Changes window open.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ChangesMI_Click(object sender, RoutedEventArgs e)
        {
            (new ChangesWindow()).ShowDialog();
        }

        /// <summary>
        /// Test delete block with 0 id.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestsDelete0BlockMI_Click(object sender, RoutedEventArgs e)
        {
            GridCleaner gc = new GridCleaner(Grid);

            gc.DeleteBlock(Grid.Blocks[0]);

            UpdateBriefGridStatistic();
        }

        /// <summary>
        /// Test leave only blocks with given border conditions names.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestsLeaveOnlyNeededBCondBlocksMI_Click(object sender, RoutedEventArgs e)
        {
            GridCleaner gc = new GridCleaner(Grid);

            // Use Partition field for help.
            foreach (Lib.MathMod.Grid.Block b in Grid.Blocks)
            {
                b.PartitionNumber = -1;
            }

            // Mark blocks to be deleted.
            foreach (BCond bc in Grid.BConds)
            {
                if ((bc.Label.Name == "W") || (bc.Label.Name == "C1"))
                {
                    bc.B.PartitionNumber = 0;
                }
            }

            // Delete bad blocks.
            List<Lib.MathMod.Grid.Block> bs = Grid.Blocks.FindAll(b => (b.PartitionNumber != 0));
            foreach (Lib.MathMod.Grid.Block b in bs)
            {
                gc.DeleteBlock(b);
            }

            // Clean partition number.
            foreach (Lib.MathMod.Grid.Block b in Grid.Blocks)
            {
                b.PartitionNumber = -1;
            }

            UpdateBriefGridStatistic();
        }

        /// <summary>
        /// Delete all objects but leave blocks.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestsDeleteAllButBlocksMI_Click(object sender, RoutedEventArgs e)
        {
            Grid.IfacesPairs.Clear();

            // Delete extra border conditions.
            Grid.BConds.RemoveAll(bc => (bc.Label.Name != "W") && (bc.Label.Name != "C1"));

            Grid.BCondsLinks.Clear();
            Grid.Scopes.Clear();
            UpdateBriefGridStatistic();
        }

        /// <summary>
        /// Cut blocks near faces.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestsCutNearFacesMI_Click(object sender, RoutedEventArgs e)
        {
            TestLB.Items.Clear();
            TestLB.Items.Add("TestsCutNearFacesMI_Click:");

            foreach (Lib.MathMod.Grid.Block b in Grid.Blocks)
            {
                b.PartitionNumber = 0;
            }

            foreach (BCond bc in Grid.BConds)
            {
                bc.B.PartitionNumber++;
            }

            foreach (Lib.MathMod.Grid.Block b in Grid.Blocks)
            {
                Debug.Assert(b.PartitionNumber == 1);
            }

            foreach (BCond bc in Grid.BConds)
            {
                if (bc.D.IsNeg)
                {
                    GridCutter.Cut(bc.B, !bc.D, 1);
                }
                else
                {
                    GridCutter.Cut(bc.B, bc.D, bc.B.Canvas.Size(bc.D) - 1);
                }
            }

            UpdateBriefGridStatistic();
        }

        /// <summary>
        /// Export poitns.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestsExportPoints_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = sfd.FileName;

                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename))
                    {
                        foreach (BCond bc in Grid.BConds)
                        {
                            Lib.MathMod.Grid.Block b = bc.B;
                            Dir d = bc.D;

                            switch (d.N)
                            {
                                case Dir.I0N:
                                    sw.WriteLine(String.Format("PEACE_OF_SURFACE: {0}, {1}", b.Canvas.JNodes, b.Canvas.KNodes));
                                    for (int j = 0; j < b.Canvas.JNodes; j++)
                                    {
                                        for (int k = 0; k < b.Canvas.KNodes; k++)
                                        {
                                            sw.WriteLine(String.Format("({0}, {1}, {2}),", b.C[0, j, k, 0], b.C[0, j, k, 1], b.C[0, j, k, 2]));
                                        }
                                    }
                                    break;
                                
                                case Dir.I1N:
                                    sw.WriteLine(String.Format("PEACE_OF_SURFACE: {0}, {1}", b.Canvas.JNodes, b.Canvas.KNodes));
                                    for (int j = b.Canvas.JNodes - 1; j >= 0; j--)
                                    {
                                        for (int k = 0; k < b.Canvas.KNodes; k++)
                                        {
                                            sw.WriteLine(String.Format("({0}, {1}, {2}),", b.C[1, j, k, 0], b.C[1, j, k, 1], b.C[1, j, k, 2]));
                                        }
                                    }
                                    break;

                                case Dir.J0N:
                                    sw.WriteLine(String.Format("PEACE_OF_SURFACE: {0}, {1}", b.Canvas.INodes, b.Canvas.KNodes));
                                    for (int i = b.Canvas.INodes - 1; i >= 0; i--)
                                    {
                                        for (int k = 0; k < b.Canvas.KNodes; k++)
                                        {
                                            sw.WriteLine(String.Format("({0}, {1}, {2}),", b.C[i, 0, k, 0], b.C[i, 0, k, 1], b.C[i, 0, k, 2]));
                                        }
                                    }
                                    break;

                                case Dir.J1N:
                                    sw.WriteLine(String.Format("PEACE_OF_SURFACE: {0}, {1}", b.Canvas.INodes, b.Canvas.KNodes));
                                    for (int i = 0; i < b.Canvas.INodes; i++)
                                    {
                                        for (int k = 0; k < b.Canvas.KNodes; k++)
                                        {
                                            sw.WriteLine(String.Format("({0}, {1}, {2}),", b.C[i, 1, k, 0], b.C[i, 1, k, 1], b.C[i, 1, k, 2]));
                                        }
                                    }
                                    break;

                                case Dir.K0N:
                                    sw.WriteLine(String.Format("PEACE_OF_SURFACE: {0}, {1}", b.Canvas.INodes, b.Canvas.JNodes));
                                    for (int i = 0; i < b.Canvas.INodes; i++)
                                    {
                                        for (int j = 0; j < b.Canvas.JNodes; j++)
                                        {
                                            sw.WriteLine(String.Format("({0}, {1}, {2}),", b.C[i, j, 0, 0], b.C[i, j, 0, 1], b.C[i, j, 0, 2]));
                                        }
                                    }
                                    break;
                                
                                case Dir.K1N:
                                    sw.WriteLine(String.Format("PEACE_OF_SURFACE: {0}, {1}", b.Canvas.INodes, b.Canvas.JNodes));
                                    for (int i = b.Canvas.INodes - 1; i >= 0; i--)
                                    {
                                        for (int j = 0; j < b.Canvas.JNodes; j++)
                                        {
                                            sw.WriteLine(String.Format("({0}, {1}, {2}),", b.C[i, j, 1, 0], b.C[i, j, 1, 1], b.C[i, j, 1, 2]));
                                        }
                                    }
                                    break;
                                

                                default:
                                    //throw new Exception("wrong direction");
                                    break;
                            }

                            sw.WriteLine("");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ExeDebug.ReportError(ex.Message));
                }
            }
        }
    }
}
