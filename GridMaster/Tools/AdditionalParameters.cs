using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMaster.Tools
{
    /// <summary>
    /// Parameters.
    /// </summary>
    public class AdditionalParameters
    {
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
