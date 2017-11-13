using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using DrawBox.Draw;

using SWRect = System.Windows.Rect;

using DrawBox.DrawMaster.PlanOMPDrawMaster;
using DrawBox.DrawMaster.UniformPointsDrawMaster;

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

                case DrawElement.UniformPoints:
                    UniformPointsDrawMaster.Draw(DrawAreaC);
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
        private void DrawPlanOMP4th3p2w_1_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test4th3p2w_1;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (4th 3p 2w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP4th3p2w_2_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test4th3p2w_2;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (4th 3p 2w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP4th3p2w_3_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test4th3p2w_3;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (4th 3p 2w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP4th3p2w_4_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test4th3p2w_4;
            Paint();
        }

        /// <summary>
        /// Draw Plan OMP menu item (4th 3p 2w).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DrawPlanOMP4th3p2w_5_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.PlanOMP;
            Test = PlanOMPTest.Test4th3p2w_5;
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
        /// Draw test of uniform points.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PEl = DrawElement.UniformPoints;
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

        /// <summary>
        /// Move mouse event in status bar.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void StatusBar_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Paint();
        }
    }
}
