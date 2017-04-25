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
            D = new D();
        }

        /// <summary>
        /// Convert U to D.
        /// </summary>
        /// <param name="V">volume</param>
        public void UtoD(double V)
        {
            double m = U.rho * V;

            D.m = m;
            D.P = m * U.v;
            D.I = m * (U.eps + 0.5 * U.v.Mod2);
        }

        /// <summary>
        /// Convert D to U.
        /// </summary>
        /// <param name="V">volume</param>
        public void DtoU(double V)
        {
            double m = D.m;

            U.rho = m / V;
            U.v = D.P / m;
            U.eps = D.I / m - 0.5 * U.v.Mod2;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return U.ToString() + " " + D.ToString();
        }
    }
}
