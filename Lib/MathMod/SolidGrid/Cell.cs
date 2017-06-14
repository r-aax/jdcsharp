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
        /// General data.
        /// </summary>
        public U U;

        /// <summary>
        /// Conservative data.
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
        /// Convert U data to D data.
        /// </summary>
        /// <param name="V">volume</param>
        public void UtoD(double V)
        {
            D.m = U.rho * V;
            D.P = U.v * D.m;
            D.E = U.e * V;
        }

        /// <summary>
        /// Convert D data to U data.
        /// </summary>
        /// <param name="V"></param>
        public void DtoU(double V)
        {
            U.rho = D.m / V;
            U.v = D.P / D.m;
            U.e = D.E / V;
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}", U, D);
        }
    }
}
