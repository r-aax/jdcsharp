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

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Geometry.Geometry2D;
using Lib.MathMod.SolidGrid;
using Lib.Draw;
using Lib.MathMod;
using Lib.MathMod.Solver;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using RectDrawerWPF = Lib.Draw.WPF.RectDrawer;
using Lib.Utils.Time;

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
        private double T = 0;

        /// <summary>
        /// String.
        /// </summary>
        private string StatusString = "";

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
        private Interval Graphic_rho_Interval
        {
            get
            {
                return new Interval(Double.Parse(Graphic_rho_L_TB.Text),
                                    Double.Parse(Graphic_rho_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of v.X values.
        /// </summary>
        private Interval Graphic_vX_Interval
        {
            get
            {
                return new Interval(Double.Parse(Graphic_vX_L_TB.Text),
                                    Double.Parse(Graphic_vX_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of v.Y values.
        /// </summary>
        private Interval Graphic_vY_Interval
        {
            get
            {
                return new Interval(Double.Parse(Graphic_vY_L_TB.Text),
                                    Double.Parse(Graphic_vY_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of v.Z values.
        /// </summary>
        private Interval Graphic_vZ_Interval
        {
            get
            {
                return new Interval(Double.Parse(Graphic_vZ_L_TB.Text),
                                    Double.Parse(Graphic_vZ_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of eps values.
        /// </summary>
        private Interval Graphic_eps_Interval
        {
            get
            {
                return new Interval(Double.Parse(Graphic_eps_L_TB.Text),
                                    Double.Parse(Graphic_eps_H_TB.Text));
            }
        }

        /// <summary>
        /// Interval of p values.
        /// </summary>
        private Interval Graphic_p_Interval
        {
            get
            {
                return new Interval(Double.Parse(Graphic_p_L_TB.Text),
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

            // Paint.
            RefreshVisual();
        }

        /// <summary>
        /// Refresh visual.
        /// </summary>
        private void RefreshVisual()
        {
            Paint();
            StatusTB.Text = StatusString;
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
            RiemannProblem1DTest test = RiemannProblem1DTest.Sod(1.0, 100);
            SetRiemannProblem1DTest(test);
        }       

        /// <summary>
        /// Test 1D 123.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1D123_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Problem123(1.0, 100);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Woodward & Collella left.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DWoodwardCollelaLeft_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Woodward_Colella_Left(1.0, 100);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Woodward & Collela right.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DWoodwardCollelaRight_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Woodward_Collela_Right(1.0, 100);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Test 1D Woodward & Collela collision.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DWoodwardCollelaCollision_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Woodward_Collella_Collision(1.0, 100);
            SetRiemannProblem1DTest(test);
        }

        /// <summary>
        /// Random test click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Tests1DRandom_Click(object sender, RoutedEventArgs e)
        {
            RiemannProblem1DTest test = RiemannProblem1DTest.Random(1.0, 100);
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
            double total_time;

            if (IsUseItersCountCB.IsChecked ?? true)
            {
                int iters_count = Int32.Parse(ItersCountTB.Text);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                Godunov1.Iters(Grid, dt, iters_count);
                sw.Stop();
                total_time = sw.ElapsedMilliseconds;

                T += iters_count * dt;
            }
            else if (IsUseRunToTimeCB.IsChecked ?? true)
            {
                double to_time = Double.Parse(TimeToRunTB.Text);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (T < to_time)
                {
                    Godunov1.Iter(Grid, dt);
                    T += dt;
                }
                sw.Stop();
                total_time = sw.ElapsedMilliseconds;
            }
            else
            {
                MessageBox.Show("Select way to run (iters count or run to time).");

                return;
            }

            StatusString = String.Format("T = {0}, ellapsed time = {1} ms", T, total_time);
            RefreshVisual();
        }

        /// <summary>
        /// Test 2D 1000 x 1000 grid.
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

            // Paint.
            RefreshVisual();
        }
    }
}
