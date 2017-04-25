using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;

namespace Lib.MathMod
{
    /// <summary>
    /// Flow.
    /// </summary>
    public class Q
    {
        /// <summary>
        /// Density flow.
        /// </summary>
        public double rho;

        /// <summary>
        /// Velocity.
        /// </summary>
        public Vector3D v;

        /// <summary>
        /// Flow E.
        /// </summary>
        public double E;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rho_">flow rho</param>
        /// <param name="vx_">flow vx</param>
        /// <param name="vy_">flow vy</param>
        /// <param name="vz_">flow vz</param>
        /// <param name="e">flow W</param>
        public Q(double rho_, double vx_, double vy_, double vz_, double e)
        {
            rho = rho_;
            v = new Vector3D(vx_, vy_, vz_);
            E = e;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Q()
            : this(0.0, 0.0, 0.0, 0.0, 0.0)
        {
        }

        /// <summary>
        /// Multiply.
        /// </summary>
        /// <param name="k">value</param>
        public void Mul(double k)
        {
            rho *= k;
            v *= k;
            E *= k;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("Q: {0}, {1}, {2}", rho, v, E);
        }
    }
}
