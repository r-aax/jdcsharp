using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Utils;
using Lib.Maths;
using Lib.MathMod.Grid.Cut;

namespace Lib.MathMod.Grid.Partitioning
{
    /// <summary>
    /// Partition with greedy uniform distribution.
    /// </summary>
    public class GreedyUniformPartitioner : Partitioner
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g">grid</param>
        public GreedyUniformPartitioner(StructuredGrid g)
            : base(g)
        {
        }

        /// <summary>
        /// Partition the grid.
        /// </summary>
        /// <param name="pc">partitions count</param>
        /// <param name="iters">max iterations count</param>
        /// <param name="dev">deviation</param>
        /// <returns>diagnostic string</returns>
        public string Partition(int pc, int iters, double dev)
        {
            int total_iters = 0;
            double cur_dev = 0.0;
            string diag = null;

            double[] weights;
            int[] weights_to_partitions;

            Prepare(pc);

            do
            {
                // Distribution parameters.
                int bc = Grid.BlocksCount;
                weights = new double[bc];
                weights_to_partitions = new int[bc];

                // Init weights.
                for (int i = 0; i < bc; i++)
                {
                    weights[i] = Grid.Blocks[i].CellsCount;
                }

                // Distribute.
                WeightsDistribution.GreedyDistribution(weights, pc,
                                                       PartitionsWeights, weights_to_partitions);
                cur_dev = Arrays.RelOverDeviationOfPositives(PartitionsWeights);

                // Check post conditions.
                if (cur_dev <= dev)
                {
                    diag = "deviation is reached";

                    break;
                }
                else if (total_iters >= iters)
                {
                    diag = "max iters count is reached";

                    break;
                }

                // Cut next.
                GridCutter.CutHalfMaxBlock(Grid);
                if (GridCutter.CutRejectedString == null)
                {
                    total_iters++;
                }
                else
                {
                    diag = GridCutter.CutRejectedString;

                    break;
                }
            }
            while (true);

            // Set partitions numbers for grid nodes.
            Grid.SetBlocksPartitionsNumbers(weights_to_partitions);

            return diag;
        }
    }
}
