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
        static readonly int BatchSize = 10;

        /// <summary>
        /// Generation size.
        /// </summary>
        static readonly int GenerationSize = 10;

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
            gen.Populate(new Creature(base_cortex), GenerationSize);

            Console.WriteLine("NNBrothConsole end.");
            Console.ReadKey();
        }
    }
}
