// Copyright Joy Developing.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Abstract triangle.
    /// </summary>
    public abstract class Triangle
    {
        /// <summary>
        /// Side <c>AB</c> length.
        /// </summary>
        public abstract double AB { get; }

        /// <summary>
        /// Side <c>BC</c> length.
        /// </summary>
        public abstract double BC { get; }

        /// <summary>
        /// Side <c>AC</c> length.
        /// </summary>
        public abstract double AC { get; }

        /// <summary>
        /// Perimeter.
        /// </summary>
        public double Perimeter
        {
            get
            {
                return AB + BC + AC;
            }
        }

        /// <summary>
        /// Half of perimeter.
        /// </summary>
        public double HalfPerimeter
        {
            get
            {
                return Perimeter / 2.0;
            }
        }

        /// <summary>
        /// Square.
        /// </summary>
        public double Square
        {
            get
            {
                double hp = HalfPerimeter;

                // Geron's formula.
                return Math.Sqrt(hp * (hp - AB) * (hp - BC) * (hp - AC));
            }
        }
    }
}
