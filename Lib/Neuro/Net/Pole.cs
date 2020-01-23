using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lib.Neuro.Net
{
    /// <summary>
    /// Pole of cortex (sensor or actuator).
    /// </summary>
    public class Pole
    {
        /// <summary>
        /// Neurons.
        /// </summary>
        public List<Neuron> Neurons;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Pole()
        {
            Neurons = new List<Neuron>();
        }

        /// <summary>
        /// Gather accumulators.
        /// </summary>
        /// <returns>array of accumulators</returns>
        protected double[] GatherAccumulators()
        {
            double[] accumulators = new double[Neurons.Count];

            for (int i = 0; i < Neurons.Count; i++)
            {
                accumulators[i] = Neurons[i].Accumulator;
            }

            return accumulators;
        }

        /// <summary>
        /// Scatter accumulators.
        /// </summary>
        /// <param name="accumulators">accumulators</param>
        protected void ScatterAccumulators(double[] accumulators)
        {
            Debug.Assert(accumulators.Length == Neurons.Count);

            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].Accumulator = accumulators[i];
            }
        }
    }
}
