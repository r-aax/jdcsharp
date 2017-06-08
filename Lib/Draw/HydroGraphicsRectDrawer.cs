using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;
using Lib.MathMod.SolidGrid;
using Lib.Maths.Geometry.Geometry2D;

namespace Lib.Draw
{
    /// <summary>
    /// Drawer for graphic.
    /// </summary>
    public class HydroGraphicsRectDrawer
    {
        /// <summary>
        /// Drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Axis color.
        /// </summary>
        private Color AxisColor = new Color(System.Windows.Media.Colors.Black);

        /// <summary>
        /// Color of grid cells borders.
        /// </summary>
        private Color CellcBordersColor = Color.Gray(220);

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="drawer">drawer</param>
        public HydroGraphicsRectDrawer(RectDrawer drawer)
        {
            Debug.Assert(drawer != null);
            Drawer = drawer;
        }

        /// <summary>
        /// Draw X misc.
        /// </summary>
        /// <param name="g">grid</param>
        public void DrawMiscX(SolidGrid g)
        {
            double w = Drawer.Rect.Width;
            double h = Drawer.Rect.Height;

            // Cells borders.
            Drawer.SetPenColor(CellcBordersColor);
            for (int xi = 0; xi <= g.NX; xi++)
            {
                Drawer.DrawVLine(0.05 * w + 0.9 * w * ((double)xi / (double)g.NX));
            }

            // Axis.
            double zero = 0.0;
            Drawer.SetPenColor(AxisColor);
            Drawer.DrawLine(0.04 * w, 0.05 * h, 0.96 * w, 0.05 * h);
            Drawer.DrawLine(0.05 * w, 0.06 * h, 0.05 * w, 0.04 * h);
            Drawer.DrawLine(0.95 * w, 0.06 * h, 0.95 * w, 0.04 * h);
            Drawer.DrawText(new Point(0.045 * w, 0.03 * h), zero.ToString(), 12, "Arial");
            Drawer.DrawText(new Point(0.945 * w, 0.03 * h), g.XSize.ToString(), 12, "Arial");
        }

        /// <summary>
        /// Draw all graphics.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="y"><c>y</c> coordinate</param>
        /// <param name="z"><c>z</c> coordinate</param>
        /// <param name="is_rho">is <c>rho</c> used</param>
        /// <param name="rho_int"><c>rho</c> interval</param>
        /// <param name="is_vx">is <c>v.X</c> used</param>
        /// <param name="vx_int"><c>v.X</c> interval</param>
        /// <param name="is_vy">is <c>v.Y</c> used</param>
        /// <param name="vy_int"><c>v.Y</c> interval</param>
        /// <param name="is_vz">is <c>v.Z</c> used</param>
        /// <param name="vz_int"><c>v.Z</c> interval</param>
        /// <param name="is_eps">is <c>eps</c> used</param>
        /// <param name="eps_int"><c>eps</c> interval</param>
        /// <param name="is_p">is <c>p</c> used</param>
        /// <param name="p_int"><c>p</c> interval</param>
        public void DrawAllX(SolidGrid g, double y, double z,
                             bool is_rho, Interval rho_int,
                             bool is_vx, Interval vx_int,
                             bool is_vy, Interval vy_int,
                             bool is_vz, Interval vz_int,
                             bool is_eps, Interval eps_int,
                             bool is_p, Interval p_int)
        {
            Debug.Assert((y < g.YSize) && (z < g.ZSize), "wrong y,z coordinates while drawing the rho graphic");

            int yi = (int)(y / g.CellL);
            int zi = (int)(z / g.CellL);
            double w = Drawer.Rect.Width;
            double h = Drawer.Rect.Height;
            IntervalScaler scx, scy;
            RectScaler rc;

            // Misc.
            DrawMiscX(g);

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Black));
            scx = new IntervalScaler(new Interval(0.0, g.XSize), new Interval(0.05 * w, 0.95 * w), false);
            scy = new IntervalScaler(new Interval(0.0, 5.0), new Interval(0.05 * h, 0.95 * w), false);
            rc = new RectScaler(new Rect(new Interval(0.0, g.XSize), new Interval(0.0, 5.0)),
                                new Rect(new Interval(0.05 * w, 0.95 * w), new Interval(0.05 * h, 0.95 * w)), false, false);
            for (int xi = 0; xi < g.NX; xi++)
            {
                double rho = g.Cells[xi, yi, zi].U.rho;
                Drawer.DrawLine(rc.T(new Point(xi * g.CellL, rho)), rc.T(new Point((xi + 1) * g.CellL, rho)));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Blue));
            scx = new IntervalScaler(new Interval(0.0, g.XSize), new Interval(0.05 * w, 0.95 * w), false);
            scy = new IntervalScaler(new Interval(0.0, 5.0), new Interval(0.05 * h, 0.95 * w), false);
            rc = new RectScaler(new Rect(new Interval(0.0, g.XSize), new Interval(0.0, 5.0)),
                                new Rect(new Interval(0.05 * w, 0.95 * w), new Interval(0.05 * h, 0.95 * w)), false, false);
            for (int xi = 0; xi < g.NX; xi++)
            {
                double vx = g.Cells[xi, yi, zi].U.v.X;
                Drawer.DrawLine(rc.T(new Point(xi * g.CellL, vx)), rc.T(new Point((xi + 1) * g.CellL, vx)));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Green));
            scx = new IntervalScaler(new Interval(0.0, g.XSize), new Interval(0.05 * w, 0.95 * w), false);
            scy = new IntervalScaler(new Interval(0.0, 5.0), new Interval(0.05 * h, 0.95 * w), false);
            rc = new RectScaler(new Rect(new Interval(0.0, g.XSize), new Interval(0.0, 5.0)),
                                new Rect(new Interval(0.05 * w, 0.95 * w), new Interval(0.05 * h, 0.95 * w)), false, false);
            for (int xi = 0; xi < g.NX; xi++)
            {
                double vy = g.Cells[xi, yi, zi].U.v.Y;
                Drawer.DrawLine(rc.T(new Point(xi * g.CellL, vy)), rc.T(new Point((xi + 1) * g.CellL, vy)));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Red));
            scx = new IntervalScaler(new Interval(0.0, g.XSize), new Interval(0.05 * w, 0.95 * w), false);
            scy = new IntervalScaler(new Interval(0.0, 5.0), new Interval(0.05 * h, 0.95 * w), false);
            rc = new RectScaler(new Rect(new Interval(0.0, g.XSize), new Interval(0.0, 5.0)),
                                new Rect(new Interval(0.05 * w, 0.95 * w), new Interval(0.05 * h, 0.95 * w)), false, false);
            for (int xi = 0; xi < g.NX; xi++)
            {
                double vz = g.Cells[xi, yi, zi].U.v.Z;
                Drawer.DrawLine(rc.T(new Point(xi * g.CellL, vz)), rc.T(new Point((xi + 1) * g.CellL, vz)));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Orange));
            scx = new IntervalScaler(new Interval(0.0, g.XSize), new Interval(0.05 * w, 0.95 * w), false);
            scy = new IntervalScaler(new Interval(0.0, 5.0), new Interval(0.05 * h, 0.95 * w), false);
            rc = new RectScaler(new Rect(new Interval(0.0, g.XSize), new Interval(0.0, 5.0)),
                                new Rect(new Interval(0.05 * w, 0.95 * w), new Interval(0.05 * h, 0.95 * w)), false, false);
            for (int xi = 0; xi < g.NX; xi++)
            {
                double eps = g.Cells[xi, yi, zi].U.eps;
                Drawer.DrawLine(rc.T(new Point(xi * g.CellL, eps)), rc.T(new Point((xi + 1) * g.CellL, eps)));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.IndianRed));
            scx = new IntervalScaler(new Interval(0.0, g.XSize), new Interval(0.05 * w, 0.95 * w), false);
            scy = new IntervalScaler(new Interval(0.0, 5.0), new Interval(0.05 * h, 0.95 * w), false);
            rc = new RectScaler(new Rect(new Interval(0.0, g.XSize), new Interval(0.0, 5.0)),
                                new Rect(new Interval(0.05 * w, 0.95 * w), new Interval(0.05 * h, 0.95 * w)), false, false);
            for (int xi = 0; xi < g.NX; xi++)
            {
                double p = g.Cells[xi, yi, zi].U.p;
                Drawer.DrawLine(rc.T(new Point(xi * g.CellL, p)), rc.T(new Point((xi + 1) * g.CellL, p)));
            }
        }
    }
}
