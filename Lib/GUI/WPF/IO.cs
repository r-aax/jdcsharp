// Author: Alexey Rybakov

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
        /// Get int from <c>int</c> text box.
        /// </summary>
        /// <param name="text_box">text box</param>
        /// <returns>value</returns>
        public static int GetInt(TextBox text_box)
        {
            return Lib.Utils.Convert.GetInt(text_box.Text);
        }
        
        /// <summary>
        /// Get double from <c>double</c> text box.
        /// </summary>
        /// <param name="text_box">text box</param>
        /// <returns>value</returns>
        public static double GetDouble(TextBox text_box)
        {
            return Lib.Utils.Convert.GetDouble(text_box.Text);
        }

        /// <summary>
        /// Input string (or edit given string).
        /// </summary>
        /// <param name="ini">ini</param>
        /// <param name="label">form label</param>
        /// <returns>string</returns>
        public static string InputString(string ini, string label)
        {
            EditStringWindow w = new EditStringWindow(ini, label);

            w.ShowDialog();

            return w.Result;
        }

        /// <summary>
        /// Input string (or edit given string).
        /// </summary>
        /// <param name="label">form label</param>
        /// <returns>string</returns>
        public static string InputString(string label)
        {
            return InputString("", label);
        }

        /// <summary>
        /// Input int.
        /// </summary>
        /// <param name="ini">ini</param>
        /// <param name="label">window label</param>
        /// <returns>value</returns>
        public static int InputInt(int ini, string label)
        {
            return Lib.Utils.Convert.GetInt(InputString(ini.ToString(), label));
        }

        /// <summary>
        /// Input double.
        /// </summary>
        /// <param name="ini">ini</param>
        /// <param name="label">window label</param>
        /// <returns>value</returns>
        public static double InputDouble(double ini, string label)
        {
            return Lib.Utils.Convert.GetDouble(InputString(ini.ToString(), label));
        }
    }
}
