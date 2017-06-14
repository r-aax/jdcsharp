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

            set
            {
                eps = value / ((MathModConstants.Gamma - 1.0) * rho);
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

        /// <summary>
        /// Mirror of X axis.
        /// </summary>
        public U MirrorX
        {
            get
            {
                return new U(rho, new Vector(-v.X, v.Y, v.Z), eps);
            }
        }

        /// <summary>
        /// Mirror of Y axis.
        /// </summary>
        public U MirrorY
        {
            get
            {
                return new U(rho, new Vector(v.X, -v.Y, v.Z), eps);
            }
        }

        /// <summary>
        /// Mirror of Z axis.
        /// </summary>
        public U MirrorZ
        {
            get
            {
                return new U(rho, new Vector(v.X, v.Y, -v.Z), eps);
            }
        }

        /// <summary>
        /// Flow in X direction.
        /// </summary>
        public D FlowX
        {
            get
            {
                return new D(rho * v.X,
                             rho * v.X2 + p,
                             rho * v.XY,
                             rho * v.XZ,
                             (e + p) * v.X);
            }
        }

        /// <summary>
        /// Flow in Y direction.
        /// </summary>
        public D FlowY
        {
            get
            {
                return new D(rho * v.Y,
                             rho * v.XY,
                             rho * v.Y2 + p,
                             rho * v.YZ,
                             (e + p) * v.Y);
            }
        }

        /// <summary>
        /// Flow in Z direction.
        /// </summary>
        public D FlowZ
        {
            get
            {
                return new D(rho * v.Z,
                             rho * v.XZ,
                             rho * v.YZ,
                             rho * v.Z2 + p,
                             (e + p) * v.Z);
            }
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("U: {0}, {1}, {2}", rho, v, eps);
        }
    }
}
