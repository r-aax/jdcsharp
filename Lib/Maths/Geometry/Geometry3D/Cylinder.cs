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
    /// The cylinder is based on segment [(0, 0, 0), (0, 0, h)].
    /// </summary>
    public class Cylinder
    {
        /// <summary>
        /// Height.
        /// </summary>
        public double Height;

        /// <summary>
        /// Radius.
        /// </summary>
        public double Radius;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="height">height</param>
        /// <param name="radius">radius</param>
        public Cylinder(double height, double radius)
        {
            Height = height;
            Radius = radius;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Cylinder()
            : this(0.0, 0.0)
        {
        }

        /// <summary>
        /// Base circle.
        /// </summary>
        public Circle Circle
        {
            get
            {
                return new Circle(new Point(0.0, 0.0), Radius);
            }
        }
    }
}
