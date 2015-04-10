// Author: Alexey Rybakov

using System;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Diagnostics;

namespace Lib.GUI.WindowsForms
{
    /// <summary>
    /// Input/Output for <c>Windows Forms</c>.
    /// </summary>
    public class IO
    {
        /// <summary>
        /// Get <c>int</c> from text box.
        /// </summary>
        /// <param name="tb">text box</param>
        /// <returns>value <c>int</c></returns>
        public static int GetInt(TextBox tb)
        {
            int i = 0;
            NumberFormatInfo info = CultureInfo.GetCultureInfo("en-US").NumberFormat.Clone()
                                    as NumberFormatInfo;
            info.NumberDecimalSeparator = ".";

            try
            {
                i = Convert.ToInt32(tb.Text, info);
            }
            catch (Exception)
            {
                // Not nesessary to fault.
                Debug.Assert(false);
            }

            return i;
        }

        /// <summary>
        /// Get <c>double</c> from text box.
        /// </summary>
        /// <param name="tb">text box</param>
        /// <returns>value <c>double</c></returns>
        public static double GetDouble(TextBox tb)
        {
            double d = 0.0;
            NumberFormatInfo info = CultureInfo.GetCultureInfo("en-US").NumberFormat.Clone()
                                    as NumberFormatInfo;
            info.NumberDecimalSeparator = ".";

            try
            {
                d = Convert.ToDouble(tb.Text, info);
            }
            catch (Exception)
            {
                // Not nesessary to fault.
                Debug.Assert(false);
            }

            return d;
        }

        /// <summary>
        /// Get color.
        /// </summary>
        /// <param name="init_color">initial color</param>
        /// <returns>colot</returns>
        public static Color GetColor(Color init_color)
        {
            Color color = init_color;
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                color = cd.Color;
            }

            return color;
        }

        /// <summary>
        /// Get color.
        /// </summary>
        /// <param name="init_color">initial color</param>
        /// <param name="pb">paint area to deactivate</param>
        /// <param name="paint_handler">paint handler</param>
        /// <returns>color</returns>
        public static Color GetColor(Color init_color,
                                     PictureBox pb,
                                     PaintEventHandler paint_handler)
        {
            Color color;

            pb.Paint -= paint_handler;

            color = GetColor(init_color);

            pb.Paint += new PaintEventHandler(paint_handler);
            pb.Focus();

            return color;
        }

        /// <summary>
        /// Set color for text box <c>TextBox</c>.
        /// </summary>
        /// <param name="tb">text box</param>
        /// <returns>color</returns>
        public static Color SetColor(TextBox tb)
        {
            Color color = GetColor(tb.BackColor);

            tb.BackColor = color;

            return color;
        }

        /// <summary>
        /// Set color for <c>TextBox</c>.
        /// </summary>
        /// <param name="tb">text box</param>
        /// <param name="pb">paint area to deactivate</param>
        /// <param name="paint_handler">paint handler</param>
        /// <returns>color</returns>
        public static Color SetColor(TextBox tb,
                                     PictureBox pb,
                                     PaintEventHandler paint_handler)
        {
            Color color = GetColor(tb.BackColor, pb, paint_handler);

            tb.BackColor = color;

            return color;
        }

        /// <summary>
        /// Input string (or edit given string).
        /// </summary>
        /// <param name="initial_string">initial string</param>
        /// <param name="label">form label</param>
        /// <returns></returns>
        public static string InputString(string initial_string, string label)
        {
            EditStringForm f = new EditStringForm(initial_string, label);

            f.ShowDialog();

            return f.Result;
        }
    }
}
