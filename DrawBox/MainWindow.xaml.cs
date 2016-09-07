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

using DrawBox.DrawMaster.PlanOMPDrawMaster;

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
        /// Test.
        /// </summary>
        private PlanOMPTest Test = PlanOMPTest.Test244thGround;

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
                    PlanOMPDrawMaster.Draw(DrawAreaC, Test);
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
        /// Draw Plan OMP menu item (4th 3p 2w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP4th3p2w_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test4th3p2w;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (8th 4p 3w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP8th4p3w_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test8th4p3w;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (16th 7p 4w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP16th7p4w_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test16th7p4w;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (16th 8p 5w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP16th8p5w_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test16th8p5w;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (244th 34p 21w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP244th34p21w_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test244th34p21w;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (244th ground grid based test example).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP244thGround_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test244thGround;
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
