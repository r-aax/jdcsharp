// Author: Alexey Rybakov

namespace Lib.Maths.Geometry.Geometry3D
{
    /// <summary>
    /// Sphere.
    /// </summary>
    public class Sphere
    {
        /// <summary>
        /// Center.
        /// </summary>
        public Point Center;

        /// <summary>
        /// Radius.
        /// </summary>
        public double Radius;

        /// <summary>
        /// Constructor from center and radius.
        /// </summary>
        /// <param name="center">center</param>
        /// <param name="radius">radius</param>
        public Sphere(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Sphere()
            : this(new Point(0.0, 0.0, 0.0), 0.0)
        {
        }
    }
}
