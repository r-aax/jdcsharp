using System;

using Lib.Maths.Geometry;

namespace Lib.MathMod
{
    /// <summary>
    /// Data of cell.
    /// </summary>
    public class D
    {
        /// <summary>
        /// Mass.
        /// </summary>
        public double m;

        /// <summary>
        /// Impulse.
        /// </summary>
        public Vector P;

        /// <summary>
        /// Full energy.
        /// </summary>
        public double E;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public D()
        {
            P = new Vector();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="m_">mass</param>
        /// <param name="p">impulse</param>
        /// <param name="e">energy</param>
        public D(double m_, Vector p, double e)
        {
            m = m_;
            P = new Vector(p);
            E = e;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="m_">mass</param>
        /// <param name="px">impulse X component</param>
        /// <param name="py">impulse Y component</param>
        /// <param name="e">full energy</param>
        public D(double m_, double px, double py, double e)
        {
            m = m_;
            P = new Vector(px, py);
            E = e;
        }

        /// <summary>
        /// Multuply.
        /// </summary>
        /// <param name="d">D</param>
        /// <param name="k">double coefficient</param>
        /// <returns>new D data</returns>
        public static D operator *(D d, double k)
        {
            return new D(d.m * k, d.P * k, d.E * k);
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        /// <param name="k">coefficient</param>
        /// <param name="d">D data</param>
        /// <returns>new D data</returns>
        public static D operator *(double k, D d)
        {
            return d * k;
        }

        /// <summary>
        /// Addition.
        /// </summary>
        /// <param name="d1">first D data</param>
        /// <param name="d2">second D data</param>
        /// <returns>new D data</returns>
        public static D operator +(D d1, D d2)
        {
            return new D(d1.m + d2.m, d1.P + d2.P, d1.E + d2.E);
        }

        /// <summary>
        /// Substraction.
        /// </summary>
        /// <param name="d1">first D data</param>
        /// <param name="d2">second D data</param>
        /// <returns>new D data</returns>
        public static D operator -(D d1, D d2)
        {
            return new D(d1.m - d2.m, d1.P - d2.P, d1.E - d2.E);
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("D: {0}, {1}, {2}", m, P, E);
        }
    }
}
