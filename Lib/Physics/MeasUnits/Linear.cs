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
    /// Linear.
    /// </summary>
    public class Linear
    {
        /// <summary>
        /// Value.
        /// </summary>
        public double Value;

        /// <summary>
        /// Unit.
        /// </summary>
        public LinearMeasUnit Unit;

        /// <summary>
        /// Meters count in one foot.
        /// </summary>
        public const double FootMeters = 0.3048;

        /// <summary>
        /// Meters count in one nautical mile.
        /// </summary>
        public const double NauticalMileMeters = 1852.0;

        /// <summary>
        /// Feet count in one nautical mile.
        /// </summary>
        public const double NauticalMileFeet = NauticalMileMeters / FootMeters;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="unit">unit</param>
        public Linear(double value, LinearMeasUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Convert operator.
        /// </summary>
        /// <param name="angle">angle</param>
        /// <returns>double value</returns>
        public static implicit operator double(Linear angle)
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
                    case LinearMeasUnit.Meter:
                        return "m";

                    case LinearMeasUnit.Kilometer:
                        return "km";

                    case LinearMeasUnit.Foot:
                        return "ft";

                    case LinearMeasUnit.NauticalMile:
                        return "nmi";

                    default:
                        Debug.Assert(false);
                        return "";
                }
            }
        }

        /// <summary>
        /// Meters in linear
        /// </summary>
        /// <param name="unit">unit</param>
        /// <returns>angle degrees count</returns>
        public static double LinearMeters(LinearMeasUnit unit)
        {
            switch (unit)
            {
                case LinearMeasUnit.Meter:
                    return 1.0;

                case LinearMeasUnit.Kilometer:
                    return Constants.Kilo;

                case LinearMeasUnit.Foot:
                    return FootMeters;

                case LinearMeasUnit.NauticalMile:
                    return NauticalMileMeters;

                default:
                    Debug.Assert(false);
                    return 1.0;
            }
        }

        /// <summary>
        /// Get value with given unit.
        /// </summary>
        /// <param name="unit">measurements unit</param>
        /// <returns>value of linear</returns>
        public double Get(LinearMeasUnit unit)
        {
            return Value * (LinearMeters(Unit) / LinearMeters(unit));
        }

        /// <summary>
        /// Convert inner value.
        /// </summary>
        /// <param name="unit">measurements unit</param>
        public void Convert(LinearMeasUnit unit)
        {
            Value = Get(unit);
            Unit = unit;
        }
    }
}