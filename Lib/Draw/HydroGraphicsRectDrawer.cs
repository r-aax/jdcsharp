using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;
using Lib.MathMod.SolidGrid;
using Lib.Maths.Geometry.Geometry2D;
using SColor = System.Windows.Media.Color;
using SColors = System.Windows.Media.Colors;

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
        private static Color AxisColor = new Color(System.Windows.Media.Colors.Black);

        /// <summary>
        /// Color of grid cells borders.
        /// </summary>
        private static Color CellcBordersColor = Color.Gray(220);

        /// <summary>
        /// rho inner color.
        /// </summary>
        public static SColor SColor_rho = SColors.Black;

        /// <summary>
        /// v.X inner color.
        /// </summary>
        public static SColor SColor_vx = SColors.LightBlue;

        /// <summary>
        /// v.Y inner color.
        /// </summary>
        public static SColor SColor_vy = SColors.LightGreen;

        /// <summary>
        /// v.Z inner color.
        /// </summary>
        public static SColor SColor_vz = SColors.LightCoral;

        /// <summary>
        /// eps inner color.
        /// </summary>
        public static SColor SColor_eps = SColors.Green;

        /// <summary>
        /// p inner color.
        /// </summary>
        public static SColor SColor_p = SColors.Red;

        /// <summary>
        /// Rho color.
        /// </summary>
        private static Color Color_rho = new Color(SColor_rho);

        /// <summary>
        /// v.X color.
        /// </summary>
        private static Color Color_vx = new Color(SColor_vx);

        /// <summary>
        /// v.Y color.
        /// </summary>
        private static Color Color_vy = new Color(SColor_vy);

        /// <summary>
        /// v.Z color.
        /// </summary>
        private static Color Color_vz = new Color(SColor_vz);

        /// <summary>
        /// eps color.
        /// </summary>
        private static Color Color_eps = new Color(SColor_eps);

        /// <summary>
        /// p color.
        /// </summary>
        private static Color Color_p = new Color(SColor_p);

        /// <summary>
        /// Thickness.
        /// </summary>
        private static double GraphicPenThickness = 2.0;

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
            Drawer.SetPen(CellcBordersColor, 1.0);
            for (int xi = 0; xi <= g.NX; xi++)
            {
                double x = 0.05 * w + 0.9 * w * ((double)xi / (double)g.NX);
                Drawer.DrawLine(x, 0.03 * h, x, 0.97 * h);
            }

            // Axis.
            double zero = 0.0;
            Drawer.SetPen(AxisColor, 1.0);
            Drawer.DrawLine(0.04 * w, 0.05 * h, 0.96 * w, 0.05 * h);
            Drawer.DrawLine(0.05 * w, 0.06 * h, 0.05 * w, 0.04 * h);
            Drawer.DrawLine(0.95 * w, 0.06 * h, 0.95 * w, 0.04 * h);
            Drawer.DrawText(new Point(0.045 * w, 0.03 * h), zero.ToString(), 12, "Arial");
            Drawer.DrawText(new Point(0.945 * w, 0.03 * h), g.XSize.ToString(), 12, "Arial");
        }

        /// <summary>
        /// Draw value.
        /// </summary>
        /// <param name="w"><c>X</c> coordinate</param>
        /// <param name="h"><c>Y</c> coordinate</param>
        /// <param name="val">value</param>
        /// <param name="color">color</param>
        public void DrawValue(double w, double h, double val, Color color)
        {
            Drawer.DrawText(new Point(w, h), String.Format("[{0}]", val), 12, "Arial", color);
        }

        /// <summary>
        /// Draw values of rho, vx, vy, vz, eps, p.
        /// </summary>
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
        public void DrawValues(bool is_rho, Interval rho_int,
                               bool is_vx, Interval vx_int,
                               bool is_vy, Interval vy_int,
                               bool is_vz, Interval vz_int,
                               bool is_eps, Interval eps_int,
                               bool is_p, Interval p_int)
        {
            double w = Drawer.Rect.Width;
            double h = Drawer.Rect.Height;

            if (is_rho)
            {
                DrawValue(0.05 * w + 0.03 * w, 0.03 * h, rho_int.L, Color_rho);
                DrawValue(0.05 * w + 0.03 * w, h, rho_int.H, Color_rho);
            }

            if (is_vx)
            {
                DrawValue(0.05 * w + 0.18 * w, 0.03 * h, vx_int.L, Color_vx);
                DrawValue(0.05 * w + 0.18 * w, h, vx_int.H, Color_vx);
            }

            if (is_vy)
            {
                DrawValue(0.05 * w + 0.33 * w, 0.03 * h, vy_int.L, Color_vy);
                DrawValue(0.05 * w + 0.33 * w, h, vy_int.H, Color_vy);
            }

            if (is_vz)
            {
                DrawValue(0.05 * w + 0.48 * w, 0.03 * h, vz_int.L, Color_vz);
                DrawValue(0.05 * w + 0.48 * w, h, vz_int.H, Color_vz);
            }

            if (is_eps)
            {
                DrawValue(0.05 * w + 0.63 * w, 0.03 * h, eps_int.L, Color_eps);
                DrawValue(0.05 * w + 0.63 * w, h, eps_int.H, Color_eps);
            }

            if (is_p)
            {
                DrawValue(0.05 * w + 0.78 * w, 0.03 * h, p_int.L, Color_p);
                DrawValue(0.05 * w + 0.78 * w, h, p_int.H, Color_p);
            }
        }

        /// <summary>
        /// Draw all graphics (with const segments).
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
        public void DrawAllX_Const(SolidGrid g, double y, double z,
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
            Interval inner_w = new Interval(0.05 * w, 0.95 * w);
            Interval inner_h = new Interval(0.05 * h, 0.95 * h);
            Interval xint = new Interval(0.0, g.XSize);
            Rect inner_rect = new Rect(inner_w, inner_h);
            RectScaler rc;

            // Misc.
            DrawMiscX(g);

            if (is_rho)
            {
                Drawer.SetPen(Color_rho, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, rho_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX; xi++)
                {
                    double rho = g.Cells[xi, yi, zi].U.rho;
                    Drawer.DrawLine(rc.T(new Point(xi * g.CellL, rho)), rc.T(new Point((xi + 1) * g.CellL, rho)));
                }
            }

            if (is_vx)
            {
                Drawer.SetPen(Color_vx, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, vx_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX; xi++)
                {
                    double vx = g.Cells[xi, yi, zi].U.v.X;
                    Drawer.DrawLine(rc.T(new Point(xi * g.CellL, vx)), rc.T(new Point((xi + 1) * g.CellL, vx)));
                }
            }

            if (is_vy)
            {
                Drawer.SetPen(Color_vy, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, vy_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX; xi++)
                {
                    double vy = g.Cells[xi, yi, zi].U.v.Y;
                    Drawer.DrawLine(rc.T(new Point(xi * g.CellL, vy)), rc.T(new Point((xi + 1) * g.CellL, vy)));
                }
            }

            if (is_vz)
            {
                Drawer.SetPen(Color_vz, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, vz_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX; xi++)
                {
                    double vz = 0.0;
                    Drawer.DrawLine(rc.T(new Point(xi * g.CellL, vz)), rc.T(new Point((xi + 1) * g.CellL, vz)));
                }
            }

            if (is_eps)
            {
                Drawer.SetPen(Color_eps, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, eps_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX; xi++)
                {
                    double eps = g.Cells[xi, yi, zi].U.eps;
                    Drawer.DrawLine(rc.T(new Point(xi * g.CellL, eps)), rc.T(new Point((xi + 1) * g.CellL, eps)));
                }
            }

            if (is_p)
            {
                Drawer.SetPen(Color_p, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, p_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX; xi++)
                {
                    double p = g.Cells[xi, yi, zi].U.p;
                    Drawer.DrawLine(rc.T(new Point(xi * g.CellL, p)), rc.T(new Point((xi + 1) * g.CellL, p)));
                }
            }

            DrawValues(is_rho, rho_int, is_vx, vx_int, is_vy, vy_int, is_vz, vz_int, is_eps, eps_int, is_p, p_int);
        }

        /// <summary>
        /// Draw all graphics (with linear segments).
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
        public void DrawAllX_Line(SolidGrid g, double y, double z,
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
            Interval inner_w = new Interval(0.05 * w, 0.95 * w);
            Interval inner_h = new Interval(0.05 * h, 0.95 * h);
            Interval xint = new Interval(0.0, g.XSize);
            Rect inner_rect = new Rect(inner_w, inner_h);
            RectScaler rc;

            // Misc.
            DrawMiscX(g);

            if (is_rho)
            {
                Drawer.SetPen(Color_rho, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, rho_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX - 1; xi++)
                {
                    double rho1 = g.Cells[xi, yi, zi].U.rho;
                    double rho2 = g.Cells[xi + 1, yi, zi].U.rho;
                    Drawer.DrawLine(rc.T(new Point((xi + 0.5) * g.CellL, rho1)),
                                    rc.T(new Point((xi + 1.5) * g.CellL, rho2)));
                }
            }

            if (is_vx)
            {
                Drawer.SetPen(Color_vx, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, vx_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX - 1; xi++)
                {
                    double vx1 = g.Cells[xi, yi, zi].U.v.X;
                    double vx2 = g.Cells[xi + 1, yi, zi].U.v.X;
                    Drawer.DrawLine(rc.T(new Point((xi + 0.5) * g.CellL, vx1)),
                                    rc.T(new Point((xi + 1.5) * g.CellL, vx2)));
                }
            }

            if (is_vy)
            {
                Drawer.SetPen(Color_vy, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, vy_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX - 1; xi++)
                {
                    double vy1 = g.Cells[xi, yi, zi].U.v.Y;
                    double vy2 = g.Cells[xi + 1, yi, zi].U.v.Y;
                    Drawer.DrawLine(rc.T(new Point((xi + 0.5) * g.CellL, vy1)),
                                    rc.T(new Point((xi + 1.5) * g.CellL, vy2)));
                }
            }

            if (is_vz)
            {
                Drawer.SetPen(Color_vz, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, vz_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX - 1; xi++)
                {
                    double vz1 = 0.0;
                    double vz2 = 0.0;
                    Drawer.DrawLine(rc.T(new Point((xi + 0.5) * g.CellL, vz1)),
                                    rc.T(new Point((xi + 1.5) * g.CellL, vz2)));
                }
            }

            if (is_eps)
            {
                Drawer.SetPen(Color_eps, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, eps_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX - 1; xi++)
                {
                    double eps1 = g.Cells[xi, yi, zi].U.eps;
                    double eps2 = g.Cells[xi + 1, yi, zi].U.eps;
                    Drawer.DrawLine(rc.T(new Point((xi + 0.5) * g.CellL, eps1)),
                                    rc.T(new Point((xi + 1.5) * g.CellL, eps2)));
                }
            }

            if (is_p)
            {
                Drawer.SetPen(Color_p, GraphicPenThickness);
                rc = new RectScaler(new Rect(xint, p_int), inner_rect, false, false);
                for (int xi = 0; xi < g.NX - 1; xi++)
                {
                    double p1 = g.Cells[xi, yi, zi].U.p;
                    double p2 = g.Cells[xi + 1, yi, zi].U.p;
                    Drawer.DrawLine(rc.T(new Point((xi + 0.5) * g.CellL, p1)),
                                    rc.T(new Point((xi + 1.5) * g.CellL, p2)));
                }
            }

            DrawValues(is_rho, rho_int, is_vx, vx_int, is_vy, vy_int, is_vz, vz_int, is_eps, eps_int, is_p, p_int);
        }
    }
}
