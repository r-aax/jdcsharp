using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.MathMod.Grid.Cut;

namespace Lib.MathMod.Grid.Partitioning
{
    /// <summary>
    /// Partitioner with minimum cuts count.
    /// </summary>
    public class MinimalCutsPartitioner : Partitioner
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g"></param>
        public MinimalCutsPartitioner(StructuredGrid g)
            : base(g)
        {
        }

        /// <summary>
        /// Partiton the grid.
        /// </summary>
        /// <param name="pc">partitions count</param>
        /// <param name="margin">margin</param>
        public void Partition(int pc, int margin)
        {
            Prepare(pc);

            bool is_overflow = true;

            // Do infinite loop.
            do
            {
                if (Grid.NoPartitionBlocks.Count == 0)
                {
                    // There are no blocks without partitions.
                    // The distribution has completed.
                    break;
                }

                // Create array of weights to fill.
                double[] weights = new double[PartitionsWeights.Length];
                for (int i = 0; i < weights.Length; i++)
                {
                    weights[i] = MaxWeight - PartitionsWeights[i];
                }
            
                Cut.Cut c = null;
                double dev;
                int partition;

                // Actions:
                // 1) try to find full block with underflow.
                // 2) first time try to find overflow (full, cuts).
                // 3) try underflow
                // 4) try any overflow

                c = GridCutter.NearestCut(Grid, weights, margin, true, false, false, out partition, out dev);

                if (c == null)
                {
                    if (!is_overflow)
                    {
                        c = GridCutter.NearestCut(Grid, weights, margin, true, true, false, out partition, out dev);
                    }

                    if (c == null)
                    {
                        c = GridCutter.NearestCut(Grid, weights, margin, true, true, true, out partition, out dev);
                    }
                }

                if (!c.IsNoCut)
                {
                    GridCutter.Cut(c.B, c.D, c.Pos);
                }

                SetBlockPartition(c.B, partition);
            }
            while (true);
        }
    }
}
