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

            for (int i = 0; i < ws.Count(); i++)
            {
                ws[i] = 0.0;
            }

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
        public static double MinPartitionWeight(Graph g)
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
        /// Sum of edges weights.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>sum weight</returns>
        public static double EdgesWeightsSum(Graph g)
        {
            double d = 0.0;

            foreach (Edge e in g.Edges)
            {
                d += e.Weight;
            }

            return d;
        }

        /// <summary>
        /// Factor of interpartition edges weights.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>interpartition edges factor</returns>
        public static double InterpartitionEdgesFactor(Graph g)
        {
            double[] ws = new double[MaxPartitionNumber(g) + 1];
            double w = 2.0 * EdgesWeightsSum(g) / ws.Count();

            for (int i = 0; i < ws.Count(); i++)
            {
                ws[i] = 0.0;
            }

            foreach (Edge e in g.Edges)
            {
                if (e.A.Partition == e.B.Partition)
                {
                    // Edge in partition.
                    // The weight does not matter.
                    continue;
                }

                ws[e.A.Partition] += e.Weight;
                ws[e.B.Partition] += e.Weight;
            }

            return (ws.Max() / w) * 100.0;
        }

        /// <summary>
        /// String of description partitioning quality.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>partitioning quality description string</returns>
        public static string PartitioningQualityDescription(Graph g)
        {
            return String.Format("partitions = {0}, deviation = {1:0.####}%, cross_edges_f = {2:0.####}%",
                                 PartitionWeights(g).Count(),
                                 DeviationMaxPartitionWeightFromAvg(g),
                                 InterpartitionEdgesFactor(g));
        }
    }
}
