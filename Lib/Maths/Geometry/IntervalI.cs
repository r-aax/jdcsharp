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
        /// <param name="l">low end value</param>
        /// <param name="h">high end value</param>
        public IntervalI(int l, int h)
            : base(l, h)
        {
        }

        /// <summary>
        /// Constructor from interval length - high end.
        /// </summary>
        /// <param name="h">high end value</param>
        public IntervalI(int h)
            : this(0, h)
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

        /// <summary>
        /// Return cutted interval.
        /// </summary>
        /// <param name="margin"></param>
        /// <returns>cutted interval</returns>
        public IntervalI Cutted(int margin)
        {
            return new IntervalI(L + margin, H - margin);
        }

        /// <summary>
        /// Copy.
        /// </summary>
        public IntervalI Copy
        {
            get
            {
                return Clone() as IntervalI;
            }
        }

        /// <summary>
        /// Comparison.
        /// </summary>
        /// <param name="i1">first interval</param>
        /// <param name="i2">second interval</param>
        /// <returns>true - if equal, false - if not equal</returns>
        public static bool operator ==(IntervalI i1, IntervalI i2)
        {
            if (ReferenceEquals(i1, null) || ReferenceEquals(i2, null))
            {
                return ReferenceEquals(i1, i2);
            }
            else
            {
                return (i1.L == i2.L) && (i1.H == i2.H);
            }
        }

        /// <summary>
        /// Comparison for not equal.
        /// </summary>
        /// <param name="i1">first interval</param>
        /// <param name="i2">second interval</param>
        /// <returns>true - if not equal, false - if equal</returns>
        public static bool operator !=(IntervalI i1, IntervalI i2)
        {
            return !(i1 == i2);
        }
    }
}
