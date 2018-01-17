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
        /// Generate uniform point in rectangle.
        /// Points have weights.
        /// </summary>
        /// <param name="ws">points weights</param>
        /// <param name="rect">rectangle</param>
        /// <returns>points array</returns>
        public static Point[] UniformPointsInRect(double[] ws, Rect rect)
        {
            int n = ws.Count();
            int steps = 1000;
            Point[] ps = RandomPointsInRect(n, rect);
            Vector[] fs = new Vector[ps.Count()];

            // Simulate antigravity in terms of toroid distances.
            // (points push each other).
            for (int h = 0; h < steps; h++)
            {
                // Zero all forces.
                for (int i = 0; i < ps.Count(); i++)
                {
                    fs[i] = new Vector(0.0, 0.0);
                }

                // Calculate all forces.
                for (int i = 0; i < ps.Count(); i++)
                {
                    for (int j = i + 1; j < ps.Count(); j++)
                    {
                        Vector d = ps[i].ToroidDir(ps[j], rect);

                        // Scale to 1.
                        d.Scale(1.0 / rect.Width, 1.0 / rect.Height, 1.0);

                        // Calculate force.
                        Vector f = (ws[i] * ws[j]) * d * (1 / (d.Mod * d.Mod2));

                        // Add to sum forces for i-th and j-th points.
                        fs[j] += f;
                        fs[i] -= f;
                    }
                }

                // Now correct positions of all points.
                for (int i = 0; i < ps.Count(); i++)
                {
                    while (fs[i].Mod > 0.01 * rect.Radius)
                    {
                        fs[i] *= 0.1;
                    }

                    ps[i].ToroidMove(fs[i], rect);
                }
            }

            // Now we have all point.
            // Last action is to move barycenter of all these points to the center of the rectangle.
            Vector b = new Vector(0.0, 0.0);
            for (int i = 0; i < ps.Count(); i++)
            {
                b += ps[i];
            }
            b /= ps.Count();
            b -= rect.Mid;
            for (int i = 0; i < ps.Count(); i++)
            {
                ps[i].ToroidMove(-b, rect);
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
            double[] ws = new double[n];

            for (int i = 0; i < ws.Count(); i++)
            {
                ws[i] = 1.0;
            }

            return UniformPointsInRect(ws, rect);
        }
    }
}
