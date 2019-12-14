using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Sensor.
    /// </summary>
    class Sensor : Node
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
            for (int i = 0; i < OutLinks.Count; i++)
            {
                OutLinks[i].Signal = in_signals[i];
            }
        }
    }
}
