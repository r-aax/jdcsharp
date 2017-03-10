using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Utils;

namespace Lib.Maths
{
    /// <summary>
    /// Weights distrubution between bags.
    /// </summary>
    public class WeightsDistribution
    {
        /// <summary>
        /// Greedy distribution.
        /// </summary>
        /// <param name="weights">weights array</param>
        /// <param name="bags_count">bags count</param>
        /// <param name="bags_weights">weights of bags</param>
        /// <param name="weights_to_bags">weights to bags distribution indices</param>
        public static void GreedyDistribution(double[] weights, int bags_count,
                                              double[] bags_weights, int[] weights_to_bags)
        {
            if ((weights.Length != weights_to_bags.Length)
                || (bags_weights.Length != bags_count))
            {
                throw new Exception("wrong data");
            }

            // Zero bags weights.
            for (int i = 0; i < bags_weights.Length; i++)
            {
                bags_weights[i] = 0.0;
            }

            // Zero weights to bags.
            for (int i = 0; i < weights_to_bags.Length; i++)
            {
                weights_to_bags[0] = 0;
            }

            // Sort.
            Array.Sort(weights);

            for (int i = 0; i < weights.Length; i++)
            {
                int min_ind = Arrays.MinIndex(bags_weights);
                weights_to_bags[i] = min_ind;
                bags_weights[min_ind] += weights[i];
            }
        }
    }
}
