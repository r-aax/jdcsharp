// Copyright Joy Developing.

namespace Lib.Maths.Geometry.Geometry3D
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
        /// <param name="v">direction vector</param>
        public Line(Point p, Vector v)
        {
            P = p;
            V = v;
        }
    }
}
