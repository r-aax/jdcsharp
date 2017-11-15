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
using System.Threading;

using Lib.Maths.Numbers;
using Lib.Draw;
using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Load;
using Lib.DataStruct.Graph.Partitioning;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using SWPoint = System.Windows.Point;
using SWRect = System.Windows.Rect;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using LVector = Lib.Maths.Geometry.Vector;
using LPoint = Lib.Maths.Geometry.Point;
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
            Drawer.DrawText(Drawer.Scaler.F(new LPoint(15.0, DrawAreaC.ActualHeight - 25.0)),
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
            Drawer.Rect.RelMove(new LVector(Parameters.AreaMoveCoefficient, 0.0));
            Paint();
        }

        /// <summary>
        /// Move to the left.
        /// </summary>
        private void AreaLeft()
        {
            Drawer.Rect.RelMove(new LVector(-Parameters.AreaMoveCoefficient, 0.0));
            Paint();
        }

        /// <summary>
        /// Move up.
        /// </summary>
        private void AreaUp()
        {
            Drawer.Rect.RelMove(new LVector(0.0, Parameters.AreaMoveCoefficient));
            Paint();
        }

        /// <summary>
        /// Move down.
        /// </summary>
        private void AreaDown()
        {
            Drawer.Rect.RelMove(new LVector(0.0, -Parameters.AreaMoveCoefficient));
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
        /// Get point of event.
        /// </summary>
        /// <param name="e">parameters</param>
        /// <returns>point</returns>
        private LPoint GetEventPoint(System.Windows.Input.MouseEventArgs e)
        {
            SWPoint p = e.GetPosition(DrawAreaC);

            return Drawer.Scaler.F(new LPoint(p.X, p.Y));
        }

        /// <summary>
        /// Get point of event.
        /// </summary>
        /// <param name="e">parameters</param>
        /// <returns>point</returns>
        private LPoint GetEventPoint(MouseButtonEventArgs e)
        {
            SWPoint p = e.GetPosition(DrawAreaC);

            return Drawer.Scaler.F(new LPoint(p.X, p.Y));
        }

        /// <summary>
        /// Event when mouse leaves draw area.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void DrawAreaC_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LPoint fp = GetEventPoint(e);

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
            LPoint fp = GetEventPoint(e);

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
            LPoint fp = GetEventPoint(e);

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
            LPoint fp = GetEventPoint(e);

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
            LPoint fp = GetEventPoint(e);

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
                else if ((extension == ".pfg") || (extension == ".PFG"))
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
                            string extension_ibc = (extension == ".pfg") ? ".ibc" : ".IBC";

                            Graph new_graph = new Graph();

                            if (GraphLoaderPFG.LoadBlocksAdjacency(new_graph, filename, 
                                                                   filename.Replace(extension, extension_ibc),
                                                                   w.IsIBlank))
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

        /// <summary>
        /// Click on menu item with Uniform Greedy partitioning.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AlgorithmsPartitioningUG_MI_Click(object sender, RoutedEventArgs e)
        {
            int partitions_count = 5;

            EditIntWindow w = new EditIntWindow(partitions_count, "Enter partitions count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                UniformGreedyPartitioner.Partition(Graph, w.Result);
                DrawPropertiesManager.RepaintNodesAccordingToTheirLabels(Graph, w.Result);
                PictureName = "UG : " + PartitioningStatistics.PartitioningQualityDescription(Graph);
                Paint();
            }
        }

        /// <summary>
        /// Click on menu item with Random Volume Points partitioning.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AlgorithmsPartitioningRVPS_MI_Click(object sender, RoutedEventArgs e)
        {
            int partitions_count = 5;

            EditIntWindow w = new EditIntWindow(partitions_count, "Enter partitions count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                RandomVolumePointsPartitioner.PartitionSimple(Graph, w.Result);
                DrawPropertiesManager.RepaintNodesAccordingToTheirLabels(Graph, w.Result);
                PictureName = "RVPS : " + PartitioningStatistics.PartitioningQualityDescription(Graph);
                Paint();
            }
        }

        /// <summary>
        /// Click on menu item with Random Volume Points to Nearest Propagation.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AlgorithmsPartitioningRVPNP_MI_Click(object sender, RoutedEventArgs e)
        {
            int partitions_count = 5;

            EditIntWindow w = new EditIntWindow(partitions_count, "Enter partitions count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                RandomVolumePointsPartitioner.PartitionToNearestPropagation(Graph, w.Result);
                DrawPropertiesManager.RepaintNodesAccordingToTheirLabels(Graph, w.Result);
                PictureName = "RVPNP : " + PartitioningStatistics.PartitioningQualityDescription(Graph);
                Paint();
            }
        }

        /// <summary>
        /// Click on menu item with Random Volume Points with Edges Propagation (Nodes weights metric).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AlgorithmsPartitioningRVPEP_NM_MI_Click(object sender, RoutedEventArgs e)
        {
            int partitions_count = 5;

            EditIntWindow w = new EditIntWindow(partitions_count, "Enter partitions count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                RandomVolumePointsPartitioner.PartitionEdgesPropagationNodesMetric(Graph, w.Result);
                DrawPropertiesManager.RepaintNodesAccordingToTheirLabels(Graph, w.Result);
                PictureName = "RVPEP/NM : " + PartitioningStatistics.PartitioningQualityDescription(Graph);
                Paint();
            }
        }

        /// <summary>
        /// Click on menu item with Random Volume Points with Edges Propagation (Edges weights metric).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AlgorithmsPartitioningRVPEP_EM_MI_Click(object sender, RoutedEventArgs e)
        {
            int partitions_count = 5;

            EditIntWindow w = new EditIntWindow(partitions_count, "Enter partitions count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                RandomVolumePointsPartitioner.PartitionEdgesPropagationEdgesMetric(Graph, w.Result);
                DrawPropertiesManager.RepaintNodesAccordingToTheirLabels(Graph, w.Result);
                PictureName = "RVPEP/EM : " + PartitioningStatistics.PartitioningQualityDescription(Graph);
                Paint();
            }
        }

        /// <summary>
        /// Test random points for graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestRandomPoints_MI_Click(object sender, RoutedEventArgs e)
        {
            int count = 5;

            EditIntWindow w = new EditIntWindow(count, "Enter points count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Parallelepiped par = Graph.WraparoundParallelepiped();

                LPoint[] ps = Lib.Maths.Geometry.Geometry3D.Generator.UniformPointsInParallelepiped(w.Result, par); 

                for (int i = 0; i < w.Result; i++)
                {
                    Node n = Graph.AddNode();
                    n.P = ps[i];
                    n.CreateOwnDrawProperties();
                    n.DrawProperties.BorderRadius = 8.0;
                    n.DrawProperties.InnerRadius = 8.0;
                    n.DrawProperties.Color = new Lib.Draw.Color(Colors.Black);
                }

                Paint();
            }
        }

        /// <summary>
        /// Test partitioning algorithms.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TestPartitioning_MI_Click(object sender, RoutedEventArgs e)
        {
            LB.Items.Clear();

            int pc = 32;
            double eps1 = 0.5;
            double eps2 = 0.01;

            // Get processes count.
            EditIntWindow w = new EditIntWindow(pc, "Enter points count");
            w.ShowDialog();
            if (!w.IsAccepted)
            {
                return;
            }
            pc = w.Result;

            // Title.
            LB.Items.Add(String.Format("RVPEP pc = {0}", pc));

            // Partition.
            LPoint[] points = RandomVolumePointsPartitioner.RandomPoints(Graph, pc);
            double alpha = 1.0;
            while (alpha >= 0.0)
            {
                RandomVolumePointsPartitioner.PartitionEdgesPropagation(Graph, points, pc, alpha);
                LB.Items.Add(PartitioningStatistics.PartitioningQualityDescription(Graph));
                alpha -= eps1;
            }

            DialogResult res = System.Windows.Forms.MessageBox.Show("Continue with eps = 0.01?", "Long eps confirm",
                                                                    MessageBoxButtons.YesNo);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                List<string> list = new List<string>();

                // Partition (second wave).
                alpha = 1.0;
                LB.Items.Add("---");
                while (alpha >= 0.0)
                {
                    RandomVolumePointsPartitioner.PartitionEdgesPropagation(Graph, points, pc, alpha);
                    LB.Items.Add(PartitioningStatistics.InterpartitionEdgesFactor(Graph));
                    list.Add(PartitioningStatistics.DeviationMaxPartitionWeightFromAvg(Graph).ToString());
                    alpha -= eps2;
                }

                LB.Items.Add("===");
                for (int i = 0; i < list.Count; i++)
                {
                    LB.Items.Add(list[i]);
                }
            }
        }

        /// <summary>
        /// Save text.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void TextSave_MI_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text (*.txt)|*.txt";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    for (int i = 0; i < LB.Items.Count; i++)
                    {
                        sw.Write(LB.Items[i] + "\n");
                    }

                    sw.Close();
                }
            }
        }
    }
}
