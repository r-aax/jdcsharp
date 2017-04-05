using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod
{
    /// <summary>
    /// General physical data.
    /// </summary>
    public class U
    {
        /// <summary>
        /// rho - density
        /// </summary>
        public double rho;

        /// <summary>
        /// u - speed x component
        /// </summary>
        public double u;

        /// <summary>
        /// v - speed y component
        /// </summary>
        public double v;

        /// <summary>
        /// w - speed z component
        /// </summary>
        public double w;

        /// <summary>
        /// eps - energy density
        /// </summary>
        public double eps;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rho_">rho</param>
        /// <param name="u_">u</param>
        /// <param name="v_">v</param>
        /// <param name="w_">w</param>
        /// <param name="eps_">eps</param>
        public U(double rho_, double u_, double v_, double w_, double eps_)
        {
            rho = rho_;
            u = u_;
            v = v_;
            w = w_;
            eps = eps_;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public U()
            : this(0.0, 0.0, 0.0, 0.0, 0.0)
        {
        }
    }
}
