﻿using System;
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
            Src = src;
            Dst = dst;
            Weight = weight;
            Signal = 0.0;
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
            return new Link(Weight);
        }
    }
}
