using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.IO
{
    /// <summary>
    /// Converter to string.
    /// </summary>
    public class ToStringConverter
    {
        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <param name="a">array</param>
        /// <returns>string</returns>
        public static string Convert(double[] a)
        {
            string r = "[";

            if (a.Length > 0)
            {
                r += a[0].ToString();
            }

            for (int i = 1; i < a.Length; i++)
            {
                r += String.Format(", {0}", a[i]);
            }

            r += "]";

            return r;
        }
    }
}
