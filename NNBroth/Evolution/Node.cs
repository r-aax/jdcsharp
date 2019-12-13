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
    class Node
    {
        /// <summary>
        /// Input links.
        /// </summary>
        private List<Link> InLinks;

        /// <summary>
        /// Output links.
        /// </summary>
        private List<Link> OutLinks;

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
        /// Distribute vector of signals to links list.
        /// Each vector element to each link.
        /// </summary>
        /// <param name="signals">signals vector</param>
        /// <param name="links">links list</param>
        private void DistributeSignalsVector(double[] signals, List<Link> links)
        {
            Debug.Assert(signals.Length == links.Count);

            for (int i = 0; i < signals.Length; i++)
            {
                links[i].Signal = signals[i];
            }
        }

        /// <summary>
        /// Distribute vector of signals in forward direction.
        /// </summary>
        /// <param name="signals">signals vector</param>
        private void DistributeSignalsVectorForward(double[] signals)
        {
            DistributeSignalsVector(signals, OutLinks);
        }

        /// <summary>
        /// Distribute vector of signals in back direction.
        /// </summary>
        /// <param name="signals">signals vector</param>
        private void DistributeSignalsVectorBack(double[] signals)
        {
            DistributeSignalsVector(signals, InLinks);
        }
    }
}
