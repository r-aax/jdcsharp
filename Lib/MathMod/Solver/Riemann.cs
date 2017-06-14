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
            U ru = new U();

            ru.rho = 0.5 * (u1.rho + u2.rho);
            ru.v = 0.5 * (u1.v + u2.v);
            ru.p = 0.5 * (u1.p + u2.p);

            return ru;
        }  
    }
}
