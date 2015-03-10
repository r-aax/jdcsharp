// Author: Alexey Rybakov

using System;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// General vector.
    /// </summary>
    public abstract class Vector
    {
        /// <summary>
        /// Square of module.
        /// </summary>
        public abstract double Mod2 { get; }

        /// <summary>
        /// Module.
        /// </summary>
        public double Mod
        {
            get
            {
                return Math.Sqrt(Mod2);
            }
        }

        /// <summary>
        /// Scaling.
        /// </summary>
        /// <param name="k">coefficient</param>
        public abstract void Scale(double k);

        /// <summary>
        /// Normalization to given size <c>k</c>.
        /// </summary>
        /// <param name="k">size</param>
        public void Normalize(double k)
        {
            Scale(k / Mod);
        }
    }
}
