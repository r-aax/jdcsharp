using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Neuro.Tests;
using Lib.Neuro.Net;
using Lib.Neuro.Evolution;

namespace NNBrothConsole
{
    /// <summary>
    /// Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generation size.
        /// </summary>
        static readonly int GenerationSize = 50;

        /// <summary>
        /// Count of generations.
        /// </summary>
        static readonly int GenerationsCount = 100000;

        /// <summary>
        /// Count of creatures to rebirth.
        /// </summary>
        static readonly int RebirthCreaturesCount = 2;

        /// <summary>
        /// Enter point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("NNBrothConsole begin.");

            // Prepare batch and generation.
            // We test on batch with fixed size.
            Batch batch = new Prime5();
            Cortex base_cortex = Cortex.CreateMultilayerCortex(new int[] { 5, 2 });
            Generation gen = new Generation();
            Creature base_creature = new Creature(null);
            base_creature.Cortex = base_cortex;
            gen.Populate(base_creature, GenerationSize);

            // Main cycle.
            for (int generation_number = 0; generation_number < GenerationsCount; generation_number++)
            {
                Console.WriteLine("--- Generation {0:D3} ---", generation_number);
                gen.Scoring(batch);
                gen.IncAge();
                gen.SortByScores();
                gen.PrintInfo();

                if (gen.Creatures[0].RightAnswersPart == 1.0)
                {

                    for (int i = 0; i < gen.Creatures.Count; i++)
                    {
                        Console.WriteLine("Kill creature : id = {0}, age = {1}, life years : [{2} - {3}]",
                                          gen.Creatures[i].Cortex.Id,
                                          gen.Creatures[i].Age,
                                          gen.Creatures[i].BirthYear,
                                          generation_number);
                    }

                    break;
                }

                if (generation_number < GenerationsCount - 1)
                {
                    int to_rebirth = gen.DeleteTail(RebirthCreaturesCount, generation_number);
                    gen.Rebirth(to_rebirth, generation_number + 1);
                    gen.MutateLast(to_rebirth);
                }
            }

            Console.WriteLine("NNBrothConsole end.");
            //Console.ReadKey();
        }
    }
}
