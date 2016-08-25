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
            // 4 threads.
            // 3 procesures with parallel part 2.
            //int master_threads = 3;
            //int max_threads = 4;
            //ThreadsCountChangeElement[] els =
            //{
            //    new ThreadsCountChangeElement(2, 388.114, +1, true),
            //    new ThreadsCountChangeElement(2, 388.364, -1, false),
            //    new ThreadsCountChangeElement(1, 388.365, +1, true),
            //    new ThreadsCountChangeElement(2, 388.365, +0, true),
            //    new ThreadsCountChangeElement(2, 388.465, -0, false),
            //    new ThreadsCountChangeElement(1, 388.566, -1, false),
            //    new ThreadsCountChangeElement(0, 388.567, +1, true),
            //    new ThreadsCountChangeElement(1, 388.568, +0, true),
            //    new ThreadsCountChangeElement(1, 388.669, -0, false),
            //    new ThreadsCountChangeElement(0, 388.768, -1, false),
            //    new ThreadsCountChangeElement(1, 388.768, +1, true),
            //    new ThreadsCountChangeElement(0, 388.769, +0, true),
            //    new ThreadsCountChangeElement(0, 388.869, -0, false),
            //    new ThreadsCountChangeElement(1, 388.969, -1, false),
            //    new ThreadsCountChangeElement(2, 388.97, +1, true),
            //    new ThreadsCountChangeElement(1, 388.97, +0, true),
            //    new ThreadsCountChangeElement(1, 389.07, -0, false),
            //    new ThreadsCountChangeElement(2, 389.22, -1, false),
            //    new ThreadsCountChangeElement(0, 389.22, +1, true),
            //    new ThreadsCountChangeElement(2, 389.221, +0, true),
            //    new ThreadsCountChangeElement(2, 389.321, -0, false),
            //    new ThreadsCountChangeElement(0, 389.421, -1, false),
            //    new ThreadsCountChangeElement(1, 389.422, +1, true),
            //    new ThreadsCountChangeElement(0, 389.423, +0, true),
            //    new ThreadsCountChangeElement(0, 389.524, -0, false),
            //    new ThreadsCountChangeElement(1, 389.623, -1, false),
            //    new ThreadsCountChangeElement(2, 389.624, +1, true),
            //    new ThreadsCountChangeElement(2, 389.874, -1, false),
            //    new ThreadsCountChangeElement(0, 389.875, +1, true),
            //    new ThreadsCountChangeElement(0, 390.075, -1, false)
            //};

            // 8 threads.
            // 4 procedures with parallel part 3.
            //int master_threads = 4;
            //int max_threads = 8;
            //ThreadsCountChangeElement[] els =
            //{
            //    new ThreadsCountChangeElement(2, 408.677, +2, true),
            //    new ThreadsCountChangeElement(1, 408.679, +2, true),
            //    new ThreadsCountChangeElement(2, 409.007, -2, false),
            //    new ThreadsCountChangeElement(3, 409.008, +2, true),
            //    new ThreadsCountChangeElement(2, 409.008, +0, true),
            //    new ThreadsCountChangeElement(1, 409.052, -2, false),
            //    new ThreadsCountChangeElement(1, 409.053, +0, true),
            //    new ThreadsCountChangeElement(0, 409.055, +2, true),
            //    new ThreadsCountChangeElement(2, 409.112, -0, false),
            //    new ThreadsCountChangeElement(1, 409.16, -0, false),
            //    new ThreadsCountChangeElement(3, 409.309, -2, false),
            //    new ThreadsCountChangeElement(3, 409.311, +0, true),
            //    new ThreadsCountChangeElement(1, 409.312, +2, true),
            //    new ThreadsCountChangeElement(0, 409.385, -2, false),
            //    new ThreadsCountChangeElement(0, 409.387, +0, true),
            //    new ThreadsCountChangeElement(2, 409.389, +2, true),
            //    new ThreadsCountChangeElement(3, 409.412, -0, false),
            //    new ThreadsCountChangeElement(0, 409.66, -0, false),
            //    new ThreadsCountChangeElement(1, 409.68, -2, false),
            //    new ThreadsCountChangeElement(1, 409.688, +0, true),
            //    new ThreadsCountChangeElement(0, 409.688, +2, true),
            //    new ThreadsCountChangeElement(2, 409.751, -2, false),
            //    new ThreadsCountChangeElement(2, 409.753, +0, true),
            //    new ThreadsCountChangeElement(3, 409.76, +2, true),
            //    new ThreadsCountChangeElement(1, 409.789, -0, false),
            //    new ThreadsCountChangeElement(2, 409.856, -0, false),
            //    new ThreadsCountChangeElement(0, 410.065, -2, false),
            //    new ThreadsCountChangeElement(1, 410.067, +2, true),
            //    new ThreadsCountChangeElement(0, 410.07, +0, true),
            //    new ThreadsCountChangeElement(3, 410.138, -2, false),
            //    new ThreadsCountChangeElement(3, 410.139, +0, true),
            //    new ThreadsCountChangeElement(0, 410.17, -0, false),
            //    new ThreadsCountChangeElement(0, 410.17, +2, true),
            //    new ThreadsCountChangeElement(3, 410.241, -0, false),
            //    new ThreadsCountChangeElement(1, 410.448, -2, false),
            //    new ThreadsCountChangeElement(2, 410.448, +2, true),
            //    new ThreadsCountChangeElement(0, 410.54, -2, false),
            //    new ThreadsCountChangeElement(3, 410.544, +2, true),
            //    new ThreadsCountChangeElement(2, 410.849, -2, false),
            //    new ThreadsCountChangeElement(3, 410.853, -2, false),
            //};

            // 16 threads.
            // 8 procedures with parallel part 5.
            int master_threads = 8;
            int max_threads = 16;
            ThreadsCountChangeElement[] els =
            {
new ThreadsCountChangeElement(5, 5577833.0099753961, +4, true),
new ThreadsCountChangeElement(3, 5577833.0099943206, +4, true),
new ThreadsCountChangeElement(5, 5577833.5124745565, -4, false),
new ThreadsCountChangeElement(0, 5577833.5125041064, +4, true),
new ThreadsCountChangeElement(5, 5577833.5125268744, +0, true),
new ThreadsCountChangeElement(3, 5577833.5471619712, -4, false),
new ThreadsCountChangeElement(4, 5577833.5471749380, +4, true),
new ThreadsCountChangeElement(3, 5577833.5471887691, +0, true),
new ThreadsCountChangeElement(5, 5577833.6125374380, -0, false),
new ThreadsCountChangeElement(3, 5577833.6471966403, -0, false),
new ThreadsCountChangeElement(0, 5577834.0340576628, -4, false),
new ThreadsCountChangeElement(7, 5577834.0340704927, +4, true),
new ThreadsCountChangeElement(0, 5577834.0346367927, +0, true),
new ThreadsCountChangeElement(4, 5577834.0655096648, -4, false),
new ThreadsCountChangeElement(2, 5577834.0655218307, +4, true),
new ThreadsCountChangeElement(4, 5577834.0655408408, +0, true),
new ThreadsCountChangeElement(0, 5577834.1346462630, -0, false),
new ThreadsCountChangeElement(4, 5577834.1655496806, -0, false),
new ThreadsCountChangeElement(7, 5577834.5444746763, -4, false),
new ThreadsCountChangeElement(1, 5577834.5444871774, +4, true),
new ThreadsCountChangeElement(7, 5577834.5445210636, +0, true),
new ThreadsCountChangeElement(2, 5577834.5842119688, -4, false),
new ThreadsCountChangeElement(5, 5577834.5842235191, +4, true),
new ThreadsCountChangeElement(2, 5577834.5842359466, +0, true),
new ThreadsCountChangeElement(7, 5577834.6445294069, -0, false),
new ThreadsCountChangeElement(2, 5577834.6842435403, -0, false),
new ThreadsCountChangeElement(1, 5577835.0565129193, -4, false),
new ThreadsCountChangeElement(7, 5577835.0565260584, +4, true),
new ThreadsCountChangeElement(1, 5577835.0565369902, +0, true),
new ThreadsCountChangeElement(5, 5577835.1140246792, -4, false),
new ThreadsCountChangeElement(2, 5577835.1140343836, +4, true),
new ThreadsCountChangeElement(5, 5577835.1140469294, +0, true),
new ThreadsCountChangeElement(1, 5577835.1565456940, -0, false),
new ThreadsCountChangeElement(5, 5577835.2140535619, -0, false),
new ThreadsCountChangeElement(7, 5577835.5742434701, -4, false),
new ThreadsCountChangeElement(1, 5577835.5742751639, +4, true),
new ThreadsCountChangeElement(7, 5577835.5742879147, +0, true),
new ThreadsCountChangeElement(2, 5577835.6452569077, -4, false),
new ThreadsCountChangeElement(5, 5577835.6452702312, +4, true),
new ThreadsCountChangeElement(2, 5577835.6452785227, +0, true),
new ThreadsCountChangeElement(7, 5577835.6742966063, -0, false),
new ThreadsCountChangeElement(2, 5577835.7452854794, -0, false),
new ThreadsCountChangeElement(1, 5577836.1042396221, -4, false),
new ThreadsCountChangeElement(2, 5577836.1042530127, +4, true),
new ThreadsCountChangeElement(1, 5577836.1042645304, +0, true),
new ThreadsCountChangeElement(5, 5577836.1640223833, -4, false),
new ThreadsCountChangeElement(4, 5577836.1640571915, +4, true),
new ThreadsCountChangeElement(1, 5577836.2042734539, -0, false),
new ThreadsCountChangeElement(2, 5577836.6178896176, -4, false),
new ThreadsCountChangeElement(1, 5577836.6179036163, +4, true),
new ThreadsCountChangeElement(4, 5577836.6750670411, -4, false),
new ThreadsCountChangeElement(0, 5577836.6750839837, +4, true),
new ThreadsCountChangeElement(4, 5577836.6750964643, +0, true),
new ThreadsCountChangeElement(4, 5577836.7751029292, -0, false),
new ThreadsCountChangeElement(1, 5577837.1205102904, -4, false),
new ThreadsCountChangeElement(7, 5577837.1205306854, +4, true),
new ThreadsCountChangeElement(0, 5577837.1930591632, -4, false),
new ThreadsCountChangeElement(4, 5577837.1930715563, +4, true),
new ThreadsCountChangeElement(0, 5577837.1930795750, +0, true),
new ThreadsCountChangeElement(0, 5577837.2930873139, -0, false),
new ThreadsCountChangeElement(7, 5577837.6250944706, -4, false),
new ThreadsCountChangeElement(0, 5577837.6251213914, +4, true),
new ThreadsCountChangeElement(4, 5577837.6941649364, -4, false),
new ThreadsCountChangeElement(6, 5577837.6941757919, +4, true),
new ThreadsCountChangeElement(0, 5577838.1252781218, -4, false),
new ThreadsCountChangeElement(3, 5577838.1253044363, +4, true),
new ThreadsCountChangeElement(6, 5577838.1953132842, -4, false),
new ThreadsCountChangeElement(6, 5577838.1953254333, +0, true),
new ThreadsCountChangeElement(6, 5577838.2953344677, -0, false),
new ThreadsCountChangeElement(6, 5577838.2953495011, +4, true),
new ThreadsCountChangeElement(3, 5577838.6254296834, -4, false),
new ThreadsCountChangeElement(3, 5577838.6254535802, +0, true),
new ThreadsCountChangeElement(3, 5577838.7254626239, -0, false),
new ThreadsCountChangeElement(3, 5577838.7254729578, +4, true),
new ThreadsCountChangeElement(6, 5577838.7961322945, -4, false),
new ThreadsCountChangeElement(6, 5577838.7961454904, +0, true),
new ThreadsCountChangeElement(6, 5577838.8961538943, -0, false),
new ThreadsCountChangeElement(6, 5577838.8961666226, +4, true),
new ThreadsCountChangeElement(3, 5577839.2255727733, -4, false),
new ThreadsCountChangeElement(6, 5577839.4085708056, -4, false)
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

                            case 3:
                                c = Colors.Yellow;
                                break;

                            case 4:
                                c = Colors.SteelBlue;
                                break;

                            case 5:
                                c = Colors.Coral;
                                break;

                            case 6:
                                c = Colors.Khaki;
                                break;

                            case 7:
                                c = Colors.Orange;
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
