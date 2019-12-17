using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Generation.
    /// </summary>
    class Generation
    {
        /// <summary>
        /// Number of generation.
        /// </summary>
        public int N
        {
            get;
            private set;
        }

        /// <summary>
        /// List of creatures.
        /// </summary>
        private List<Creature> Creatures;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Generation()
        {
            N = 0;
            Creatures = new List<Creature>();
        }

        /// <summary>
        /// Increment number.
        /// </summary>
        public void IncN()
        {
            N++;
        }

        /// <summary>
        /// Scoring for all creatures in generation.
        /// </summary>
        /// <param name="test">test</param>
        public void ProcessScoring(Tests.DoublesToInt test)
        {
            foreach (Creature creature in Creatures)
            {
                creature.ProcessScoring(test);
            }
        }

        /// <summary>
        /// Add default creature.
        /// </summary>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public void AddDefaultCreature(int sensor_dimension, int actuator_dimension)
        {
            Creature creature = new Creature(sensor_dimension, actuator_dimension);

            Creatures.Add(creature);
        }

        /// <summary>
        /// Add several default creatures.
        /// </summary>
        /// <param name="count">creatures count</param>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public void AddDefaultCreatures(int count, int sensor_dimension, int actuator_dimension)
        {
            for (int i = 0; i < count; i++)
            {
                AddDefaultCreature(sensor_dimension, actuator_dimension);
            }
        }

        /// <summary>
        /// Get string representation.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string n_string = String.Format("[G{0:D5}] ", N);

            if (Creatures.Count > 0)
            {
                n_string += String.Format("{0}", Creatures[0].Score);
            }

            for (int i = 1; i < Creatures.Count; i++)
            {
                n_string += String.Format(", {0}", Creatures[i].Score);
            }

            return n_string;
        }

        /// <summary>
        /// Selection.
        /// </summary>
        /// <param name="test">test</param>
        public void Selection(Tests.DoublesToInt test)
        {
            ProcessScoring(test);
            IncN();
        }
    }
}
