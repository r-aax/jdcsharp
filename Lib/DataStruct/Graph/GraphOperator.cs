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
            if (e == null)
            {
                return null;
            }

            Node a = e.A;
            Node b = e.B;

            // We have to delete all edges, equal to e.
            List<Edge> le = g.Edges.FindAll((fe) => fe.IsEq(e));
            foreach (Edge de in le)
            {
                g.DeleteEdge(de);
            }

            return g.MergeNodes(a, b);
        }

        /// <summary>
        /// Min edge contraction.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>deleted node</returns>
        public static Node MinEdgeContraction(Graph g)
        {
            return EdgeContraction(g, g.MinEdge());
        }

        /// <summary>
        /// Delete <c>c</c> min edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="c">count of edges</param>
        public static void MinEdgesContraction(Graph g, int c)
        {
            for (int i = 0; i < c; i++)
            {
                MinEdgeContraction(g);
            }
        }

        /// <summary>
        /// Delete all parallel edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>count of deleted edges</returns>
        public static int DeleteParallelEdges(Graph g)
        {
            int c = 0;

            for (int i = 0; i < g.Size; i++)
            {
                for (int j = i + 1; j < g.Size; j++)
                {
                    if (g.Edges[j].IsEq(g.Edges[i]))
                    {
                        g.DeleteEdge(g.Edges[j]);
                        c++;
                    }
                }
            }

            return c;
        }
    }
}
