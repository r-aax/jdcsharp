// Author: Alexey Rybakov

namespace Lib.Physics.MeasUnits
{
    /// <summary>
    /// General convert.
    /// </summary>
    public class Convert
    {
        /// <summary>
        /// Kilo conversion.
        /// </summary>
        /// <param name="v">initial value</param>
        /// <returns>result</returns>
        public static double Kilo(double v)
        {
            return v / Constants.Kilo;
        }

        /// <summary>
        /// Milli conversion.
        /// </summary>
        /// <param name="v">initial value</param>
        /// <returns>result</returns>
        public static double Milli(double v)
        {
            return v / Constants.Milli;
        }
    }
}
