﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Segment with integer points.
    /// </summary>
    public class IntervalI : Interval<int>
    {
        /// <summary>
        /// Constructor from low and high ends.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="h"></param>
        public IntervalI(int l, int h)
            : base(l, h)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="s"></param>
        public IntervalI(IntervalI s)
            : this(s.L, s.H)
        {
        }

        /// <summary>
        /// Length of integer segment.
        /// </summary>
        public int Length
        {
            get
            {
                return H - L;
            }
        }

        /// <summary>
        /// Check if segment contains value.
        /// </summary>
        /// <param name="v">value</param>
        /// <returns><c>true</c> - if segment contains value, <c>false</c> - otherwise</returns>
        public bool Contains(int v)
        {
            return (v >= L) && (v <= H);
        }

        /// <summary>
        /// Segment decrement.
        /// </summary>
        /// <param name="v">value</param>
        public void Dec(int v)
        {
            L -= v;
            H -= v;
        }

        /// <summary>
        /// Decrement to zero lower value.
        /// </summary>
        public void DecTo0()
        {
            Dec(L);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public new object Clone()
        {
            return new IntervalI(L, H);
        }
    }
}
