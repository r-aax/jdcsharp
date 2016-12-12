// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;

using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Serialized;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using Point3D = Lib.Maths.Geometry.Geometry3D.Point;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Graph.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Dimensionality.
        /// </summary>
        public GraphDimensionality Dimensionality = GraphDimensionality.None;

        /// <summary>
        /// 2D check.
        /// </summary>
        public bool Is2D
        {
            get
            {
                return (Dimensionality == GraphDimensionality.D2);
            }
        }

        /// <summary>
        /// 3D check.
        /// </summary>
        public bool Is3D
        {
            get
            {
                return (Dimensionality == GraphDimensionality.D3);
            }
        }

        /// <summary>
        /// Nodes.
        /// </summary>
        public List<Node> Nodes
        {
            get;
            private set;
        }

        /// <summary>
        /// Order of graph.
        /// </summary>
        public int Order
        {
            get
            {
                return Nodes.Count;
            }
        }

        /// <summary>
        /// Empty check.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (Order == 0);
            }
        }

        /// <summary>
        /// Count of isolated nodes.
        /// </summary>
        public int IsolatedNodesCount
        {
            get
            {
                return Nodes.FindAll(n => n.IsIsolated).Count;
            }
        }

        /// <summary>
        /// Count of hanging nodes.
        /// </summary>
        public int HangingNodesCount
        {
            get
            {
                return Nodes.FindAll(n => n.IsHanging).Count;
            }
        }

        /// <summary>
        /// Edges.
        /// </summary>
        public List<Edge> Edges
        {
            get;
            private set;
        }

        /// <summary>
        /// Size of graph.
        /// </summary>
        public int Size
        {
            get
            {
                return Edges.Count;
            }
        }

        /// <summary>
        /// Density of graph.
        /// </summary>
        public double Density
        {
            get
            {
                return (double)Size / (double)Order;
            }
        }

        /// <summary>
        /// Draw properties.
        /// </summary>
        public GraphDrawProperties DrawProperties = null;

        /// <summary>
        /// Copy node draw properties flag.
        /// </summary>
        public bool IsCopyNodeDrawProperties
        {
            get;
            set;
        }

        /// <summary>
        /// Copy edge draw properties flag.
        /// </summary>
        public bool IsCopyEdgeDrawProperties
        {
            get;
            set;
        }

        /// <summary>
        /// Max node identifier.
        /// </summary>
        private int MaxNodeId;

        /// <summary>
        /// Constructor from dimensionality.
        /// </summary>
        /// <param name="dim">dimensionality</param>
        public Graph(GraphDimensionality dim)
        {
            // Dimensionality.
            Dimensionality = dim;

            // Empty lists of edges and nodes.
            Nodes = new List<Node>();
            Edges = new List<Edge>();

            // Draw properties.
            DrawProperties = new GraphDrawProperties();

            // Flags of copy nodes and edges draw properties.
            IsCopyNodeDrawProperties = false;
            IsCopyEdgeDrawProperties = false;

            // No nodes yet.
            MaxNodeId = -1;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Graph()
            : this(GraphDimensionality.None)
        {
        }

        /// <summary>
        /// Check dimensionality of graph.
        /// </summary>
        /// <param name="dim">new dimensionality</param>
        public void ChangeDimensionality(GraphDimensionality dim)
        {
            if (Dimensionality == dim)
            {
                return;
            }

            // TODO:
            // To implement 2D <-> 3D.
            Debug.Assert((Nodes.Count == 0) && (Edges.Count == 0));

            Dimensionality = dim;
        }

        /// <summary>
        /// Add new node.
        /// </summary>
        /// <returns>node</returns>
        public Node AddNode()
        {
            Node node = new Node(MaxNodeId + 1, this);

            if (IsCopyNodeDrawProperties)
            {
                node.DrawProperties = DrawProperties.DefaultNodeDrawProperties.Clone() as NodeDrawProperties;
            }

            Nodes.Add(node);
            MaxNodeId++;

            return node;
        }

        /// <summary>
        /// Add new edge.
        /// </summary>
        /// <param name="a">first incident node</param>
        /// <param name="b">second incident node</param>
        /// <param name="is_oriented">oriented flag</param>
        /// <returns>edge</returns>
        public Edge AddEdge(Node a, Node b, bool is_oriented)
        {
            Edge edge = new Edge(this, a, b, is_oriented);

            if (IsCopyEdgeDrawProperties)
            {
                edge.DrawProperties = DrawProperties.DefaultEdgeDrawProperties.Clone() as EdgeDrawProperties;
            }

            Edges.Add(edge);
            a.Edges.Add(edge);
            b.Edges.Add(edge);

            return edge;
        }

        /// <summary>
        /// Add new edge.
        /// </summary>
        /// <param name="a">first incident node</param>
        /// <param name="b">second incident node</param>
        /// <returns>edge</returns>
        public Edge AddEdge(Node a, Node b)
        {
            return AddEdge(a, b, false);
        }

        /// <summary>
        /// Add new edge.
        /// </summary>
        /// <param name="ai">first incident node index</param>
        /// <param name="bi">second incident node index</param>
        /// <param name="is_oriented">oriented flag</param>
        /// <returns>edge</returns>
        public Edge AddEdge(int ai, int bi, bool is_oriented)
        {
            return AddEdge(Nodes[ai], Nodes[bi], is_oriented);
        }

        /// <summary>
        /// Add new edge.
        /// </summary>
        /// <param name="ai">first incident node index</param>
        /// <param name="bi">second incident node index</param>
        /// <returns>edge</returns>
        public Edge AddEdge(int ai, int bi)
        {
            return AddEdge(Nodes[ai], Nodes[bi], false);
        }

        /// <summary>
        /// Find edge with given nodes.
        /// </summary>
        /// <param name="a">first node</param>
        /// <param name="b">second node</param>
        /// <returns>edge or null</returns>
        public Edge FindEdge(Node a, Node b)
        {
            Debug.Assert((a.Parent == this) && (b.Parent == this));

            foreach (Edge e in a.Edges)
            {
                if (e.IsIncident(b))
                {
                    return e;
                }
            }

            return null;
        }

        /// <summary>
        /// Check if edge in graph.
        /// </summary>
        /// <param name="a">first node</param>
        /// <param name="b">second node</param>
        /// <returns>true - if edge in graph, false - if there is no such edge</returns>
        public bool IsEdge(Node a, Node b)
        {
            return FindEdge(a, b) != null;
        }

        /// <summary>
        /// Check if edge in graph.
        /// </summary>
        /// <param name="ai">first incident node index</param>
        /// <param name="bi">second incident node index</param>
        /// <returns>true - if edge in graph, false - if there is no such edge</returns>
        public bool IsEdge(int ai, int bi)
        {
            return IsEdge(Nodes[ai], Nodes[bi]);
        }

        /// <summary>
        /// Center point (center of minimal outer rectangle or parallelepiped).
        /// </summary>
        /// <returns>center point</returns>
        public object Center()
        {
            double min_x, max_x, min_y, max_y, min_z, max_z;

            if (IsEmpty)
            {
                return null;
            }

            if (Is2D)
            {
                min_x = Nodes[0].Point2D.X;
                max_x = min_x;
                min_y = Nodes[0].Point2D.Y;
                max_y = min_y;

                foreach (Node n in Nodes)
                {
                    double x = n.Point2D.X;
                    double y = n.Point2D.X;

                    min_x = Math.Min(min_x, x);
                    max_x = Math.Max(max_x, x);
                    min_y = Math.Min(min_y, y);
                    max_y = Math.Max(max_y, y);
                }

                return new Point2D(0.5 * (min_x + max_x),
                                   0.5 * (min_y + max_y));
            }
            else
            {
                min_x = Nodes[0].Point3D.X;
                max_x = min_x;
                min_y = Nodes[0].Point3D.Y;
                max_y = min_y;
                min_z = Nodes[0].Point3D.Z;
                max_z = min_z;

                foreach (Node n in Nodes)
                {
                    double x = n.Point3D.X;
                    double y = n.Point3D.X;
                    double z = n.Point3D.Z;

                    min_x = Math.Min(min_x, x);
                    max_x = Math.Max(max_x, x);
                    min_y = Math.Min(min_y, y);
                    max_y = Math.Max(max_y, y);
                    min_z = Math.Min(min_z, z);
                    max_z = Math.Max(max_z, z);
                }

                return new Point3D(0.5 * (min_x + max_x),
                                   0.5 * (min_y + max_y),
                                   0.5 * (min_z + max_z));
            }
        }

        /// <summary>
        /// Barycenter.
        /// </summary>
        /// <returns>barycenter</returns>
        public object Barycenter()
        {
            if (Is2D)
            {
                Point2D[] p = new Point2D[Order];

                for (int i = 0; i < Order; i++)
                {
                    p[i] = Nodes[i].Point2D;
                }

                return new Point2D(Point2D.Avg(p));
            }
            else
            {
                Point3D[] p = new Point3D[Order];

                for (int i = 0; i < Order; i++)
                {
                    p[i] = Nodes[i].Point3D;
                }

                return new Point3D(Point3D.Avg(p));
            }
        }

        /// <summary>
        /// Rotate X.
        /// </summary>
        /// <param name="angle">angle</param>
        public void RotX(double angle)
        {
            Debug.Assert(Is3D);

            Point3D bc = Barycenter() as Point3D;
            Nodes.ForEach((Node n) => n.Point3D.RotX(bc, angle));
        }

        /// <summary>
        /// Rotate Y.
        /// </summary>
        /// <param name="angle">angle</param>
        public void RotY(double angle)
        {
            Debug.Assert(Is3D);

            Point3D bc = Barycenter() as Point3D;
            Nodes.ForEach((Node n) => n.Point3D.RotY(bc, angle));
        }

        /// <summary>
        /// Rotate Z.
        /// </summary>
        /// <param name="angle">angle</param>
        public void RotZ(double angle)
        {
            if (Is2D)
            {
                Point2D bc = Barycenter() as Point2D;
                Nodes.ForEach((Node n) => n.Point2D.Rot(bc, angle));
            }
            else
            {
                Point3D bc = Barycenter() as Point3D;
                Nodes.ForEach((Node n) => n.Point3D.RotZ(bc, angle));
            }
        }

        /// <summary>
        /// Transform to 2D graph.
        /// </summary>
        public void TransformTo2D()
        {
            if (Is2D)
            {
                return;
            }

            foreach (Node n in Nodes)
            {
                Point3D p3d = n.Point3D;

                n.Point2D = new Point2D(p3d.X, p3d.Y);
            }

            Dimensionality = GraphDimensionality.D2;
        }

        /// <summary>
        /// Transform to 3D graph.
        /// </summary>
        public void TransformTo3D()
        {
            if (Is3D)
            {
                return;
            }

            foreach (Node n in Nodes)
            {
                Point2D p2d = n.Point2D;

                n.Point3D = new Point3D(p2d.X, p2d.Y, 0.0);
            }

            Dimensionality = GraphDimensionality.D3;
        }

        /// <summary>
        /// Find nearest node.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>nearest node</returns>
        public Node FindNearestNode(Point2D p)
        {
            if (Order == 0)
            {
                return null;
            }

            Node n = Nodes[0];

            for (int i = 1; i < Order; i++)
            {
                Point2D p2 = Nodes[i].Point2D;

                if (p.Dist(p2) < p.Dist(n.Point2D))
                {
                    n = Nodes[i];
                }
            }

            return n;
        }

        /// <summary>
        /// Export to graph serialized.
        /// </summary>
        /// <returns>serialization information</returns>
        public GraphSerialized ToSerialized()
        {
            return new GraphSerialized(this);
        }

        /// <summary>
        /// Serialize graph.
        /// </summary>
        /// <param name="file_name">file</param>
        public void XmlSerialize(string file_name)
        {
            ToSerialized().XmlSerialize(file_name);
        }
    }
}
