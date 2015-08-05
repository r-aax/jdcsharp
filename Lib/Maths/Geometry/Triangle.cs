// Author: Alexey Rybakov

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

        /// <summary>
        /// Altitude from A to BC.
        /// </summary>
        public double AltitudeA
        {
            get
            {
                // Square = 0.5 * BC * AltitudeA.
                // AltitudeA = Square / (0.5 * BC) = 2 * Square / BC.
                return 2.0 * Square / BC;
            }
        }

        /// <summary>
        /// Altitude from B to AC.
        /// </summary>
        public double AltitudeB
        {
            get
            {
                // Square = 0.5 * AC * AltitudeB.
                // AltitudeB = Square / (0.5 * AC) = 2 * Square / AC.
                return 2.0 * Square / AC;
            }
        }

        /// <summary>
        ///  Altitute from C to AB.
        /// </summary>
        public double AltitudeC
        {
            get
            {
                // Square = 0.5 * AB * AltitudeC.
                // AltitudeC = Square / (0.5 * AB) = 2 * Square / AB.
                return 2.0 * Square / AB;
            }
        }
    }
}
