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
    /// Speed.
    /// </summary>
    public class Speed
    {
        /// <summary>
        /// Value.
        /// </summary>
        public double Value;

        /// <summary>
        /// Unit.
        /// </summary>
        public SpeedMeasUnit Unit;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="unit">unit</param>
        public Speed(double value, SpeedMeasUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Convert operator.
        /// </summary>
        /// <param name="angle">angle</param>
        /// <returns>double value</returns>
        public static implicit operator double(Speed speed)
        {
            return speed.Value;
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
                    case SpeedMeasUnit.MPS:
                        return "m/s";

                    case SpeedMeasUnit.KMPH:
                        return "km/h";

                    case SpeedMeasUnit.FPM:
                        return "ft/m";

                    case SpeedMeasUnit.Knots:
                        return "knot";

                    default:
                        Debug.Assert(false);
                        return "";
                }
            }
        }

        /// <summary>
        /// MPS for speed.
        /// </summary>
        /// <param name="unit">unit</param>
        /// <returns>MPS value</returns>
        public static double SpeedMPS(SpeedMeasUnit unit)
        {
            switch (unit)
            {
                case SpeedMeasUnit.MPS:
                    return 1.0;

                case SpeedMeasUnit.KMPH:
                    return Constants.Kilo / Constants.HourSeconds;

                case SpeedMeasUnit.FPM:
                    return Linear.FootMeters / Constants.MinuteSeconds;

                case SpeedMeasUnit.Knots:
                    return Linear.NauticalMileMeters / Constants.HourSeconds;

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
        public double Get(SpeedMeasUnit unit)
        {
            return Value * (SpeedMPS(Unit) / SpeedMPS(unit));
        }

        /// <summary>
        /// Convert inner value.
        /// </summary>
        /// <param name="unit">measurements unit</param>
        public void Convert(SpeedMeasUnit unit)
        {
            Value = Get(unit);
            Unit = unit;
        }
    }
}