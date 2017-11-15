// Author: Alexey Rybakov

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Point (2D or 3D).
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
        /// Constructor by coordinates.
        /// </summary>
        /// <param name="x">coordinate <c>x</c></param>
        /// <param name="y">coordinate <c>y</c></param>
        public Point(double x, double y)
            : base(x, y)
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
        /// Distance to point on axis <c>X</c>.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistX(Point p)
        {
            return Math.Abs(X - p.X);
        }

        /// <summary>
        /// Distance to point on axis <c>Y</c>.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>distance</returns>
        public double DistY(Point p)
        {
            return Math.Abs(Y - p.Y);
        }

        /// <summary>
        /// Distance to point on axis <c>Z</c>.
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
        /// Multiply on double.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="d">value</param>
        /// <returns>new point</returns>
        public static Point operator *(Point p, double d)
        {
            return new Point((p as Vector) * 0.25);
        }

        /// <summary>
        /// Multiply on double.
        /// </summary>
        /// <param name="d">value</param>
        /// <param name="p">point</param>
        /// <returns>new point</returns>
        public static Point operator *(double d, Point p)
        {
            return p * d;
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
        /// Random point.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        /// <returns>random point</returns>
        public static new Point Random(Parallelepiped par)
        {
            return new Point(Vector.Random(par));
        }

        /// <summary>
        /// Random point on parallelepiped surface.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        /// <returns>random point</returns>
        public static new Point RandomOnSurface(Parallelepiped par)
        {
            return new Point(Vector.RandomOnSurface(par));
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public override object Clone()
        {
            return new Point(X, Y, Z);
        }

        /// <summary>
        /// Copy of point.
        /// </summary>
        public new Point Copy
        {
            get
            {
                return Clone() as Point;
            }
        }

        /// <summary>
        /// Return point to parallelepiped.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        public void ReturnToParallelepiped(Parallelepiped par)
        {
            if (X > par.Right)
            {
                X = par.Right;
            }

            if (X < par.Left)
            {
                X = par.Left;
            }

            if (Y > par.Top)
            {
                Y = par.Top;
            }

            if (Y < par.Bottom)
            {
                Y = par.Bottom;
            }

            if (Z > par.Front)
            {
                Z = par.Front;
            }

            if (Z < par.Back)
            {
                Z = par.Back;
            }
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
        /// Check if point is inside of parallelepiped.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        /// <returns><c>true</c> - if point is inside of parallelepiped, <c>false</c> - otherwise</returns>
        public bool IsIn(Parallelepiped par)
        {
            return par.XInterval.Contains(X) && par.YInterval.Contains(Y) && par.ZInterval.Contains(Z);
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
        /// Toroid distance.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="par">parallelepiped</param>
        /// <returns>distance</returns>
        public double ToroidDist(Point p, Parallelepiped par)
        {
            return ToroidDir(p, par).Mod;
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

            // The same action for Y component.
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

        /// <summary>
        /// Toroid direction to point.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="par">parallelepiped</param>
        /// <returns>vector in direction to given point</returns>
        public Vector ToroidDir(Point p, Parallelepiped par)
        {
            Debug.Assert(IsIn(par) && p.IsIn(par), "Toroid direction is available only for inner points.");

            // First find direction for X component.
            double dx = DistX(p);
            double edx = 0.0;
            if (dx <= par.Width - dx)
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
                    edx = (par.Right - X) + (p.X - par.Left);
                }
                else
                {
                    // Direction to the left.
                    // |<===== this ----- p -----|
                    edx = (par.Left - X) + (p.X - par.Right);
                }
            }

            // The same action for Y component.
            double dy = DistY(p);
            double edy = 0.0;
            if (dy <= par.Height - dy)
            {
                edy = p.Y - Y;
            }
            else
            {
                if (Y > p.Y)
                {
                    edy = (par.Top - Y) + (p.Y - par.Bottom);
                }
                else
                {
                    edy = (par.Bottom - Y) + (p.Y - par.Top);
                }
            }

            // The same action for Z component.
            double dz = DistZ(p);
            double edz = 0.0;
            if (dz <= par.Depth - dz)
            {
                edz = p.Z - Z;
            }
            else
            {
                if (Z > p.Z)
                {
                    edz = (par.Front - Z) + (p.Z - par.Back);
                }
                else
                {
                    edz = (par.Back - Z) + (p.Z - par.Front);
                }
            }

            return new Vector(edx, edy, edz);
        }

        /// <summary>
        /// Move point in toroid.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="rect">rect</param>
        public void ToroidMove(Vector v, Rect rect)
        {
            Debug.Assert(IsIn(rect), "Toroid operations are available only for inner points.");

            X += v.X;
            if (X > rect.Right)
            {
                X -= rect.Width;
            }
            else if (X < rect.Left)
            {
                X += rect.Width;
            }

            Y += v.Y;
            if (Y > rect.Top)
            {
                Y -= rect.Height;
            }
            else if (Y < rect.Bottom)
            {
                Y += rect.Height;
            }

            Debug.Assert(IsIn(rect), "Too big shift for toroid operation.");
        }

        /// <summary>
        /// Move point in toroid.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="par">parallelepiped</param>
        public void ToroidMove(Vector v, Parallelepiped par)
        {
            Debug.Assert(IsIn(par), "Toroid operations are available only for inner points.");

            X += v.X;
            if (X > par.Right)
            {
                X -= par.Width;
            }
            else if (X < par.Left)
            {
                X += par.Width;
            }

            Y += v.Y;
            if (Y > par.Top)
            {
                Y -= par.Height;
            }
            else if (Y < par.Bottom)
            {
                Y += par.Height;
            }

            Z += v.Z;
            if (Z > par.Front)
            {
                Z -= par.Depth;
            }
            else if (Z < par.Back)
            {
                Z += par.Depth;
            }

            Debug.Assert(IsIn(par), "Too big shift for toroid operation.");
        }

        /// <summary>
        /// Check if points are equal.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns><c>true</c> - if points are equal, <c>false</c> - otherwise</returns>
        public bool IsEq(Point p)
        {
            return (Math.Abs(X - p.X) + Math.Abs(Y - p.Y) + Math.Abs(Z - p.Z) < Maths.Eps);
        }
    }
}
