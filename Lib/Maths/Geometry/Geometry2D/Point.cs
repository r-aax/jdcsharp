// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// 2D point.
    /// </summary>
    public class Point : Vector, ICloneable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Point()
            : base()
        {
        }

        /// <summary>
        /// Constructor by coordinates.
        /// </summary>
        /// <param name="x">coordinate <c>x</c></param>
        /// <param name="y">coordinate <c>y</c></param>
        public Point(double x, double y)
            : base(x, y)
        {
        }

        /// <summary>
        /// Constructor by vector.
        /// </summary>
        /// <param name="v">vector</param>
        public Point(Vector v)
        {
            X = v.X;
            Y = v.Y;
        }

        /// <summary>
        /// Distance to point.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double Dist(Point p)
        {
            return (this - p).Mod;
        }

        /// <summary>
        /// Distance <c>x</c> to point.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistX(Point p)
        {
            return Math.Abs(X - p.X);
        }

        /// <summary>
        /// Distance <c>y</c> to point.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistY(Point p)
        {
            return Math.Abs(Y - p.Y);
        }

        /// <summary>
        /// Distance to point in the given axis.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="ax">axis</param>
        /// <returns>distance</returns>
        public double Dist(Point p, AxisType ax)
        {
            switch (ax)
            {
                case AxisType.X:
                    return DistX(p);

                case AxisType.Y:
                    return DistY(p);

                default:
                    Debug.Assert(false);
                    return 0.0;
            }
        }

        /// <summary>
        /// Adding vector to point.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="v">vector</param>
        /// <returns>new point</returns>
        public static Point operator +(Point p, Vector v)
        {
            return new Point((p as Vector) + v);
        }

        /// <summary>
        /// Subtraction vector from point.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="v">vector</param>
        /// <returns>new point</returns>
        public static Point operator -(Point p, Vector v)
        {
            return p + (-v);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Point(X, Y);
        }

        /// <summary>
        /// Random point.
        /// </summary>
        /// <param name="rect">rectangle</param>
        /// <returns>random point</returns>
        public static new Point Random(Rect rect)
        {
            return new Point(Vector.Random(rect));
        }
    }
}
