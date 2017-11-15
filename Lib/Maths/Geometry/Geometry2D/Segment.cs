using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// Segment.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// First point.
        /// </summary>
        public Point A;

        /// <summary>
        /// Second point.
        /// </summary>
        public Point B;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">first point</param>
        /// <param name="b">second point</param>
        public Segment(Point a, Point b)
        {
            A = a;
            B = b;
        }
    }
}
