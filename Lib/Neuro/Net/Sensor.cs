using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lib.Neuro.Net
{
    /// <summary>
    /// Sensor.
    /// </summary>
    class Sensor : Pole
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Sensor() : base()
        {
        }

        /// <summary>
        /// Sense signals.
        /// </summary>
        /// <param name="in_signals">signals</param>
        public void Sense(double[] in_signals)
        {
            ScatterAccumulators(in_signals);
        }

        /// <summary>
        /// To string conversion.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "Sensor";
        }
    }
}
