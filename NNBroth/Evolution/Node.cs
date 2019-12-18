using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Neural network node.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Input links.
        /// </summary>
        public List<Link> InLinks;

        /// <summary>
        /// Output links.
        /// </summary>
        public List<Link> OutLinks;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Node()
        {
            InLinks = new List<Link>();
            OutLinks = new List<Link>();
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
        protected void BroadcastSignalForward(double signal)
        {
            BroadcastSignal(signal, OutLinks);
        }

        /// <summary>
        /// Broadcast signal in back direction.
        /// </summary>
        /// <param name="signal">signal</param>
        protected void BroadcastSignalBack(double signal)
        {
            BroadcastSignal(signal, InLinks);
        }

        /// <summary>
        /// Scatter vector of signals to links list.
        /// Each vector element to each link.
        /// </summary>
        /// <param name="signals">signals vector</param>
        /// <param name="links">links list</param>
        private void ScatterSignalsVector(double[] signals, List<Link> links)
        {
            Debug.Assert(signals.Length == links.Count);

            for (int i = 0; i < signals.Length; i++)
            {
                links[i].Signal = signals[i];
            }
        }

        /// <summary>
        /// Scatter vector of signals in forward direction.
        /// </summary>
        /// <param name="signals">signals vector</param>
        protected void ScatterSignalsVectorForward(double[] signals)
        {
            ScatterSignalsVector(signals, OutLinks);
        }

        /// <summary>
        /// Scatter vector of signals in back direction.
        /// </summary>
        /// <param name="signals">signals vector</param>
        protected void ScatterSignalsVectorBack(double[] signals)
        {
            ScatterSignalsVector(signals, InLinks);
        }

        /// <summary>
        /// Gather vector of signals in forward direction.
        /// </summary>
        /// <returns>gathered signals vector</returns>
        protected double[] GatherWeightedSignalsVectorForward()
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
        protected double[] GatherErrorsVectorBack()
        {
            double[] errors = new double[OutLinks.Count];

            for (int i = 0; i < OutLinks.Count; i++)
            {
                errors[i] = OutLinks[i].Error;
            }

            return errors;
        }
    }
}
