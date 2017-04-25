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
                         0.5 * (U1.v.X + U2.v.X),
                         0.5 * (U1.v.Y + U2.v.Y),
                         0.5 * (U1.v.Z + U2.v.Z),
                         0.5 * (U1.eps + U2.eps));
        }
      
    }
}
