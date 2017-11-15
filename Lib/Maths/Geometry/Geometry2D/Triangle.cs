// Author: Alexey Rybakov

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// 2D triangle.
    /// </summary>
    public class Triangle : Lib.Maths.Geometry.Triangle
    {
        /// <summary>
        /// First point.
        /// </summary>
        public Point A = null;

        /// <summary>
        /// Second point.
        /// </summary>
        public Point B = null;

        /// <summary>
        /// Third point.
        /// </summary>
        public Point C = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">first point</param>
        /// <param name="b">second point</param>
        /// <param name="c">third point</param>
        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// <c>AB</c> length.
        /// </summary>
        public override double AB
        {
            get
            {
                return (A - B).Mod;
            }
        }

        /// <summary>
        /// <c>BC</c> length.
        /// </summary>
        public override double BC
        {
            get
            {
                return (B - C).Mod;
            }
        }

        /// <summary>
        /// <c>AC</c> length.
        /// </summary>
        public override double AC
        {
            get
            {
                return (A - C).Mod;
            }
        }

        /// <summary>
        /// Barycenter.
        /// </summary>
        /// <returns>barycenter</returns>
        public Point Barycenter()
        {
            Vector bcm = Point.Mid(B, C);

            return new Point(Point.Mean(A, bcm, 2.0 / 3.0));
        }

        /// <summary>
        /// Oriented area.
        /// </summary>
        public double OrientedArea
        {
            get
            {
                return (B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X);
            }
        }

        /// <summary>
        /// Sign of oriented area.
        /// </summary>
        public double OrientedAreaSign
        {
            get
            {
                double a = OrientedArea;

                if (a > 0.0)
                {
                    return 1.0;
                }
                else if (a < 0.0)
                {
                    return -1.0;
                }
                else
                {
                    return 0.0;
                }
            }
        }
    }
}
