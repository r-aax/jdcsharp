// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lib.Physics.MeasUnits
{
    /// <summary>
    /// Angle.
    /// </summary>
    public class Angle
    {
        /// <summary>
        /// Value.
        /// </summary>
        public double Value;

        /// <summary>
        /// Unit.
        /// </summary>
        public AngleMeasUnit Unit;

        /// <summary>
        /// Degrees in PI.
        /// </summary>
        public const double PIDegrees = 180.0;

        /// <summary>
        /// Degrees in radian.
        /// </summary>
        public const double RadianDegrees = PIDegrees / Math.PI;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="unit">unit</param>
        public Angle(double value, AngleMeasUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Convert operator.
        /// </summary>
        /// <param name="angle">angle</param>
        /// <returns>double value</returns>
        public static implicit operator double(Angle angle)
        {
            return angle.Value;
        }

        /// <summary>
        /// Name of measuring unit.
        /// </summary>
        public string Name
        {
            get
            {
                switch (Unit)
                {
                    case AngleMeasUnit.Degree:
                        return "deg";

                    case AngleMeasUnit.Radian:
                        return "rad";

                    default:
                        Debug.Assert(false);
                        return "";
                }
            }
        }

        /// <summary>
        /// Degrees count in angle.
        /// </summary>
        /// <param name="unit">unit</param>
        /// <returns>angle degrees count</returns>
        public static double AngleDegrees(AngleMeasUnit unit)
        {
            switch (unit)
            {
                case AngleMeasUnit.Degree:
                    return 1.0;

                case AngleMeasUnit.Radian:
                    return RadianDegrees;

                default:
                    Debug.Assert(false);
                    return 1.0;
            }
        }

        /// <summary>
        /// Get value with given unit.
        /// </summary>
        /// <param name="unit">measurements unit</param>
        /// <returns>value of angle</returns>
        public double Get(AngleMeasUnit unit)
        {
            return Value * (AngleDegrees(Unit) / AngleDegrees(unit));
        }

        /// <summary>
        /// Convert inner value.
        /// </summary>
        /// <param name="unit">measurements unit</param>
        public void Convert(AngleMeasUnit unit)
        {
            Value = Get(unit);
            Unit = unit;
        }
    }
}
