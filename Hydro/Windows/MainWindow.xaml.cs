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
        /// Hydro rect drawer.
        /// </summary>
        private HydroRectDrawer HydroDrawer = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
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

                Drawer = new RectDrawerWPF(new Rect2D(Grid.Size.X, Grid.Size.Y), DrawAreaC, true, false);
                HydroDrawer = new HydroRectDrawer(Drawer);
            }

            // Begin draw.
            Drawer.BeginDraw();

            double lo = Double.Parse(UComponentRangeL_TB.Text);
            double hi = Double.Parse(UComponentRangeH_TB.Text);
            HydroDrawer.DrawField(Grid, lo, hi);

            // End draw.
            Drawer.EndDraw();
        }

        /// <summary>
        /// Start button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void StartB_Click(object sender, RoutedEventArgs e)
        {
            Grid = new SolidGrid(new Vector3D(2.0, 1.0, 1.1), 1.0);

            for (int i = 0; i < Grid.XISize; i++)
            {
                for (int j = 0; j < Grid.YISize; j++)
                {
                    for (int k = 0; k < Grid.ZISize; k++)
                    {
                        Grid.Cells[i, j, k].U.eps = 1.0;
                    }
                }
            }
            Grid.Cells[0, 0, 0].U.rho = 100.0;
            Grid.Cells[1, 0, 0].U.rho = 10.0;
            //Grid.Cells[2, 0, 0].U.rho = 10.0;

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
