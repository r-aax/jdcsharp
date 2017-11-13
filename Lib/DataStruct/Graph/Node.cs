// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Serialized;
using Lib.Maths.Geometry;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Node.
    /// </summary>
    public class Node : SelectableObject
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id
        {
            get;
            private set;
        }

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
        /// Partition (number).
        /// </summary>
        public int Partition
        {
            get;
            set;
        }

        /// <summary>
        /// Position.
        /// </summary>
        public Point P = null;

        /// <summary>
        /// Draw properties.
        /// </summary>
        private NodeDrawProperties _DrawProperties = null;

        /// <summary>
        /// Draw properties access.
        /// </summary>
        public NodeDrawProperties DrawProperties
        {
            get
            {
                return (_DrawProperties != null)
                       ? _DrawProperties
                       : Parent.DrawProperties.DefaultNodeDrawProperties;
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
        /// Set own draw properties for node.
        /// </summary>
        public void CreateOwnDrawProperties()
        {
            if (IsParentDrawProperties)
            {
                _DrawProperties = DrawProperties.Clone() as NodeDrawProperties;
            }
        }
        
        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X
        {
            get
            {
                return P.X;
            }
        }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y
        {
            get
            {
                return P.Y;
            }
        }

        /// <summary>
        /// Z coordinate.
        /// </summary>
        public double Z
        {
            get
            {
                return P.Z;
            }
        }

        /// <summary>
        /// Position as string.
        /// </summary>
        /// <returns>string</returns>
        public string PositionString()
        {
            return P.ToString();
        }

        /// <summary>
        /// Incident edges access.
        /// </summary>
        public List<Edge> Edges
        {
            get;
            private set;
        }

        /// <summary>
        /// Count of incident edges.
        /// </summary>
        public int Degree
        {
            get
            {
                return Edges.Count();
            }
        }

        /// <summary>
        /// Check if node is isolated.
        /// </summary>
        public bool IsIsolated
        {
            get
            {
                return Degree == 0;
            }
        }

        /// <summary>
        /// Check if node is hanging.
        /// </summary>
        public bool IsHanging
        {
            get
            {
                return Degree == 1;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">parent graph</param>
        /// <param name="id">identifier</param>
        public Node(Graph parent, int id)
        {
            Id = id;
            Label = Id.ToString();
            Weight = 0.0;
            Partition = 0;
            Parent = parent;
            Edges = new List<Edge>();
        }

        /// <summary>
        /// Constructor from node serialized.
        /// </summary>
        /// <param name="parent">parent graph</param>
        /// <param name="ns">node serialized</param>
        public Node(Graph parent, NodeSerialized ns)
            : this(parent, ns.Id)
        {
            Label = ns.Label;
            SetPosition(ns.Position);
            Weight = ns.Weight;

            if (ns.DrawProperties == null)
            {
                DrawProperties = null;
            }
            else
            {
                DrawProperties = new NodeDrawProperties(ns.DrawProperties);
            }
        }

        /// <summary>
        /// Check if incident to edge.
        /// </summary>
        /// <param name="edge">edge</param>
        /// <returns>result</returns>
        public bool IsIncident(Edge edge)
        {
            return Edges.Any(e => e == edge);
        }

        /// <summary>
        /// Find in edges.
        /// </summary>
        public List<Edge> InEdges
        {
            get
            {
                return Edges.FindAll(e => !e.IsOriented || (e.B == this));
            }
        }

        /// <summary>
        /// Count of in edges.
        /// </summary>
        public int InDegree
        {
            get
            {
                return InEdges.Count();
            }
        }

        /// <summary>
        /// Find out edges.
        /// </summary>
        public List<Edge> OutEdges
        {
            get
            {
                return Edges.FindAll(e => !e.IsOriented || (e.A == this));
            }
        }

        /// <summary>
        /// Count of out edges.
        /// </summary>
        public int OutDegree
        {
            get
            {
                return OutEdges.Count();
            }
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "Node";
        }

        /// <summary>
        /// Restore position from string.
        /// </summary>
        /// <param name="str">string</param>
        public void SetPosition(string str)
        {
            string[] s = str.Split(new char[] { '(', ')', ',' });
            int n = s.Count();

            switch (n)
            {
                case 4:
                    P = new Point(Double.Parse(s[1]), Double.Parse(s[2]));
                    break;

                case 5:
                    P = new Point(Double.Parse(s[1]), Double.Parse(s[2]), Double.Parse(s[3]));
                    break;

                default:
                    Debug.Assert(false, "unknown node position type");
                    break;
            }
        }

        /// <summary>
        /// Get node successor by the edge.
        /// </summary>
        /// <param name="e">edge</param>
        /// <returns>node successor</returns>
        public Node Succ(Edge e)
        {
            if (e.A == this)
            {
                return e.B;
            }

            if (e.B == this)
            {
                return e.IsOriented ? null : e.A;
            }

            return null;
        }

        /// <summary>
        /// Get node predecessor by edge.
        /// </summary>
        /// <param name="e">edge</param>
        /// <returns>node predecessor</returns>
        public Node Pred(Edge e)
        {
            if (e.B == this)
            {
                return e.A;
            }

            if (e.A == this)
            {
                return e.IsOriented ? null : e.B;
            }

            return null;
        }

        /// <summary>
        /// Get node neighbour by edge.
        /// </summary>
        /// <param name="e">edge</param>
        /// <returns>node neighbour</returns>
        public Node Neighbour(Edge e)
        {
            if (e.A == this)
            {
                return e.B;
            }

            if (e.B == this)
            {
                return e.A;
            }

            return null;
        }
    }
}
