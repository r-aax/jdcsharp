// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Maths.Numbers
{
    /// <summary>
    /// Cyclic arithmetic with given base.
    /// </summary>
    public class CyclicArithmetic
    {
        /// <summary>
        /// Base.
        /// </summary>
        private int Base;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="base_">base</param>
        public CyclicArithmetic(int base_)
        {
            Base = base_;
        }

        /// <summary>
        /// Sum.
        /// </summary>
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>sum</returns>
        public int Add(int a, int b)
        {
            Debug.Assert((a >= 0) && (b >= 0));

            return (a + b) % Base;
        }

        /// <summary>
        /// Subtraction of two values.
        /// </summary>
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>result</returns>
        public int Sub(int a, int b)
        {
            Debug.Assert((a >= 0) && (b >= 0));

            return (a + (Base - b)) % Base;
        }

        /// <summary>
        /// Multiplication.
        /// </summary>
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>result</returns>
        public int Mul(int a, int b)
        {
            Debug.Assert((a >= 0) && (b >= 0));

            return (a * b) % Base;
        }

        /// <summary>
        /// Power.
        /// </summary>
        /// <param name="a">value</param>
        /// <param name="p">power</param>
        /// <returns>result</returns>
        public int Pow(int a, int p)
        {
            if (p == 0)
            {
                return 1;
            }

            Debug.Assert(p > 0);

            int res = a % Base;

            for (int i = 1; i < p; i++)
            {
                res = (res * a) % Base;
            }

            return res;
        }

        /// <summary>
        /// Distance.
        /// </summary>
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>distance</returns>
        public int Dist(int a, int b)
        {
            int d = System.Math.Abs(a - b);
            return System.Math.Min(d, Base - d);
        }

        /// <summary>
        /// Find middle multiplicated by 2.
        /// </summary>
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>middle</returns>
        public int DoubleMid(int a, int b)
        {
            // Middle can be not integer (with fraction 0.5),
            // so we calculate middle multiplicated by 2.
            // If multiplicated middle is even then middle is integer.
            // If multiplicated middle is odd then middle is not integer.
            CyclicArithmetic arith2 = new CyclicArithmetic(Base * 2);
            int a2 = a * 2;
            int b2 = b * 2;
            int mid1 = (a2 + b2) / 2;
            int mid2 = arith2.Add(mid1, Base);

            return Math.Min(mid1, mid2);
        }
    }
}
