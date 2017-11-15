﻿// Author: Alexey Rybakov

using System;

namespace Lib.Maths.Geometry.Geometry3D
{
    /// <summary>
    /// Parallelepiped.
    /// </summary>
    public class Parallelepiped
    {
        /// <summary>
        /// Interval <c>x</c>.
        /// </summary>
        public IntervalD XInterval;

        /// <summary>
        /// Interval <c>y</c>.
        /// </summary>
        public IntervalD YInterval;

        /// <summary>
        /// Interval <c>z</c>.
        /// </summary>
        public IntervalD ZInterval;

        /// <summary>
        /// Constructor from intervals.
        /// </summary>
        /// <param name="xi">interval <c>x</c></param>
        /// <param name="yi">interval <c>y</c></param>
        /// <param name="zi">interval <c>z</c></param>
        public Parallelepiped(IntervalD xi, IntervalD yi, IntervalD zi)
        {
            XInterval = xi;
            YInterval = yi;
            ZInterval = zi;
        }

        /// <summary>
        /// Constructor from sizes.
        /// </summary>
        /// <param name="xsize">size <c>x</c></param>
        /// <param name="ysize">size <c>y</c></param>
        /// <param name="zsize">size <c>z</c></param>
        public Parallelepiped(double xsize, double ysize, double zsize)
            : this(new IntervalD(xsize), new IntervalD(ysize), new IntervalD(zsize))
        {
        }

        /// <summary>
        /// Constructor from all min and max values.
        /// </summary>
        /// <param name="x_min">min <c>x</c></param>
        /// <param name="x_max">max <c>x</c></param>
        /// <param name="y_min">min <c>y</c></param>
        /// <param name="y_max">max <c>y</c></param>
        /// <param name="z_min">min <c>z</c></param>
        /// <param name="z_max">max <c>z</c></param>
        public Parallelepiped(double x_min, double x_max,
                              double y_min, double y_max,
                              double z_min, double z_max)
            : this(new IntervalD(x_min, x_max), new IntervalD(y_min, y_max), new IntervalD(z_min, z_max))
        {
        }

        /// <summary>
        /// Left side.
        /// </summary>
        public double Left
        {
            get
            {
                return XInterval.L;
            }
        }

        /// <summary>
        /// Right side.
        /// </summary>
        public double Right
        {
            get
            {
                return XInterval.H;
            }
        }

        /// <summary>
        /// Bottom side.
        /// </summary>
        public double Bottom
        {
            get
            {
                return YInterval.L;
            }
        }

        /// <summary>
        /// Top side.
        /// </summary>
        public double Top
        {
            get
            {
                return YInterval.H;
            }
        }

        /// <summary>
        /// Back side.
        /// </summary>
        public double Back
        {
            get
            {
                return ZInterval.L;
            }
        }

        /// <summary>
        /// Front side.
        /// </summary>
        public double Front
        {
            get
            {
                return ZInterval.H;
            }
        }

        /// <summary>
        /// Width.
        /// </summary>
        public double Width
        {
            get
            {
                return XInterval.Length;
            }
        }

        /// <summary>
        /// Height.
        /// </summary>
        public double Height
        {
            get
            {
                return YInterval.Length;
            }
        }

        /// <summary>
        /// Depth.
        /// </summary>
        public double Depth
        {
            get
            {
                return ZInterval.Length;
            }
        }

        /// <summary>
        /// Middle point of parallelepiped.
        /// </summary>
        public Point Mid
        {
            get
            {
                return new Point(XInterval.Mid, YInterval.Mid, ZInterval.Mid);
            }
        }

        /// <summary>
        /// Parallelepiped radius.
        /// </summary>
        public double Radius
        {
            get
            {
                double m = Math.Min(Width, Height);

                return Math.Min(m, Depth) / 2.0;
            }
        }
    }
}
