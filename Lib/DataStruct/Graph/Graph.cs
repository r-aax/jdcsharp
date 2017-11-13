// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Diagnostics;

using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Serialized;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;

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
        /// List of selected nodes.
        /// </summary>
        public List<Node> SelectedNodes
        {
            get
            {
                return Nodes.FindAll(n => n.IsSelected);
            }
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
        /// List of selected edges.
        /// </summary>
        public List<Edge> SelectedEdges
        {
            get
            {
                return Edges.FindAll(e => e.IsSelected);
            }
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
        /// Constructor from serialized information.
        /// </summary>
        /// <param name="s">serialized information of graph</param>
        public Graph(GraphSerialized s)
        {
            Dimensionality = s.Dimensionality;

            // Draw properties.
            DrawProperties = new GraphDrawProperties();
            DrawProperties.DefaultNodeDrawProperties = new NodeDrawProperties(s.DrawProperties.DefaultNodeDrawProperties);
            DrawProperties.SelectedNodeColor = new Draw.Color(s.DrawProperties.DefaultSelectedNodeColor);
            DrawProperties.CapturedNodeColor = new Draw.Color(s.DrawProperties.DefaultCapturedNodeColor);
            DrawProperties.DefaultEdgeDrawProperties = new EdgeDrawProperties(s.DrawProperties.DefaultEdgeDrawProperties);
            DrawProperties.SelectedEdgeColor = new Draw.Color(s.DrawProperties.DefaultSelectedEdgeColor);

            // Nodes.
            Nodes = new List<Node>();
            MaxNodeId = -1;
            for (int i = 0; i < s.Nodes.Count; i++)
            {
                Node n = new Node(this, s.Nodes[i]);
                Nodes.Add(n);

                if (n.Id > MaxNodeId)
                {
                    MaxNodeId = n.Id;
                }
            }

            // Edges.
            Edges = new List<Edge>();
            for (int i = 0; i < s.Edges.Count; i++)
            {
                Edge e = new Edge(this, s.Edges[i]);
                Edges.Add(e);
                e.A.Edges.Add(e);
                e.B.Edges.Add(e);
            }
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
            Node node = new Node(this, MaxNodeId + 1);

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
        /// Find node by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>node or null</returns>
        public Node FindNode(int id)
        {
            foreach (Node n in Nodes)
            {
                if (n.Id == id)
                {
                    return n;
                }
            }

            return null;
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
        /// Minimum x coordinate.
        /// </summary>
        /// <returns>minimum x coordinate</returns>
        public double MinX()
        {
            Debug.Assert(Order > 0, "the graph has no nodes");

            double min_x = Nodes[0].X;

            foreach (Node n in Nodes)
            {
                min_x = Math.Min(min_x, n.X);
            }

            return min_x;
        }

        /// <summary>
        /// Maximum x coordinate.
        /// </summary>
        /// <returns>maximum x coordinate</returns>
        public double MaxX()
        {
            Debug.Assert(Order > 0, "the graph has no nodes");

            double max_x = Nodes[0].X;

            foreach (Node n in Nodes)
            {
                max_x = Math.Max(max_x, n.X);
            }

            return max_x;
        }

        /// <summary>
        /// Minimum y coordinate.
        /// </summary>
        /// <returns>minimum y coordinate</returns>
        public double MinY()
        {
            Debug.Assert(Order > 0, "the graph has no nodes");

            double min_y = Nodes[0].Y;

            foreach (Node n in Nodes)
            {
                min_y = Math.Min(min_y, n.Y);
            }

            return min_y;
        }

        /// <summary>
        /// Maximum y coordinate.
        /// </summary>
        /// <returns>maximum y coordinate</returns>
        public double MaxY()
        {
            Debug.Assert(Order > 0, "the graph has no nodes");

            double max_y = Nodes[0].Y;

            foreach (Node n in Nodes)
            {
                max_y = Math.Max(max_y, n.Y);
            }

            return max_y;
        }

        /// <summary>
        /// Minimum z coordinate.
        /// </summary>
        /// <returns>minimum z coordinate</returns>
        public double MinZ()
        {
            Debug.Assert(Order > 0, "the graph has no nodes");

            double min_z = Nodes[0].Z;

            foreach (Node n in Nodes)
            {
                min_z = Math.Min(min_z, n.Z);
            }

            return min_z;
        }

        /// <summary>
        /// Maximum z coordinate.
        /// </summary>
        /// <returns>maximum z coordinate</returns>
        public double MaxZ()
        {
            Debug.Assert(Order > 0, "the graph has no nodes");

            double max_z = Nodes[0].Z;

            foreach (Node n in Nodes)
            {
                max_z = Math.Max(max_z, n.Z);
            }

            return max_z;
        }

        /// <summary>
        /// Center point (center of minimal outer rectangle or parallelepiped).
        /// </summary>
        /// <returns>center point</returns>
        public object Center()
        {
            if (IsEmpty)
            {
                return null;
            }

            if (Is2D)
            {
                return new Point(0.5 * (MinX() + MaxX()),
                                 0.5 * (MinY() + MaxY()));
            }
            else
            {
                return new Point(0.5 * (MinX() + MaxX()),
                                 0.5 * (MinY() + MaxY()),
                                 0.5 * (MinZ() + MaxZ()));
            }
        }

        /// <summary>
        /// Barycenter.
        /// </summary>
        /// <returns>barycenter</returns>
        public Point Barycenter()
        {
            if (IsEmpty)
            {
                return new Point();
            }
            else
            {
                Point[] p = new Point[Order];

                for (int i = 0; i < Order; i++)
                {
                    p[i] = Nodes[i].Position;
                }

                return new Point(Point.Avg(p));
            }
        }

        /// <summary>
        /// Wraparound rectangle for graph.
        /// </summary>
        /// <param name="margin_k">margin coefficient</param>
        /// <param name="wh_ratio">width/height ratio</param>
        /// <returns></returns>
        public Rect2D WraparoundRect(double margin_k, double wh_ratio)
        {
            double min_x = MinX();
            double max_x = MaxX();

            if (min_x == max_x)
            {
                // Some margin for too thin graph (in y coordinate).
                min_x -= 1.0;
                max_x += 1.0;
            }

            double min_y = MinY();
            double max_y = MaxY();

            if (min_y == max_y)
            {
                // Some margin for too thin graph (in x coordinate).
                min_y -= 1.0;
                max_y += 1.0;
            }

            double mx = (max_x - min_x) * margin_k;
            double my = (max_y - min_y) * margin_k;
            double w = max_x - min_x + 2 * mx;
            double h = max_y - min_y + 2 * my;
            double r = w / h;

            if (r > wh_ratio)
            {
                my += 0.5 * (w / wh_ratio - h);
            }
            else if (r < wh_ratio)
            {
                mx += 0.5 * (h * wh_ratio - w);
            }

            return new Rect2D(min_x - mx, max_x + mx, min_y - my, max_y + my);
        }

        /// <summary>
        /// Wraparound rectange for graph.
        /// </summary>
        /// <param name="margin_k">margin coefficient</param>
        /// <returns>rectangle</returns>
        public Rect2D WraparoundRect(double margin_k)
        {
            return WraparoundRect(margin_k, 1.0);
        }

        /// <summary>
        /// Wraparound parallelepiped for graph.
        /// </summary>
        /// <returns>parallelepiped</returns>
        public Parallelepiped WraparoundParallelepiped()
        {
            Debug.Assert(Is3D, "Need 3D graph for wraparound parallelepiped.");

            return new Parallelepiped(MinX(), MaxX(), MinY(), MaxY(), MinZ(), MaxZ());
        }

        /// <summary>
        /// Rotate X.
        /// </summary>
        /// <param name="angle">angle</param>
        public void RotX(double angle)
        {
            Debug.Assert(Is3D);

            Point bc = Barycenter();
            Nodes.ForEach((Node n) => n.Position.RotX(bc, angle));
        }

        /// <summary>
        /// Rotate Y.
        /// </summary>
        /// <param name="angle">angle</param>
        public void RotY(double angle)
        {
            Debug.Assert(Is3D);

            Point bc = Barycenter();
            Nodes.ForEach((Node n) => n.Position.RotY(bc, angle));
        }

        /// <summary>
        /// Rotate Z.
        /// </summary>
        /// <param name="angle">angle</param>
        public void RotZ(double angle)
        {
            Point bc = Barycenter();
            Nodes.ForEach((Node n) => n.Position.RotZ(bc, angle));
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
                n.Position.Z = 0.0;
            }

            Dimensionality = GraphDimensionality.D2;
        }

        /// <summary>
        /// Transform to 3D graph.
        /// </summary>
        public void TransformTo3D()
        {
            Dimensionality = GraphDimensionality.D3;
        }

        /// <summary>
        /// Find nearest node.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>nearest node</returns>
        public Node FindNearestNode(Point p)
        {
            if (Order == 0)
            {
                return null;
            }

            Node n = Nodes[0];

            for (int i = 1; i < Order; i++)
            {
                Point p2 = Nodes[i].Position;

                if (p.Dist(p2) < p.Dist(n.Position))
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

        /// <summary>
        /// Create grah from serialized information.
        /// </summary>
        /// <param name="s">serialized graph</param>
        /// <returns>graph</returns>
        public static Graph FromSerialized(GraphSerialized s)
        {
            return new Graph(s);            
        }

        /// <summary>
        /// Graph deserialization.
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns>deserialized graph</returns>
        static public Graph XmlDeserialize(string file_name)
        {
            GraphSerialized s = GraphSerialized.XmlDeserialize(file_name);

            return Graph.FromSerialized(s);
        }

        /// <summary>
        /// Set simpe style for graph (no nodes, no edges margins).
        /// </summary>
        public void SetStyleSimple()
        {
            DrawProperties.DefaultNodeDrawProperties = null;
            DrawProperties.DefaultEdgeDrawProperties.NodesMargin = 0.0;
        }

        /// <summary>
        /// Set selection values for nodes and edges.
        /// </summary>
        /// <param name="value">selected or not</param>
        /// <param name="is_nodes">if apply to nodes</param>
        /// <param name="is_edges">if apply to edges</param>
        public void SetSelection(bool value, bool is_nodes, bool is_edges)
        {
            if (is_nodes)
            {
                Nodes.ForEach(n => n.SetSelection(value));
            }

            if (is_edges)
            {
                Edges.ForEach(e => e.SetSelection(value));
            }
        }

        /// <summary>
        /// Invert selection of nodes and edges.
        /// </summary>
        /// <param name="is_nodes">if apply to nodes</param>
        /// <param name="is_edges">if apply to edges</param>
        public void InvertSelection(bool is_nodes, bool is_edges)
        {
            if (is_nodes)
            {
                Nodes.ForEach(n => n.SwitchSelection());
            }

            if (is_edges)
            {
                Edges.ForEach(e => e.SwitchSelection());
            }
        }

        /// <summary>
        /// Add incident objects to selection.
        /// </summary>
        /// <param name="is_nodes">apply to nodes</param>
        /// <param name="is_edges">apply to edges</param>
        public void SelectIncident(bool is_nodes, bool is_edges)
        {
            if (is_nodes)
            {
                foreach (Edge e in Edges)
                {
                    if (e.IsSelected)
                    {
                        e.A.Select();
                        e.B.Select();
                    }
                }
            }

            if (is_edges)
            {
                foreach (Edge e in Edges)
                {
                    if (e.A.IsSelected || e.B.IsSelected)
                    {
                        e.Select();
                    }
                }
            }
        }
    }
}
