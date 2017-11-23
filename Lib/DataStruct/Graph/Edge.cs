// Author: Alexey Rybakov

using System;
using System.Xml.Serialization;
using System.IO;

using Lib.DataStruct.Graph.Serialized;
using Lib.DataStruct.Graph.DrawProperties;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Edge.
    /// </summary>
    public class Edge : SelectableObject
    {
        /// <summary>
        /// Parent graph.
        /// </summary>
        public Graph Parent = null;

        /// <summary>
        /// Label.
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// Weight.
        /// </summary>
        public double Weight
        {
            get;
            set;
        }

        /// <summary>
        /// First incident node access.
        /// </summary>
        public Node A
        {
            get;
            set;
        }

        /// <summary>
        /// Second incident node access.
        /// </summary>
        public Node B
        {
            get;
            set;
        }

        /// <summary>
        /// Oriented edge flag access.
        /// </summary>
        public bool IsOriented
        {
            get;
            set;
        }

        /// <summary>
        /// Length (distance between its ends).
        /// </summary>
        public double Length
        {
            get
            {
                return A.P.Dist(B.P);
            }
        }

        /// <summary>
        /// Draw properties.
        /// </summary>
        private EdgeDrawProperties _DrawProperties = null;

        /// <summary>
        /// Draw properties access.
        /// </summary>
        public EdgeDrawProperties DrawProperties
        {
            get
            {
                return (_DrawProperties != null)
                       ? _DrawProperties
                       : Parent.DrawProperties.DefaultEdgeDrawProperties;
            }

            set
            {
                _DrawProperties = value;
            }
        }

        /// <summary>
        /// Check if parent draw properties used.
        /// </summary>
        public bool IsParentDrawProperties
        {
            get
            {
                return _DrawProperties == null;
            }
        }

        /// <summary>
        /// Create own draw properties from parent.
        /// </summary>
        public void CreateOwnDrawProperties()
        {
            if (IsParentDrawProperties)
            {
                _DrawProperties = DrawProperties.Clone() as EdgeDrawProperties;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">parent graph</param>
        /// <param name="a">first incident node</param>
        /// <param name="b">second incident node</param>
        /// <param name="is_oriented">oriented flag</param>
        public Edge(Graph parent, Node a, Node b, bool is_oriented)
        {
            Parent = parent;
            A = a;
            B = b;
            Label = "";
            Weight = 1.0;
            IsOriented = is_oriented;
        }

        /// <summary>
        /// Constructor from edge serialized.
        /// </summary>
        /// <param name="parent">parent graph</param>
        /// <param name="es">esge serialized</param>
        public Edge(Graph parent, EdgeSerialized es)
            : this(parent, parent.FindNode(es.AId), parent.FindNode(es.BId), es.IsOriented)
        {
            Label = es.Label;
            Weight = es.Weight;

            if (es.DrawProperties == null)
            {
                DrawProperties = null;
            }
            else
            {
                DrawProperties = new EdgeDrawProperties(es.DrawProperties);
            }
        }

        /// <summary>
        /// Check if edge incident to given node.
        /// </summary>
        /// <param name="node">node</param>
        /// <returns>result</returns>
        public bool IsIncident(Node node)
        {
            return (node == A) || (node == B);
        }

        /// <summary>
        /// Circle flag.
        /// </summary>
        public bool IsCircle
        {
            get
            {
                return A == B;
            }
        }

        /// <summary>
        /// Oriented edge inversion.
        /// </summary>
        public void Invert()
        {
            if (IsOriented)
            {
                Node tmp = A;

                A = B;
                B = tmp;
            }
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string delim = IsOriented ? "-->" : "--";

            return String.Format("[{0} {1} {2}]", A.ToString(), delim, B.ToString());
        }

        /// <summary>
        /// Check if edge is equal to another edge.
        /// </summary>
        /// <param name="e">edge</param>
        /// <returns><c>true</c> - if edges are equal, <c>false</c> - otherwise</returns>
        public bool IsEq(Edge e)
        {
            // Edges must be same oriented.
            if (IsOriented != e.IsOriented)
            {
                return false;
            }

            if ((A == e.A) && (B == e.B))
            {
                return true;
            }

            // For not oriented edges AB = BA.
            if (!IsOriented && (A == e.B) && (B == e.A))
            {
                return true;
            }

            return false;
        }
    }
}
