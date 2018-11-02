using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry2D;

namespace Lib.Maths.Geometry.Geometry3D
{
    /// <summary>
    /// Cylinder.
    /// </summary>
    public class Cylinder
    {
        /// <summary>
        /// Center point (in plane <c>XY</c>).
        /// </summary>
        public Point Center;

        /// <summary>
        /// Radius.
        /// </summary>
        public double Radius;

        /// <summary>
        /// Height.
        /// </summary>
        public double Height;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="center">center</param>
        /// <param name="radius">radius</param>
        /// <param name="height">height</param>
        public Cylinder(Point center, double radius, double height)
        {
            Center = center.Copy;
            Radius = radius;
            Height = height;
        }

        /// <summary>
        /// Constructor from circle.
        /// </summary>
        /// <param name="circle">circle</param>
        /// <param name="height">height</param>
        public Cylinder(Circle circle, double height)
            : this(circle.Center, circle.Radius, height)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Cylinder()
            : this(new Point(), 0.0, 0.0)
        {
        }

        /// <summary>
        /// Base circle.
        /// </summary>
        public Circle Circle
        {
            get
            {
                return new Circle(Center.Copy, Radius);
            }
        }
    }
}
