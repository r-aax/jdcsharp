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
        private static readonly Rect2D RelDrawRect = new Rect2D(100.0, 100.0);

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
            Drawer.DrawLine(new Point2D(0.0, 0.0), new Point2D(100.0, 100.0));
            Drawer.DrawLine(new Point2D(0.0, 100.0), new Point2D(100.0, 0.0));
        }

        /// <summary>
        /// Paint Plan OMP.
        /// </summary>
        private void PaintPlanOMP()
        {
            ;
        }

        /// <summary>
        /// Paint.
        /// </summary>
        private void Paint()
        {
            CreateDrawer();
            Drawer.BeginDraw();

            switch (PEl)
            {
                case PaintElement.Test:
                    PaintTest();
                    break;

                case PaintElement.PlanOMP:
                    PaintPlanOMP();
                    break;
            }

            Drawer.EndDraw();
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
