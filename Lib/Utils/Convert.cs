using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;

namespace Lib.Utils
{
    /// <summary>
    /// Class for converting string to int or double.
    /// </summary>
    public class Convert
    {
        /// <summary>
        /// Global format.
        /// </summary>
        private static NumberFormatInfo Info = CultureInfo.GetCultureInfo("en-US").NumberFormat.Clone()
                                               as NumberFormatInfo;

        /// <summary>
        /// Get int from string.
        /// </summary>
        /// <param name="t">string</param>
        /// <returns>int</returns>
        public static int GetInt(string t)
        {
            int i = 0;
            Info.NumberDecimalSeparator = ".";

            try
            {
                i = System.Convert.ToInt32(t, Info);
            }
            catch (Exception)
            {
                // Not nesessary to fault.
                Debug.Assert(false);
            }

            return i;
        }

        /// <summary>
        /// Get double from string.
        /// </summary>
        /// <param name="t">string</param>
        /// <returns>double</returns>
        public static double GetDouble(string t)
        {
            double d = 0.0;
            Info.NumberDecimalSeparator = ".";

            try
            {
                d = System.Convert.ToDouble(t, Info);
            }
            catch (Exception)
            {
                // Not nesessary to fault.
                Debug.Assert(false);
            }

            return d;
        }

        /// <summary>
        /// Convert double to string.
        /// </summary>
        /// <param name="d">double</param>
        /// <returns>string</returns>
        public static string GetString(double d)
        {
            Info.NumberDecimalSeparator = ".";

            return System.Convert.ToString(d, Info);
        }
    }
}
