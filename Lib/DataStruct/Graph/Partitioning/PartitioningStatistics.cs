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
        /// String with distribution.
        /// </summary>
        /// <param name="g">graph</param>
        /// <returns>string</returns>
        public static string DistributionString(Graph g)
        {
            string str = "";

            if (g.Order > 0)
            {
                str = g.Nodes[0].Partition.ToString();

                if (g.Order > 1)
                {
                    for (int i = 1; i < g.Order; i++)
                    {
                        str = String.Format("{0}-{1}", str, g.Nodes[i].Partition);
                    }
                }
            }

            return str;
        }

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

        /// <summary>
        /// Get quality values with cluster topology using.
        /// </summary>
        /// <param name="g">task graph</param>
        /// <param name="h">cluster graph</param>
        /// <param name="t1_max">maximum first time</param>
        /// <param name="t1_min">minimum first time</param>
        /// <param name="t2_max">maximum second time</param>
        /// <param name="t2_min">minimum second time</param>
        public static void TopoQualityValues(Graph g, Graph h,
                                             out double t1_max, out double t1_min,
                                             out double t2_max, out double t2_min)
        {
            // Nodes.
            double[] hnw = new double[h.Order];
            for (int i = 0; i < hnw.Count(); i++)
            {
                hnw[i] = 0.0;
            }
            foreach (Node node in g.Nodes)
            {
                hnw[node.Partition] += node.Weight;
            }
            for (int i = 0; i < hnw.Count(); i++)
            {
                hnw[i] /= h.Nodes[i].Weight;
            }
            t1_max = hnw.Max();
            t1_min = hnw.Min();

            // Edges.
            double[] hew = new double[h.Size];
            for (int i = 0; i < hew.Count(); i++)
            {
                hew[i] = 0.0;
            }
            foreach (Edge edge in g.Edges)
            {
                hew[h.FindEdgeIndex(edge.A.Partition, edge.B.Partition)] += edge.Weight;
            }
            for (int i = 0; i < hew.Count(); i++)
            {
                hew[i] /= h.Edges[i].Weight;
            }
            t2_max = hew.Max();
            t2_min = hew.Min();
        }

        /// <summary>
        /// Get quality value with cluster topology using.
        /// </summary>
        /// <param name="g">task graph</param>
        /// <param name="h">cluster graph</param>
        /// <returns>quality value</returns>
        public static double TopoQualityValue(Graph g, Graph h)
        {
            double t1_max, t1_min, t2_max, t2_min;

            TopoQualityValues(g, h, out t1_max, out t1_min, out t2_max, out t2_min);

            // Full time of one iteration of calculations and interprocess exchanges.
            return t1_max + t2_max;
        }

        /// <summary>
        /// Get quality description with cluster topology using.
        /// </summary>
        /// <param name="g">task graph</param>
        /// <param name="h">cluster graph</param>
        /// <returns>quality description</returns>
        public static string TopoQualityDescription(Graph g, Graph h)
        {
            double t1_max, t1_min, t2_max, t2_min;

            TopoQualityValues(g, h, out t1_max, out t1_min, out t2_max, out t2_min);

            // Full time of one iteration of calculations and interprocess exchanges.
            double t_max = t1_max + t2_max;
            double t_min = t1_min + t2_min;
            double d = (t_max - t_min) / Math.Abs(t_max) * 100.0;

            return String.Format("{0:0.##}+{1:0.##}={2:0.##}/{3:0.##}+{4:0.##}={5:0.##}, d={6:0.##}%",
                                 t1_max, t2_max, t_max, t1_min, t2_min, t_min, d);
        }
    }
}
