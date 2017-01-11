// Author: Alexey Rybakov

using System;
using System.Diagnostics;
using System.Windows.Media;

using Lib.Maths.Numbers;

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
        /// Empty constructor for serialization.
        /// </summary>
        public Color()
        {
        }

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
        /// Constructor from <c>System.Drawing.Color</c>.
        /// </summary>
        /// <param name="color">color</param>
        public Color(System.Drawing.Color color)
            : this(color.A, color.R, color.G, color.B)
        {
        }

        /// <summary>
        /// Constructor from string #AARRGGBB.
        /// </summary>
        /// <param name="str">string</param>
        public Color(string str)
        {
            string aa = str.Substring(1, 2);
            string rr = str.Substring(3, 2);
            string gg = str.Substring(5, 2);
            string bb = str.Substring(7, 2);

            A = Byte.Parse(aa, System.Globalization.NumberStyles.AllowHexSpecifier);
            R = Byte.Parse(rr, System.Globalization.NumberStyles.AllowHexSpecifier);
            G = Byte.Parse(gg, System.Globalization.NumberStyles.AllowHexSpecifier);
            B = Byte.Parse(bb, System.Globalization.NumberStyles.AllowHexSpecifier);
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

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("#{0,2:X2}{1,2:X2}{2,2:X2}{3,2:X2}", A, R, G, B);
        }

        /// <summary>
        /// Convert to System.Windows.Media.Color.
        /// </summary>
        /// <returns>color</returns>
        public System.Windows.Media.Color ToSWMColor()
        {
            return System.Windows.Media.Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        /// Convert to System.Drawing.Color.
        /// </summary>
        /// <returns>color</returns>
        public System.Drawing.Color ToSDColor()
        {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        /// Get random color.
        /// </summary>
        /// <returns>random color</returns>
        public static Color Random()
        {
            return new Color(0, Randoms.RandomByte(), Randoms.RandomByte(), Randoms.RandomByte());               
        }

        /// <summary>
        /// Get array of random colors.
        /// </summary>
        /// <param name="n">count of colors</param>
        /// <returns>array of random colors</returns>
        public static Color[] ArrayOfRandoms(int n)
        {
            Color[] arr = new Color[n];

            for (int i = 0; i < n; i++)
            {
                arr[i] = Random();
            }

            return arr;
        }
    }
}
