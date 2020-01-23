using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NNBroth.Tests;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Trainer.
    /// </summary>
    public class Trainer
    {
        /// <summary>
        /// Learning rate.
        /// </summary>
        public static double DefaultLearningRate = 3.0;
    
        /// <summary>
        /// Train cortex with single batch.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batch">batch</param>
        public static void Train(Cortex cortex, Batch batch)
        {
            int c = 0;

            cortex.ZeroDWeightsAndDBiases();

            for (int i = 0; i < batch.TestCasesCount; i++)
            {
                double[] x = batch.GetInput(i);
                double[] y = batch.GetOutput(i);

                double[] a = cortex.SenseForward(x);

                //if (!Batch.IsAnswersEq(a, y))
                {
                    cortex.SenseBack(y);
                    cortex.StoreDWeightsAndDBiases();
                    c++;
                }
            }

            if (c == 0.0)
            {
                throw new Exception("Trainer.Train : division by zero while training.");
            }

            cortex.CorrectWeightsAndBiases(DefaultLearningRate / c);
        }

        /// <summary>
        /// Train cortex.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batches">batches</param>
        public static void Train(Cortex cortex, List<Batch> batches)
        {
            foreach (Batch batch in batches)
            {
                Train(cortex, batch);
            }
        }
    }
}
