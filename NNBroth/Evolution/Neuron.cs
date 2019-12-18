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
        /// Identifier.
        /// </summary>
        public int Id;

        /// <summary>
        /// Bias.
        /// </summary>
        private double Bias;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Neuron(double bias = 0.0) : base()
        {
            Id = 0;
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
            Neuron neuron = new Neuron(Bias);

            neuron.Id = Id;

            return neuron;
        }

        /// <summary>
        /// To string conversion.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("N {0} : B {1:F3}", Id, Bias);
        }
    }
}
