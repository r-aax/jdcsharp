using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        /// Random points in rectangle.
        /// </summary>
        /// <param name="n">points count</param>
        /// <param name="rect">rectangle</param>
        /// <returns></returns>
        public static Point[] RandomPointsInRect(int n, Rect rect)
        {
            Point[] ps = new Point[n];

            for (int i = 0; i < ps.Count(); i++)
            {
                ps[i] = Point.Random(rect);
            }

            return ps;
        }

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

        /// <summary>
        /// Generate points on circle.
        /// </summary>
        /// <param name="c">circle</param>
        /// <param name="dir">direction</param>
        /// <param name="sa">start angle</param>
        /// <param name="da">direction</param>
        /// <param name="n">number of points</param>
        /// <returns>points</returns>
        public static Point[] PointsOnCircle(Circle c, Direction dir,
                                             double sa, double da,
                                             int n)
        {
            Debug.Assert((dir == Direction.Clockwise) || (dir == Direction.ContraClockwise));

            Point[] ps = new Point[n];

            for (int i = 0; i < ps.Count(); i++)
            {
                double a = sa + i * da;

                if (dir == Direction.Clockwise)
                {
                    a = -a;
                }

                ps[i] = c.Center + new Vector(c.Radius, 0.0);
                ps[i].Rot(c.Center, a);
            }

            return ps;
        }

        /// <summary>
        /// Generate points on circle.
        /// </summary>
        /// <param name="c">circle</param>
        /// <param name="sa">start angle</param>
        /// <param name="n">number of points</param>
        /// <returns>points</returns>
        public static Point[] PointsOnCircle(Circle c, int n)
        {
            return PointsOnCircle(c, Direction.ContraClockwise, 0.0, 2 * Math.PI / n, n);
        }
    }
}
