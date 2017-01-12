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
    }
}
