// Author: Alexey Rybakov

using System;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Class interval scaler that implements bijection from one interval to another (mapping).
    /// We have interval <c>From</c> and there is bijection <c>Trom</c> to <c>To</c> interval.
    /// It can be direct bijection (when <c>From.L -> To.L, From.H -> To.H</c>) or
    /// inverted bijection (when <c>From.L -> To.H, From.H -> To.L)</c>.
    /// General task of this class is to find image of point from interval <c>From</c> to
    /// interval <c>To</c> and vice versa.
    /// </summary>
    public class IntervalScaler
    {
        /// <summary>
        /// Initial interval.
        /// </summary>
        public IntervalD From;

        /// <summary>
        /// Target interval.
        /// </summary>
        public IntervalD To;

        /// <summary>
        /// Inversion bijection flag.
        /// </summary>
        public bool IsInvert;

        /// <summary>
        /// Invertion coefficient.
        /// </summary>
        public double InvertCoefficient
        {
            get
            {
                return IsInvert ? -1.0 : 1.0;
            }
        }

        /// <summary>
        /// Inner variables.
        /// </summary>
        private double Bd, Bb, Cd, Cb;

        /// <summary>
        /// Scale factor of mapping.
        /// </summary>
        public double ScaleFactor
        {
            get
            {
                return Math.Abs(Bd);
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="from">initial interval</param>
        /// <param name="to">target interval</param>
        /// <param name="is_invert">inversion flag</param>
        public IntervalScaler(IntervalD from, IntervalD to, bool is_invert)
        {
            From = from;
            To = to;
            IsInvert = is_invert;

            CalcKoefficients();
        }

        /// <summary>
        /// Coefficients calculation.
        /// </summary>
        private void CalcKoefficients()
        {
            double flen, tlen, d;

            flen = From.Length;
            tlen = To.Length;

            if (!IsInvert)
            {
                d = From.H * To.L - From.L * To.H;
                Bd = tlen / flen;
                Bb = flen / tlen;
                Cd = d / flen;
                Cb = -(d / tlen);
            }
            else
            {
                d = From.H * To.H - From.L * To.L;
                Bd = -(tlen / flen);
                Bb = -(flen / tlen);
                Cd = d / flen;
                Cb = d / tlen;
            }
        }

        /// <summary>
        /// Direct mapping.
        /// </summary>
        /// <param name="x">argument</param>
        /// <returns>image</returns>
        public double T(double x)
        {
            return Bd * x + Cd;
        }

        /// <summary>
        /// Back mapping.
        /// </summary>
        /// <param name="y">image</param>
        /// <returns>argument</returns>
        public double F(double y)
        {
            return Bb * y + Cb;
        }
    }
}
