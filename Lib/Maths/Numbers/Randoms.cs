// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using Point3D = Lib.Maths.Geometry.Geometry3D.Point;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;

namespace Lib.Maths.Numbers
{
    /// <summary>
    /// Random numbers class.
    /// </summary>
    public static class Randoms
    {
        /// <summary>
        /// Pseudo random numbers generator.
        /// </summary>
        private static readonly Random Rand = new Random();

        /// <summary>
        /// Random in interval.
        /// </summary>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <returns>random float</returns>
        public static double RandomInInterval(double from, double to)
        {
            Debug.Assert(from <= to);

            return from + (to - from) * Rand.NextDouble();
        }

        /// <summary>
        /// Random in interval.
        /// </summary>
        /// <param name="interval">interval</param>
        /// <returns>random float</returns>
        public static double RandomInInterval(Interval interval)
        {
            return RandomInInterval(interval.L, interval.H);
        }

        /// <summary>
        /// Random integer in interval.
        /// </summary>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <returns>random integer</returns>
        public static int RandomInInterval(int from, int to)
        {
            int r = (int)RandomInInterval((double)from, (double)(to + 1));

            if (r > to)
            {
                r = to;
            }

            return r;
        }

        /// <summary>
        /// Random boolean.
        /// </summary>
        /// <returns>random boolean</returns>
        public static bool RandomBool()
        {
            return Rand.NextDouble() < 0.5;
        }
    }
}
