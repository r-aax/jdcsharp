using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataStruct.Graph.Partitioning
{
    // Some statistics of partitioning.
    public class PartitioningStatistics
    {
        /// <summary>
        /// Get max partition number of graph.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>max partition number</returns>
        public static int MaxPartitionNumber(Graph g)
        {
            int max = -1;

            foreach (Node n in g.Nodes)
            {
                if (n.Partition > max)
                {
                    max = n.Partition;
                }
            }

            return max;
        }

        /// <summary>
        /// Fill array of partition weights.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>partitions weights array</returns>
        public static double[] PartitionWeights(Graph g)
        {
            double[] ws = new double[MaxPartitionNumber(g) + 1];

            foreach (Node n in g.Nodes)
            {
                ws[n.Partition] += n.Weight;
            }

            return ws;
        }

        /// <summary>
        /// Get maximum partition weight.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>max partition weight</returns>
        public static double MaxPartitionWeight(Graph g)
        {
            return PartitionWeights(g).Max();
        }

        /// <summary>
        /// Get minimum partition weight.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>min partition weight</returns>
        public static double MinPartirionWeight(Graph g)
        {
            return PartitionWeights(g).Min();
        }

        /// <summary>
        /// Get average partition weight.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>average partition weight</returns>
        public static double AvgPartitionWeight(Graph g)
        {
            double[] ws = PartitionWeights(g);

            return ws.Sum() / ws.Count();
        }

        /// <summary>
        /// Get deviation of max partition weight from average
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>deviation of max partition weight from average</returns>
        public static double DeviationMaxPartitionWeightFromAvg(Graph g)
        {
            double max = MaxPartitionWeight(g);
            double avg = AvgPartitionWeight(g);

            return ((max - avg) / avg) * 100.0;
        }

        /// <summary>
        /// String of description partitioning quality.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>partitioning quality description string</returns>
        public static string PartitioningQualityDescription(Graph g)
        {
            return String.Format("partitions = {0}, deviation = {1}",
                                 PartitionWeights(g).Count(),
                                 DeviationMaxPartitionWeightFromAvg(g));
        }
    }
}
