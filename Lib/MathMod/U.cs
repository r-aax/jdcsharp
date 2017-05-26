using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry3D;

namespace Lib.MathMod
{
    /// <summary>
    /// General cell data for 
    /// </summary>
    public class U
    {
        /// <summary>
        /// Density.
        /// </summary>
        public double rho;

        // Velocity.
        public Vector v;

        /// <summary>
        /// Inner energy per mass.
        /// </summary>
        public double eps;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rho_">density</param>
        /// <param name="v_">velocity</param>
        /// <param name="eps_">inner energy per mass</param>
        public U(double rho_, Vector v_, double eps_)
        {
            rho = rho_;
            v = new Vector(v_);
            eps = eps_;
        }

        /// <summary>
        /// Zero constructor.
        /// </summary>
        public U()
            : this(0.0, new Vector(), 0.0)
        {
        }

        /// <summary>
        /// Pressure.
        /// </summary>
        public double p
        {
            get
            {
                return (MathModConstants.Gamma - 1.0) * rho * eps;
            }
        }

        /// <summary>
        /// Full energy of volume unit.
        /// </summary>
        public double e
        {
            get
            {
                return rho * (eps + 0.5 * v.Mod2);
            }
        }
    }
}
