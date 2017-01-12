using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry.Geometry3D;

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
                // Find min partition.
                int min_partition_index = -1;
                double min_partition_weight = 0.0;
                for (int i = 0; i < pc; i++)
                {
                    if ((min_partition_index == -1) || (PartitionsWeights[i] < min_partition_weight))
                    {
                        min_partition_index = i;
                        min_partition_weight = PartitionsWeights[min_partition_index];
                    }
                }

                Debug.Assert(false, "Not implemented.");
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

            Debug.Assert(false, "Not implemented.");
        }
    }
}
