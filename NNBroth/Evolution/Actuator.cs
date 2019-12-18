using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Utils;

namespace NNBroth.Evolution
{
    class Actuator : Node
    {
        /// <summary>
        /// Actuator.
        /// </summary>
        public Actuator() : base()
        {
        }

        /// <summary>
        /// Get answer from network.
        /// </summary>
        /// <returns>answer</returns>
        public double[] GetOutputs()
        {
            return GatherWeightedSignalsVectorForward();
        }
    }
}
