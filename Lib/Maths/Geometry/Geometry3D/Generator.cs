using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry.Geometry3D
{
    /// <summary>
    /// Generator of 3D objects.
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Generate random points in parallelepiped.
        /// </summary>
        /// <param name="n">count of points</param>
        /// <param name="par">parallelepiped</param>
        /// <returns>random points</returns>
        public static Point[] RandomPointsInParallelepiped(int n, Parallelepiped par)
        {
            Point[] ps = new Point[n];

            for (int i = 0; i < ps.Count(); i++)
            {
                ps[i] = Point.Random(par);
            }

            return ps;
        }

        /// <summary>
        /// Generate random points on parallelepiped surface
        /// </summary>
        /// <param name="n">count of points</param>
        /// <param name="par">parallelepiped</param>
        /// <returns>random points</returns>
        public static Point[] RandomPointsOnParallelepipedSurface(int n, Parallelepiped par)
        {
            Point[] ps = new Point[n];

            for (int i = 0; i < ps.Count(); i++)
            {
                ps[i] = Point.RandomOnSurface(par);
            }

            return ps;
        }

        /// <summary>
        /// Generate uniform points in parallelepiped.
        /// </summary>
        /// <param name="n">count of points</param>
        /// <param name="par">parallelepiped</param>
        /// <returns>points</returns>
        public static Point[] UniformPointsInParallelepiped(int n, Parallelepiped par)
        {
            return RandomPointsInParallelepiped(n, par);
        }
    }
}
