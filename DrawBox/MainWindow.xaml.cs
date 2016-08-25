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
using DrawBox.Draw;
using DrawBox.DrawMaster;

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Element of painting.
        /// </summary>
        private DrawElement PEl = DrawElement.None;

        /// <summary>
        /// Threads in examle for PlanOMP.
        /// </summary>
        private int ThreadsInExample = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Paint.
        /// </summary>
        private void Paint()
        {
            switch (PEl)
            {
                case DrawElement.Test:
                    DrawMaster.TestDrawMaster.TestDrawMaster.Draw(DrawAreaC);
                    break;

                case DrawElement.PlanOMP:
                    DrawMaster.PlanOMPDrawMaster.PlanOMPDrawMaster.Draw(DrawAreaC, ThreadsInExample);
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
            PEl = DrawElement.Test;
            Paint();
        }

        /// <summary>
        /// Click on Draw PlanOMP 4 threads example.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP4MI_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            ThreadsInExample = 4;
            Paint();
        }

        /// <summary>
        /// Click on Draw PlanOMP 8 threads example.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP8MI_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            ThreadsInExample = 8;
            Paint();
        }

        /// <summary>
        /// Click on Draw PlanOMP 16 threads example.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP16MI_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            ThreadsInExample = 16;
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
