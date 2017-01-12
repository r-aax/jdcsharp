// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Serialized;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using Point3D = Lib.Maths.Geometry.Geometry3D.Point;

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
        /// Position (2D or 3D point).
        /// </summary>
        private object Position = null;

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
        /// Check if position is 2D.
        /// </summary>
        /// <returns><c>true</c> - if position is 2D, <c>false</c> - otherwise.</returns>
        public bool Is2D()
        {
            return Position is Point2D;
        }

        /// <summary>
        /// Check if position is 3D.
        /// </summary>
        /// <returns><c>true</c> - if position is 3D, <c>false</c> - otherwise.</returns>
        public bool Is3D()
        {
            return Position is Point3D;
        }

        /// <summary>
        /// 2D point.
        /// </summary>
        public Point2D Point2D
        {
            get
            {
                if (Position is Point2D)
                {
                    return Position as Point2D;
                }
                else if (Position is Point3D)
                {
                    Point3D p3d = Position as Point3D;

                    return new Point2D(p3d.X, p3d.Y);
                }
                else
                {
                    return null;
                }
            }

            set
            {
                Position = value;
            }
        }

        /// <summary>
        /// 3D point.
        /// </summary>
        public Point3D Point3D
        {
            get
            {
                if (Position is Point3D)
                {
                    return Position as Point3D;
                }
                else if (Position is Point2D)
                {
                    Point2D p2d = Position as Point2D;

                    return new Point3D(p2d.X, p2d.Y, 0.0);
                }
                else
                {
                    return null;
                }
            }

            set
            {
                Position = value;
            }
        }
        
        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X
        {
            get
            {
                return Is2D() ? Point2D.X : Point3D.X;
            }
        }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y
        {
            get
            {
                return Is2D() ? Point2D.Y : Point3D.Y;
            }
        }

        /// <summary>
        /// Z coordinate.
        /// </summary>
        public double Z
        {
            get
            {
                return Is2D() ? 0.0 : Point3D.Z;
            }
        }

        /// <summary>
        /// Position as string.
        /// </summary>
        /// <returns>string</returns>
        public string PositionString()
        {
            return (Position is Point2D)
                   ? (Position as Point2D).ToString()
                   : (Position as Point3D).ToString();
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
                    Point2D = new Point2D(Double.Parse(s[1]), Double.Parse(s[2]));
                    break;

                case 5:
                    Point3D = new Point3D(Double.Parse(s[1]), Double.Parse(s[2]), Double.Parse(s[3]));
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
