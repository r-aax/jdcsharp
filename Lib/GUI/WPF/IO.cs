// Copyright Joy Developing.

using System;
using System.Windows.Controls;
using System.Globalization;
using System.Diagnostics;

namespace Lib.GUI.WPF
{
    /// <summary>
    /// Input/output for <c>WPF</c>.
    /// </summary>
    public class IO
    {
        /// <summary>
        /// Get double from <c>double</c> text box.
        /// </summary>
        /// <param name="text_box">text box</param>
        /// <returns>value</returns>
        public static double GetDouble(TextBox text_box)
        {
            double d = 0.0;
            NumberFormatInfo info = CultureInfo.GetCultureInfo("en-US").NumberFormat.Clone()
                                    as NumberFormatInfo;
            info.NumberDecimalSeparator = ".";

            try
            {
                d = Convert.ToDouble(text_box.Text, info);
            }
            catch (Exception)
            {
                // Not nessesary to fault.
                Debug.Assert(false);
            }

            return d;
        }
    }
}
