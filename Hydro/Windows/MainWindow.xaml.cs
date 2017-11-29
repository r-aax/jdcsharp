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
using System.Diagnostics;
using System.IO;

using Lib.Maths.Geometry;
using Lib.MathMod.SolidGrid;
using Lib.Draw;
using Lib.MathMod;
using Lib.MathMod.Solver;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using RectDrawerWPF = Lib.Draw.WPF.RectDrawer;
using SWRect = System.Windows.Rect;

namespace Hydro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Grid.
        /// </summary>
        private SolidGrid Grid = null;

        /// <summary>
        /// Time.
        /// </summary>
        private double T = 0.0;

        /// <summary>
        /// Time ellapsed.
        /// </summary>
        private double TimeElapsed = 0.0;

        /// <summary>
        /// Rect drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Second drawer.
        /// </summary>
        private RectDrawer Drawer2 = null;

        /// <summary>
        /// Hydro rect drawer.
        /// </summary>
        private HydroRectDrawer HydroDrawer = null;

        /// <summary>
        /// Drawer for graphic.
        /// </summary>
        private HydroGraphicsRectDrawer GraphicDrawer = null;

        /// <summary>
        /// Interval of rho values.
        /// </summary>
        private IntervalD Graphic_rho_Interval
        {
            get
            {
                return new IntervalD(Double.Parse(Graphic_rho_L_TB.Text),
                                    Double.Parse(Graphic_rho_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of v.X values.
        /// </summary>
        private IntervalD Graphic_vX_Interval
        {
            get
            {
                return new IntervalD(Double.Parse(Graphic_vX_L_TB.Text),
                                    Double.Parse(Graphic_vX_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of v.Y values.
        /// </summary>
        private IntervalD Graphic_vY_Interval
        {
            get
            {
                return new IntervalD(Double.Parse(Graphic_vY_L_TB.Text),
                                    Double.Parse(Graphic_vY_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of v.Z values.
        /// </summary>
        private IntervalD Graphic_vZ_Interval
        {
            get
            {
                return new IntervalD(Double.Parse(Graphic_vZ_L_TB.Text),
                                    Double.Parse(Graphic_vZ_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of eps values.
        /// </summary>
        private IntervalD Graphic_eps_Interval
        {
            get
            {
                return new IntervalD(Double.Parse(Graphic_eps_L_TB.Text),
                                    Double.Parse(Graphic_eps_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of p values.
        /// </summary>
        private IntervalD Graphic_p_Interval
        {
            get
            {
                return new IntervalD(Double.Parse(Graphic_p_L_TB.Text),
                                    Double.Parse(Graphic_p_H_TB.Text));
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Colors of labels.
            IsGraphic_rho_Used.Foreground = new SolidColorBrush(HydroGraphicsRectDrawer.SColor_rho);
            IsGraphic_vX_Used.Foreground = new SolidColorBrush(HydroGraphicsRectDrawer.SColor_vx);
            IsGraphic_vY_Used.Foreground = new SolidColorBrush(HydroGraphicsRectDrawer.SColor_vy);
            IsGraphic_vZ_Used.Foreground = new SolidColorBrush(HydroGraphicsRectDrawer.SColor_vz);
            IsGraphic_eps_Used.Foreground = new SolidColorBrush(HydroGraphicsRectDrawer.SColor_eps);
            IsGraphic_p_Used.Foreground = new SolidColorBrush(HydroGraphicsRectDrawer.SColor_p);
        }

        /// <summary>
        /// Set appropriate time step.
        /// </summary>
        /// <param name="tb">text box</param>
        private void SetAppropriateDt(TextBox tb)
        {
            double dt = 1.0;

            while (Grid.MaxCourantXYZ(dt) > 1.0)
            {
                dt /= 10.0;
            }

            tb.Text = dt.ToString();
        }

        /// <summary>
        /// Check if we run with given iterations count.
        /// </summary>
        /// <returns><c>true</c> - if we run with given iterations count, <c>false</c> - otherwise</returns>
        public bool IsRunTypeIterationsCount()
        {
            return RunTypeCB.SelectedIndex == 0;
        }

        /// <summary>
        /// Check if we run to time.
        /// </summary>
        /// <returns><c>true</c> - if we run to time, <c>false</c> - otherwise</returns>
        public bool IsRunTypeRunToTime()
        {
            return RunTypeCB.SelectedIndex == 1;
        }

        /// <summary>
        /// Set checked data graphics.
        /// </summary>
        /// <param name="is_rho_ch">density</param>
        /// <param name="is_vx_ch">X component of velocity</param>
        /// <param name="is_vy_ch">Y component of velocity</param>
        /// <param name="is_vz_ch">Z component of velocity</param>
        /// <param name="is_eps_ch">inner energy</param>
        /// <param name="is_p_ch">pressure</param>
        private void SetGraphicsDataChecked(bool is_rho_ch, bool is_vx_ch, bool is_vy_ch, bool is_vz_ch,
                                            bool is_eps_ch, bool is_p_ch)
        {
            IsGraphic_rho_Used.IsChecked = is_rho_ch;
            IsGraphic_vX_Used.IsChecked = is_vx_ch;
            IsGraphic_vY_Used.IsChecked = is_vy_ch;
            IsGraphic_vZ_Used.IsChecked = is_vz_ch;
            IsGraphic_eps_Used.IsChecked = is_eps_ch;
            IsGraphic_p_Used.IsChecked = is_p_ch;
        }

        /// <summary>
        /// Set 1D test.
        /// </summary>
        /// <param name="test">test</param>
        private void SetRiemannProblem1DTest(RiemannProblem1DTest test)
        {
            // Create grid.
            Grid = new SolidGrid(test.CellsCount, 1, 1, test.XLength / test.CellsCount);

            // Fill cells data.
            for (int i = 0; i < Grid.NX; i++)
            {
                for (int j = 0; j < Grid.NY; j++)
                {
                    for (int k = 0; k < Grid.NZ; k++)
                    {
                        if (i < test.CellsCount / 2)
                        {
                            Grid.Cells[i, j, k].U = new U(test.rho_l, test.vX_l, 0.0, test.p_l);
                        }
                        else
                        {
                            Grid.Cells[i, j, k].U = new U(test.rho_r, test.vX_r, 0.0, test.p_r);
                        }
                    }
                }
            }

            // Set up graphhics intervals.
            SetGraphicsDataChecked(true, true, false, false, true, true);
            //
            Graphic_rho_L_TB.Text = test.rho_int.L.ToString();
            Graphic_rho_H_TB.Text = test.rho_int.H.ToString();
            //
            Graphic_vX_L_TB.Text = test.vX_int.L.ToString();
            Graphic_vX_H_TB.Text = test.vX_int.H.ToString();
            //
            Graphic_eps_L_TB.Text = test.eps_int.L.ToString();
            Graphic_eps_H_TB.Text = test.eps_int.H.ToString();
            //
            Graphic_p_L_TB.Text = test.p_int.L.ToString();
            Graphic_p_H_TB.Text = test.p_int.H.ToString();

            // Time.
            T = 0.0;
            TimeElapsed = 0.0;

            // Set appropriate dt.
            SetAppropriateDt(DtTB);

            // Paint.
            RefreshVisual();
        }

        /// <summary>
        /// Refresh visual.
        /// </summary>
        private void RefreshVisual()
        {
            Paint();

            if (Grid != null)
            {
                string grid_str = String.Format("Grid : {0} x {1} x {2} (dl = {3})", Grid.NX, Grid.NY, Grid.NZ, Grid.CellL);
                string othr_str = String.Format("T = {0}, time elapsed = {1}, sum of max Courant = {2}",
                                              T, TimeElapsed, Grid.MaxCourantXYZ(Double.Parse(DtTB.Text)));
                StatusTB.Text = grid_str + ", " + othr_str;
            }
        }

        /// <summary>
        /// Paint action.
        /// </summary>
        private void Paint()
        {
            if (Drawer == null)
            {
                if (Grid == null)
                {
                    // No grid - no picture.
                    return;
                }

                Drawer = new RectDrawerWPF(new Rect2D(Grid.XSize, Grid.YSize), DrawAreaC, true, false);
                HydroDrawer = new HydroRectDrawer(Drawer);
            }

            if (Drawer2 == null)
            { 
                if (Grid == null)
                {
                    // No grid - no picture.
                    return;
                }

                // We create drawer for fix rectangle because we have to draw coordinate axis
                // and other misc objects. For each graphic we have to implement its own scaler.
                Drawer2 = new RectDrawerWPF(new Rect2D(100.0, 100.0), DrawArea2C, true, false);
                GraphicDrawer = new HydroGraphicsRectDrawer(Drawer2);
            }

            // Begin draw.
            Drawer.BeginDraw();
            //
            double lo = Double.Parse(UComponentRangeL_TB.Text);
            double hi = Double.Parse(UComponentRangeH_TB.Text);
            HydroDrawer.DrawField(Grid, lo, hi);
            // End draw.
            Drawer.EndDraw();

            // Begin draw.
            Drawer2.BeginDraw();
            //
            GraphicDrawer.DrawAllX_Line(Grid, Grid.YSize / 2.0, Grid.ZSize / 2.0,
                                        (bool)IsGraphic_rho_Used.IsChecked, Graphic_rho_Interval,
                                        (bool)IsGraphic_vX_Used.IsChecked, Graphic_vX_Interval,
                                        (bool)IsGraphic_vY_Used.IsChecked, Graphic_vY_Interval,
                                        (bool)IsGraphic_vZ_Used.IsChecked, Graphic_vZ_Interval,
                                        (bool)IsGraphic_eps_Used.IsChecked, Graphic_eps_Interval,
                                        (bool)IsGraphic_p_Used.IsChecked, Graphic_p_Interval);

            // End draw.
            Drawer2.EndDraw();
        }

        /// <summary>
        /// Change size of window event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshVisual();
        }

        /// <summary>
        /// Test 1D Sod.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DSodMI_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Sod(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Modified Sod.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DModifiedSodMI_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.ModifiedSod(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Lax.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>

        private void Tests1DLaxMI_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Lax(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Supersonic shock tube.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DSupersonicShockTube_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.SupersonicShockTube(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DMach3_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Mach3(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DStationaryContactDiscontinuity_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.StationaryContactDiscontinuity(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DSlowlyMovingContactDiscontinuity_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.SlowlyMovingContactDiscontinuity(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DSlowlyMovingWeakShock_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.SlowlyMovingWeakShock(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DStrongShock_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.StrongShock(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DHighMach_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.HighMach(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Einfeldt.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1EinfeldtMI_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Einfeldt(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Woodward-Collela problem.
        /// Source:
        /// Steven Brill.
        /// Verification of the wavelet adaptive multiresolution
        /// representation method to a prescibed error threshold.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tests1DWoodwardCollela_Click(object sender, RoutedEventArgs e)
        {
            int cells_count = 100;

            // Grid.
            Grid = new SolidGrid(cells_count, 1, 1, 0.001);

            // Cells.
            for (int i = 0; i < Grid.NX; i++)
            {
                for (int j = 0; j < Grid.NY; j++)
                {
                    for (int k = 0; k < Grid.NZ; k++)
                    {
                        if (i < cells_count / 10)
                        {
                            Grid.Cells[i, j, k].U = new U(1.0, 0.0, 0.0, 1000.0);
                        }
                        else if (i < (cells_count / 10) * 9)
                        {
                            Grid.Cells[i, j, k].U = new U(1.0, 0.0, 0.0, 0.01);
                        }
                        else
                        {
                            Grid.Cells[i, j, k].U = new U(1.0, 0.0, 0.0, 100.0);
                        }
                    }
                }
            }

            // Set up graphhics intervals.
            SetGraphicsDataChecked(true, true, false, false, false, false);
            //
            Graphic_rho_L_TB.Text = "0.0";
            Graphic_rho_H_TB.Text = "7.0";
            //
            Graphic_vX_L_TB.Text = "0.0";
            Graphic_vX_H_TB.Text = "20.0";
            //
            Graphic_p_L_TB.Text = "0.0";
            Graphic_p_H_TB.Text = "450.0";

            // Time.
            T = 0.0;
            TimeElapsed = 0.0;

            // Set appropriate dt.
            SetAppropriateDt(DtTB);

            // Paint.
            RefreshVisual();
        }

        /// <summary>
        /// Shu-Osher problem.
        /// Source:
        /// Steven Brill.
        /// Verification of the wavelet adaptive multiresolution
        /// representation method to a prescibed error threshold.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tests1DShuOsher_Click(object sender, RoutedEventArgs e)
        {
            // Grid.
            Grid = new SolidGrid(1000, 1, 1, 0.001);

            // Cells.
            for (int i = 0; i < Grid.NX; i++)
            {
                for (int j = 0; j < Grid.NY; j++)
                {
                    for (int k = 0; k < Grid.NZ; k++)
                    {
                        if (i < 100)
                        {
                            Grid.Cells[i, j, k].U = new U(3.857143, 2.629369, 0.0, 10.33333);
                        }
                        else
                        {
                            Grid.Cells[i, j, k].U = new U(1.0 + 0.2 * Math.Sin(5.0 * ((i - 100.0)/100.0)), 0.0, 0.0, 1.0);
                        }
                    }
                }
            }

            // Set up graphhics intervals.
            SetGraphicsDataChecked(true, true, false, false, false, true);
            //
            Graphic_rho_L_TB.Text = "0.0";
            Graphic_rho_H_TB.Text = "5.0";
            //
            Graphic_vX_L_TB.Text = "0.0";
            Graphic_vX_H_TB.Text = "3.0";
            //
            Graphic_p_L_TB.Text = "0.0";
            Graphic_p_H_TB.Text = "12.0";

            // Time.
            T = 0.0;
            TimeElapsed = 0.0;

            // Set appropriate dt.
            SetAppropriateDt(DtTB);

            // Paint.
            RefreshVisual();
        }

        /// <summary>
        /// Random test click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DRandom_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Random(1.0, 1000);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Run.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void RunB_Click(object sender, RoutedEventArgs e)
        {
            double dt = Double.Parse(DtTB.Text);
            Godunov1 solver = new Godunov1(Grid, dt, BordersTB.Text);

            if (IsRunTypeIterationsCount())
            {
                int iters_count = Int32.Parse(RunTypeParTB.Text);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                solver.Iters(iters_count);
                sw.Stop();
                TimeElapsed = sw.ElapsedMilliseconds;

                T += iters_count * dt;
            }
            else if (IsRunTypeRunToTime())
            {
                double to_time = Double.Parse(RunTypeParTB.Text);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (T < to_time)
                {
                    solver.Iter();
                    T += dt;
                }
                sw.Stop();
                TimeElapsed = sw.ElapsedMilliseconds;
            }
            else
            {
                MessageBox.Show("Select way to run (iters count or run to time).");

                return;
            }

            RefreshVisual();
        }

        /// <summary>
        /// Test 2D 100 x 100 grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests2D100x100TwoCirclesMI_Click(object sender, RoutedEventArgs e)
        {
            // Grid.
            Grid = new SolidGrid(100, 100, 1, 1.0);

            // Cells.
            for (int i = 0; i < Grid.NX; i++)
            {
                for (int j = 0; j < Grid.NY; j++)
                {
                    for (int k = 0; k < Grid.NZ; k++)
                    {
                        int dx1 = Math.Abs(i - 33);
                        int dy1 = Math.Abs(j - 33);
                        int dx2 = Math.Abs(i - 67);
                        int dy2 = Math.Abs(j - 67);

                        if ((dx1 * dx1 + dy1 * dy1 < 400) || (dx2 * dx2 + dy2 * dy2 < 400))
                        {
                            Grid.Cells[i, j, k].U = new U(5.0, 0.0, 0.0, 1.0);
                        }
                        else
                        {
                            Grid.Cells[i, j, k].U = new U(1.0, 0.0, 0.0, 0.5);
                        }
                    }
                }
            }

            // Set up graphhics intervals.
            SetGraphicsDataChecked(true, true, true, false, true, true);
            //
            Graphic_rho_L_TB.Text = "0.0";
            Graphic_rho_H_TB.Text = "5.0";
            //
            Graphic_vX_L_TB.Text = "0.0";
            Graphic_vX_H_TB.Text = "5.0";
            //
            Graphic_eps_L_TB.Text = "0.0";
            Graphic_eps_H_TB.Text = "5.0";
            //
            Graphic_p_L_TB.Text = "0.0";
            Graphic_p_H_TB.Text = "5.0";

            // Time.
            T = 0.0;
            TimeElapsed = 0.0;

            // Set appropriate dt.
            SetAppropriateDt(DtTB);

            // Paint.
            RefreshVisual();
        }

        /// <summary>
        /// Richtmeyer-Meshkov instability.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests2DRichtmyerMeshkovInstability_Click(object sender, RoutedEventArgs e)
        {
            // Grid.
            Grid = new SolidGrid(1 * 100, 1 * 100, 1, 1.0);

            // Cells.
            for (int i = 0; i < Grid.NX; i++)
            {
                for (int j = 0; j < Grid.NY; j++)
                {
                    for (int k = 0; k < Grid.NZ; k++)
                    {
                        if (i < 1 * ( 70 + 10 * Math.Sin((double)j / 5.0)))
                        {
                            Grid.Cells[i, j, k].U = new U(5.0, 0.0, 0.0, 1.0);
                            Grid.Cells[i, j, k].U.p = 1.0;
                        }
                        else if (i < 1 * 95)
                        {
                            Grid.Cells[i, j, k].U = new U(1.0, 0.0, 0.0, 1.0);
                            Grid.Cells[i, j, k].U.p = 1.0;
                        }
                        else
                        {
                            Grid.Cells[i, j, k].U = new U(1.0, 0.0, 0.0, 20.0);
                        }
                    }
                }
            }

            // Set up graphhics intervals.
            SetGraphicsDataChecked(true, true, true, false, true, true);
            //
            Graphic_rho_L_TB.Text = "0.0";
            Graphic_rho_H_TB.Text = "5.0";
            //
            Graphic_vX_L_TB.Text = "0.0";
            Graphic_vX_H_TB.Text = "5.0";
            //
            Graphic_eps_L_TB.Text = "0.0";
            Graphic_eps_H_TB.Text = "5.0";
            //
            Graphic_p_L_TB.Text = "0.0";
            Graphic_p_H_TB.Text = "5.0";


            // Time.
            T = 0.0;
            TimeElapsed = 0.0;

            // Set appropriate dt.
            SetAppropriateDt(DtTB);

            // Paint.
            RefreshVisual();
        }

        /// <summary>
        /// Select type of run.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void RunTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RunTypeParTB == null)
            {
                return;
            }

            if (IsRunTypeIterationsCount())
            {
                // Iterations count by default.
                RunTypeParTB.Text = "100";
            }
            else if (IsRunTypeRunToTime())
            {
                // Run time by default.
                RunTypeParTB.Text = "0.5";
            }
        }

        /// <summary>
        /// Save 1D picture.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void PictureSave1D_MI_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            string filename = null;

            sfd.Filter = "Pictures (*.png)|*.png";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = sfd.FileName;

                Transform transform = DrawArea2C.LayoutTransform;
                DrawArea2C.LayoutTransform = null;
                Size size = new Size(DrawArea2C.ActualWidth, DrawArea2C.ActualHeight);
                DrawArea2C.Measure(size);
                DrawArea2C.Arrange(new SWRect(size));
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)size.Width,
                                                                   (int)size.Height,
                                                                   96d,
                                                                   96d,
                                                                   PixelFormats.Pbgra32);
                bitmap.Render(DrawArea2C);

                using (FileStream out_stream = new FileStream(filename, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();

                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(out_stream);
                }

                DrawArea2C.LayoutTransform = transform;
            }
        }

        /// <summary>
        /// Save 2D picture.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void PictureSave2D_MI_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            string filename = null;

            sfd.Filter = "Pictures (*.png)|*.png";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = sfd.FileName;

                Transform transform = DrawAreaC.LayoutTransform;
                DrawAreaC.LayoutTransform = null;
                Size size = new Size(DrawAreaC.ActualWidth, DrawAreaC.ActualHeight);
                DrawAreaC.Measure(size);
                DrawAreaC.Arrange(new SWRect(size));
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)size.Width,
                                                                   (int)size.Height,
                                                                   96d,
                                                                   96d,
                                                                   PixelFormats.Pbgra32);
                bitmap.Render(DrawAreaC);

                using (FileStream out_stream = new FileStream(filename, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();

                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(out_stream);
                }

                DrawAreaC.LayoutTransform = transform;
            }
        }
    }
}
