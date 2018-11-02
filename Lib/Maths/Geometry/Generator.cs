using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Generator of 3D objects.
    /// </summary>
    public class PointsGenerator
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
            int steps = 1000;
            Point[] ps = RandomPointsInParallelepiped(n, par);
            Vector[] fs = new Vector[ps.Count()];

            // Simulate gravity in terms of toroid distances.
            for (int h = 0; h < steps; h++)
            {
                // Zero all forces.
                for (int i = 0; i < ps.Count(); i++)
                {
                    fs[i] = new Vector(0.0, 0.0, 0.0);
                }

                // Calculate all forces.
                for (int i = 0; i < ps.Count(); i++)
                {
                    for (int j = i + 1; j < ps.Count(); j++)
                    {
                        Vector d = ps[i].ToroidDir(ps[j], par);

                        // Scale to 1.
                        d.Scale(1.0 / par.Width, 1.0 / par.Height, 1.0 / par.Depth);

                        // Calculate force.
                        Vector f = d * (1 / (d.Mod * d.Mod2));

                        // Add to sum forces for i-th and j-th points.
                        fs[j] += f;
                        fs[i] -= f;
                    }
                }

                // Now correct positions of all points.
                for (int i = 0; i < ps.Count(); i++)
                {
                    while (fs[i].Mod > 0.01 * par.Radius)
                    {
                        fs[i] *= 0.1;
                    }

                    ps[i].ToroidMove(fs[i], par);
                }
            }

            // Now we have all point.
            // Last action is to move barycenter of all these points to the center of the rectangle.
            Vector b = new Vector(0.0, 0.0, 0.0);
            for (int i = 0; i < ps.Count(); i++)
            {
                b += ps[i];
            }
            b /= ps.Count();
            b -= par.Mid;
            for (int i = 0; i < ps.Count(); i++)
            {
                ps[i].ToroidMove(-b, par);
            }

            return ps;
        }
    }
}
