using System;
using System.Linq;

using Lib.Draw;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Utils;

namespace GridMaster.Tools
{
    /// <summary>
    /// Histogramm.
    /// </summary>
    public class Histogram
    {
        /// <summary>
        /// Values.
        /// </summary>
        public double[] V;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="n">count of elements</param>
        public Histogram(int n)
        {
            V = new double[n];
        }

        /// <summary>
        /// Draw histogram in rect drawer.
        /// </summary>
        /// <param name="rd">rect drawer</param>
        public void Draw(RectDrawer rd)
        {
            // We have n data rectangles of width w,
            // (n - 1) distances between each pair of them (of length dw),
            // and we have 2 margins of length mw.
            // Total width: n * w + (n - 1) * dw + 2 * mw = 100.0

            // Let margin be 10.0
            // n * w + (n - 1) * dw = 80.0
            // n * (w + dw) - dw = 80.0
            double mw = 10.0;

            // Let w / dw be 3.0, so w = 3.0 * dw
            // n * 4.0 * dw - dw = 80.0
            // dw * (4 * n - 1) = 80.0
            // dw = 80.0 / (4 * n - 1)
            double dw = 80.0 / (4.0 * (double)V.Length - 1.0);
            double w = 3.0 * dw;

            int max_index = Arrays.MaxIndex(V);

            // Let vertical margin be 20.0
            double mh = 20.0;

            // We have to display [0, max] to vertical rectangle [20.0, 80.0]
            double k = 60.0 / V[max_index];

            // Set colors.
            rd.SetBrush(new Color(System.Windows.Media.Colors.Orange));

            // Draw all data items.
            for (int i = 0; i < V.Length; i++)
            {
                rd.FillRect(new Rect(mw + i * (w + dw), mw + i * (w + dw) + w, mh, mh + k * V[i]));
            }

            rd.SetPenThickness(2.5);
            rd.SetPenColor(new Color(System.Windows.Media.Colors.Black));
            rd.DrawLine(new Point(5.0, mh), new Point(95.0, mh));

            double mid = V.Sum() / (double)V.Length;

            rd.SetPenThickness(1.0);
            rd.SetPenColor(new Color(System.Windows.Media.Colors.Silver));
            rd.DrawLine(new Point(5.0, mh + k * mid), new Point(95.0, mh + k * mid));
            rd.DrawText(new Point(5.0, mh + k * mid + 4.0), String.Format("mid: {0}", mid), 12.0, "Courier New");
            rd.DrawText(new Point(5.0, mh + k * mid),
                        String.Format("dev: {0}%", Arrays.RelOverDeviationOfPositives(V) * 100.0),
                        12.0, "Courier New");
        }
    }
}
