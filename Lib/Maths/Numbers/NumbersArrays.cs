using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Numbers
{
    /// <summary>
    /// Class for arrays processing.
    /// </summary>
    public class NumbersArrays
    {
        /// <summary>
        /// Index of maximum element of array.
        /// </summary>
        /// <param name="a">array</param>
        /// <returns>index of maximum element</returns>
        public static int MaxIndex(double[] a)
        {
            if (a.Count() == 0)
            {
                return -1;
            }

            int ind = 0;

            for (int i = 1; i < a.Count(); i++)
            {
                if (a[i] > a[ind])
                {
                    ind = i;
                }
            }

            return ind;
        }

        /// <summary>
        /// Index of minimum element of array.
        /// </summary>
        /// <param name="a">array</param>
        /// <returns>index of minimum element</returns>
        public static int MinIndex(double[] a)
        {
            if (a.Count() == 0)
            {
                return -1;
            }

            int ind = 0;

            for (int i = 1; i < a.Count(); i++)
            {
                if (a[i] < a[ind])
                {
                    ind = i;
                }
            }

            return ind;
        }
    }
}
