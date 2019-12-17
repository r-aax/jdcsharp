using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Utils;

namespace NNBroth.Evolution
{
    class Actuator : Node, ICloneable
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
            double[] res = new double[InLinks.Count];

            // Fill answer array.
            for (int i = 0; i < InLinks.Count; i++)
            {
                res[i] = InLinks[i].WeightedSignal;
            }

            return res;
        }

        /// <summary>
        /// Get answer.
        /// </summary>
        /// <returns>answer</returns>
        public int GetAnswer()
        {
            return Arrays.MaxIndex(GetOutputs());
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new Actuator();
        }
    }
}
