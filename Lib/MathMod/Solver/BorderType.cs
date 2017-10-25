using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Type of border.
    /// </summary>
    public enum BorderType
    {
        /// <summary>
        /// Soft border.
        /// Neighbour = Cell.
        /// </summary>
        Soft = 0,

        /// <summary>
        /// Hard border.
        /// Neighbour = Mirror(Cell).
        /// </summary>
        Hard = 1        
    }
}
