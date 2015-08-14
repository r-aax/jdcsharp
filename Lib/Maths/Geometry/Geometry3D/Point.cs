// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Maths.Geometry.Geometry3D
{
    /// <summary>
    /// 3D point.
    /// </summary>
    public class Point : Vector
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Point()
            : base()
        {
        }

        /// <summary>
        /// Constructor from coordinates.
        /// </summary>
        /// <param name="x">coordinate <c>x</c></param>
        /// <param name="y">coordinate <c>y</c></param>
        /// <param name="z">coordinate <c>z</c></param>
        public Point(double x, double y, double z)
            : base(x, y, z)
        {
        }

        /// <summary>
        /// Constructor from vector.
        /// </summary>
        /// <param name="v">vector</param>
        public Point(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
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
        /// Distance to point on axis <c>x</c>.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistX(Point p)
        {
            return Math.Abs(X - p.X);
        }

        /// <summary>
        /// Distance to point on axis <c>y</c>.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistY(Point p)
        {
            return Math.Abs(Y - p.Y);
        }

        /// <summary>
        /// Distance to point on axis <c>z</c>.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistZ(Point p)
        {
            return Math.Abs(Z - p.Z);
        }

        /// <summary>
        /// Distance to point on given axis.
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

                case AxisType.Z:
                    return DistZ(p);

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
        /// Subtracting vector from point.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="v">vector</param>
        /// <returns>new point</returns>
        public static Point operator -(Point p, Vector v)
        {
            return p + (-v);
        }

        /// <summary>
        /// Random point.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        /// <returns>random point</returns>
        public static new Point Random(Parallelepiped par)
        {
            return new Point(Vector.Random(par));
        }
    }
}
