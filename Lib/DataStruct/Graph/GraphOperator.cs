using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Class for some complex operations with graph.
    /// </summary>
    public class GraphOperator
    {
        /// <summary>
        /// Take graph neighbourhood of nodes list.
        /// </summary>
        /// <param name="nodes">nodes</param>
        /// <returns>neighbourhood</returns>
        public static List<Node> Neighbourhood(List<Node> nodes)
        {
            List<Node> neighbourhood = new List<Node>();

            foreach (Node n in nodes)
            {
                foreach (Edge e in n.Edges)
                {
                    Node neighbour = n.Neighbour(e);

                    if (neighbour != null)
                    {
                        if (!neighbourhood.Contains(neighbour))
                        {
                            neighbourhood.Add(neighbour);
                        }
                    }
                }
            }

            return neighbourhood;
        }

        /// <summary>
        /// Edge constraction.
        /// Remove given edge from the graph and merge its ends to single node
        /// (in the middle of the deleted edge).
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="e">edge</param>
        /// <returns>deleted node</returns>
        public static Node EdgeContraction(Graph g, Edge e)
        {
            Node a = e.A;
            Node b = e.B;

            g.DeleteEdge(e);

            return g.MergeNodes(a, b);
        }
    }
}
