using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Utils;

namespace NNBroth.Evolution
{
    class Actuator : Pole
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
            return GatherAccumulators();
        }

        /// <summary>
        /// To string conversion.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "Actuator";
        }

        /// <summary>
        /// Init last layer while sense back.
        /// </summary>
        /// <param name="ys">right answer vector</param>
        public void SenseBackInitLastLayer(double[] ys)
        {
            Debug.Assert(ys.Length == Neurons.Count);

            for (int i = 0; i < Neurons.Count; i++)
            {
                Neuron neuron = Neurons[i];

                neuron.Error = (neuron.Accumulator - ys[i]) * neuron.Accumulator * (1.0 - neuron.Accumulator);
            }
        }
    }
}
