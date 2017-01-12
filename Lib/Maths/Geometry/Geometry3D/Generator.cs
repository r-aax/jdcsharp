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
            Point[] ps = RandomPointsInParallelepiped(n, par);
            Vector[] vs = new Vector[ps.Count()];

            // Some actions to improve random points configurations.
            // To make it more uniform.
            // It is dangerous method.
            // We just simulate 1000 steps when points push each other to
            // make distribution more uniform.
            for (int h = 0; h < 1000; h++)
            {
                for (int i = 0; i < ps.Count(); i++)
                {
                    vs[i] = new Vector(1.0 / ((ps[i].X - par.Left) * (ps[i].X - par.Left)), 0.0, 0.0)
                            + new Vector(-1.0 / ((ps[i].X - par.Right) * (ps[i].X - par.Right)), 0.0, 0.0)
                            + new Vector(0.0, 1.0 / ((ps[i].Y - par.Bottom) * (ps[i].Y - par.Bottom)), 0.0)
                            + new Vector(0.0, -1.0 / ((ps[i].Y - par.Top) * (ps[i].Y - par.Top)), 0.0)
                            + new Vector(0.0, 0.0, 1.0 / ((ps[i].Z - par.Back) * (ps[i].Z - par.Back)))
                            + new Vector(0.0, 0.0, -1.0 / ((ps[i].Z - par.Front) * (ps[i].Z - par.Front)));
                }

                for (int i = 0; i < ps.Count(); i++)
                {
                    for (int j = 0; j < ps.Count(); j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        Vector v = ps[j] - ps[i];
                        Vector f = (1.0 / (v.Mod * v.Mod2)) * v;
                        vs[j] += f;
                        vs[i] -= f;
                    }
                }

                for (int i = 0; i < ps.Count(); i++)
                {
                    ps[i] += 0.01 * vs[i];
                }
            }

            return ps;
        }
    }
}
