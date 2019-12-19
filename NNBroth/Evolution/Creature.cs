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
    public class Creature : ICloneable
    {
        /// <summary>
        /// Cortex;
        /// </summary>
        public Cortex Cortex;

        /// <summary>
        /// Score.
        /// </summary>
        public double FineScore;

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Creature()
        {
            Cortex = null;
            FineScore = 0.0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public Creature(int sensor_dimension, int actuator_dimension) : this()
        {
            Cortex = Cortex.CreateMultilayerCortex(new int[] { sensor_dimension, actuator_dimension });
        }

        /// <summary>
        /// Sense signals.
        /// </summary>
        /// <param name="in_signals">signals</param>
        /// <returns>answer</returns>
        public double[] Sense(double[] in_signals)
        {
            return Cortex.SenseForward(in_signals);
        }

        /// <summary>
        /// Process scoring.
        /// </summary>
        /// <param name="test">test</param>
        public void ProcessScoring(Tests.Batch test)
        {
            FineScore = 0;

            for (int i = 0; i < test.TestCasesCount; i++)
            {
                FineScore += test.Cost(Cortex, i);
            }
        }

        /// <summary>
        /// Compare to other creature.
        /// </summary>
        /// <param name="creature">creature</param>
        /// <returns>compare result</returns>
        public int CompareToByFineScore(Creature creature)
        {
            if (FineScore > creature.FineScore)
            {
                return 1;            
            }
            else if (FineScore < creature.FineScore)
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
    }
}
