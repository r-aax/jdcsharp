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
        /// Stub for Riemann task.
        /// </summary>
        /// <param name="u1">left side of Riemann task</param>
        /// <param name="u2">right side of Riemann task</param>
        /// <returns><c>U</c> data (simple case)</returns>
        public static U Stub(U u1, U u2)
        {
            return new U(0.5 * (u1.rho + u2.rho),
                         0.5 * (u1.v + u2.v),
                         0.5 * (u1.eps + u2.eps));
        }
    }
}
