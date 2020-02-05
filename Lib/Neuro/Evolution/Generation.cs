using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Neuro.Tests;
using Lib.Neuro.Net;

namespace Lib.Neuro.Evolution
{
    /// <summary>
    /// Generation.
    /// </summary>
    public class Generation
    {
        /// <summary>
        /// List of creatures
        /// </summary>
        private List<Creature> Creatures = null;

        /// <summary>
        /// Generation size.
        /// </summary>
        public int Size
        {
            get
            {
                return Creatures.Count;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Generation()
        {
            Creatures = new List<Creature>();
        }

        /// <summary>
        /// Populate generation with clones of creature.
        /// </summary>
        /// <param name="creature">creature</param>
        /// <param name="count">creatures count</param>
        public void Populate(Creature creature, int count)
        {
            while (Size < count)
            {
                Creatures.Add(creature.Clone() as Creature);
            }
        }

        /// <summary>
        /// Scoring.
        /// </summary>
        /// <param name="batch">batch</param>
        public void Scoring(Batch batch)
        {
            Console.Write("Scoring [");

            foreach (Creature creature in Creatures)
            {
                creature.Scoring(batch);
                Console.Write("#");
            }

            Console.WriteLine("]");
        }

        /// <summary>
        /// Increment age.
        /// </summary>
        public void IncAge()
        {
            foreach (Creature creature in Creatures)
            {
                creature.Age++;
            }
        }

        /// <summary>
        /// Reset all cortexes characteristics : neuron biases and links weights.
        /// </summary>
        public void ResetNeuronsBiasesAndLinksWeights()
        {
            foreach (Creature creature in Creatures)
            {
                creature.Cortex.ResetNeuronsBiasesAndLinksWeights();
                creature.Score = 0.0;
            }
        }

        /// <summary>
        /// Sort by scores.
        /// </summary>
        public void SortByScores()
        {
            Creatures.Sort((cr1, cr2) => cr1.CompareByScore(cr2));
        }

        /// <summary>
        /// Print info.
        /// </summary>
        public void PrintInfo()
        {
            Console.Write("Info [");

            for (int i = 0; i < Creatures.Count; i++)
            {
                Console.Write("(I_{0:D4}, A_{1:D2}, S_{2}, N_{3:D2}, L_{4:D3})",
                              Creatures[i].Cortex.Id, Creatures[i].Age, Creatures[i].Score, 
                              Creatures[i].Cortex.Neurons.Count, Creatures[i].Cortex.Links.Count);

                if (i < Creatures.Count - 1)
                {
                    Console.Write("; ");
                }
            }

            Console.WriteLine("]");
        }

        /// <summary>
        /// Delete elements from tail.
        /// </summary>
        /// <param name="count">count to delete</param>
        public void DeleteTail(int count)
        {
            Creatures.RemoveRange(Size - count, count);
        }

        /// <summary>
        /// Rebirth the best creatures.
        /// </summary>
        /// <param name="count">count to rebirth</param>
        public void Rebirth(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Creatures.Add(Creatures[i].Clone() as Creature);
            }
        }

        /// <summary>
        /// Mutate.
        /// </summary>
        /// <param name="count">count of tail creatures</param>
        public void MutateLast(int count)
        {
            for (int i = Creatures.Count - count; i <= Creatures.Count - 1; i++)
            {
                Creatures[i].Cortex.Mutate();
            }
        }
    }
}
