using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Neuron.
    /// </summary>
    class Neuron : Node, ICloneable
    {
        /// <summary>
        /// Bias.
        /// </summary>
        private double Bias;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Neuron(double bias = 0.0) : base()
        {
            Bias = bias;
        }

        /// <summary>
        /// Propagate signal in forward direction.
        /// </summary>
        public void PropagateSignalForward()
        {
            double sum_of_signals = InLinks.Aggregate(0.0, (acc, link) => acc + link.WeightedSignal);
            double signal_to_propagate = Maths.Sigmoid(sum_of_signals + Bias);

            BroadcastSignalForward(signal_to_propagate);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new Neuron(Bias);
        }
    }
}
