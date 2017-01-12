using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.DataStruct.Graph;
using Lib.Maths.Numbers;

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
    public class UniformGreedyPartitioner : Partitioner
    {
        /// <summary>
        /// Partition the given graph.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pc">partitions count</param>
        public static void Partition(Graph g, int pc)
        {
            Debug.Assert(pc > 0, "Partitions count must be greater than 0.");

            InitPartitionsWeigths(pc);
            EraseNodesPartitions(g);

            // Outer cycle.
            while (true)
            {
                Node max_node = FindMaxNodeWithoutPartition(g);

                // If there is no next node - process is finished.
                if (max_node == null)
                {
                    break;
                }

                int min_partition_index = NumbersArrays.MinIndex(PartitionsWeights);

                // We have to put node max_node to partition min_partition_index.
                max_node.Partition = min_partition_index;
                PartitionsWeights[min_partition_index] += max_node.Weight;
            }
        }
    }
}
