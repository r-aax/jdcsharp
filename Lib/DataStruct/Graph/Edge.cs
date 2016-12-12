﻿// Author: Alexey Rybakov

using System;
using System.Xml.Serialization;
using System.IO;

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
        /// First incident node access.
        /// </summary>
        public Node A
        {
            get;
            private set;
        }

        /// <summary>
        /// Second incident node access.
        /// </summary>
        public Node B
        {
            get;
            private set;
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
        /// Empty consstructor for serialization.
        /// </summary>
        public Edge()
        {
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
            IsOriented = is_oriented;
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
    }
}
