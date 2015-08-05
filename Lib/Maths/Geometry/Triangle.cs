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
        /// Angle A.
        /// </summary>
        public double AngleA
        {
            get
            {
                return Math.Acos((AB * AB + AC * AC - BC * BC) / (2.0 * AB * AC));
            }
        }

        /// <summary>
        /// Angle B.
        /// </summary>
        public double AngleB
        {
            get
            {
                return Math.Acos((AB * AB + BC * BC - AC * AC) / (2.0 * AB * BC));
            }
        }

        /// <summary>
        /// Angle C.
        /// </summary>
        public double AngleC
        {
            get
            {
                return Math.Acos((BC * BC + AC * AC - AB * AB) / (2.0 * BC * AC));
            }
        }

        /// <summary>
        /// Height from A to BC.
        /// </summary>
        public double HeightA
        {
            get
            {
                // Square = 0.5 * BC * HeightA.
                // HeightA = Square / (0.5 * BC) = 2 * Square / BC.
                return 2.0 * Square / BC;
            }
        }

        /// <summary>
        /// Height from B to AC.
        /// </summary>
        public double HeightB
        {
            get
            {
                // Square = 0.5 * AC * HeightB.
                // HeightB = Square / (0.5 * AC) = 2 * Square / AC.
                return 2.0 * Square / AC;
            }
        }

        /// <summary>
        ///  Height from C to AB.
        /// </summary>
        public double HeightC
        {
            get
            {
                // Square = 0.5 * AB * HeightC.
                // HeightC = Square / (0.5 * AB) = 2 * Square / AB.
                return 2.0 * Square / AB;
            }
        }
    }
}
