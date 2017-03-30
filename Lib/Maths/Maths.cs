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
        /// Hypot 2 function.
        /// </summary>
        /// <param name="a">first cathet</param>
        /// <param name="b">second cathet</param>
        /// <returns>hypotenuse in double power</returns>
        public static double Hypot2(double a, double b)
        {
            return a * a + b * b;
        }
    }
}
