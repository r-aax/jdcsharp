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
        /// Kill the weak.
        /// </summary>
        /// <param name="k"></param>
        private void KillTheWeak(double k)
        {
            Creatures.Sort((cr1, cr2) => cr1.CompareTo(cr2));

            int count_to_kill = (int)(Creatures.Count * k);

            Creatures.RemoveRange(Creatures.Count - count_to_kill, count_to_kill);
        }

        /// <summary>
        /// Restore population.
        /// </summary>
        /// <param name="origin_count"></param>
        private void RestorePopulation(int origin_count)
        {
            int creatures_to_create = origin_count - Creatures.Count;
            List<Creature> new_creatures = new List<Creature>();

            for (int i = 0; i < creatures_to_create; i++)
            {
                new_creatures.Add(Creatures[0].Clone() as Creature);
            }

            foreach (Creature creature in new_creatures)
            {
                creature.Mutate();
            }

            Creatures.AddRange(new_creatures);
        }

        /// <summary>
        /// Selection.
        /// </summary>
        /// <param name="test">test</param>
        public void Selection(Tests.DoublesToInt test)
        {
            int origin_count = Creatures.Count;
            ProcessScoring(test);
            KillTheWeak(0.5);
            RestorePopulation(origin_count);
            IncN();
        }
    }
}
