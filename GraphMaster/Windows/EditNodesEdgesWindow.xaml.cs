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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;

using Lib.Draw;
using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;

using SWMColor = System.Windows.Media.Color;
using SWMBrushes = System.Windows.Media.Brushes;

namespace GraphMaster.Windows
{
    /// <summary>
    /// Interaction logic for EditNodesEdgesWindow.xaml
    /// </summary>
    public partial class EditNodesEdgesWindow : Window
    {
        /// <summary>
        /// Node for edit single node.
        /// </summary>
        public Node Node = null;

        /// <summary>
        /// Edge for edit single edge.
        /// </summary>
        public Edge Edge = null;

        /// <summary>
        /// Nodes list for edit many nodes.
        /// </summary>
        public List<Node> Nodes = null;

        /// <summary>
        /// Edges list for edit many edges.
        /// </summary>
        public List<Edge> Edges = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditNodesEdgesWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Switch on node draw properties.
        /// </summary>
        /// <param name="dp">draw properties</param>
        private void NodeDrawPropertiesOn(NodeDrawProperties dp)
        {
            NodeHasDrawPropertiesCB.IsChecked = true;
            NodeInnerRadiusTB.IsEnabled = true;
            NodeBorderRadiusTB.IsEnabled = true;
            NodeColorTB.IsEnabled = true;
            NodeBorderColorTB.IsEnabled = true;
            NodeLabelVisibilityCB.IsEnabled = true;
            NodeLabelOffsetX_TB.IsEnabled = true;
            NodeLabelOffsetY_TB.IsEnabled = true;
            NodeFontSizeTB.IsEnabled = true;
            NodeInnerRadiusTB.Text = dp.InnerRadius.ToString();
            NodeBorderRadiusTB.Text = dp.BorderRadius.ToString();
            NodeColorTB.Background = new SolidColorBrush(dp.Color.ToSWMColor());
            NodeBorderColorTB.Background = new SolidColorBrush(dp.BorderColor.ToSWMColor());
            if (dp.LabelVisibility == Lib.DataStruct.Graph.DrawProperties.Visibility.No)
            {
                NodeLabelVisibilityCB.SelectedIndex = 0;
            }
            else if (dp.LabelVisibility == Lib.DataStruct.Graph.DrawProperties.Visibility.Yes)
            {
                NodeLabelVisibilityCB.SelectedIndex = 1;
            }
            else if (dp.LabelVisibility == Lib.DataStruct.Graph.DrawProperties.Visibility.Parent)
            {
                NodeLabelVisibilityCB.SelectedIndex = 2;
            }
            NodeLabelOffsetX_TB.Text = dp.LabelOffset.X.ToString();
            NodeLabelOffsetY_TB.Text = dp.LabelOffset.Y.ToString();
            NodeFontSizeTB.Text = dp.FontSize.ToString();
        }

        /// <summary>
        /// Switch off node draw properties.
        /// </summary>
        private void NodeDrawPropertiesOff()
        {
            NodeHasDrawPropertiesCB.IsChecked = false;
            NodeInnerRadiusTB.IsEnabled = false;
            NodeBorderRadiusTB.IsEnabled = false;
            NodeColorTB.IsEnabled = false;
            NodeBorderColorTB.IsEnabled = false;
            NodeLabelVisibilityCB.IsEnabled = false;
            NodeLabelOffsetX_TB.IsEnabled = false;
            NodeLabelOffsetY_TB.IsEnabled = false;
            NodeFontSizeTB.IsEnabled = false;
            NodeInnerRadiusTB.Text = "";
            NodeBorderRadiusTB.Text = "";
            NodeColorTB.Background = SWMBrushes.Black;
            NodeBorderColorTB.Background = SWMBrushes.Black;
            NodeLabelVisibilityCB.SelectedIndex = 0;
            NodeLabelOffsetX_TB.Text = "";
            NodeLabelOffsetY_TB.Text = "";
            NodeFontSizeTB.Text = "";
        }

        /// <summary>
        /// Set node.
        /// </summary>
        private void SetNode()
        {
            NodeLabelTB.Text = Node.Label;

            if (Node.DrawProperties != null)
            {
                NodeDrawPropertiesOn(Node.DrawProperties);
            }
            else
            {
                NodeDrawPropertiesOff();
            }
        }

        /// <summary>
        /// Switch on edge draw properties.
        /// </summary>
        /// <param name="dp">draw properties</param>
        private void EdgeDrawPropertiesOn(EdgeDrawProperties dp)
        {
            EdgeHasDrawPropertiesCB.IsChecked = true;
            EdgeColorTB.IsEnabled = true;
            EdgeThicknessTB.IsEnabled = true;
            EdgeNodesMarginTB.IsEnabled = true;
            EdgeColorTB.Background = new SolidColorBrush(dp.Color.ToSWMColor());
            EdgeThicknessTB.Text = dp.Thickness.ToString();
            EdgeNodesMarginTB.Text = dp.NodesMargin.ToString();
        }

        /// <summary>
        /// Switch off edge draw properties.
        /// </summary>
        private void EdgeDrawPropertiesOff()
        {
            EdgeHasDrawPropertiesCB.IsChecked = false;
            EdgeColorTB.IsEnabled = false;
            EdgeThicknessTB.IsEnabled = false;
            EdgeNodesMarginTB.IsEnabled = false;
            EdgeColorTB.Background = SWMBrushes.Black;
            EdgeThicknessTB.Text = "";
            EdgeNodesMarginTB.Text = "";
        }

        /// <summary>
        /// Set edge.
        /// </summary>
        private void SetEdge()
        {
            EdgeLabelTB.Text = Edge.Label;

            if (Edge.DrawProperties != null)
            {
                EdgeDrawPropertiesOn(Edge.DrawProperties);
            }
            else
            {
                EdgeDrawPropertiesOff();
            }
        }

        /// <summary>
        /// Window loaded.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Node != null)
            {
                // Single node.
                NodeNodesGB.Header = "Node";
                SetNode();
            }
            else if (Nodes != null)
            {
                // Many nodes.
                NodeNodesGB.Header = "Nodes";
                NodeLabelTB.IsEnabled = false;
                NodeLabelTB.Text = "inaccessible";
                NodeDrawPropertiesOff();
            }
            else
            {
                // No nodes.
                NodeNodesGB.Header = "No nodes";
                NodeNodesGB.IsEnabled = false;
                NodeHasDrawPropertiesCB.IsEnabled = false;
                NodeLabelTB.IsEnabled = false;
                NodeLabelTB.Text = "inaccessible";
                NodeDrawPropertiesOff();
            }

            if (Edge != null)
            {
                // Single edge.
                EdgeEdgesGB.Header = "Edge";
                SetEdge();
            }
            else if (Edges != null)
            {
                // Many edges.
                EdgeEdgesGB.Header = "Edges";
                EdgeLabelTB.IsEnabled = false;
                EdgeLabelTB.Text = "inaccessible";
                EdgeDrawPropertiesOff();
            }
            else
            {
                // No edges.
                EdgeEdgesGB.Header = "No edges";
                EdgeEdgesGB.IsEnabled = false;
                EdgeHasDrawPropertiesCB.IsEnabled = false;
                EdgeLabelTB.IsEnabled = false;
                EdgeLabelTB.Text = "inaccessible";
                EdgeDrawPropertiesOff();
            }
        }

        /// <summary>
        /// Checkbox for node draw properties.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void NodeHasDrawPropertiesCB_Checked(object sender, RoutedEventArgs e)
        {
            NodeDrawPropertiesOn(new NodeDrawProperties());
        }

        /// <summary>
        /// Uncheck node draw properties.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void NodeHasDrawPropertiesCB_Unchecked(object sender, RoutedEventArgs e)
        {
            NodeDrawPropertiesOff();
        }

        /// <summary>
        /// Check edge draw properties enabled.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void EdgeHasDrawPropertiesCB_Checked(object sender, RoutedEventArgs e)
        {
            EdgeDrawPropertiesOn(new EdgeDrawProperties());
        }

        /// <summary>
        /// Uncheck edge draw properties enabled.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void EdgeHasDrawPropertiesCB_Unchecked(object sender, RoutedEventArgs e)
        {
            EdgeDrawPropertiesOff();
        }

        /// <summary>
        /// Get color.
        /// </summary>
        /// <param name="c">initial color</param>
        /// <returns>color</returns>
        private SWMColor GetColor(SWMColor c)
        {
            ColorDialog d = new ColorDialog();

            d.Color = new Lib.Draw.Color(c).ToSDColor();

            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return new Lib.Draw.Color(d.Color).ToSWMColor();
            }

            return c;
        }

        /// <summary>
        /// Node color change click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void NodeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (NodeColorTB.Background as SolidColorBrush).Color;
            NodeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Node border color change click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void NodeBorderColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (NodeBorderColorTB.Background as SolidColorBrush).Color;
            NodeBorderColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Edge color change click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void EdgeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (EdgeColorTB.Background as SolidColorBrush).Color;
            EdgeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Get node draw properties from the form.
        /// </summary>
        /// <returns>node draw properties</returns>
        private NodeDrawProperties GetNodeDrawProperties()
        {
            NodeDrawProperties dp = new NodeDrawProperties();

            dp.InnerRadius = Lib.GUI.WPF.IO.GetDouble(NodeInnerRadiusTB);
            dp.BorderRadius = Lib.GUI.WPF.IO.GetDouble(NodeBorderRadiusTB);
            dp.Color = new Lib.Draw.Color((NodeColorTB.Background as SolidColorBrush).Color);
            dp.BorderColor = new Lib.Draw.Color((NodeBorderColorTB.Background as SolidColorBrush).Color);
            switch (NodeLabelVisibilityCB.SelectedIndex)
            {
                case 0:
                    dp.LabelVisibility = Lib.DataStruct.Graph.DrawProperties.Visibility.No;
                    break;

                case 1:
                    dp.LabelVisibility = Lib.DataStruct.Graph.DrawProperties.Visibility.Yes;
                    break;

                case 2:
                    dp.LabelVisibility = Lib.DataStruct.Graph.DrawProperties.Visibility.Parent;
                    break;

                default:
                    throw new ApplicationException();
            }
            dp.LabelOffset = new Lib.Maths.Geometry.Vector(Lib.GUI.WPF.IO.GetDouble(NodeLabelOffsetX_TB),
                                                           Lib.GUI.WPF.IO.GetDouble(NodeLabelOffsetY_TB));
            dp.FontSize = Lib.GUI.WPF.IO.GetDouble(NodeFontSizeTB);

            return dp;
        }

        /// <summary>
        /// Get edge draw properties from the form.
        /// </summary>
        /// <returns>edge draw properties</returns>
        private EdgeDrawProperties GetEdgeDrawProperties()
        {
            EdgeDrawProperties dp = new EdgeDrawProperties();

            dp.Color = new Lib.Draw.Color((EdgeColorTB.Background as SolidColorBrush).Color);
            dp.Thickness = Lib.GUI.WPF.IO.GetDouble(EdgeThicknessTB);
            dp.NodesMargin = Lib.GUI.WPF.IO.GetDouble(EdgeNodesMarginTB);

            return dp;
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            if (Node != null)
            {
                // Single node.
                Node.Label = NodeLabelTB.Text;
                Node.DrawProperties = ((bool)NodeHasDrawPropertiesCB.IsChecked)
                                      ? GetNodeDrawProperties()
                                      : null;
            }
            else if (Nodes != null)
            {
                // Many nodes.
                foreach (Node node in Nodes)
                {
                    node.DrawProperties = ((bool)NodeHasDrawPropertiesCB.IsChecked)
                                          ? GetNodeDrawProperties()
                                          : null;
                }
            }

            if (Edge != null)
            {
                // Single edge.
                Edge.Label = EdgeLabelTB.Text;
                Edge.DrawProperties = ((bool)EdgeHasDrawPropertiesCB.IsChecked)
                                      ? GetEdgeDrawProperties()
                                      : null;
            }
            else if (Edges != null)
            {
                // Many edges.
                foreach (Edge edge in Edges)
                {
                    edge.DrawProperties = ((bool)EdgeHasDrawPropertiesCB.IsChecked)
                                          ? GetEdgeDrawProperties()
                                          : null;
                }
            }

            Close();
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing.
            Close();
        }
    }
}
