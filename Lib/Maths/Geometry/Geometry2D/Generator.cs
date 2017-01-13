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
            return RandomPointsInRect(n, rect);
        }
    }
}
