using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataStruct.Graph.Partitioning
{
    /// <summary>
    /// Class with generic functions for partitioning.
    /// </summary>
    public class Partitioner
    {
        /// <summary>
        /// Array with partitions wweights.
        /// </summary>
        protected static double[] PartitionsWeights;

        /// <summary>
        /// Init partitions weights with nulls.
        /// </summary>
        /// <param name="pc">partitions count</param>
        protected static void InitPartitionsWeigths(int pc)
        {
            PartitionsWeights = new double[pc];

            for (int i = 0; i < pc; i++)
            {
                PartitionsWeights[i] = 0.0;
            }
        }

        /// <summary>
        /// Erase all partitions numbers for all nodes.
        /// </summary>
        /// <param name="g">graph</param>
        protected static void EraseNodesPartitions(Graph g)
        {
            foreach (Node n in g.Nodes)
            {
                n.Partition = -1;
            }
        }

        /// <summary>
        /// Find max node without partition.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>node</returns>
        protected static Node FindMaxNodeWithoutPartition(Graph g)
        {
            Node max_node = null;

            foreach (Node n in g.Nodes)
            {
                if (n.Partition != -1)
                {
                    continue;
                }

                if ((max_node == null) || (n.Weight > max_node.Weight))
                {
                    max_node = n;
                }
            }

            return max_node;
        }

        /// <summary>
        /// Get all nodes of given partition.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pn">partition number</param>
        /// <returns>partition nodes</returns>
        protected static List<Node> PartitionNodes(Graph g, int pn)
        {
            List<Node> nodes = new List<Node>();

            foreach (Node n in g.Nodes)
            {
                if (n.Partition == pn)
                {
                    nodes.Add(n);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Get all nodes without partition.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>nodes without partition</returns>
        protected static List<Node> NoPartitionNodes(Graph g)
        {
            return PartitionNodes(g, -1);
        }

        /// <summary>
        /// Sum of distances from one node to list of nodes.
        /// </summary>
        /// <param name="n">node</param>
        /// <param name="ns">list of nodes</param>
        /// <returns>sum of distances</returns>
        protected static double DistFromNodeToNodes(Node n, List<Node> ns)
        {
            double d = 0.0;

            foreach (Node tn in ns)
            {
                d += n.Point3D.Dist(tn.Point3D);
            }

            return d;
        }
    }
}
