// Author: Alexey Rybakov

using System;
using System.Diagnostics;

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

            double abs_dev = AbsDeviation(d);
            double abs_mid = Math.Abs(Sum(d) / d.Length);

            return abs_dev / abs_mid;
        }

        /// <summary>
        /// Over deviation from middle value.
        /// </summary>
        /// <param name="d">positive values array</param>
        /// <returns>over deviation</returns>
        public static double AbsOverDeviationOfPositives(double[] d)
        {
            if (d.Length <= 1)
            {
                return 0.0;
            }

            double mid = Sum(d) / d.Length;
            double[] r = new double[d.Length];

            for (int i = 0; i < d.Length; i++)
            {
                r[i] = Math.Max(d[i] - mid, 0.0);
            }

            return Max(r);
        }

        /// <summary>
        /// Relative over deviation from middle values.
        /// </summary>
        /// <param name="d">positive values array</param>
        /// <returns>relative over deviation</returns>
        public static double RelOverDeviationOfPositives(double[] d)
        {
            if (d.Length <= 1)
            {
                return 0.0;
            }

            double abs_dev = AbsOverDeviationOfPositives(d);
            double abs_mid = Sum(d) / d.Length;

            return abs_dev / abs_mid;
        }

        /// <summary>
        /// Square difference between two arrays.
        /// </summary>
        /// <param name="a">first array</param>
        /// <param name="b">second array</param>
        /// <returns>square difference</returns>
        public static double SquareDifference(double[] a, double[] b)
        {
            Debug.Assert(a.Length == b.Length);

            double sq = 0.0;
            
            for (int i = 0; i < a.Length; i++)
            {
                double d = a[i] - b[i];

                sq += d * d;
            }

            return sq;
        }

        /// <summary>
        /// Mean square dirrerence.
        /// </summary>
        /// <param name="a">first array</param>
        /// <param name="b">seconds array</param>
        /// <returns>mean square difference</returns>
        public static double MeanSquareDifference(double[] a, double[] b)
        {
            return SquareDifference(a, b) / a.Length;
        }
    }
}
