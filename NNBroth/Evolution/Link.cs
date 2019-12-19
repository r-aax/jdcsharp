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
        public Neuron Src;

        /// <summary>
        /// Destination node.
        /// </summary>
        public Neuron Dst;

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
        public Link(Neuron src, Neuron dst, double weight = 1.0)
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
            return String.Format("{0} - {1} : W {2:F3}, S {3:F3}, E {4:F3}",
                                 Src.Id, Dst.Id, Weight, Signal, Error);
        }
    }
}
