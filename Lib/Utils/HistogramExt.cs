using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Draw;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Utils;
using SWMColor = System.Windows.Media.Color;
using Lib.MathMod.Grid;

namespace Lib.Utils
{
    /// <summary>
    ///  Extended histogram.
    /// </summary>
    public class HistogramExt
    {
        /// <summary>
        /// First color.
        /// </summary>
        public static SWMColor FirstColor = System.Windows.Media.Colors.Silver; //Orange;

        /// <summary>
        /// Second color.
        /// </summary>
        public static SWMColor SecondColor = System.Windows.Media.Colors.Gray; //LightSteelBlue;

        /// <summary>
        /// Full values.
        /// </summary>
        public double[] V;

        /// <summary>
        /// Detail values.
        /// </summary>
        public List<double>[] W;

        /// <summary>
        /// Cells count.
        /// </summary>
        public int Cells = 0;

        /// <summary>
        /// Cells count.
        /// </summary>
        public int IfaceCells = 0;

        /// <summary>
        /// Cross cells count.
        /// </summary>
        public int CrossCells = 0;

        /// <summary>
        /// Cuts count.
        /// </summary>
        public int Cuts = 0;

        /// <summary>
        /// Extended histogram.
        /// </summary>
        /// <param name="n"></param>
        public HistogramExt(int n)
        {
            V = new double[n];
            W = new List<double>[n];

            for (int i = 0; i < n; i++)
            {
                W[i] = new List<double>();
            }
        }

        /// <summary>
        /// Init histogram by partitions count and grid.
        /// </summary>
        /// <param name="n">partitions count</param>
        /// <param name="grid">grid</param>
        public HistogramExt(int n, StructuredGrid grid)
            : this(n)
        {
            for (int i = 0; i < grid.BlocksCount; i++)
            {
                Block b = grid.Blocks[i];
                W[b.PartitionNumber].Add(b.Canvas.CellsCount);
            }

            FormV();
        }

        /// <summary>
        /// Deviation.
        /// </summary>
        public double Dev
        {
            get
            {
                return Arrays.RelOverDeviationOfPositives(V) * 100.0;
            }
        }

        /// <summary>
        /// Form V array from W.
        /// </summary>
        public void FormV()
        {
            for (int i = 0; i < V.Length; i++)
            {
                double s = 0.0;

                for (int j = 0; j < W[i].Count(); j++)
                {
                    s += W[i][j];
                }

                V[i] = s;
            }
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
            Color[] colors = new Color[2]{ new Color(FirstColor), new Color(SecondColor) };

            // Draw all data items.
            for (int i = 0; i < V.Length; i++)
            {
                double s = 0.0;

                for (int j = 0; j < W[i].Count; j++)
                {
                    double t = W[i][j];
                    rd.SetBrush(colors[j % 2]);
                    rd.FillRect(new Rect(mw + i * (w + dw), mw + i * (w + dw) + w,
                                         mh + k * s, mh + k * (s + t)));
                    s += t;
                }
            }

            rd.SetPenThickness(2.5);
            rd.SetPenColor(new Color(System.Windows.Media.Colors.Black));
            rd.DrawLine(new Point(5.0, mh), new Point(95.0, mh));

            int mid = (int)(V.Sum() / (double)V.Length);

            rd.SetPenThickness(1.0);
            rd.SetPenColor(new Color(System.Windows.Media.Colors.Silver));
            rd.DrawLine(new Point(5.0, mh + k * mid), new Point(95.0, mh + k * mid));

            // Text.
            double text_size = 12.0;
            Vector upv = new Vector(0.0, -(text_size + 1.0));
            Point xp1 = new Point(5.0, /* mh + k * mid */ mh);
            //
            rd.DrawText(xp1, String.Format("mid: {0}", mid), text_size, "Courier New");
            rd.DrawText(xp1, -upv, String.Format("dev: {0:0.000}%", Dev), text_size, "Courier New");
            //
            string cells_str = String.Format("iface cells: {0:0.000}%", 100.0 * ((double)IfaceCells / (double)Cells));
            string cross_str = String.Format("cross cells: {0:0.000}%", 100.0 * ((double)CrossCells / (double)Cells));
            Vector v2 = new Vector(-cells_str.Length * 7.0 - 3.0, 0.0);
            Point xp2 = new Point(95.0, /* mh + k * mid */ mh);
            rd.DrawText(xp2, v2, cells_str, text_size, "Courier New");
            rd.DrawText(xp2, -upv  + v2, cross_str, text_size, "Courier New");
            //
            string proc_str = String.Format("{0,12}", V.Length);
            string cuts_str = String.Format("{0,12}", Cuts);
            int min = Math.Min(Strings.LeadingSymbolsCount(proc_str, ' '),
                               Strings.LeadingSymbolsCount(cuts_str, ' '));
            proc_str = "proc: " + proc_str.Substring(min);
            cuts_str = "cuts: " + cuts_str.Substring(min);
            rd.DrawText(new Point(50.0, mh), new Vector(-8 * 7.0 - 3.0, 0.0),
                        proc_str, text_size, "Courier New");
            rd.DrawText(new Point(50.0, mh), new Vector(-8 * 7.0 - 3.0, 0.0) - upv,
                        cuts_str, text_size, "Courier New");
        }
    }
}
