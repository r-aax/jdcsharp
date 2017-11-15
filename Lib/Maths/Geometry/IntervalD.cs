// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Interval <c>(L, H)</c>.
    /// </summary>
    public class IntervalD : Interval<double>, ICloneable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="l">begin</param>
        /// <param name="h">end</param>
        public IntervalD(double l, double h)
        {
            Debug.Assert(l <= h);

            L = l;
            H = h;
        }

        /// <summary>
        /// Constructor by length.
        /// </summary>
        /// <param name="len">length</param>
        public IntervalD(double len)
            : this(0.0, len)
        {
        }

        /// <summary>
        /// Set from two values.
        /// </summary>
        /// <param name="v1">first value</param>
        /// <param name="v2">second value</param>
        /// <returns>interval</returns>
        public static IntervalD SetFrom(double v1, double v2)
        {
            return (v1 < v2) ? new IntervalD(v1, v2) : new IntervalD(v2, v1);
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("[{0:0.##} - {1:0.##}]", L, H);
        }

        /// <summary>
        /// Length.
        /// </summary>
        public double Length
        {
            get
            {
                return H - L;
            }
        }

        /// <summary>
        /// Middle.
        /// </summary>
        public double Mid
        {
            get
            {
                return 0.5 * (L + H);
            }
        }

        /// <summary>
        /// Inner point coordinate.
        /// </summary>
        /// <param name="p">relative coordinate (from <c>0.0 - L</c>, to <c>1.0 - H</c>)</param>
        /// <returns>coordinate</returns>
        public double Inner(double p)
        {
            Debug.Assert((p >= 0.0) && (p <= 1.0));

            return L + p * Length;
        }

        /// <summary>
        /// Check if point is in interval.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>check result</returns>
        public bool Contains(double p)
        {
            return ((p >= L) && (p <= H));
        }

        /// <summary>
        /// Moving.
        /// </summary>
        /// <param name="d">distance</param>
        public void Move(double d)
        {
            L += d;
            H += d;
        }

        /// <summary>
        /// Length relative moving.
        /// </summary>
        /// <param name="k">coefficient of moving</param>
        public void RelMove(double k)
        {
            Move(k * Length);
        }

        /// <summary>
        /// Interval scaling.
        /// </summary>
        /// <param name="p">point of scaling</param>
        /// <param name="k">coefficient of scaling</param>
        public void Scale(double p, double k)
        {
            double l = p + (L - p) * k;
            double h = p + (H - p) * k;

            L = l;
            H = h;
        }

        /// <summary>
        /// Constructing the copy of interval and scaling it.
        /// </summary>
        /// <param name="p">point of scaling</param>
        /// <param name="k">coefficient of scaling</param>
        /// <returns>scaled interval</returns>
        public IntervalD Scaled(double p, double k)
        {
            IntervalD interval = Clone() as IntervalD;

            interval.Scale(p, k);

            return interval;
        }

        /// <summary>
        /// Center scaling.
        /// </summary>
        /// <param name="k">coefficient of scaling</param>
        public void CenterScale(double k)
        {
            Scale(Mid, k);
        }

        /// <summary>
        /// Constructing central scaling interval.
        /// </summary>
        /// <param name="k">coefficient of scaling</param>
        /// <returns>center scaled interval</returns>
        public IntervalD CenterScaled(double k)
        {
            return Scaled(Mid, k);
        }

        /// <summary>
        /// Get clone copy.
        /// </summary>
        /// <returns>clone copy</returns>
        public new object Clone()
        {
            return new IntervalD(L, H);
        }

        /// <summary>
        /// Check if intervals intersect.
        /// </summary>
        /// <param name="i1">first interval</param>
        /// <param name="i2">second interval</param>
        /// <returns><c>true</c> - if intervals intersect, <c>false</c> - otherwise</returns>
        public static bool IsIntersect(IntervalD i1, IntervalD i2)
        {
            return i1.H >= i2.L;
        }
    }
}
