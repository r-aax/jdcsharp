using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry2D;
using Lib.MathMod.Solver;

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
        /// In
        /// </summary>
        public double eps;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public U()
        {
            v = new Vector();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rho_">density</param>
        /// <param name="v_">velocity</param>
        /// <param name="eps_">inner energy per mass</param>
        public U(double rho_, Vector v_, double eps_)
        {
            rho = rho_;
            v = v_.Copy;
            eps = eps_;
        }

        /// <summary>
        /// Constructor from components of 2D velocity.
        /// </summary>
        /// <param name="rho_">density</param>
        /// <param name="vx"><c>X</c> component of velocity</param>
        /// <param name="vy"><c>Y</c> component of velocity</param>
        /// <param name="eps_">inner energy per mass</param>
        public U(double rho_, double vx, double vy, double eps_)
        {
            rho = rho_;
            v = new Vector(vx, vy);
            eps = eps_;
        }

        /// <summary>
        /// Pressure.
        /// </summary>
        public double p
        {
            get
            {
                return (RiemannToro.gama - 1.0) * rho * eps;
            }

            set
            {
                eps = value / ((RiemannToro.gama - 1.0) * rho);
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

            set
            {
                eps = value / rho - 0.5 * v.Mod2;
            }
        }

        /// <summary>
        /// Sound speed.
        /// </summary>
        public double c
        {
            get
            {
                return Math.Sqrt(RiemannToro.gama * p / rho);
            }
        }

        /// <summary>
        /// Mirror of X axis.
        /// </summary>
        public U MirrorX
        {
            get
            {
                return new U(rho, v.InvertedX(), eps);
            }
        }

        /// <summary>
        /// Mirror of Y axis.
        /// </summary>
        public U MirrorY
        {
            get
            {
                return new U(rho, v.InvertedY(), eps);
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
                             (e + p) * v.Y);
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
        
        /// <summary>
        /// Get value of data item.
        /// </summary>
        /// <param name="di">data item</param>
        /// <returns>value</returns>
        public double Item(DataItem di)
        {
            switch (di)
            {
                case DataItem.rho:
                    return rho;

                case DataItem.vX:
                    return v.X;

                case DataItem.vY:
                    return v.Y;

                case DataItem.eps:
                    return eps;

                default:
                    throw new Exception("wrong data item");
            }
        }
    }
}
