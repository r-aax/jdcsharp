// Author: Alexey Rybakov

using System.Diagnostics;

using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;
using Lib.Maths.Geometry;

namespace Lib.Draw
{
    /// <summary>
    /// Drawer of graph.
    /// </summary>
    public class GraphRectDrawer
    {
        /// <summary>
        /// General drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Temporry barycenter for nodes and edges drawing.
        /// </summary>
        private Point TmpBarycenter;

        /// <summary>
        /// Lightning coefficient of nodes which is placed deeper than barycenter.
        /// </summary>
        private readonly double ShiftToWhiteCoefficient = 0.05;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="drawer">general drawer</param>
        public GraphRectDrawer(RectDrawer drawer)
        {
            Debug.Assert(drawer != null);
            Drawer = drawer;
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="graph">graph</param>
        public void DrawGraph(Graph graph)
        {
            TmpBarycenter = graph.Barycenter() as Point;

            graph.Edges.ForEach((Edge edge) => DrawEdge(edge));
            graph.Nodes.ForEach((Node node) => DrawNode(node));
        }

        /// <summary>
        /// Draw node.
        /// </summary>
        /// <param name="node">node</param>
        /// <param name="nprops">draw properties</param>
        public void DrawNode(Node node, NodeDrawProperties nprops)
        {
            // Do not draw if no draw properties.
            if (nprops == null)
            {
                return;
            }

            Graph g = node.Parent;
            Point p = node.Point2D;
            Color color = color = nprops.Color;

            if (g.Is2D)
            {
                Drawer.SetBrush(nprops.BorderColor);
                Drawer.FillPoint(p, nprops.BorderRadius);
                Drawer.SetBrush(color);
                Drawer.FillPoint(p, nprops.InnerRadius);
            }
            else
            {
                Color bcolor = nprops.BorderColor;
                double in_front_of_barycenter = node.Z - TmpBarycenter.Z;

                if (in_front_of_barycenter < 0.0)
                {
                    bcolor = bcolor.ShiftToWhite(1.0 + (-in_front_of_barycenter * ShiftToWhiteCoefficient));
                    color = color.ShiftToWhite(1.0 + (-in_front_of_barycenter * ShiftToWhiteCoefficient));
                }

                Drawer.SetBrush(bcolor);
                Drawer.FillPoint(p, nprops.BorderRadius);
                Drawer.SetBrush(color);
                Drawer.FillPoint(p, nprops.InnerRadius);
            }

            // Draw label.

            bool is_visible = false;

            if (nprops.LabelVisibility == Visibility.Yes)
            {
                is_visible = true;
            }
            else if (nprops.LabelVisibility == Visibility.Parent)
            {
                if (g.DrawProperties.DefaultNodeDrawProperties != null)
                {
                    if (g.DrawProperties.DefaultNodeDrawProperties.LabelVisibility == Visibility.Yes)
                    {
                        is_visible = true;
                    }
                }
            }

            if (is_visible)
            {
                Drawer.DrawVerdanaText(p, nprops.LabelOffset, node.Label, nprops.FontSize);
            }
        }

        /// <summary>
        /// Draw node.
        /// </summary>
        /// <param name="node">node</param>
        public void DrawNode(Node node)
        {
            NodeDrawProperties nprops = node.DrawProperties;

            if (node.IsSelected)
            {
                Color save_color = nprops.Color;
                nprops.Color = node.Parent.DrawProperties.SelectedNodeColor;
                DrawNode(node, nprops);
                nprops.Color = save_color;
            }
            else
            {
                DrawNode(node, nprops);
            }
        }

        /// <summary>
        /// Draw edge.
        /// </summary>
        /// <param name="edge">edge</param>
        /// <param name="eprops">draw properties</param>
        public void DrawEdge(Edge edge, EdgeDrawProperties eprops)
        {
            Graph g = edge.Parent;

            if (g.Is2D)
            {
                Drawer.SetPen(eprops.Color, eprops.Thickness);
            }
            else
            {
                Color color = eprops.Color;
                Vector v = Vector.Mid(edge.A.Point3D, edge.B.Point3D);
                double in_front_of_barycenter = v.Z - TmpBarycenter.Z;

                if (in_front_of_barycenter < 0.0)
                {
                    color = color.ShiftToWhite(1.0 + (-in_front_of_barycenter * ShiftToWhiteCoefficient));
                }

                Drawer.SetPen(color, eprops.Thickness);
            }

            Drawer.DrawMarginedLine(edge.A.Point2D, edge.B.Point2D, eprops.NodesMargin);
        }

        /// <summary>
        /// Draw edge.
        /// </summary>
        /// <param name="edge">edge</param>
        public void DrawEdge(Edge edge)
        {
            EdgeDrawProperties eprops = edge.DrawProperties;

            if (edge.IsSelected)
            {
                Color save_color = eprops.Color;
                eprops.Color = edge.Parent.DrawProperties.SelectedEdgeColor;
                DrawEdge(edge, eprops);
                eprops.Color = save_color;
            }
            else
            {
                DrawEdge(edge, eprops);
            }
        }
    }
}
