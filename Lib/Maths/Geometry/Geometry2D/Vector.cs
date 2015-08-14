// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Numbers;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// 2D-vector.
    /// </summary>
    public class Vector : Lib.Maths.Geometry.Vector
    {
        /// <summary>
        /// Coordinate <c>x</c>.
        /// </summary>
        public double X;

        /// <summary>
        /// Coordinate <c>y</c>.
        /// </summary>
        public double Y;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">first component</param>
        /// <param name="y">second component</param>
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Vector()
            : this(0.0, 0.0)
        {
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("({0:0.##}, {1:0.##})", X, Y);
        }

        /// <summary>
        /// Square of module.
        /// </summary>
        public override double Mod2
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        /// <summary>
        /// Sum.
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>sum</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Unary minus.
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>inverted vector</returns>
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y);
        }

        /// <summary>
        /// Subtraction.
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>result</returns>
        public static Vector operator -(Vector a, Vector b)
        {
            return a + (-b);
        }

        /// <summary>
        /// Multiplication with number (right).
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="k">number</param>
        /// <returns>result</returns>
        public static Vector operator *(Vector v, double k)
        {
            return new Vector(v.X * k, v.Y * k);
        }

        /// <summary>
        /// Multiplication with number (left).
        /// </summary>
        /// <param name="k">number</param>
        /// <param name="v">vector</param>
        /// <returns>result</returns>
        public static Vector operator *(double k, Vector v)
        {
            return v * k;
        }

        /// <summary>
        /// Division by number.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="k">number</param>
        /// <returns>result</returns>
        public static Vector operator /(Vector v, double k)
        {
            return v * (1.0 / k);
        }

        /// <summary>
        /// Orthogonal vector.
        /// </summary>
        /// <returns>orthogonal vector</returns>
        public Vector Orthogonal()
        {
            return new Vector(Y, -X);
        }

        /// <summary>
        /// Vector move.
        /// </summary>
        /// <param name="x">move <c>x</c></param>
        /// <param name="y">move <c>y</c></param>
        public void Move(double x, double y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        /// Move by vector.
        /// </summary>
        /// <param name="v">vector</param>
        public void Move(Vector v)
        {
            Move(v.X, v.Y);
        }

        /// <summary>
        /// Scaling.
        /// </summary>
        /// <param name="kx">scaling coefficient <c>x</c></param>
        /// <param name="ky">scaling coefficient<c>y</c></param>
        public void Scale(double kx, double ky)
        {
            X *= kx;
            Y *= ky;
        }

        /// <summary>
        /// General scaling.
        /// </summary>
        /// <param name="k">coefficient</param>
        public override void Scale(double k)
        {
            Scale(k, k);
        }

        /// <summary>
        /// Scaling relative to vector.
        /// </summary>
        /// <param name="k">coefficient</param>
        /// <param name="v">vector</param>
        public void Scale(double k, Vector v)
        {
            Move(-v);
            Scale(k);
            Move(v);
        }

        /// <summary>
        /// Rotation.
        /// </summary>
        /// <param name="a">angle</param>
        public void Rot(double a)
        {
            double x = X;
            double y = Y;
            double s = Math.Sin(a);
            double c = Math.Cos(a);

            X = x * c - y * s;
            Y = x * s + y * c;
        }

        /// <summary>
        /// Rotation around given point.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="a">angle</param>
        public void Rot(Vector v, double a)
        {
            Move(-v);
            Rot(a);
            Move(v);
        }

        /// <summary>
        /// Mean vector.
        /// </summary>
        /// <param name="v1">first vector</param>
        /// <param name="v2">second vector</param>
        /// <param name="k">coefficient</param>
        /// <returns></returns>
        public static Vector Mean(Vector v1, Vector v2, double k)
        {
            // With k = 0.0 we have first vector.
            // With k = 1.0 we have second vector.
            // In other case we have mean vector.

            Debug.Assert((k >= 0.0) && (k <= 1.0));

            return (1.0 - k) * v1 + k * v2;
        }

        /// <summary>
        /// Mean vector.
        /// </summary>
        /// <param name="v1">first vector</param>
        /// <param name="v2">second vector</param>
        /// <returns>mean vector</returns>
        public static Vector Mid(Vector v1, Vector v2)
        {
            return Mean(v1, v2, 0.5);
        }

        /// <summary>
        /// Vectors array average vector.
        /// </summary>
        /// <param name="vs">vectors array</param>
        /// <returns>average vector</returns>
        public static Vector Avg(Vector[] vs)
        {
            if (vs.Length == 0)
            {
                return null;
            }

            Vector v = vs[0];

            for (int i = 1; i < vs.Length; i++)
            {
                v += vs[i];
            }

            return v / vs.Length;
        }

        /// <summary>
        /// Componentwise multiplication.
        /// </summary>
        /// <param name="v1">first vector</param>
        /// <param name="v2">second vector</param>
        /// <returns>result</returns>
        public static Vector ComponentWiseMul(Vector v1, Vector v2)
        {
            return new Vector(v1.X * v2.X, v1.Y * v2.Y);
        }

        /// <summary>
        /// Random vector.
        /// </summary>
        /// <param name="rect">rectangle</param>
        /// <returns>random vector</returns>
        public static Vector Random(Rect rect)
        {
            return new Vector(Randoms.RandomInInterval(rect.XInterval),
                              Randoms.RandomInInterval(rect.YInterval));
        }
    }
}
