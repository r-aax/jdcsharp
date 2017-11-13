// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Vector (2D or 3D).
    /// </summary>
    public class Vector : ICloneable
    {
        /// <summary>
        /// Coordinate <c>X</c>.
        /// </summary>
        public double X = 0.0;

        /// <summary>
        /// Coordinate <c>Y</c>.
        /// </summary>
        public double Y = 0.0;

        /// <summary>
        /// Coordinate <c>Z</c>.
        /// </summary>
        public double Z = 0.0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">first component</param>
        /// <param name="y">second component</param>
        /// <param name="z">third component</param>
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Constructor for <c>2D</c> vector.
        /// </summary>
        /// <param name="x">first component</param>
        /// <param name="y">second component</param>
        public Vector(double x, double y)
            : this(x, y, 0.0)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Vector()
            : this(0.0, 0.0, 0.0)
        {
        }

        /// <summary>
        /// Vector copy constructor.
        /// </summary>
        /// <param name="v">vector</param>
        public Vector(Vector v)
            : this(v.X, v.Y, v.Z)
        {
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("({0},{1},{2})", X, Y, Z);
        }

        /// <summary>
        /// Square of module.
        /// </summary>
        public double Mod2
        {
            get
            {
                return X * X + Y * Y + Z * Z;
            }
        }

        /// <summary>
        /// Module.
        /// </summary>
        public double Mod
        {
            get
            {
                return Math.Sqrt(Mod2);
            }
        }

        /// <summary>
        /// Square of <c>X</c>.
        /// </summary>
        public double X2
        {
            get
            {
                return X * X;
            }
        }

        /// <summary>
        /// Square of <c>Y</c>.
        /// </summary>
        public double Y2
        {
            get
            {
                return Y * Y;
            }
        }

        /// <summary>
        /// Square of <c>Z</c>.
        /// </summary>
        public double Z2
        {
            get
            {
                return Z * Z;
            }
        }

        /// <summary>
        /// <c>X</c> and <c>Y</c> multiplication.
        /// </summary>
        public double XY
        {
            get
            {
                return X * Y;
            }
        }

        /// <summary>
        /// <c>X</c> and <c>Z</c> multiplication.
        /// </summary>
        public double XZ
        {
            get
            {
                return X * Z;
            }
        }

        /// <summary>
        /// <c>Y</c> and <c>Z</c> multiplication.
        /// </summary>
        public double YZ
        {
            get
            {
                return Y * Z;
            }
        }

        /// <summary>
        /// Sum of vectors.
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>sum</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Unary minus.
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>negated vector</returns>
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        /// <summary>
        /// Vectors subtraction.
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
            return new Vector(v.X * k, v.Y * k, v.Z * k);
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
        /// Division.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="k">number</param>
        /// <returns>result</returns>
        public static Vector operator /(Vector v, double k)
        {
            return v * (1.0 / k);
        }

        /// <summary>
        /// Orthogonal vector in <c>XY</c> plane.
        /// </summary>
        /// <returns>orthogonal vector</returns>
        public Vector OrthogonalXY()
        {
            return new Vector(Y, -X);
        }

        /// <summary>
        /// Acces with iterator.
        /// </summary>
        /// <param name="i">number</param>
        /// <returns>vector element</returns>
        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return X;

                    case 1:
                        return Y;

                    case 2:
                        return Z;

                    default:
                        throw new Exception("wrong coordinate of 3D vector");
                }
            }

            set
            {
                switch (i)
                {
                    case 0:
                        X = value;
                        break;

                    case 1:
                        Y = value;
                        break;

                    case 2:
                        Z = value;
                        break;

                    default:
                        throw new Exception("wrong coordinate of 3D vector");
                }
            }
        }

        /// <summary>
        /// Move vector.
        /// </summary>
        /// <param name="x">move <c>x</c></param>
        /// <param name="y">move <c>y</c></param>
        /// <param name="z">move <c>z</c></param>
        public void Move(double x, double y, double z)
        {
            X += x;
            Y += y;
            Z += z;
        }

        /// <summary>
        /// Move by vector.
        /// </summary>
        /// <param name="v">vector</param>
        public void Move(Vector v)
        {
            Move(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// Scaling.
        /// </summary>
        /// <param name="kx">scale factor <c>x</c></param>
        /// <param name="ky">scale factor <c>y</c></param>
        /// <param name="kz">scale factor <c>z</c></param>
        public void Scale(double kx, double ky, double kz)
        {
            X *= kx;
            Y *= ky;
            Z *= kz;
        }

        /// <summary>
        /// Scaling.
        /// </summary>
        /// <param name="k">scale factor</param>
        public void Scale(double k)
        {
            Scale(k, k, k);
        }

        /// <summary>
        /// Scaling relative to vector.
        /// </summary>
        /// <param name="k">scale factor</param>
        /// <param name="v">vector</param>
        public void Scale(double k, Vector v)
        {
            Move(-v);
            Scale(k);
            Move(v);
        }

        /// <summary>
        /// Rotate <c>X</c> contraclockwise.
        /// </summary>
        /// <param name="s">angle sine</param>
        /// <param name="c">angle cosine</param>
        public void RotX(double s, double c)
        {
            double y = Y;
            double z = Z;

            Y = y * c - z * s;
            Z = y * s + z * c;
        }

        /// <summary>
        /// Rotate <c>X</c> contraclockwise.
        /// </summary>
        /// <param name="a">angle</param>
        public void RotX(double a)
        {
            RotX(Math.Sin(a), Math.Cos(a));
        }

        /// <summary>
        /// Rotate <c>X</c> with given vector.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="a">angle</param>
        public void RotX(Vector v, double a)
        {
            Move(-v);
            RotX(a);
            Move(v);
        }

        /// <summary>
        /// Rotate <c>Y</c> contraclockwise.
        /// </summary>
        /// <param name="a">angle</param>
        public void RotY(double a)
        {
            double x = X;
            double z = Z;
            double s = Math.Sin(a);
            double c = Math.Cos(a);

            X = x * c + z * s;
            Z = -x * s + z * c;
        }

        /// <summary>
        /// Rotate <c>Y</c> with given vector.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="a">angle</param>
        public void RotY(Vector v, double a)
        {
            Move(-v);
            RotY(a);
            Move(v);
        }

        /// <summary>
        /// Rotate <c>Z</c> contraclockwise.
        /// </summary>
        /// <param name="a">angle</param>
        public void RotZ(double a)
        {
            double x = X;
            double y = Y;
            double s = Math.Sin(a);
            double c = Math.Cos(a);

            X = x * c - y * s;
            Y = x * s + y * c;
        }

        /// <summary>
        /// Rotate <c>Z</c> with given vector.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="a">angle</param>
        public void RotZ(Vector v, double a)
        {
            Move(-v);
            RotZ(a);
            Move(v);
        }

        /// <summary>
        /// Rotate with given axis.
        /// </summary>
        /// <param name="a">angle</param>
        /// <param name="ax">axis</param>
        public void Rot(double a, AxisType ax)
        {
            switch (ax)
            {
                case AxisType.X:
                    RotX(a);
                    break;

                case AxisType.Y:
                    RotY(a);
                    break;

                case AxisType.Z:
                    RotZ(a);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// Rotate with given axis with given vector.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="a">angle</param>
        /// <param name="ax">axis</param>
        public void Rot(Vector v, double a, AxisType ax)
        {
            switch (ax)
            {
                case AxisType.X:
                    RotX(v, a);
                    break;

                case AxisType.Y:
                    RotY(v, a);
                    break;

                case AxisType.Z:
                    RotZ(v, a);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// Rotate <c>Z</c> contraclockwise.
        /// </summary>
        /// <param name="a">angle</param>
        public void Rot(double a)
        {
            RotZ(a);
        }

        /// <summary>
        /// Rotate <c>Z</c> with given vector.
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="a">angle</param>
        public void Rot(Vector v, double a)
        {
            RotZ(v, a);
        }

        /// <summary>
        /// Normalization to given size <c>k</c>.
        /// </summary>
        /// <param name="k">size</param>
        public void Normalize(double k)
        {
            Scale(k / Mod);
        }

        /// <summary>
        /// Mean vector.
        /// </summary>
        /// <param name="v1">first vector</param>
        /// <param name="v2">second vector</param>
        /// <param name="k">coefficient</param>
        /// <returns>mean vector</returns>
        public static Vector Mean(Vector v1, Vector v2, double k)
        {
            // With k = 0.0 we have first vector.
            // With k = 1.0 we have second vector.
            // In other cases we have mean vector.

            Debug.Assert((k >= 0.0) && (k <= 1.0));

            return (1.0 - k) * v1 + k * v2;
        }

        /// <summary>
        /// Middle vector.
        /// </summary>
        /// <param name="v1">first vector</param>
        /// <param name="v2">second</param>
        /// <returns>middle vector</returns>
        public static Vector Mid(Vector v1, Vector v2)
        {
            return Mean(v1, v2, 0.5);
        }

        /// <summary>
        /// Average vector.
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
            return new Vector(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
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

        /// <summary>
        /// Random vector.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        /// <returns>random vector</returns>
        public static Vector Random(Parallelepiped par)
        {
            return new Vector(Randoms.RandomInInterval(par.XInterval),
                              Randoms.RandomInInterval(par.YInterval),
                              Randoms.RandomInInterval(par.ZInterval));
        }

        /// <summary>
        /// Random vector on surface of parallelepiped.
        /// </summary>
        /// <param name="par">parallelepiped</param>
        /// <returns>random vector</returns>
        public static Vector RandomOnSurface(Parallelepiped par)
        {
            Interval xi = par.XInterval;
            Interval yi = par.YInterval;
            Interval zi = par.ZInterval;
            double sx = yi.Length * zi.Length;
            double sy = xi.Length * zi.Length;
            double sz = xi.Length * yi.Length;
            double s = sx + sy + sz;
            double r = Randoms.RandomInInterval(0.0, s);

            if (r <= sx)
            {
                // On x facet.
                return new Vector(Randoms.RandomBool() ? par.Right : par.Left,
                                  Randoms.RandomInInterval(par.YInterval),
                                  Randoms.RandomInInterval(par.ZInterval));
            }
            else if (r <= sx + sy)
            {
                // On y facet.
                return new Vector(Randoms.RandomInInterval(par.XInterval),
                                  Randoms.RandomBool() ? par.Top : par.Bottom,
                                  Randoms.RandomInInterval(par.ZInterval));
            }
            else
            {
                // On z facet.
                return new Vector(Randoms.RandomInInterval(par.XInterval),
                                  Randoms.RandomInInterval(par.YInterval),
                                  Randoms.RandomBool() ? par.Front : par.Back);
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public virtual object Clone()
        {
            return new Vector(X, Y, Z);
        }

        /// <summary>
        /// Get copy of vector.
        /// </summary>
        public Vector Copy
        {
            get
            {
                return Clone() as Vector;
            }
        }

        /// <summary>
        /// Invert <c>X</c> direction.
        /// </summary>
        public void InvertX()
        {
            X = -X;
        }

        /// <summary>
        /// Invert <c>Y</c> direction.
        /// </summary>
        public void InvertY()
        {
            Y = -Y;
        }

        /// <summary>
        /// Invert <c>Z</c> direction.
        /// </summary>
        public void InvertZ()
        {
            Z = -Z;
        }

        /// <summary>
        /// Inverted copy of vector around <c>X</c> axis.
        /// </summary>
        /// <returns>inverted vector</returns>
        public Vector InvertedX()
        {
            Vector cv = Copy;

            cv.InvertX();

            return cv;
        }

        /// <summary>
        /// Inverted copy of vector around <c>Y</c> axis.
        /// </summary>
        /// <returns>inverted vector</returns>
        public Vector InvertedY()
        {
            Vector cv = Copy;

            cv.InvertY();

            return cv;
        }

        /// <summary>
        /// Inverted copy of vector around <c>Z</c> axis.
        /// </summary>
        /// <returns>inverted vector</returns>
        public Vector InvertedZ()
        {
            Vector cv = Copy;

            cv.InvertZ();

            return cv;
        }
    }
}
