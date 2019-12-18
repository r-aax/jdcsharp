using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Numbers;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Link between two nodes.
    /// </summary>
    public class Link : ICloneable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id;

        /// <summary>
        /// Source node.
        /// </summary>
        public Node Src;

        /// <summary>
        /// Destination node.
        /// </summary>
        public Node Dst;

        /// <summary>
        /// Weight.
        /// </summary>
        public double Weight;

        /// <summary>
        /// Signal.
        /// </summary>
        public double Signal;

        /// <summary>
        /// Error.
        /// </summary>
        public double Error;

        /// <summary>
        /// Weighted signal.
        /// </summary>
        public double WeightedSignal
        {
            get
            {
                return Signal * Weight;
            }
        }

        /// <summary>
        /// Link constructor.
        /// </summary>
        /// <param name="src">source</param>
        /// <param name="dst">destination</param>
        /// <param name="weight">weight</param>
        public Link(Node src, Node dst, double weight = 1.0)
        {
            Id = 0;
            Src = src;
            Dst = dst;
            Weight = weight;
            Signal = 0.0;
            Error = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="weight">weight</param>
        public Link(double weight = 1.0) : this(null, null, weight)
        {
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            Link link = new Link(Weight);

            link.Id = Id;

            return link;
        }

        /// <summary>
        /// To string conversion.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string label = "";

            if (Src is Sensor)
            {
                Neuron DstN = Dst as Neuron;

                label = String.Format("S - {0}", DstN.Id);
            }
            else if (Dst is Actuator)
            {
                Neuron SrcN = Src as Neuron;

                label = String.Format("{0} - A", SrcN.Id);
            }
            else
            {
                Neuron SrcN = Src as Neuron;
                Neuron DstN = Dst as Neuron;

                label = String.Format("{0} - {1}", SrcN.Id, DstN.Id);
            }

            return label + String.Format(" : W {0:F3}, S {1:F3}, E {2:F3}",
                                         Weight, Signal, Error);
        }
    }
}
