using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.DataStruct.Graph;

namespace Lib.DataStruct.Graph.Partitioning
{
    /// <summary>
    /// Uniform greedy partitioner.
    /// Analizes only weights of nodes.
    /// Uses simple greedy algorithm:
    /// - construct list of nodes,
    /// - sort list from maximum weight to minimum,
    /// - while there is next node without cluster - get it,
    /// - put current node to cluster with minimum total weight.
    /// </summary>
    public class UniformGreedyPartitioner
    {
        /// <summary>
        /// Partition the given graph.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pc">partitions count</param>
        public static void Partition(Graph g, int pc)
        {
            Debug.Assert(pc > 0, "Partitions count must be greater than 0.");

            // Init empty array of partiotions weights.
            double[] partitions_weights = new double[pc];
            for (int i = 0; i < pc; i++)
            {
                partitions_weights[i] = 0.0;
            }

            // Erase nodes labels.
            foreach (Node n in g.Nodes)
            {
                n.Label = "";
            }

            // Outer cycle.
            // Find node without label with maximum weight.
            while (true)
            {
                Node max_node = null;
                foreach (Node n in g.Nodes)
                {
                    if (n.Label != "")
                    {
                        continue;
                    }

                    if ((max_node == null) || (n.Weight > max_node.Weight))
                    {
                        max_node = n;
                    }
                }

                // If there is no next node - process is finished.
                if (max_node == null)
                {
                    break;
                }

                // We have next node to process.
                int min_partition_index = -1;
                double min_partition_weight = 0.0;
                for (int i = 0; i < pc; i++)
                {
                    if ((min_partition_index == -1) || (partitions_weights[i] < min_partition_weight))
                    {
                        min_partition_index = i;
                        min_partition_weight = partitions_weights[min_partition_index];
                    }
                }

                // We have to put node max_node to partition min_partition_index.
                max_node.Label = min_partition_index.ToString();
            }
        }
    }
}
