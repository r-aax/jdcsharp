﻿using System.Linq;
using System.Windows.Controls;

using Lib.Draw.WPF;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;

namespace DrawBox.DrawMaster.UniformPointsDrawMaster
{
    /// <summary>
    /// Draw master for uniform points.
    /// </summary>
    public class UniformPointsDrawMaster
    {
        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="cnv">canvas</param>
        public static void Draw(Canvas cnv)
        {
            RectDrawer d = new RectDrawer(new Rect2D(100.0, 100.0), cnv, false, true);
            d.BeginDraw();

            Rect2D rect = new Rect2D(10.0, 90.0, 10.0, 90.0);

            //d.SetBrush(new Lib.Draw.Color(System.Windows.Media.Colors.Silver));
            //d.FillRect(rect);
            d.DrawRect(rect);

            int n = 250;
            double[] ws = new double[n];
            for (int i = 0; i < n; i++)
            {
                ws[i] = 1.0;
            }
            ws[0] = 10.0;
            ws[1] = 20.0;
            ws[2] = 30.0;
            Point[] ps = Generator.UniformPointsInRect(ws, rect);

            d.SetBrush(new Lib.Draw.Color(System.Windows.Media.Colors.Black));

            for (int i = 0; i < ps.Count(); i++)
            {
                d.FillPoint(ps[i], 2.0);
            }

            d.EndDraw();
        }
    }
}
