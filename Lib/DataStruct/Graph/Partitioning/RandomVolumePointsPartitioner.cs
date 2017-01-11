using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry3D;

namespace Lib.DataStruct.Graph.Partitioning
{
    /// <summary>
    /// Partition by method of random points n volume.
    /// We take n points in volume of graph and then
    /// associate each node with nearest point.
    /// </summary>
    public class RandomVolumePointsPartitioner
    {
        /// <summary>
        /// Partition.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="pc">partitions count</param>
        public static void Partition(Graph g, int pc)
        {
            Parallelepiped par = g.WraparoundParallelepiped();

            // Generate random points.
            Point[] points = new Point[pc];
            for (int i = 0; i < pc; i++)
            {
                points[i] = Point.Random(par);
            }

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
    }
}
