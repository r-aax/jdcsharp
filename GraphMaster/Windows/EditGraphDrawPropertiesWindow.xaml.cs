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

using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;

using SWMColor = System.Windows.Media.Color;
using SWMBrushes = System.Windows.Media.Brushes;

namespace GraphMaster.Windows
{
    /// <summary>
    /// Interaction logic for EditGraphDrawPropertiesWindow.xaml
    /// </summary>
    public partial class EditGraphDrawPropertiesWindow : Window
    {
        /// <summary>
        /// Graph
        /// </summary>
        public Graph Graph = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditGraphDrawPropertiesWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Switch on node draw properties.
        /// </summary>
        /// <param name="dp">draw properties</param>
        private void NodeDrawPropertiesOn(NodeDrawProperties dp)
        {
            DefNodeHasDrawPropertiesCB.IsChecked = true;
            DefNodeInnerRadiusTB.IsEnabled = true;
            DefNodeBorderRadiusTB.IsEnabled = true;
            DefNodeColorTB.IsEnabled = true;
            DefNodeBorderColorTB.IsEnabled = true;
            DefNodeLabelVisibilityCB.IsEnabled = true;
            DefNodeLabelOffsetX_TB.IsEnabled = true;
            DefNodeLabelOffsetY_TB.IsEnabled = true;
            DefNodeFontSizeTB.IsEnabled = true;
            DefNodeInnerRadiusTB.Text = dp.InnerRadius.ToString();
            DefNodeBorderRadiusTB.Text = dp.BorderRadius.ToString();
            DefNodeColorTB.Background = new SolidColorBrush(dp.Color.ToSWMColor());
            DefNodeBorderColorTB.Background = new SolidColorBrush(dp.BorderColor.ToSWMColor());
            if (dp.LabelVisibility == Lib.DataStruct.Graph.DrawProperties.Visibility.No)
            {
                DefNodeLabelVisibilityCB.SelectedIndex = 0;
            }
            else if (dp.LabelVisibility == Lib.DataStruct.Graph.DrawProperties.Visibility.Yes)
            {
                DefNodeLabelVisibilityCB.SelectedIndex = 1;
            }
            else if (dp.LabelVisibility == Lib.DataStruct.Graph.DrawProperties.Visibility.Parent)
            {
                DefNodeLabelVisibilityCB.SelectedIndex = 2;
            }
            DefNodeLabelOffsetX_TB.Text = dp.LabelOffset.X.ToString();
            DefNodeLabelOffsetY_TB.Text = dp.LabelOffset.Y.ToString();
            DefNodeFontSizeTB.Text = dp.FontSize.ToString();
        }

        /// <summary>
        /// Switch off node draw properties.
        /// </summary>
        private void NodeDrawPropertiesOff()
        {
            DefNodeHasDrawPropertiesCB.IsChecked = false;
            DefNodeInnerRadiusTB.IsEnabled = false;
            DefNodeBorderRadiusTB.IsEnabled = false;
            DefNodeColorTB.IsEnabled = false;
            DefNodeBorderColorTB.IsEnabled = false;
            DefNodeLabelVisibilityCB.IsEnabled = false;
            DefNodeLabelOffsetX_TB.IsEnabled = false;
            DefNodeLabelOffsetY_TB.IsEnabled = false;
            DefNodeFontSizeTB.IsEnabled = false;
            DefNodeInnerRadiusTB.Text = "";
            DefNodeBorderRadiusTB.Text = "";
            DefNodeColorTB.Background = SWMBrushes.Black;
            DefNodeBorderColorTB.Background = SWMBrushes.Black;
            DefNodeLabelVisibilityCB.SelectedIndex = 0;
            DefNodeLabelOffsetX_TB.Text = "";
            DefNodeLabelOffsetY_TB.Text = "";
            DefNodeFontSizeTB.Text = "";
        }

        /// <summary>
        /// Switch on edge draw properties.
        /// </summary>
        /// <param name="dp">draw properties</param>
        private void EdgeDrawPropertiesOn(EdgeDrawProperties dp)
        {
            DefEdgeHasDrawPropertiesCB.IsChecked = true;
            DefEdgeColorTB.IsEnabled = true;
            DefEdgeThicknessTB.IsEnabled = true;
            DefEdgeNodesMarginTB.IsEnabled = true;
            DefEdgeColorTB.Background = new SolidColorBrush(dp.Color.ToSWMColor());
            DefEdgeThicknessTB.Text = dp.Thickness.ToString();
            DefEdgeNodesMarginTB.Text = dp.NodesMargin.ToString();
        }

        /// <summary>
        /// Switch off edge draw properties.
        /// </summary>
        private void EdgeDrawPropertiesOff()
        {
            DefEdgeHasDrawPropertiesCB.IsChecked = false;
            DefEdgeColorTB.IsEnabled = false;
            DefEdgeThicknessTB.IsEnabled = false;
            DefEdgeNodesMarginTB.IsEnabled = false;
            DefEdgeColorTB.Background = SWMBrushes.Black;
            DefEdgeThicknessTB.Text = "";
            DefEdgeNodesMarginTB.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Graph.DrawProperties.DefaultNodeDrawProperties != null)
            {
                NodeDrawPropertiesOn(Graph.DrawProperties.DefaultNodeDrawProperties);
            }
            else
            {
                NodeDrawPropertiesOff();
            }

            if (Graph.DrawProperties.DefaultEdgeDrawProperties != null)
            {
                EdgeDrawPropertiesOn(Graph.DrawProperties.DefaultEdgeDrawProperties);
            }
            else
            {
                EdgeDrawPropertiesOff();
            }

            SelectedNodeColorTB.Background = new SolidColorBrush(Graph.DrawProperties.SelectedNodeColor.ToSWMColor());
            CapturedNodeColorTB.Background = new SolidColorBrush(Graph.DrawProperties.CapturedNodeColor.ToSWMColor());
            SelectedEdgeColorTB.Background = new SolidColorBrush(Graph.DrawProperties.SelectedEdgeColor.ToSWMColor());
        }

        /// <summary>
        /// Default node draw properties check.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefNodeHasDrawPropertiesCB_Checked(object sender, RoutedEventArgs e)
        {
            if (Graph.DrawProperties.DefaultNodeDrawProperties != null)
            {
                NodeDrawPropertiesOn(Graph.DrawProperties.DefaultNodeDrawProperties);
            }
            else
            {
                NodeDrawPropertiesOn(new NodeDrawProperties());
            }
        }

        /// <summary>
        /// Default node draw properties uncheck.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefNodeHasDrawPropertiesCB_Unchecked(object sender, RoutedEventArgs e)
        {
            NodeDrawPropertiesOff();
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
        /// Default node color click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefNodeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (DefNodeColorTB.Background as SolidColorBrush).Color;
            DefNodeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Default node border color click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefNodeBorderColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (DefNodeBorderColorTB.Background as SolidColorBrush).Color;
            DefNodeBorderColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Check default edge draw properties.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefEdgeHasDrawPropertiesCB_Checked(object sender, RoutedEventArgs e)
        {
            if (Graph.DrawProperties.DefaultEdgeDrawProperties != null)
            {
                EdgeDrawPropertiesOn(Graph.DrawProperties.DefaultEdgeDrawProperties);
            }
            else
            {
                EdgeDrawPropertiesOn(new EdgeDrawProperties());
            }
        }

        /// <summary>
        /// Uncheck default edge draw properties.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefEdgeHasDrawPropertiesCB_Unchecked(object sender, RoutedEventArgs e)
        {
            EdgeDrawPropertiesOff();
        }

        /// <summary>
        /// Default edge color click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DefEdgeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (DefEdgeColorTB.Background as SolidColorBrush).Color;
            DefEdgeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Selected node color click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void SelectedNodeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (SelectedNodeColorTB.Background as SolidColorBrush).Color;
            SelectedNodeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Captured node color click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CapturedNodeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (CapturedNodeColorTB.Background as SolidColorBrush).Color;
            CapturedNodeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Selected edge color click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void SelectedEdgeColorTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SWMColor c = (SelectedEdgeColorTB.Background as SolidColorBrush).Color;
            SelectedEdgeColorTB.Background = new SolidColorBrush(GetColor(c));
        }

        /// <summary>
        /// Accept button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            // Default node draw properties.
            if ((bool)DefNodeHasDrawPropertiesCB.IsChecked)
            {
                NodeDrawProperties dp = new NodeDrawProperties();

                dp.InnerRadius = Lib.GUI.WPF.IO.GetDouble(DefNodeInnerRadiusTB);
                dp.BorderRadius = Lib.GUI.WPF.IO.GetDouble(DefNodeBorderRadiusTB);
                dp.Color = new Lib.Draw.Color((DefNodeColorTB.Background as SolidColorBrush).Color);
                dp.BorderColor = new Lib.Draw.Color((DefNodeBorderColorTB.Background as SolidColorBrush).Color);
                switch (DefNodeLabelVisibilityCB.SelectedIndex)
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
                dp.LabelOffset = new Lib.Maths.Geometry.Geometry2D.Vector(Lib.GUI.WPF.IO.GetDouble(DefNodeLabelOffsetX_TB),
                                                                          Lib.GUI.WPF.IO.GetDouble(DefNodeLabelOffsetY_TB));
                dp.FontSize = Lib.GUI.WPF.IO.GetDouble(DefNodeFontSizeTB);

                Graph.DrawProperties.DefaultNodeDrawProperties = dp;
            }
            else
            {
                Graph.DrawProperties.DefaultNodeDrawProperties = null;
            }

            // Default edge draw properties.
            if ((bool)DefEdgeHasDrawPropertiesCB.IsChecked)
            {
                EdgeDrawProperties dp = new EdgeDrawProperties();

                dp.Color = new Lib.Draw.Color((DefEdgeColorTB.Background as SolidColorBrush).Color);
                dp.Thickness = Lib.GUI.WPF.IO.GetDouble(DefEdgeThicknessTB);
                dp.NodesMargin = Lib.GUI.WPF.IO.GetDouble(DefEdgeNodesMarginTB);

                Graph.DrawProperties.DefaultEdgeDrawProperties = dp;
            }
            else
            {
                Graph.DrawProperties.DefaultEdgeDrawProperties = null;
            }

            // Other properties.
            Graph.DrawProperties.SelectedNodeColor = new Lib.Draw.Color((SelectedNodeColorTB.Background as SolidColorBrush).Color);
            Graph.DrawProperties.CapturedNodeColor = new Lib.Draw.Color((CapturedNodeColorTB.Background as SolidColorBrush).Color);
            Graph.DrawProperties.SelectedEdgeColor = new Lib.Draw.Color((SelectedEdgeColorTB.Background as SolidColorBrush).Color);

            Close();
        }

        /// <summary>
        /// Cancel button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
