using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Segment with integer points.
    /// </summary>
    public class ISegm : Segm<int>
    {
        /// <summary>
        /// Constructor from low and high ends.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="h"></param>
        public ISegm(int l, int h)
            : base(l, h)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="s"></param>
        public ISegm(ISegm s)
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
    }
}
