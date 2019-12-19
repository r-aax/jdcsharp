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
    public class Neuron : ICloneable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id;

        /// <summary>
        /// Input links.
        /// </summary>
        public List<Link> InLinks;

        /// <summary>
        /// Output links.
        /// </summary>
        public List<Link> OutLinks;

        /// <summary>
        /// Bias.
        /// </summary>
        public double Bias;

        /// <summary>
        /// Delta Bias.
        /// </summary>
        public double dBias;

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
        public Neuron(double bias = 0.0)
        {
            InLinks = new List<Link>();
            OutLinks = new List<Link>();
            Id = 0;
            Bias = bias;
            dBias = 0.0;
            Accumulator = 0.0;
            Error = 0.0;
        }

        /// <summary>
        /// Check if it is the first layer.
        /// </summary>
        public bool IsFirstLayer
        {
            get
            {
                return InLinks.Count == 0;
            }
        }

        /// <summary>
        /// Check if it is the last layer.
        /// </summary>
        public bool IsLastLayer
        {
            get
            {
                return OutLinks.Count == 0;
            }
        }

        /// <summary>
        /// Add in link.
        /// </summary>
        /// <param name="in_link">link</param>
        public void AddInLink(Link in_link)
        {
            InLinks.Add(in_link);
        }

        /// <summary>
        /// Add out link.
        /// </summary>
        /// <param name="out_link">link</param>
        public void AddOutLink(Link out_link)
        {
            OutLinks.Add(out_link);
        }

        /// <summary>
        /// Broadcast signal.
        /// Send the signal to every link.
        /// </summary>
        /// <param name="signal">signal</param>
        /// <param name="links">links list</param>
        private void BroadcastSignal(double signal, List<Link> links)
        {
            foreach (Link link in links)
            {
                link.Signal = signal;
            }
        }

        /// <summary>
        /// Broadcast signal in forward direction.
        /// </summary>
        /// <param name="signal">signal</param>
        private void BroadcastSignalForward(double signal)
        {
            BroadcastSignal(signal, OutLinks);
        }

        /// <summary>
        /// Broadcast signal in back direction.
        /// </summary>
        /// <param name="signal">signal</param>
        private void BroadcastSignalBack(double signal)
        {
            BroadcastSignal(signal, InLinks);
        }

        /// <summary>
        /// Gather vector of signals in forward direction.
        /// </summary>
        /// <returns>gathered signals vector</returns>
        private double[] GatherWeightedSignalsVectorForward()
        {
            double[] weighted_signals = new double[InLinks.Count];

            for (int i = 0; i < InLinks.Count; i++)
            {
                weighted_signals[i] = InLinks[i].WeightedSignal;
            }

            return weighted_signals;
        }

        /// <summary>
        /// Gather vector of errors in back direction.
        /// </summary>
        /// <returns>gathered vector of errors</returns>
        private double[] GatherErrorsVectorBack()
        {
            double[] errors = new double[OutLinks.Count];

            for (int i = 0; i < OutLinks.Count; i++)
            {
                errors[i] = OutLinks[i].Error;
            }

            return errors;
        }

        /// <summary>
        /// Propagate signal in forward direction.
        /// </summary>
        public void PropagateSignalForward()
        {
            if (!IsFirstLayer)
            {
                double[] gathered_signals_vector = GatherWeightedSignalsVectorForward();

                Accumulator = Maths.Sigmoid(gathered_signals_vector.Sum() + Bias);
            }

            BroadcastSignalForward(Accumulator);
        }

        /// <summary>
        /// Propagate error back.
        /// </summary>
        public void PropagateErrorBack()
        {
            if (!IsLastLayer)
            {
                double[] gathered_errors_vector = GatherErrorsVectorBack();

                // Here is derivative of sigmoid.
                Error = gathered_errors_vector.Sum() * Accumulator * (1.0 - Accumulator);
            }

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

            return neuron;
        }

        /// <summary>
        /// To string conversion.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("N {0} : B {1:F3}, A {2:F3}, E {3:F3}",
                                 Id, Bias, Accumulator, Error);
        }
    }
}
