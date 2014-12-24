// Copyright Joy Developing.

using System;

using Lib.DataStruct.Graph.DrawProperties;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Edge.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Parent graph.
        /// </summary>
        public Graph Parent = null;

        /// <summary>
        /// First incident node.
        /// </summary>
        private Node _A = null;

        /// <summary>
        /// First incident node access.
        /// </summary>
        public Node A
        {
            get
            {
                return _A;
            }

            private set
            {
                _A = value;
            }
        }

        /// <summary>
        /// Second incident node.
        /// </summary>
        private Node _B = null;

        /// <summary>
        /// Second incident node access.
        /// </summary>
        public Node B
        {
            get
            {
                return _B;
            }

            private set
            {
                _B = value;
            }
        }

        /// <summary>
        /// Oriented edge check.
        /// If edge is oriented it is from <c>A</c> to <c>B</c>.
        /// </summary>
        private bool _IsOriented = false;

        /// <summary>
        /// Oriented edge flag access.
        /// </summary>
        public bool IsOriented
        {
            get
            {
                return _IsOriented;
            }

            private set
            {
                _IsOriented = value;
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
