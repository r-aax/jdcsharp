using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Creature.
    /// </summary>
    class Creature
    {
        /// <summary>
        /// Score.
        /// </summary>
        public double Score
        {
            get;
            private set;
        }

        /// <summary>
        /// Cortex;
        /// </summary>
        public Cortex Cortex
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public Creature(int sensor_dimension, int actuator_dimension)
        {
            Score = 0.0;
            Cortex = new Cortex(sensor_dimension, actuator_dimension);
        }

        /// <summary>
        /// Sense signals.
        /// </summary>
        /// <param name="in_signals">signals</param>
        /// <returns>answer</returns>
        public int Sense(double[] in_signals)
        {
            return Cortex.Sense(in_signals);
        }

        /// <summary>
        /// Process scoring.
        /// </summary>
        /// <param name="test">test</param>
        public void ProcessScoring(Tests.DoublesToInt test)
        {
            int right = 0;

            for (int i = 0; i < test.TestCasesCount; i++)
            {
                if (test.Test(Cortex, i))
                {
                    right++;
                }
            }

            Score = (double)right / (double)test.TestCasesCount;
        }
    }
}
