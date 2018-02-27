using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid.Load
{
    /// <summary>
    /// Properties of PFG load/save.
    /// </summary>
    public static class GridLoadSavePFGProperties
    {
        /// <summary>
        /// Uppercase extension.
        /// </summary>
        public static bool IsExtensionUppercase = true;

        /// <summary>
        /// Use iblank data.
        /// </summary>
        public static bool IsIBlank = false;

        /// <summary>
        /// Epsilon for check match of two border conditions (for parallel move).
        /// </summary>
        public static double EpsForBCondsMatchParallelMove = 1e-2;

        /// <summary>
        /// Epsilon for check match of two border conditions (for rotation).
        /// </summary>
        public static double EpsForBCondsMatchRotation = 1e-2;
    }
}
