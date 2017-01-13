using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// Generator for 2D geometric objects.
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Generate array of random points in rectangle.
        /// </summary>
        /// <param name="n">count of points</param>
        /// <param name="rect">rectangle</param>
        /// <returns>array of random points</returns>
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
        /// Generate uniform points in rectangle.
        /// </summary>
        /// <param name="n">count of points</param>
        /// <param name="rect">rectangle</param>
        /// <returns>uniform points</returns>
        public static Point[] UniformPointsInRect(int n, Rect rect)
        {
            Point[] ps = RandomPointsInRect(n, rect);
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
                    vs[i] = new Vector(1.0 / ((ps[i].X - rect.Left) * (ps[i].X - rect.Left)), 0.0)
                            + new Vector(-1.0 / ((ps[i].X - rect.Right) * (ps[i].X - rect.Right)), 0.0)
                            + new Vector(0.0, 1.0 / ((ps[i].Y - rect.Bottom) * (ps[i].Y - rect.Bottom)))
                            + new Vector(0.0, -1.0 / ((ps[i].Y - rect.Top) * (ps[i].Y - rect.Top)));
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
