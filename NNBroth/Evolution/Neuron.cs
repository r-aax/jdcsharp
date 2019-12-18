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
        /// Signal to propagate.
        /// </summary>
        public double Accumulator;

        /// <summary>
        /// Error.
        /// </summary>
        public double Error;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Neuron(double bias = 0.0) : base()
        {
            Id = 0;
            Bias = bias;
            Accumulator = 0.0;
            Error = 0.0;
        }

        /// <summary>
        /// Propagate signal in forward direction.
        /// </summary>
        public void PropagateSignalForward()
        {
            double[] gathered_signals_vector = GatherWeightedSignalsVectorForward();

            Accumulator = Maths.Sigmoid(gathered_signals_vector.Sum() + Bias);

            BroadcastSignalForward(Accumulator);
        }

        /// <summary>
        /// Propagate error back.
        /// </summary>
        public void PropagateErrorBack()
        {
            double[] gathered_errors_vector = GatherErrorsVectorBack();

            // Here is derivative of sigmoid.
            Error = gathered_errors_vector.Sum() * Accumulator * (1.0 - Accumulator);

            // Broadcast error back with weight.
            foreach (Link link in InLinks)
            {
                link.Error = Error * link.Weight;
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            Neuron neuron = new Neuron(Bias);

            neuron.Id = Id;
            neuron.Accumulator = Accumulator;
            neuron.Error = Error;

            return neuron;
        }

        /// <summary>
        /// To string conversion.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("N {0} : B {1:F3}, A {2:F3}", Id, Bias, Accumulator);
        }
    }
}
