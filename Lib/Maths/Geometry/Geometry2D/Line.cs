// Copyright Joy Developing.

using System.Diagnostics;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// Line.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Base point.
        /// </summary>
        public Point P;

        /// <summary>
        /// Direction vector.
        /// </summary>
        public Vector V;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="p">base point</param>
        /// <param name="v">direction vecto</param>
        public Line(Point p, Vector v)
        {
            Debug.Assert(V.Mod2 > 0.0);

            P = p;
            V = v;
        }
    }
}
