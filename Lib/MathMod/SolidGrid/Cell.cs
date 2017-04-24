using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.SolidGrid
{
    /// <summary>
    /// Cell.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// U data.
        /// </summary>
        public U U;

        /// <summary>
        /// Data.
        /// </summary>
        public D D;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cell()
        {
            U = new U();
        }

        /// <summary>
        /// Convert U to D.
        /// </summary>
        /// <param name="V">volume</param>
        public void UtoD(double V)
        {
            double m = U.rho * V;

            D.m = m;
            D.Px = m * U.vx;
            D.Py = m * U.vy;
            D.Pz = m * U.vz;
            D.I = m * (U.eps + 0.5 * (U.vx * U.vx + U.vy * U.vy + U.vz * U.vz));
        }

        /// <summary>
        /// Convert D to U.
        /// </summary>
        /// <param name="V">volume</param>
        public void DtoU(double V)
        {
            double m = D.m;
            double vx = D.Px / m;
            double vy = D.Py / m;
            double vz = D.Pz / m;

            U.rho = m / V;
            U.vx = vx;
            U.vy = vy;
            U.vz = vz;
            U.eps = D.I / m - 0.5 * (vx * vx + vy * vy + vz * vz);
        }
    }
}
