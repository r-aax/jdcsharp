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
        /// Size of batch.
        /// </summary>
        static readonly int BatchSize = 1;

        /// <summary>
        /// Generation size.
        /// </summary>
        static readonly int GenerationSize = 5;

        /// <summary>
        /// Count of generations.
        /// </summary>
        static readonly int GenerationsCount = 10;

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
            Batch batch = (new MNIST("../../../Lib/Neuro/Tests/mnist/train-images.idx3-ubyte",
                                     "../../../Lib/Neuro/Tests/mnist/train-labels.idx1-ubyte")).RandomMiniBatch(BatchSize);
            Cortex base_cortex = Cortex.CreateMultilayerCortex(new int[] { 784, 15, 10 });
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

                if (generation_number < GenerationsCount - 1)
                {
                    gen.DeleteTail(RebirthCreaturesCount);
                    gen.Rebirth(RebirthCreaturesCount);
                    gen.Mutate();
                }
            }

            Console.WriteLine("NNBrothConsole end.");
            Console.ReadKey();
        }
    }
}
