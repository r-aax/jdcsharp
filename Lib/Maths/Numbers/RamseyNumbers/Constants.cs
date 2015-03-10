// Author: Alexey Rybakov

using System.Diagnostics;

namespace Lib.Maths.Numbers.RamseyNumbers
{
    /// <summary>
    /// Ramsey numbers constants.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Maximum size of clique.
        /// </summary>
        private readonly int MaxCliqueOrder = 10;

        /// <summary>
        /// Ramsey numbers known values bounds.
        /// </summary>
        private int[, ,] Bounds =
        {
            {
                {  6,  6 }, {  9,   9 }, {  14,  14 }, {  18,   18 }, {  23,   23 }, {  28,   28 }, {  36,    36 }, {  40,    42 },
                {  9,  9 }, { 18,  18 }, {  25,  25 }, {  36,   41 }, {  49,   61 }, {  58,   84 }, {  73,   115 }, {  91,   149 },
                { 14, 14 }, { 25,  25 }, {  43,  49 }, {  58,   87 }, {  80,  143 }, { 101,  216 }, { 126,   316 }, { 144,   442 },
                { 18, 18 }, { 36,  41 }, {  58,  87 }, { 102,  165 }, { 113,  298 }, { 132,  495 }, { 169,   780 }, { 179,  1171 },
                { 23, 23 }, { 49,  61 }, {  80, 143 }, { 113,  298 }, { 205,  540 }, { 217, 1031 }, { 241,  1713 }, { 289,  2826 },
                { 28, 28 }, { 58,  84 }, { 101, 216 }, { 132,  495 }, { 217, 1031 }, { 282, 1870 }, { 317,  3583 }, { 331,  6090 },
                { 36, 36 }, { 73, 115 }, { 126, 316 }, { 169,  780 }, { 241, 1713 }, { 317, 3583 }, { 565,  6588 }, { 581, 12677 },
                { 40, 42 }, { 92, 149 }, { 144, 442 }, { 179, 1171 }, { 289, 2826 }, { 331, 6090 }, { 581, 12677 }, { 798, 23556 }
            },
        };

        /// <summary>
        /// Lower bound of Ramsey number.
        /// </summary>
        /// <param name="r">red clique order</param>
        /// <param name="b">blue clique order</param>
        /// <returns>bound</returns>
        public int RLoBound(int r, int b)
        {
            if (r > b)
            {
                return RLoBound(b, r);
            }

            Debug.Assert((r <= MaxCliqueOrder) && (b <= MaxCliqueOrder));

            return Bounds[r - 3, b - 3, 0];
        }

        /// <summary>
        /// Upper bound of Ramsey number.
        /// </summary>
        /// <param name="r">red clique order</param>
        /// <param name="b">blue clique order</param>
        /// <returns>bound</returns>
        public int RHiBound(int r, int b)
        {
            if (r > b)
            {
                return RHiBound(b, r);
            }

            Debug.Assert((r <= MaxCliqueOrder) && (b <= MaxCliqueOrder));

            return Bounds[r - 3, b - 3, 1];
        }

        /// <summary>
        /// Ramsey number.
        /// </summary>
        /// <param name="r">red clique order</param>
        /// <param name="b">blue clique order</param>
        /// <returns>Ramsey number</returns>
        public int R(int r, int b)
        {
            int lo = RLoBound(r, b);

            Debug.Assert(lo == RHiBound(r, b));

            return lo;
        }
    }
}
