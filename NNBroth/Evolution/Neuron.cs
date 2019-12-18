using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths;
using Lib.Maths.Numbers;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Neuron.
    /// </summary>
    public class Neuron : Node, ICloneable
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
            double[] gathered_signals_vector = GatherWeightedSignalsVectorForward();
            double signal_to_propagate = Maths.Sigmoid(gathered_signals_vector.Sum() + Bias);

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
