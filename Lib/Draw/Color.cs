// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Draw
{
    /// <summary>
    /// Color.
    /// </summary>
    public class Color : ICloneable
    {
        /// <summary>
        /// Alpha channel.
        /// </summary>
        public byte A = 0;

        /// <summary>
        /// Red.
        /// </summary>
        public byte R = 0;

        /// <summary>
        /// Green.
        /// </summary>
        public byte G = 0;

        /// <summary>
        /// Blue.
        /// </summary>
        public byte B = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">alpha value</param>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        public Color(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Constructor from <c>System.Windows.Media</c>.
        /// </summary>
        /// <param name="color">color</param>
        public Color(System.Windows.Media.Color color)
            : this(color.A, color.R, color.G, color.B)
        {
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new Color(A, R, G, B);
        }

        /// <summary>
        /// Shift to black.
        /// </summary>
        /// <param name="k">shift factor</param>
        /// <returns>new color</returns>
        public Color ShiftToBlack(double k)
        {
            Debug.Assert(k >= 1.0);

            return new Color(A, (byte)(R / k), (byte)(G / k), (byte)(B / k));
        }

        /// <summary>
        /// Shift to white.
        /// </summary>
        /// <param name="k">shift factor</param>
        /// <returns>new color</returns>
        public Color ShiftToWhite(double k)
        {
            Debug.Assert(k >= 1.0);

            return new Color(A,
                             (byte)(255 - (255 - R) / k),
                             (byte)(255 - (255 - G) / k),
                             (byte)(255 - (255 - B) / k));
        }
    }
}
