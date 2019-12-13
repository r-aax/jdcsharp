﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Link between two nodes.
    /// </summary>
    class Link
    {
        /// <summary>
        /// Source node.
        /// </summary>
        private Node Src;

        /// <summary>
        /// Destination node.
        /// </summary>
        private Node Dst;

        /// <summary>
        /// Weight.
        /// </summary>
        private double Weight;

        /// <summary>
        /// Signal.
        /// </summary>
        public double Signal
        {
            private get;
            set;
        }

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
        public Link(Node src, Node dst, double weight)
        {
            Src = src;
            Dst = dst;
            Weight = weight;
            Signal = 0.0;
        }
    }
}
