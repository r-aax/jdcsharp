using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Numbers;
using Lib.Maths.Geometry;

namespace Lib.DataStruct.Graph.Partitioning
{
    /// <summary>
    /// Partition by method of random points n volume.
    /// We take n points in volume of graph and then
    /// associate each node with nearest point in simple case
    /// or apply some propagation methods.
    /// </summary>
    public class RandomVolumePointsPartitioner : Partitioner
    {
        /// <summary>
        /// Get array of random points.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        /// <returns>random points</returns>
        public static Point[] RandomPoints(Graph g, int pc)
        {
            return PointsGenerator.UniformPointsInParallelepiped(pc, g.WraparoundParallelepiped(0.01));
        }

        /// <summary>
        /// Partition.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        public static void PartitionSimple(Graph g, int pc)
        {
            Point[] points = RandomPoints(g, pc);

            // Analize each node.
            foreach (Node n in g.Nodes)
            {
                int nearest_point_index = -1;
                double nearest_point_dist = 0.0;

                // Find nearest point.
                for (int pi = 0; pi < pc; pi++)
                {
                    if ((nearest_point_index == -1) || (n.P.Dist(points[pi]) < nearest_point_dist))
                    {
                        nearest_point_index = pi;
                        nearest_point_dist = n.P.Dist(points[nearest_point_index]);
                    }
                }

                n.Partition = nearest_point_index;
            }
        }

        /// <summary>
        /// Partitioning with propagation to nearest node.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        public static void PartitionToNearestPropagation(Graph g, int pc)
        {
            Point[] points = RandomPoints(g, pc);
            InitPartitionsWeigths(pc);
            EraseNodesPartitions(g);

            // In infinite loop we should propagate min partition.
            while (true)
            {
                List<Node> no_partition_nodes = NoPartitionNodes(g);

                // We finish when there is no nodes without partition.
                // Nowhere to propagate.
                if (no_partition_nodes.Count == 0)
                {
                    break;
                }

                int min_partition_index = NumbersArrays.MinIndex(PartitionsWeights);
                List<Node> partition_nodes = PartitionNodes(g, min_partition_index);

                // Allocate array for no partition nodes metrics.
                double[] ms = new double[no_partition_nodes.Count];
                for (int i = 0; i < ms.Count(); i++)
                {
                    ms[i] = no_partition_nodes[i].P.Dist(points[min_partition_index])
                            + DistFromNodeToNodes(no_partition_nodes[i], partition_nodes);
                }

                // We use nearest node for propagation.
                Node n = no_partition_nodes[NumbersArrays.MinIndex(ms)];

                // Update partitions information.
                n.Partition = min_partition_index;
                PartitionsWeights[min_partition_index] += n.Weight;
            }
        }

        /// <summary>
        /// Partitioning with propagation by edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        /// <param name="alpha">parameter for nodes and edges metric prefer</param>
        public static void PartitionEdgesPropagation(Graph g, int pc, double alpha)
        {
            PartitionEdgesPropagation(g, RandomPoints(g, pc), pc, alpha);
        }

        /// <summary>
        /// Partitioning with propagation by edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="rp">random points</param>
        /// <param name="pc">partitions count</param>
        /// <param name="alpha">parameter for nodes and edges metric prefer</param>
        public static void PartitionEdgesPropagation(Graph g, Point[] points, int pc, double alpha)
        {
            Debug.Assert((new IntervalD(0.0, 1.0)).Contains(alpha),
                         "alpha is out of range in RandomVolumePointsPartitioner.PartitionEdgesPropagation");

            InitPartitionsWeigths(pc);
            EraseNodesPartitions(g);

            // In infinite loop we should propagate min partition.
            while (true)
            {
                List<Node> no_partition_nodes = NoPartitionNodes(g);

                // We finish when there is no nodes without partition.
                // Nowhere to propagate.
                if (no_partition_nodes.Count == 0)
                {
                    break;
                }

                int min_partition_index = NumbersArrays.MinIndex(PartitionsWeights);
                List<Node> partition_nodes = PartitionNodes(g, min_partition_index);

                // Node.
                Node n = null;

                // Take neighbourhood without partition.
                List<Node> neighbourhood = GraphOperator.Neighbourhood(partition_nodes);
                List<Node> no_partition_neighbourhood = neighbourhood.FindAll(nn => nn.Partition == -1);

                if ((partition_nodes.Count == 0) || (no_partition_neighbourhood.Count == 0))
                {
                    double[] ms = new double[no_partition_nodes.Count];

                    // If partition is empty we take nearest node to random point.
                    for (int i = 0; i < ms.Count(); i++)
                    {
                        ms[i] = no_partition_nodes[i].P.Dist(points[min_partition_index]);
                    }

                    n = no_partition_nodes[NumbersArrays.MinIndex(ms)];
                }
                else
                {
                    double[] ms = new double[no_partition_neighbourhood.Count];

                    for (int i = 0; i < ms.Count(); i++)
                    {
                        double nm_val = 0.0;
                        double em_val = 0.0;

                        // I. Weight metric.
                        nm_val = no_partition_neighbourhood[i].Weight;

                        // II. Sum of edges weighs metric.
                        Node _n = no_partition_neighbourhood[i];
                        foreach (Edge _e in _n.Edges)
                        {
                            Node _nn = _n.Neighbour(_e);

                            if (_nn.Partition == min_partition_index)
                            {
                                em_val += _e.Weight;
                            }
                        }

                        // Total metric.
                        nm_val = Math.Pow(nm_val, 1.0 / 3.0);
                        em_val = Math.Sqrt(em_val);
                        ms[i] = alpha * nm_val + (1.0 - alpha) * em_val;
                    }

                    n = no_partition_neighbourhood[NumbersArrays.MaxIndex(ms)];
                }

                // Update partitions information.
                n.Partition = min_partition_index;
                PartitionsWeights[min_partition_index] += n.Weight;
            }
        }

        /// <summary>
        /// Partitioning with propagation by edges (nodes metric).
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        public static void PartitionEdgesPropagationNodesMetric(Graph g, int pc)
        {
            PartitionEdgesPropagation(g, pc, 1.0);
        }

        /// <summary>
        /// Partitioning with propagation by edges (edges metric).
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        public static void PartitionEdgesPropagationEdgesMetric(Graph g, int pc)
        {
            PartitionEdgesPropagation(g, pc, 0.0);
        }

        /// <summary>
        /// Partitioning using topology data.
        /// </summary>
        /// <param name="g">task graph</param>
        /// <param name="h">cluster graph</param>
        public static void PartitionWithTopologyData(Graph g, Graph h)
        {
            Point[] points = RandomPoints(g, h.Order);
            InitPartitionsWeigths(h.Order);
            EraseNodesPartitions(g);

            // In infinite loop we should propagate min partition.
            while (true)
            {
                List<Node> no_partition_nodes = NoPartitionNodes(g);

                // We finish when there is no nodes without partition.
                // Nowhere to propagate.
                if (no_partition_nodes.Count == 0)
                {
                    break;
                }

                int min_partition_index = NumbersArrays.MinIndex(PartitionsWeights);
                List<Node> partition_nodes = PartitionNodes(g, min_partition_index);

                // Node.
                Node n = null;

                // Take neighbourhood without partition.
                List<Node> neighbourhood = GraphOperator.Neighbourhood(partition_nodes);
                List<Node> no_partition_neighbourhood = neighbourhood.FindAll(nn => nn.Partition == -1);

                if ((partition_nodes.Count == 0) || (no_partition_neighbourhood.Count == 0))
                {
                    double[] ms = new double[no_partition_nodes.Count];

                    // If partition is empty we take nearest node to random point.
                    for (int i = 0; i < ms.Count(); i++)
                    {
                        ms[i] = no_partition_nodes[i].P.Dist(points[min_partition_index]);
                    }

                    n = no_partition_nodes[NumbersArrays.MinIndex(ms)];
                }
                else
                {
                    double[] ms = new double[no_partition_neighbourhood.Count];

                    for (int i = 0; i < ms.Count(); i++)
                    {
                        double nm_val = 0.0;
                        double em_val = 0.0;

                        // I. Weight metric.
                        nm_val = no_partition_neighbourhood[i].Weight / h.Nodes[min_partition_index].Weight;

                        // II. Sum of edges weighs metric.
                        Node _n = no_partition_neighbourhood[i];
                        foreach (Edge _e in _n.Edges)
                        {
                            Node _nn = _n.Neighbour(_e);

                            if (_nn.Partition == min_partition_index)
                            {
                                em_val += _e.Weight;
                            }
                        }
                        em_val /= h.AvgEdgeWeight;

                        // Total metric.
                        ms[i] = nm_val + em_val;
                    }

                    n = no_partition_neighbourhood[NumbersArrays.MaxIndex(ms)];
                }

                // Update partitions information.
                n.Partition = min_partition_index;
                PartitionsWeights[min_partition_index] += n.Weight / h.Nodes[min_partition_index].Weight;
            }
        }
    }
}
