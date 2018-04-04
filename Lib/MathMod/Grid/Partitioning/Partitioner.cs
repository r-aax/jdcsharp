
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.Grid;

namespace Lib.MathMod.Grid.Partitioning
{
    /// <summary>
    /// Grid partitioning master.
    /// </summary>
    public class Partitioner
    {
        /// <summary>
        /// Grid.
        /// </summary>
        public StructuredGrid Grid;

        /// <summary>
        /// Weights of partitions.
        /// </summary>
        public double[] PartitionsWeights;

        /// <summary>
        /// Partitions count.
        /// </summary>
        public int PartitionsCount
        {
            get
            {
                return PartitionsWeights.Length;
            }
        }

        /// <summary>
        /// Middle weight for partition.
        /// </summary>
        public double MidWeight;

        /// <summary>
        /// Max weight for current partition fill.
        /// </summary>
        public double MaxWeight;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g"></param>
        public Partitioner(StructuredGrid g)
        {
            Grid = g;
        }

        /// <summary>
        /// Init partitions weights with nulls.
        /// </summary>
        /// <param name="pc">partitions count</param>
        private void InitPartitionsWeigths(int pc)
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
        private void EraseNodesPartitions()
        {
            
            foreach (Block b in Grid.Blocks)
            {
                b.PartitionNumber = -1;
            }
        }

        /// <summary>
        /// Prepare to partitioning.
        /// </summary>
        /// <param name="pc">partitions count</param>
        protected void Prepare(int pc)
        {
            InitPartitionsWeigths(pc);
            EraseNodesPartitions();
            MidWeight = (double)Grid.CellsCount() / (double)pc;
            MaxWeight = MidWeight;
        }

        /// <summary>
        /// Get maximum block without partiotion.
        /// </summary>
        /// <returns>maximum block</returns>
        protected Block MaxBlockWithoutPartition()
        {
            Block cur = null;

            foreach (Block b in Grid.NoPartitionBlocks)
            {
                if (cur == null)
                {
                    cur = b;
                }
                else if (b.Canvas.CellsCount > cur.Canvas.CellsCount)
                {
                    cur = b;
                }
            }

            return cur;
        }

        /// <summary>
        /// Set block partition number.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="partition">partition number</param>
        public void SetBlockPartition(Block b, int partition)
        {
            b.PartitionNumber = partition;
            PartitionsWeights[partition] += b.Canvas.CellsCount;
            MaxWeight = Math.Max(MaxWeight, PartitionsWeights[partition]);
        }
    }
}
