// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Numbers
{
    /// <summary>
    /// Naturals numbers analyzer.
    /// </summary>
    public static class Naturals
    {
        /// <summary>
        /// Check if natural number is prime.
        /// </summary>
        /// <param name="n">natural number</param>
        /// <returns><c>true</c> - if number is prime, <c>false</c> - if number is not prime</returns>
        public static bool IsPrime(Int64 n)
        {
            if (n <= 1)
            {
                return false;
            }

            if (n == 2)
            {
                return true;
            }

            // Simple check for factors from 2 to sqrt(n).
            for (Int64 i = 2; i <= (int)Math.Sqrt(n); i++)
            {
                if (IsPrime(i) && (n % i == 0))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
