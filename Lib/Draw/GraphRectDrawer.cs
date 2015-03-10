// Author: Alexey Rybakov

using System.Diagnostics;

using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;
using Lib.Maths.Geometry.Geometry3D;

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
            graph.Edges.ForEach((Edge edge) => DrawEdge(edge));
            graph.Nodes.ForEach((Node node) => DrawNode(node));
        }

        /// <summary>
        /// Draw node.
        /// </summary>
        /// <param name="node">node</param>
        public void DrawNode(Node node)
        {
            Graph g = node.Parent;
            NodeDrawProperties nprops = node.DrawProperties;

            if (g.Is2D)
            {
                Drawer.SetBrush(nprops.BorderColor);
                Drawer.FillPoint(node.Point2D, nprops.BorderRadius);
                Drawer.SetBrush(nprops.Color);
                Drawer.FillPoint(node.Point2D, nprops.InnerRadius);
            }
            else
            {
                Color bcolor = nprops.BorderColor;
                Color color = nprops.Color;
                double in_front_of_barycenter = node.Point3D.Z - (g.Barycenter() as Point).Z;

                if (in_front_of_barycenter < 0.0)
                {
                    bcolor = bcolor.ShiftToWhite(1.0 + (-in_front_of_barycenter * ShiftToWhiteCoefficient));
                    color = color.ShiftToWhite(1.0 + (-in_front_of_barycenter * ShiftToWhiteCoefficient));
                }

                Drawer.SetBrush(bcolor);
                Drawer.FillPoint(node.Point2D, nprops.BorderRadius);
                Drawer.SetBrush(color);
                Drawer.FillPoint(node.Point2D, nprops.InnerRadius);
            }
        }

        /// <summary>
        /// Draw edge.
        /// </summary>
        /// <param name="edge">edge</param>
        public void DrawEdge(Edge edge)
        {
            Graph g = edge.Parent;
            EdgeDrawProperties eprops = edge.DrawProperties;

            if (g.Is2D)
            {
                Drawer.SetPen(eprops.Color, eprops.Thickness);
            }
            else
            {
                Color color = eprops.Color;
                Vector v = Vector.Mid(edge.A.Point3D, edge.B.Point3D);
                double in_front_of_barycenter = v.Z - (g.Barycenter() as Point).Z;

                if (in_front_of_barycenter < 0.0)
                {
                    color = color.ShiftToWhite(1.0 + (-in_front_of_barycenter * ShiftToWhiteCoefficient));
                }

                Drawer.SetPen(color, eprops.Thickness);
            }

            Drawer.DrawMarginedLine(edge.A.Point2D, edge.B.Point2D, eprops.NodesMargin);
        }
    }
}
