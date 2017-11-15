// Author: Alexey Rybakov

using Scaler1D = Lib.Maths.Geometry.IntervalDScaler;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// Mapping one rectangle to another.
    /// </summary>
    public class RectScaler
    {
        /// <summary>
        /// Coordinate <c>x</c> mapper.
        /// </summary>
        public Scaler1D ScX;

        /// <summary>
        /// Coordinate <c>y</c> mapper.
        /// </summary>
        public Scaler1D ScY;

        /// <summary>
        /// Scale factor <c>x</c>.
        /// </summary>
        public double XScaleFactor
        {
            get
            {
                return ScX.ScaleFactor;
            }
        }

        /// <summary>
        /// Scale factor <c>y</c>.
        /// </summary>
        public double YScaleFactor
        {
            get
            {
                return ScY.ScaleFactor;
            }
        }

        /// <summary>
        /// Invert coefficients vector.
        /// </summary>
        public Vector InvertCoefficientsVector
        {
            get
            {
                return new Vector(ScX.InvertCoefficient, ScY.InvertCoefficient);
            }
        }

        /// <summary>
        /// Constructor from rectangles.
        /// </summary>
        /// <param name="from">initial rectangle</param>
        /// <param name="to">target rectangle</param>
        /// <param name="is_x_invert">invert flag <c>x</c></param>
        /// <param name="is_y_invert">invert flag <c>y</c></param>
        public RectScaler(Rect from, Rect to, bool is_x_invert, bool is_y_invert)
        {
            ScX = new Scaler1D(from.XInterval, to.XInterval, is_x_invert);
            ScY = new Scaler1D(from.YInterval, to.YInterval, is_y_invert);
        }

        /// <summary>
        /// Direct mapping.
        /// </summary>
        /// <param name="p">argement</param>
        /// <returns>image</returns>
        public Point T(Point p)
        {
            return new Point(ScX.T(p.X), ScY.T(p.Y));
        }

        /// <summary>
        /// Back mapping.
        /// </summary>
        /// <param name="p">image</param>
        /// <returns>argument</returns>
        public Point F(Point p)
        {
            return new Point(ScX.F(p.X), ScY.F(p.Y));
        }
    }
}
