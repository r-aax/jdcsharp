using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths
{
    /// <summary>
    /// General maths functions.
    /// </summary>
    public class Maths
    {
        /// <summary>
        /// Small value.
        /// </summary>
        public static double Eps = 1e-9;

        /// <summary>
        /// Hypot 2 function.
        /// </summary>
        /// <param name="a">first cathet</param>
        /// <param name="b">second cathet</param>
        /// <returns>hypotenuse in double power</returns>
        public static double Hypot2(double a, double b)
        {
            return a * a + b * b;
        }

        /// <summary>
        /// Hypot function.
        /// </summary>
        /// <param name="a">first cathet</param>
        /// <param name="b">second cathet</param>
        /// <returns>hypotenuse in double power</returns>
        public static double Hypot(double a, double b)
        {
            return Math.Sqrt(Hypot2(a, b));
        }
    }
}
