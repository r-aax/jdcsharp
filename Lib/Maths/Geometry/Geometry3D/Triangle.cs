// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Maths.Geometry.Geometry3D
{
    /// <summary>
    /// Triangle.
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
    }
}
