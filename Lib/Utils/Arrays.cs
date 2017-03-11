// Author: Alexey Rybakov

using System;

namespace Lib.Utils
{
    /// <summary>
    /// Arrays functionality.
    /// </summary>
    public static class Arrays
    {
        /// <summary>
        /// Range of values.
        /// </summary>
        /// <param name="from">from index</param>
        /// <param name="to">to index</param>
        /// <returns>range</returns>
        public static int[] Range(int from, int to)
        {
            int[] range = new int[to - from + 1];

            for (int i = from; i <= to; i++)
            {
                range[i - from] = i;
            }

            return range;
        }

        /// <summary>
        /// Min index.
        /// </summary>
        /// <param name="d">array</param>
        /// <returns>min index</returns>
        public static int MinIndex(double[] d)
        {
            if (d.Length == 0)
            {
                return -1;
            }

            int j = 0;

            for (int i = 1; i < d.Length; i++)
            {
                if (d[i] < d[j])
                {
                    j = i;
                }
            }

            return j;
        }

        /// <summary>
        /// Minimum value of array.
        /// </summary>
        /// <param name="d">array</param>
        /// <returns>minimum value</returns>
        public static double Min(double[] d)
        {
            return d[MinIndex(d)];
        }

        /// <summary>
        /// Max index.
        /// </summary>
        /// <param name="d">array</param>
        /// <returns>max index</returns>
        public static int MaxIndex(double[] d)
        {
            if (d.Length == 0)
            {
                return -1;
            }

            int j = 0;

            for (int i = 1; i < d.Length; i++)
            {
                if (d[i] > d[j])
                {
                    j = i;
                }
            }

            return j;
        }

        /// <summary>
        /// Maximum value of array.
        /// </summary>
        /// <param name="d">array</param>
        /// <returns>maximum value</returns>
        public static double Max(double[] d)
        {
            return d[MaxIndex(d)];
        }

        /// <summary>
        /// Sum of array.
        /// </summary>
        /// <param name="d">array</param>
        /// <returns>sum</returns>
        public static double Sum(double[] d)
        {
            double s = 0.0;

            for (int i = 0; i < d.Length; i++)
            {
                s += d[i];
            }

            return s;
        }

        /// <summary>
        /// Deviation from middle value.
        /// </summary>
        /// <param name="d">values array</param>
        /// <returns>deviation</returns>
        public static double AbsDeviation(double[] d)
        {
            if (d.Length <= 1)
            {
                return 0.0;
            }

            double mid = Sum(d) / d.Length;
            double[] r = new double[d.Length];

            for (int i = 0; i < d.Length; i++)
            {
                r[i] = Math.Abs(mid - d[i]);
            }

            return Max(r);
        }

        /// <summary>
        /// Relative deviation.
        /// </summary>
        /// <param name="d">array</param>
        /// <returns>relative deviation</returns>
        public static double RelDeviation(double[] d)
        {
            if (d.Length <= 1)
            {
                return 0.0;
            }

            return AbsDeviation(d) / Math.Abs(Sum(d));
        }
    }
}
