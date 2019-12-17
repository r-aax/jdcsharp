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
    class Creature : ICloneable
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
        /// Private constructor.
        /// </summary>
        private Creature()
        {
            Score = 0.0;
            Cortex = null;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public Creature(int sensor_dimension, int actuator_dimension) : this()
        {
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

        /// <summary>
        /// Compare to other creature.
        /// </summary>
        /// <param name="creature">creature</param>
        /// <returns>compare result</returns>
        public int CompareTo(Creature creature)
        {
            if (Score > creature.Score)
            {
                return 1;            
            }
            else if (Score < creature.Score)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            Creature creature = new Creature();

            creature.Cortex = Cortex.Clone() as Cortex;

            return creature;
        }

        /// <summary>
        /// Mutation.
        /// </summary>
        public void Mutate()
        {
            // Mutate.
        }
    }
}
