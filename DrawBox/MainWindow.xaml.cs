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
using System.Windows.Forms;
using System.IO;

using Lib.Draw.WPF;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using SWRect = System.Windows.Rect;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using RectDrawerWPF = Lib.Draw.WPF.RectDrawer;

namespace DrawBox
{
    /// <summary>
    /// Paint element.
    /// </summary>
    enum PaintElement
    {
        /// <summary>
        /// Nothing to paint.
        /// </summary>
        None,

        /// <summary>
        /// Test drawing.
        /// </summary>
        Test,

        /// <summary>
        /// Plan OMP.
        /// </summary>
        PlanOMP
    };

    /// <summary>
    /// Element of threads count change.
    /// </summary>
    class ThreadsCountChangeElement
    {
        /// <summary>
        /// Thread number (parent thread).
        /// </summary>
        public int ThreadNum;

        /// <summary>
        /// World (global) time.
        /// </summary>
        public double WTime;

        /// <summary>
        /// Threads count change.
        /// </summary>
        public int ChangeCount;

        /// <summary>
        /// Check if we allocate threads.
        /// </summary>
        public bool IsAlloc;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="thread_num">thread number</param>
        /// <param name="wtime">world time</param>
        /// <param name="change_count">count of threads (change)</param>
        /// <param name="is_alloc">if we allocate threads</param>
        public ThreadsCountChangeElement(int thread_num, double wtime, int change_count, bool is_alloc)
        {
            ThreadNum = thread_num;
            WTime = wtime;
            ChangeCount = change_count;
            IsAlloc = is_alloc;
        }
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Relative rectangle for drawing.
        /// </summary>
        private Rect2D RelDrawRect = new Rect2D(100.0, 100.0);

        /// <summary>
        /// Element of painting.
        /// </summary>
        private PaintElement PEl = PaintElement.None;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create drawer.
        /// </summary>
        private void CreateDrawer()
        {
            if (Drawer == null)
            {
                Drawer = new RectDrawerWPF(RelDrawRect, DrawAreaC, false, true);
            }
        }

        /// <summary>
        /// Paint test picture.
        /// </summary>
        private void PaintTest()
        {
            CreateDrawer();
            Drawer.BeginDraw();

            Drawer.DrawLine(new Point2D(0.0, 0.0), new Point2D(100.0, 100.0));
            Drawer.DrawLine(new Point2D(0.0, 100.0), new Point2D(100.0, 0.0));

            Drawer.EndDraw();
        }

        /// <summary>
        /// Paint Plan OMP.
        /// </summary>
        private void PaintPlanOMP()
        {
            int master_threads = 3;
            int max_threads = 4;
            ThreadsCountChangeElement[] els =
            {
                new ThreadsCountChangeElement(0, 3494.15, +1, true),
                new ThreadsCountChangeElement(0, 3494.35, -1, false),
                new ThreadsCountChangeElement(1, 3494.35, +1, true),
                new ThreadsCountChangeElement(0, 3494.35, +0, true),
                new ThreadsCountChangeElement(0, 3494.45, -0, false),
                new ThreadsCountChangeElement(1, 3494.55, -1, false),
                new ThreadsCountChangeElement(2, 3494.55, +1, true),
                new ThreadsCountChangeElement(1, 3494.55, +0, true),
                new ThreadsCountChangeElement(1, 3494.65, -0, false),
                new ThreadsCountChangeElement(2, 3494.8, -1, false),
                new ThreadsCountChangeElement(1, 3494.8, +1, true),
                new ThreadsCountChangeElement(2, 3494.8, +0, true),
                new ThreadsCountChangeElement(2, 3494.9, -0, false),
                new ThreadsCountChangeElement(1, 3495, -1, false),
                new ThreadsCountChangeElement(0, 3495, +1, true),
                new ThreadsCountChangeElement(1, 3495, +0, true),
                new ThreadsCountChangeElement(1, 3495.1, -0, false),
                new ThreadsCountChangeElement(0, 3495.2, -1, false),
                new ThreadsCountChangeElement(1, 3495.2, +1, true),
                new ThreadsCountChangeElement(0, 3495.2, +0, true),
                new ThreadsCountChangeElement(0, 3495.3, -0, false),
                new ThreadsCountChangeElement(1, 3495.4, -1, false),
                new ThreadsCountChangeElement(2, 3495.4, +1, true),
                new ThreadsCountChangeElement(2, 3495.65, -1, false),
                new ThreadsCountChangeElement(0, 3495.65, +1, true),
                new ThreadsCountChangeElement(2, 3495.65, +0, true),
                new ThreadsCountChangeElement(2, 3495.76, -0, false),
                new ThreadsCountChangeElement(0, 3495.85, -1, false),
                new ThreadsCountChangeElement(2, 3495.85, +1, true),
                new ThreadsCountChangeElement(2, 3496.1, -1, false)
            };

            double beg_time = els.First<ThreadsCountChangeElement>().WTime;
            double end_time = els.Last<ThreadsCountChangeElement>().WTime;

            RelDrawRect = new Rect2D(new Interval(beg_time, end_time), new Interval(max_threads));
            Drawer = null;

            CreateDrawer();
            Drawer.BeginDraw();

            int[] ths = new int[master_threads];
            bool[] is_draw = new bool[master_threads];

            for (int i = 0; i < ths.Count<int>(); i++)
            {
                ths[i] = 1;
            }

            for (int i = 0; i < is_draw.Count<bool>(); i++)
            {
                is_draw[i] = false;
            }

            Drawer.SetPenColor(new Lib.Draw.Color(Colors.DarkGray));

            for (int i = 0; i < els.Count<ThreadsCountChangeElement>(); i++)
            {
                ThreadsCountChangeElement e = els[i];

                if (i == 0)
                {
                    ths[e.ThreadNum] += e.ChangeCount;
                    is_draw[e.ThreadNum] = e.IsAlloc;
                }
                else if (i > 0)
                {
                    ThreadsCountChangeElement pe = els[i - 1];

                    // Draw rectangles.
                    double low = 0.0;
                    for (int j = 0; j < ths.Count<int>(); j++)
                    {
                        Color c;

                        switch (j)
                        {
                            case 0:
                                c = Colors.Blue;
                                break;

                            case 1:
                                c = Colors.Green;
                                break;

                            case 2:
                                c = Colors.Red;
                                break;

                            default:
                                c = Colors.Black;
                                break;
                        }

                        if (is_draw[j])
                        {
                            Drawer.SetBrush(new Lib.Draw.Color(c));
                        }
                        else
                        {
                            Drawer.SetBrush(new Lib.Draw.Color(Colors.LightGray));
                        }

                        Drawer.FillRect(new Rect2D(new Interval(pe.WTime, e.WTime),
                                                   new Interval(low, low + ths[j])));

                        if (low > 0.0)
                        {
                            Drawer.DrawLine(new Point2D(pe.WTime, low), new Point2D(e.WTime, low));
                        }

                        low += ths[j];
                    }

                    ths[e.ThreadNum] += e.ChangeCount;
                    is_draw[e.ThreadNum] = e.IsAlloc;
                }
            }

            for (int i = 1; i < els.Count<ThreadsCountChangeElement>() - 1; i++)
            {
                ThreadsCountChangeElement e = els[i];
                Drawer.DrawLine(new Point2D(e.WTime, 0.0), new Point2D(e.WTime, max_threads));
            }

            Drawer.EndDraw();
        }

        /// <summary>
        /// Paint.
        /// </summary>
        private void Paint()
        {
            switch (PEl)
            {
                case PaintElement.Test:
                    PaintTest();
                    break;

                case PaintElement.PlanOMP:
                    PaintPlanOMP();
                    break;
            }
        }

        /// <summary>
        /// Test drawing click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawTestMI_Click(object sender, RoutedEventArgs e)
        {
            PEl = PaintElement.Test;
            Paint();
        }

        /// <summary>
        /// Click on Draw PlanOMP menu item.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP_MI_Click(object sender, RoutedEventArgs e)
        {
            PEl = PaintElement.PlanOMP;
            Paint();
        }

        /// <summary>
        /// Click on Picture Save menu item.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void PictureSaveMI_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
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

        /// <summary>
        /// Change size.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawAreaC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Paint();
        }
    }
}
