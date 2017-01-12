using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Numbers;

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
        private static Point[] RandomPoints(Graph g, int pc)
        {
            Parallelepiped par = g.WraparoundParallelepiped();
            Point[] ps = new Point[pc];

            for (int i = 0; i < pc; i++)
            {
                ps[i] = Point.Random(par);
            }

            return ps;
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
                    if ((nearest_point_index == -1) || (n.Point3D.Dist(points[pi]) < nearest_point_dist))
                    {
                        nearest_point_index = pi;
                        nearest_point_dist = n.Point3D.Dist(points[nearest_point_index]);
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
        public static void PartirionToNearestPropagation(Graph g, int pc)
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
                    ms[i] = no_partition_nodes[i].Point3D.Dist(points[min_partition_index])
                            + DistFromNodeToNodes(no_partition_nodes[i], partition_nodes);
                }

                // We use nearest node for propagation.
                Node n = no_partition_nodes[NumbersArrays.MinIndex(ms)];
                n.Partition = min_partition_index;
                PartitionsWeights[min_partition_index] += n.Weight;
            }
        }

        /// <summary>
        /// Partitioning with propagation by edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        public static void PartitionEdgesPropagation(Graph g, int pc)
        {
            Point[] points = RandomPoints(g, pc);
            InitPartitionsWeigths(pc);
            EraseNodesPartitions(g);

            // In infinite loop we should propagate min partition.
            while (true)
            {
                int min_partition_index = NumbersArrays.MinIndex(PartitionsWeights);

                Debug.Assert(false, "Not implemented.");
            }
        }
    }
}
