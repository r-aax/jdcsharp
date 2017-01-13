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

        /// <summary>
        /// Extend to 3D point.
        /// </summary>
        /// <returns>3D point</returns>
        public Lib.Maths.Geometry.Geometry3D.Point Extended()
        {
            return new Geometry3D.Point(X, Y, 0.0);
        }

        /// <summary>
        /// Check if point is in rectangle.
        /// </summary>
        /// <param name="rect">rectangle</param>
        /// <returns><c>true</c> - if point is in rectangle, <c>false</c> - otherwise</returns>
        public bool IsIn(Rect rect)
        {
            return rect.XInterval.Contains(X) && rect.YInterval.Contains(Y);
        }

        /// <summary>
        /// Toroid distance.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="rect">rectangle</param>
        /// <returns>distance</returns>
        public double ToroidDist(Point p, Rect rect)
        {
            return ToroidDir(p, rect).Mod;
        }

        /// <summary>
        /// Toroid direction to point.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="rect">rectangle</param>
        /// <returns>vector in direction to given point</returns>
        public Vector ToroidDir(Point p, Rect rect)
        {
            Debug.Assert(IsIn(rect) && p.IsIn(rect), "Toroid direction is available only for inner points.");

            // First find direction for X component.
            double dx = DistX(p);
            double edx = 0.0;
            if (dx <= rect.Width - dx)
            {
                // Inner dist.
                // |----- this =====> p -----|
                edx = p.X - X;
            }
            else
            {
                // Outer toroid dist.
                if (X > p.X)
                {
                    // Direction to the right.
                    // |----- p ----- this =====>|
                    edx = (rect.Right - X) + (p.X - rect.Left);
                }
                else
                {
                    // Direction to the left.
                    // |<===== this ----- p -----|
                    edx = (rect.Left - X) + (p.X - rect.Right);
                }
            }

            // The same action for X component.
            double dy = DistY(p);
            double edy = 0.0;
            if (dy <= rect.Height - dy)
            {
                edy = p.Y - Y;
            }
            else
            {
                if (Y > p.Y)
                {
                    edy = (rect.Top - Y) + (p.Y - rect.Bottom);
                }
                else
                {
                    edy = (rect.Bottom - Y) + (p.Y - rect.Top);
                }
            }

            return new Vector(edx, edy);
        }
    }
}
