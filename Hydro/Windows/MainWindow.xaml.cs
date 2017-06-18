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

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Geometry.Geometry2D;
using Lib.MathMod.SolidGrid;
using Lib.Draw;
using Lib.MathMod.Solver;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;
using RectDrawerWPF = Lib.Draw.WPF.RectDrawer;

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
            GraphicDrawer.DrawAllX_Line(Grid, 0.05, 0.05,
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
        /// Start button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void StartB_Click(object sender, RoutedEventArgs e)
        {
            double dl = Lib.Maths.Numbers.Randoms.RandomInInterval(0.0, 5.0);
            double epsl = Lib.Maths.Numbers.Randoms.RandomInInterval(0.0, 5.0);
            double dr = Lib.Maths.Numbers.Randoms.RandomInInterval(0.0, 5.0);
            double epsr = Lib.Maths.Numbers.Randoms.RandomInInterval(0.0, 5.0);

            Grid = new SolidGrid(50, 50, 1, 1.0);

            for (int i = 0; i < Grid.NX; i++)
            {
                for (int j = 0; j < Grid.NY; j++)
                {
                    for (int k = 0; k < Grid.NZ; k++)
                    {
                        Grid.Cells[i, j, k].U.v = new Vector3D(0.0, 0.0, 0.0);

                        if ((i - 25) * (i - 25) + (j - 25) * (j - 25) < 49)
                        {
                            Grid.Cells[i, j, k].U.rho = 20.0;
                            Grid.Cells[i, j, k].U.v.X = 0.0;
                            Grid.Cells[i, j, k].U.p = 20.0;
                        }
                        else
                        {
                            Grid.Cells[i, j, k].U.rho = 1.0;
                            Grid.Cells[i, j, k].U.v.X = 0.0;
                            Grid.Cells[i, j, k].U.p = 1.0;
                        }
                    }
                }
            }

            Paint();
        }

        /// <summary>
        /// Change size of window event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Paint();
        }

        /// <summary>
        /// Do one iteration.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void IterB_Click(object sender, RoutedEventArgs e)
        {
            int iters_count = Int32.Parse(ItersCountTB.Text);

            for (int i = 0; i < iters_count; i++)
            {
                Godunov1.Iter(Grid, 0.01);
            }

            Paint();
        }
    }
}
