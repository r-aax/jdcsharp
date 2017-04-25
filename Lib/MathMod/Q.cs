using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Flow u.
        /// </summary>
        public double vx;

        /// <summary>
        /// Flow v.
        /// </summary>
        public double vy;

        /// <summary>
        /// Flow w.
        /// </summary>
        public double vz;

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
            vx = vx_;
            vy = vy_;
            vz = vz_;
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
            vx *= k;
            vy *= k;
            vz *= k;
            E *= k;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("Q: {0}, {1}, {2}, {3}, {4}", rho, vx, vy, vz, E);
        }
    }
}
