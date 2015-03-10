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
    }
}
