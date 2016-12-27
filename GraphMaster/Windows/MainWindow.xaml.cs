// Author: Alexey Rybakov

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

using Lib.Maths.Numbers;
using Lib.Draw;
using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Load;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using SWPoint = System.Windows.Point;
using SWRect = System.Windows.Rect;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using RectDrawerWPF = Lib.Draw.WPF.RectDrawer;
using GraphMaster.Tools;
using Lib.GUI.WPF;

namespace GraphMaster.Windows
{
    /// <summary>
    /// MainWindow.xaml logic.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Graph.
        /// </summary>
        private Graph Graph = null;

        /// <summary>
        /// Minimal graph order.
        /// </summary>
        private readonly int MinOrder = 5;

        /// <summary>
        /// Miximum graph order.
        /// </summary>
        private readonly int MaxOrder = 10;

        /// <summary>
        /// Random order.
        /// </summary>
        private int RandomOrder
        {
            get
            {
                return Randoms.RandomInInterval(MinOrder, MaxOrder);
            }
        }

        /// <summary>
        /// Draw master.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Drawer.
        /// </summary>
        private GraphRectDrawer GraphDrawer = null;

        /// <summary>
        /// Relative area.
        /// </summary>
        private static readonly Rect2D RelDrawRect = new Rect2D(100.0, 100.0);

        /// <summary>
        /// Outer circle coefficient.
        /// </summary>
        private static readonly double ScaleK = 0.8;

        /// <summary>
        /// Outer circle.
        /// </summary>
        private static readonly Circle Circle = RelDrawRect.CenteredCircle(ScaleK);

        /// <summary>
        /// Outer shpere.
        /// </summary>
        private static readonly Sphere Sphere = Circle.Extended();

        /// <summary>
        /// Outer rectangle.
        /// </summary>
        private static readonly Rect2D Rect = RelDrawRect.CenterScaled(ScaleK);

        /// <summary>
        /// <c>GUI</c> manager.
        /// </summary>
        private static GUIProcessor GUIProcessor = new GUIProcessor();

        /// <summary>
        /// Picture name.
        /// </summary>
        private string PictureName = "test";

        /// <summary>
        /// Main form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ResetModeButtons();

            // Init graph.
            Graph = GraphCreator.ErdosRenyiBinomialRandomGraph(20, 0.5, Circle);
        }

        /// <summary>
        /// Set enable buttons.
        /// </summary>
        public void EnableControls()
        {
            bool is_3d = Graph.Is3D;

            // Rotate.
            RotX_CW_B.IsEnabled = is_3d;
            RotX_CCW_B.IsEnabled = is_3d;
            RotY_CW_B.IsEnabled = is_3d;
            RotY_CCW_B.IsEnabled = is_3d;
            AreaRotateX_CW.IsEnabled = is_3d;
            AreaRotateX_CCW.IsEnabled = is_3d;
            AreaRotateY_CW.IsEnabled = is_3d;
            AreaRotateY_CCW.IsEnabled = is_3d;

            // Change dimensionality.
            Transform2DTo3D.IsEnabled = !is_3d;
            Transform3DTo2D.IsEnabled = is_3d;
        }

        /// <summary>
        /// Change button colors.
        /// </summary>
        /// <param name="b">button</param>
        /// <param name="brush">brush</param>
        /// <param name="border_brush">border brush</param>
        private void SetButtonColors(System.Windows.Controls.Button b, Brush brush, Brush border_brush)
        {
            b.Background = brush;
            b.BorderBrush = border_brush;
        }

        /// <summary>
        /// Reset mode buttons.
        /// </summary>
        private void ResetModeButtons()
        {
            SetButtonColors(ModeSelectB, Brushes.LightGray, Brushes.Black);
            SetButtonColors(ModeMoveB, Brushes.LightGray, Brushes.Black);
        }

        /// <summary>
        /// Paint.
        /// </summary>
        private void Paint()
        {
            // If draw master is not created then create it.
            if (Drawer == null)
            {
                Drawer = new RectDrawerWPF(RelDrawRect, DrawAreaC, false, true);
                GraphDrawer = new GraphRectDrawer(Drawer);
            }

            // Drawing.
            Drawer.BeginDraw();
            GraphDrawer.DrawGraph(Graph);
            if ((GUIProcessor.State == GUIState.Select) && (GUIProcessor.Node != null))
            {
                NodeDrawProperties nprops = GUIProcessor.Node.DrawProperties;
                Lib.Draw.Color save_color = nprops.Color;
                nprops.Color = Graph.DrawProperties.CapturedNodeColor; 
                GraphDrawer.DrawNode(GUIProcessor.Node, nprops);
                nprops.Color = save_color;
            }
            Drawer.DrawText(Drawer.Scaler.F(new Point2D(15.0, DrawAreaC.ActualHeight - 25.0)),
                            PictureName, 12.0, "Lucida Console");
            Drawer.EndDraw();

            // Warnings.
            // I do not want to study routed events, so make a little hack here.
            DrawAreaC.Children[0].PreviewMouseLeftButtonDown += DrawAreaC_MouseLeftButtonDown;
            DrawAreaC.Children[0].PreviewMouseLeftButtonUp += DrawAreaC_MouseLeftButtonUp;
            DrawAreaC.Children[0].PreviewMouseRightButtonDown += DrawAreaC_MouseRightButtonDown;
            DrawAreaC.Children[0].PreviewMouseRightButtonUp += DrawAreaC_MouseRightButtonUp;
                
            // Mapping information.
            BijectionStrSBLBI.Content = Drawer.GetBijectionString();

            // Enable buttons.
            EnableControls();
        }

        /// <summary>
        /// Load.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Paint();
        }

        /// <summary>
        /// Change area size.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void DrawAreaC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Paint();
        }

        /// <summary>
        /// Zoom in.
        /// </summary>
        private void ZoomIn()
        {
            // Real area have to become smaller (centered scaling).
            Drawer.Rect.CenterScale(1.0 / Parameters.AreaZoomCoefficient);
            Paint();
        }

        /// <summary>
        /// Zoom out.
        /// </summary>
        private void ZoomOut()
        {
            // Real area have to become bigger (center scaling).
            Drawer.Rect.CenterScale(Parameters.AreaZoomCoefficient);
            Paint();
        }

        /// <summary>
        /// Area managing : zoom in.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaZoomInMI_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        /// <summary>
        /// Area managing : zoom out.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaZoomOutMI_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        /// <summary>
        /// Move to the right.
        /// </summary>
        private void AreaRight()
        {
            Drawer.Rect.RelMove(new Vector2D(Parameters.AreaMoveCoefficient, 0.0));
            Paint();
        }

        /// <summary>
        /// Move to the left.
        /// </summary>
        private void AreaLeft()
        {
            Drawer.Rect.RelMove(new Vector2D(-Parameters.AreaMoveCoefficient, 0.0));
            Paint();
        }

        /// <summary>
        /// Move up.
        /// </summary>
        private void AreaUp()
        {
            Drawer.Rect.RelMove(new Vector2D(0.0, Parameters.AreaMoveCoefficient));
            Paint();
        }

        /// <summary>
        /// Move down.
        /// </summary>
        private void AreaDown()
        {
            Drawer.Rect.RelMove(new Vector2D(0.0, -Parameters.AreaMoveCoefficient));
            Paint();
        }

        /// <summary>
        /// Area managing : move to the right.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRightMI_Click(object sender, RoutedEventArgs e)
        {
            AreaRight();
        }

        /// <summary>
        /// Area managing : move to the left.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaLeftMI_Click(object sender, RoutedEventArgs e)
        {
            AreaLeft();
        }

        /// <summary>
        /// Area managing : move up.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaUpMI_Click(object sender, RoutedEventArgs e)
        {
            AreaUp();
        }

        /// <summary>
        /// Area managing : move down.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaDownMI_Click(object sender, RoutedEventArgs e)
        {
            AreaDown();
        }

        /// <summary>
        /// Set area managing parameters.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaParametersMI_Click(object sender, RoutedEventArgs e)
        {
            AreaParametersWindow window = new AreaParametersWindow();

            window.ShowDialog();
        }

        /// <summary>
        /// Save picture.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
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
        /// Rotate clockwise X.
        /// </summary>
        private void RotX_CW()
        {
            Graph.RotX(-Parameters.GraphRotAngle);
            Paint();
        }

        /// <summary>
        /// Rotate contraclockwise X.
        /// </summary>
        private void RotX_CCW()
        {
            Graph.RotX(Parameters.GraphRotAngle);
            Paint();
        }

        /// <summary>
        /// Rotate clockwise Y.
        /// </summary>
        private void RotY_CW()
        {
            Graph.RotY(-Parameters.GraphRotAngle);
            Paint();
        }

        /// <summary>
        /// Rotate contraclockwise Y.
        /// </summary>
        private void RotY_CCW()
        {
            Graph.RotY(Parameters.GraphRotAngle);
            Paint();
        }

        /// <summary>
        /// Rotate clockwise Z.
        /// </summary>
        private void RotZ_CW()
        {
            Graph.RotZ(-Parameters.GraphRotAngle);
            Paint();
        }

        /// <summary>
        /// Rotate contraclockwise Z.
        /// </summary>
        private void RotZ_CCW()
        {
            Graph.RotZ(Parameters.GraphRotAngle);
            Paint();
        }

        /// <summary>
        /// Rotate clockwise X.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRotateX_CW_Click(object sender, RoutedEventArgs e)
        {
            RotX_CW();
        }

        /// <summary>
        /// Rotate contraclockwise X.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRotateX_CCW_Click(object sender, RoutedEventArgs e)
        {
            RotX_CCW();
        }

        /// <summary>
        /// Rotate clockwise Y.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRotateY_CW_Click(object sender, RoutedEventArgs e)
        {
            RotY_CW();
        }

        /// <summary>
        /// Rotate contraclockwise Y.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRotateY_CCW_Click(object sender, RoutedEventArgs e)
        {
            RotY_CCW();
        }

        /// <summary>
        /// Rotate clockwise Z.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRotateZ_CW_Click(object sender, RoutedEventArgs e)
        {
            RotZ_CW();
        }

        /// <summary>
        /// Rotate contraclockwise Z.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRotateZ_CCW_Click(object sender, RoutedEventArgs e)
        {
            RotZ_CCW();
        }

        /// <summary>
        /// Rotate clockwise X.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RotX_CW_B_Click(object sender, RoutedEventArgs e)
        {
            RotX_CW();
        }

        /// <summary>
        /// Rotate contraclockwise X.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RotX_CCW_B_Click(object sender, RoutedEventArgs e)
        {
            RotX_CCW();
        }

        /// <summary>
        /// Rotate clockwise Y.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RotY_CW_B_Click(object sender, RoutedEventArgs e)
        {
            RotY_CW();
        }

        /// <summary>
        /// Rotate contraclockwise Y.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RotY_CCW_B_Click(object sender, RoutedEventArgs e)
        {
            RotY_CCW();
        }

        /// <summary>
        /// Rotate clockwise Z.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RotZ_CW_B_Click(object sender, RoutedEventArgs e)
        {
            RotZ_CW();
        }

        /// <summary>
        /// Rotate contraclockwise Z.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RotZ_CCW_B_Click(object sender, RoutedEventArgs e)
        {
            RotZ_CCW();
        }

        /// <summary>
        /// Example. (3-3)-cage (K(4)).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FullGraph(4, Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-4)-cage (K(3,3)).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_4_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Cage_3_4(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-5)-cage (Petersen graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_5_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PerersenGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-6)-cage (Heawood graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_6_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HeawoodGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-7)-cage (McGee graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_7_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.McGeeGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-8)-cage (Tutte-Coxeter graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_8_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.TutteCoxeterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Move to the right.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaRightB_Click(object sender, RoutedEventArgs e)
        {
            AreaRight();
        }

        /// <summary>
        /// Move to the left.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaLeftB_Click(object sender, RoutedEventArgs e)
        {
            AreaLeft();
        }

        /// <summary>
        /// Move up.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaUpB_Click(object sender, RoutedEventArgs e)
        {
            AreaUp();
        }

        /// <summary>
        /// Move down.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void AreaDownB_Click(object sender, RoutedEventArgs e)
        {
            AreaDown();
        }

        /// <summary>
        /// Zoom in.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ZoomInB_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        /// <summary>
        /// Zoom out.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ZoomOutB_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        /// <summary>
        /// Transform 2D -> 3D.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void Transform2DTo3D_Click(object sender, RoutedEventArgs e)
        {
            Graph.TransformTo3D();
            Paint();
        }

        /// <summary>
        /// Transform 3D -> 2D.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void Transform3DTo2D_Click(object sender, RoutedEventArgs e)
        {
            Graph.TransformTo2D();
            Paint();
        }

        /// <summary>
        /// Desargues graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Desargues_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DesarguesGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Heawood graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Heawood_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HeawoodGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Mobius-Kantor graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_MobiusKantor_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MobiusKantorGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Pappus graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Pappus_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PappusGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Gray graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Gray_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.GrayGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Tutte-Coxeter graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_TutteCoxeter_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.TutteCoxeterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(10, 1)</c> (10-prism).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_10_1_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Prism(10, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(5, 2)</c> (Petersen graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_5_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PerersenGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(6, 2)</c> (Durer graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_6_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DurerGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(8, 3)</c> (Mobius-Kantor graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_8_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MobiusKantorGraph(Notation.GeneralizedPetersen, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(10, 2)</c> (dodecahedron).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_10_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.GeneralizedPetersenGraph(10, 2, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(10, 3)</c> (Desargues graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_10_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DesarguesGraph(Notation.GeneralizedPetersen, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(12, 5)</c> (Nauru graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_12_5_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.NauruGraph(Notation.GeneralizedPetersen, Circle);
            Paint();
        }

        /// <summary>
        /// Random generalized Petersen graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_n_k_Click(object sender, RoutedEventArgs e)
        {
            int n = Randoms.RandomInInterval(Math.Max(2 + 1, MinOrder), MaxOrder - 1);
            int k = Randoms.RandomInInterval(1, n / 2);
            PetersenGraphParametersWindow w = new PetersenGraphParametersWindow(n, k);
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.GeneralizedPetersenGraph(w.HalfOrder, w.InnerChord, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Wagner graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Wagner_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.WagnerGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Franklin graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Franklin_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FranklinGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Frucht graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Frucht_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FruchtGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Heawood graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Heawood_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HeawoodGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Mobius-Kantor graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_MobiusKantor_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MobiusKantorGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Pappus graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Pappus_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PappusGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Desargues graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Desargues_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DesarguesGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// McGee graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_McGee_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.McGeeGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Nauru graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Nauru_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.NauruGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Tutte-Coxeter graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_TutteCoxeter_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.TutteCoxeterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Dyke graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Dyck_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DyckGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Gray graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Gray_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.GrayGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Harries graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Harries_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Harries-Wong graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_HarriesWong_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesWongGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10) Balaban cage.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_3_10_Balaban_Cage_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_10_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// Foster graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Foster_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FosterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Biggs-Smith graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_BiggsSmith_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.BiggsSmithGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-11) Balaban graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_3_11_Balaban_Cage_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_11_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// Ljubljana graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Ljubljana_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.LjubljanaGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-12) Tutte graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_3_12_Tutte_Cage_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Tutte_3_12_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10) Balaban cage.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_10_1_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_10_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10)-cage (Harries graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_10_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10)-cage (Harries-Wong graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_10_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesWongGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-11) Balaban cage.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_11_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_11_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// (3-12) Tutte graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_12_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Tutte_3_12_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// Empty graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleEmpty_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.EmptyGraph(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Full graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleFull_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.FullGraph(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Cycle.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCycle_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Cycle(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Star.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleStar_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Star(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Wheel.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleWheel_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Wheel(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Get point of event.
        /// </summary>
        /// <param name="e">parameters</param>
        /// <returns>point</returns>
        private Point2D GetEventPoint(System.Windows.Input.MouseEventArgs e)
        {
            SWPoint p = e.GetPosition(DrawAreaC);

            return Drawer.Scaler.F(new Point2D(p.X, p.Y));
        }

        /// <summary>
        /// Get point of event.
        /// </summary>
        /// <param name="e">parameters</param>
        /// <returns>point</returns>
        private Point2D GetEventPoint(MouseButtonEventArgs e)
        {
            SWPoint p = e.GetPosition(DrawAreaC);

            return Drawer.Scaler.F(new Point2D(p.X, p.Y));
        }

        /// <summary>
        /// Event when mouse leaves draw area.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void DrawAreaC_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point2D fp = GetEventPoint(e);

            switch (GUIProcessor.State)
            {
                case GUIState.Common:
                    GUIProcessor.CancelNodeDrag();
                    break;

                case GUIState.Select:
                    break;

                case GUIState.Move:
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Paint();
        }

        /// <summary>
        /// Mouse left click event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void DrawAreaC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point2D fp = GetEventPoint(e);

            switch (GUIProcessor.State)
            {
                case GUIState.Common:

                    if (Graph.Is2D)
                    {
                        GUIProcessor.TryToCaptureNode(Graph, fp);
                    }

                    break;

                case GUIState.Select:

                    Node n = Graph.FindNearestNode(fp);

                    if (n != null)
                    {
                        n.SwitchSelection();
                    }

                    break;

                case GUIState.Move:
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Paint();
        }

        /// <summary>
        /// Mouse left button up event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void DrawAreaC_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point2D fp = GetEventPoint(e);

            switch (GUIProcessor.State)
            {
                case GUIState.Common:
                    GUIProcessor.FinishNodeDrag(fp);
                    break;

                case GUIState.Select:
                    break;

                case GUIState.Move:
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Paint();
        }

        /// <summary>
        /// Right button down.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DrawAreaC_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point2D fp = GetEventPoint(e);

            switch (GUIProcessor.State)
            {
                case GUIState.Common:
                    break;

                case GUIState.Select:

                    Node n = Graph.FindNearestNode(fp);

                    if (n != null)
                    {
                        if (GUIProcessor.Node == null)
                        {
                            GUIProcessor.Node = n;
                        }
                        else if (GUIProcessor.Node == n)
                        {
                            GUIProcessor.Node = null;
                        }
                        else
                        {
                            Edge edge = Graph.FindEdge(GUIProcessor.Node, n);

                            if (edge != null)
                            {
                                edge.SwitchSelection();
                            }

                            GUIProcessor.Node = n;
                        }
                    }

                    break;

                case GUIState.Move:
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Paint();
        }

        /// <summary>
        /// Right button up.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DrawAreaC_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Paint();
        }

        /// <summary>
        /// Mouse move event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void DrawAreaC_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point2D fp = GetEventPoint(e);

            switch (GUIProcessor.State)
            {
                case GUIState.Common:
                    GUIProcessor.MoveCapturedNode(fp);
                    break;

                case GUIState.Select:
                    break;

                case GUIState.Move:
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Paint();
        }

        /// <summary>
        /// 1D grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleGrid1_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Grid1D(w.Result, Rect);
            }

            Paint();
        }
        
        /// <summary>
        /// 2D grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleGrid2_Click(object sender, RoutedEventArgs e)
        {
            Grid2DSizesWindow w = new Grid2DSizesWindow(RandomOrder, RandomOrder);
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Grid2D(w.XSize, w.YSize, Rect);
            }

            Paint();
        }

        /// <summary>
        /// 3D grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleGrid3_Click(object sender, RoutedEventArgs e)
        {
            Grid3DSizesWindow w = new Grid3DSizesWindow(RandomOrder, RandomOrder, RandomOrder);
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Grid3D(w.XSize, w.YSize, w.ZSize,
                                            Rect.Extended(Rect.YInterval));
            }

            Paint();
        }

        /// <summary>
        /// Circular graph of order 5 without red r-cliques and blue b-cluques for R(3, 3).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC5R33All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 3, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 5 without red r-cliques and blue b-cluques for R(3, 3).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC5R33Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 3, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 8 without red r-cliques and blue b-cluques for R(3, 4).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC8R34All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 4, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 8 without red r-cliques and blue b-cluques for R(3, 4).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC8R34Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 4, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 13 without red r-cliques and blue b-cluques for R(3, 5).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC13R35All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 5, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 13 without red r-cliques and blue b-cluques for R(3, 5).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC13R35Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 5, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 17 without red r-cliques and blue b-cluques for R(4, 4).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC17R44All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 4, true, Circle);
            Paint();
        }
        
        /// <summary>
        /// Circular graph of order 17 without red r-cliques and blue b-cluques for R(4, 4).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC17R44Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 4, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 24 without red r-cliques and blue b-cluques for R(4, 5).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC24R45All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 5, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 24 without red r-cliques and blue b-cluques for R(4, 5).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC24R45Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 5, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 41 without red r-cliques and blue b-cluques for R(5, 5).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC41R55All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(5, 5, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 41 without red r-cliques and blue b-cluques for R(5, 5).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC41R55Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(5, 5, false, Circle);
            Paint();
        }

        /// <summary>
        /// Set picture  name.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void PictureNameMI_Click(object sender, RoutedEventArgs e)
        {
            PictureNameWindow window = new PictureNameWindow(PictureName);

            window.ShowDialog();
            PictureName = window.PictureName;
            Paint();
        }

        /// <summary>
        /// Circular graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCircular_Click(object sender, RoutedEventArgs e)
        {
            OrderAndIntsWindow w = new OrderAndIntsWindow(RandomOrder, new int[] { 1, 2 }, "Order and chords", "Chords");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.CircularGraph(w.Order, w.Ints, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Hatch graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleHatch_Click(object sender, RoutedEventArgs e)
        {
            OrderAndIntsWindow w = new OrderAndIntsWindow(RandomOrder, new int[] { 1, 2 }, "Order and doubled middles", "Doubled middles");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.HatchGraph(w.Order, w.Ints, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Random graph in Erdos - Renyi model (binomial).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleErdosRenyiBinomialRandom_Click(object sender, RoutedEventArgs e)
        {
            EditIntDoubleWindow w = new EditIntDoubleWindow(RandomOrder, 0.5,
                                                            "Erdos - Renyi binomial random graph parameters",
                                                            "Order", "Edge probability");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.ErdosRenyiBinomialRandomGraph(w.IntV, w.DoubleV, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Random graph in Erdos - Renyi model (uniform).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleErdosRenyiUniformRandom_Click(object sender, RoutedEventArgs e)
        {
            int n = RandomOrder;

            EditIntIntWindow w = new EditIntIntWindow(n, n * (n - 1) / 4,
                                                      "Erdos - Renyi uniform random graph parameters",
                                                      "Order", "Edges count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.ErdosRenyiUniformRandomGraph(w.Int1V, w.Int2V, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Grid in circle with center point.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleGridCircleWithCenter_Click(object sender, RoutedEventArgs e)
        {
            int n = RandomOrder;

            EditIntIntWindow w = new EditIntIntWindow(n, n * (n - 1) / 4,
                                                      "Grid in circle with center point parameters",
                                                      "Count of radiuses", "Points on radius");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.GridCicle(w.Int1V, w.Int2V, true, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Grid in circle without center point.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleGridCircleWithoutCenter_Click(object sender, RoutedEventArgs e)
        {
            int n = RandomOrder;

            EditIntIntWindow w = new EditIntIntWindow(n, n * (n - 1) / 4,
                                                      "Grid in circle with center point parameters",
                                                      "Count of radiuses", "Points on radius");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.GridCicle(w.Int1V, w.Int2V, false, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Tetrahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DTetrahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Tetrahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Cube.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DCube_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Cube(Sphere);
            Paint();
        }

        /// <summary>
        /// Octahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DOctahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Octahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Dodecahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DDodecahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Dodecahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Icosahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DIcosahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Icosahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Click on multiselect mode button.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ModeMultiSelectB_Click(object sender, RoutedEventArgs e)
        {
            ResetModeButtons();

            if (GUIProcessor.State == GUIState.Select)
            {
                GUIProcessor.SetState(GUIState.Common);
            }
            else
            {
                SetButtonColors(ModeSelectB, Brushes.Green, Brushes.Green);
                GUIProcessor.SetState(GUIState.Select);
            }
        }

        /// <summary>
        /// Click on drag mode button.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ModeDragB_Click(object sender, RoutedEventArgs e)
        {
            ResetModeButtons();

            if (GUIProcessor.State == GUIState.Move)
            {
                GUIProcessor.SetState(GUIState.Common);
            }
            else
            {
                SetButtonColors(ModeMoveB, Brushes.Green, Brushes.Green);
                GUIProcessor.SetState(GUIState.Move);
            }
        }

        /// <summary>
        /// Click on open graph menu item.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void GraphOpenMI_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string filename;
            string extension;

            ofd.Filter = "XML (*.xml)|*.xml|PFG (*.pfg)|*.pfg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofd.FileName;
                extension = Path.GetExtension(filename);

                if (extension == ".xml")
                {
                    Graph = Graph.XmlDeserialize(filename);
                    Drawer.SetRect(Graph.WraparoundRect(0.1, DrawAreaC.ActualWidth / DrawAreaC.ActualHeight));
                }
                else if (extension == ".pfg")
                {
                    LoadGraphPFGTypeSelectWindow w = new LoadGraphPFGTypeSelectWindow();
                    w.ShowDialog();

                    if (w.IsAccepted)
                    {
                        if (w.IsWhole)
                        {
                            Graph new_graph = new Graph();

                            if (GraphLoaderPFG.LoadWhole(new_graph, filename, w.IsIBlank))
                            {
                                Graph = new_graph;
                                Graph.SetStyleSimple();
                                Drawer.SetRect(Graph.WraparoundRect(0.1, DrawAreaC.ActualWidth / DrawAreaC.ActualHeight));
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Can not read graph from file " + filename);
                            }
                        }
                        else if (w.IsSkeleton)
                        {
                            Graph new_graph = new Graph();

                            if (GraphLoaderPFG.LoadSkeleton(new_graph, filename, w.IsIBlank))
                            {
                                Graph = new_graph;
                                Graph.SetStyleSimple();
                                Drawer.SetRect(Graph.WraparoundRect(0.1, DrawAreaC.ActualWidth / DrawAreaC.ActualHeight));
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Can not read graph from file " + filename);
                            }
                        }
                        else if (w.IsBlocksAdjacency)
                        {
                            Graph new_graph = new Graph();

                            if (GraphLoaderPFG.LoadBlocksAdjacency(new_graph, filename,  filename.Replace(".pfg", ".ibc"), w.IsIBlank))
                            {
                                Graph = new_graph;
                                Drawer.SetRect(Graph.WraparoundRect(0.1, DrawAreaC.ActualWidth / DrawAreaC.ActualHeight));
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Can not read graph from file " + filename);
                            }
                        }
                        else
                        {
                            Debug.Assert(false, "unknown PFG graph load type");
                        }
                    }
                }
                else
                {
                    throw new Exception("unknown extension while graph loading");
                }
            }

            Paint();
        }

        /// <summary>
        /// Click on save graph menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphSaveMI_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "XML (*.xml)|*.xml";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Graph.XmlSerialize(sfd.FileName);
            }
        }

        /// <summary>
        /// Edit button click event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ActionEditB_Click(object sender, RoutedEventArgs e)
        {
            List<Node> nodes = Graph.SelectedNodes;
            List<Edge> edges = Graph.SelectedEdges;
            int nc = nodes.Count;
            int ec = edges.Count;

            if ((nc == 0) && (ec == 0))
            {
                System.Windows.MessageBox.Show("No nodes or edges selected for editing!");
            }
            else
            {
                EditNodesEdgesWindow w = new EditNodesEdgesWindow();

                if (nc == 1)
                {
                    w.Node = nodes[0];
                }
                else if (nc > 1)
                {
                    w.Nodes = nodes;
                }

                if (ec == 1)
                {
                    w.Edge = edges[0];
                }
                else if (ec > 1)
                {
                    w.Edges = edges;
                }

                w.ShowDialog();
                Paint();
            }
        }

        /// <summary>
        /// Select all nodes click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsSelectAllNodesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SetSelection(true, true, false);
            Paint();
        }

        /// <summary>
        /// Select all edges click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsSelectAllEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SetSelection(true, false, true);
            Paint();
        }

        /// <summary>
        /// Select all nodes and edges click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsSelectAllNodesEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SetSelection(true, true, true);
            Paint();
        }

        /// <summary>
        /// Unselect all nodes click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsUnselectAllNodesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SetSelection(false, true, false);
            Paint();
        }

        /// <summary>
        /// Unselect all edges click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsUnselectAllEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SetSelection(false, false, true);
            Paint();
        }

        /// <summary>
        /// Unselect all nodes and edges click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsUnselectAllNodesEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SetSelection(false, true, true);
            Paint();
        }

        /// <summary>
        /// Invert nodes selection click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsInvertSelectionNodesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.InvertSelection(true, false);
            Paint();
        }

        /// <summary>
        /// Invert edges selection click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsInvertSelectionEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.InvertSelection(false, true);
            Paint();
        }

        /// <summary>
        /// Invert nodes and edges selection.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsInvertSelectionNodesEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.InvertSelection(true, true);
            Paint();
        }

        /// <summary>
        /// Add incident nodes click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsSelectIncidentNodesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SelectIncident(true, false);
            Paint();
        }

        /// <summary>
        /// Add incident edges click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void OperationsSelectIncidentEdgesMI_Click(object sender, RoutedEventArgs e)
        {
            Graph.SelectIncident(false, true);
            Paint();
        }

        /// <summary>
        /// Click on menu item edit graph draw properties.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void SettingsDefaultDrawPropertiesMI_Click(object sender, RoutedEventArgs e)
        {
            EditGraphDrawPropertiesWindow w = new EditGraphDrawPropertiesWindow();
            w.Graph = Graph;
            w.ShowDialog();
            Paint();
        }
    }
}
