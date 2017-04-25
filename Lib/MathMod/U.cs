using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;

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
        /// Velocity.
        /// </summary>
        public Vector3D v;

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
            v = new Vector3D(vx_, vy_, vz_);
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
            return new U(rho, v.X, v.Y, v.Z, eps);
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
                    copy.v.X *= -1.0;
                    break;

                case AxisType.Y:
                    copy.v.Y *= -1.0;
                    break;

                case AxisType.Z:
                    copy.v.Z *= -1.0;
                    break;

                default:
                    Debug.Fail("unknown type of axis");
                    break;                       
            }

            return copy;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("U: {0}, {1}, {2}", rho, v, eps);
        }
    }
}
