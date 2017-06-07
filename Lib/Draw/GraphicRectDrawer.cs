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
    public class GraphicRectDrawer
    {
        /// <summary>
        /// Drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="drawer">drawer</param>
        public GraphicRectDrawer(RectDrawer drawer)
        {
            Debug.Assert(drawer != null);
            Drawer = drawer;
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

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Black));
            for (int xi = 0; xi < g.NX; xi++)
            {
                double rho = g.Cells[xi, yi, zi].U.rho;
                Drawer.DrawLine(new Point(xi * g.CellL, rho), new Point((xi + 1) * g.CellL, rho));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Blue));
            for (int xi = 0; xi < g.NX; xi++)
            {
                double vx = g.Cells[xi, yi, zi].U.v.X;
                Drawer.DrawLine(new Point(xi * g.CellL, vx), new Point((xi + 1) * g.CellL, vx));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Green));
            for (int xi = 0; xi < g.NX; xi++)
            {
                double vy = g.Cells[xi, yi, zi].U.v.Y;
                Drawer.DrawLine(new Point(xi * g.CellL, vy), new Point((xi + 1) * g.CellL, vy));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Red));
            for (int xi = 0; xi < g.NX; xi++)
            {
                double vz = g.Cells[xi, yi, zi].U.v.Z;
                Drawer.DrawLine(new Point(xi * g.CellL, vz), new Point((xi + 1) * g.CellL, vz));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.Orange));
            for (int xi = 0; xi < g.NX; xi++)
            {
                double eps = g.Cells[xi, yi, zi].U.eps;
                Drawer.DrawLine(new Point(xi * g.CellL, eps), new Point((xi + 1) * g.CellL, eps));
            }

            Drawer.SetPenColor(new Color(System.Windows.Media.Colors.IndianRed));
            for (int xi = 0; xi < g.NX; xi++)
            {
                double p = g.Cells[xi, yi, zi].U.p;
                Drawer.DrawLine(new Point(xi * g.CellL, p), new Point((xi + 1) * g.CellL, p));
            }
        }
    }
}
