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

            for (int i = 0; i < Creatures.Count; i++)
            {
                if ((i == 0) && (Creatures[i].Age == 0))
                {
                    Creatures[i].Scoring(batch);
                }
                else if (i > 0)
                {
                    Creatures[i].Scoring(batch);
                }

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
                Creature c = Creatures[i];
                Console.Write("(Id_{0:D4}, Ag_{1:D2}, Lr_{2:D5}, Rh_{3:F5}, Tc_{4:F5}, Sz_{5:D2}/{6:D3})",
                              c.Cortex.Id,
                              c.Age,
                              c.LearnIterations, 
                              c.RightAnswersPart,
                              c.MinTotalCost,
                              c.Cortex.Neurons.Count,
                              c.Cortex.Links.Count);

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
        /// <returns>Deleted.</returns>
        public int DeleteTail(int count)
        {
            int del = 0;
            double mid_age = MidAge();
            double cri_age = 0.9 * mid_age;
            int origin_size = Size;

            // We must not touch first "count" creatures.
            for (int i = origin_size - 1; i > count; i--)
            {
                Creature c = Creatures[i];

                if (c.Age > cri_age)
                {
                    Creatures.Remove(c);
                    Console.WriteLine("Kill creature : id = {0}, age = {1}", c.Cortex.Id, c.Age);
                    del++;

                    if (del == count)
                    {
                        return del;
                    }
                }
            }

            return del;
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

        /// <summary>
        /// Mid age of creatures.
        /// </summary>
        /// <returns>mid age</returns>
        public double MidAge()
        {
            int sum_age = 0;

            foreach (Creature c in Creatures)
            {
                sum_age += c.Age;
            }

            return (double)sum_age / (double)Creatures.Count;
        }
    }
}
