// Author: Alexey Rybakov

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// Circle.
    /// </summary>
    public class Circle
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
        /// Constructor.
        /// </summary>
        /// <param name="center">center</param>
        /// <param name="radius">radius</param>
        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Circle()
            : this(new Point(0.0, 0.0), 0.0)
        {
        }

        /// <summary>
        /// Scaled circle.
        /// </summary>
        /// <param name="k">coefficient</param>
        /// <returns>circle</returns>
        public Circle Scaled(double k)
        {
            return new Circle(Center, Radius * k);
        }

        /// <summary>
        /// Extend to sphere.
        /// </summary>
        /// <returns>sphere</returns>
        public Lib.Maths.Geometry.Geometry3D.Sphere Extended()
        {
            return new Geometry3D.Sphere(Center.Extended(), Radius);
        }
    }
}
