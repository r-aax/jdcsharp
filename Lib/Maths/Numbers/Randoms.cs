// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Geometry;

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
        /// Random value from 0 to 1.
        /// </summary>
        /// <returns>random floating value in [0, 1]</returns>
        public static double Random01()
        {
            return RandomInInterval(0.0, 1.0);
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
        /// Ranndom boolean value with given true probability.
        /// </summary>
        /// <param name="true_p">probability of true value</param>
        /// <returns>bool value</returns>
        public static bool RandomBool(double true_p)
        {
            return Rand.NextDouble() <= true_p;
        }

        /// <summary>
        /// Random boolean.
        /// </summary>
        /// <returns>random boolean</returns>
        public static bool RandomBool()
        {
            return RandomBool(0.5);
        }
    }
}
