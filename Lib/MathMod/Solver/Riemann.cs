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
        /// Gamma value.
        /// </summary>
        public const double gama = 1.4;

        /// <summary>
        /// g1
        /// </summary>
        public const double g1 = (gama - 1.0) / (2.0 * gama);

        /// <summary>
        /// g2
        /// </summary>
        public const double g2 = (gama + 1.0) / (2.0 * gama);

        /// <summary>
        /// g3
        /// </summary>
        public const double g3 = 2.0 * gama / (gama - 1.0);

        /// <summary>
        /// g4
        /// </summary>
        public const double g4 = 2.0 / (gama - 1.0);
        
        /// <summary>
        /// g5
        /// </summary>
        public const double g5 = 2.0 / (gama + 1.0);

        /// <summary>
        /// g6
        /// </summary>
        public const double g6 = (gama - 1.0) / (gama + 1.0);

        /// <summary>
        /// g7
        /// </summary>
        public const double g7 = (gama - 1.0) / 2.0;

        /// <summary>
        /// g8
        /// </summary>
        public const double g8 = gama - 1.0;

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
