// Author: Alexey Rybakov

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
    }
}
