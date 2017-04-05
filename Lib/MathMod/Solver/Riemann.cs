using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Riemann solver.
    /// </summary>
    public class Riemann
    {
        /// <summary>
        /// Riemann solver stub.
        /// </summary>
        /// <param name="U1">first data vector</param>
        /// <param name="U2">second data vector</param>
        /// <returns></returns>
        public static U Stub(U U1, U U2)
        {
            return new U(0.5 * (U1.rho + U2.rho),
                         0.5 * (U1.u + U2.u),
                         0.5 * (U1.v + U2.v),
                         0.5 * (U1.w + U2.w),
                         0.5 * (U1.eps + U2.eps));
      
    }
}
