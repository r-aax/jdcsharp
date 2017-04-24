using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;

namespace Lib.MathMod
{
    /// <summary>
    /// General physical data.
    /// </summary>
    public class U : ICloneable
    {
        /// <summary>
        /// rho - density
        /// </summary>
        public double rho;

        /// <summary>
        /// u - speed x component
        /// </summary>
        public double vx;

        /// <summary>
        /// v - speed y component
        /// </summary>
        public double vy;

        /// <summary>
        /// w - speed z component
        /// </summary>
        public double vz;

        /// <summary>
        /// eps - inner energy density
        /// </summary>
        public double eps;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rho_">rho</param>
        /// <param name="vx_">vx</param>
        /// <param name="vy_">vy</param>
        /// <param name="vz_">vz</param>
        /// <param name="eps_">eps</param>
        public U(double rho_, double vx_, double vy_, double vz_, double eps_)
        {
            rho = rho_;
            vx = vx_;
            vy = vy_;
            vz = vz_;
            eps = eps_;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public U()
            : this(0.0, 0.0, 0.0, 0.0, 0.0)
        {
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public object Clone()
        {
            return new U(rho, vx, vy, vz, eps);
        }

        /// <summary>
        /// Mirror copy in <c>X</c>, <c>Y</c> or <c>Z</c> direction.
        /// </summary>
        /// <param name="at">axis type</param>
        /// <returns>mirror</returns>
        public U Mirror(AxisType at)
        {
            U copy = Clone() as U;

            switch (at)
            {
                case AxisType.X:
                    copy.vx *= -1.0;
                    break;

                case AxisType.Y:
                    copy.vy *= -1.0;
                    break;

                case AxisType.Z:
                    copy.vz *= -1.0;
                    break;

                default:
                    Debug.Fail("unknown type of axis");
                    break;                       
            }

            return copy;
        }
    }
}
