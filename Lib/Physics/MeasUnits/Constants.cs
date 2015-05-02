// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Physics.MeasUnits
{
    /// <summary>
    /// Constants for physics modules.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Kilo.
        /// </summary>
        public const double Kilo = 1000.0;

        /// <summary>
        /// Minutes count in one hour.
        /// </summary>
        public const double HourMinutes = 60.0;

        /// <summary>
        /// Seconds count in one minute.
        /// </summary>
        public const double MinuteSeconds = 60.0;

        /// <summary>
        /// Seconds count in one hour.
        /// </summary>
        public const double HourSeconds = HourMinutes * MinuteSeconds;
    }
}
