using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Neuro.Tests;

namespace Lib.Neuro.Net
{
    /// <summary>
    /// Trainer.
    /// </summary>
    public class Trainer
    {
        /// <summary>
        /// Learning rate.
        /// </summary>
        public double DefaultLearningRate = 3.0;
    
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="default_learning_rate">default rate of learning</param>
        public Trainer(double default_learning_rate = 3.0)
        {
            DefaultLearningRate = default_learning_rate;
        }

        /// <summary>
        /// Train cortex with single batch.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batch">batch</param>
        public void Train(Cortex cortex, Batch batch)
        {
            Debug.Assert(batch.TestCasesCount > 0);        

            cortex.ZeroDWeightsAndDBiases();

            for (int i = 0; i < batch.TestCasesCount; i++)
            {
                double[] x = batch.GetInput(i);
                double[] y = batch.GetOutput(i);
                double[] a = cortex.SenseForward(x);

                cortex.SenseBack(y);
                cortex.StoreDWeightsAndDBiases();
            }

            cortex.CorrectWeightsAndBiases(DefaultLearningRate / batch.TestCasesCount);
        }

        /// <summary>
        /// Train net several times.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batch">batch</param>
        /// <param name="count">count</param>
        public void Train(Cortex cortex, Batch batch, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Train(cortex, batch);
            }
        }

        /// <summary>
        /// Train cortex.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batches">batches</param>
        public void Train(Cortex cortex, List<Batch> batches)
        {
            foreach (Batch batch in batches)
            {
                Train(cortex, batch);
            }
        }

        /// <summary>
        /// Train cortex several times.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batches">batches</param>
        /// <param name="count">count</param>
        public void Train(Cortex cortex, List<Batch> batches, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Train(cortex, batches);
            }
        }

        /// <summary>
        /// Train while given rate of right answers is not acheved.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="batch">batch</param>
        /// <param name="rate">right answers rate</param>
        /// <param name="max_iters_count">max iters count</param>
        /// <returns></returns>
        public string TrainWhileRightAnswers(Cortex cortex, Batch batch,
                                             double rate, int max_iters_count)
        {
            double right_answers_part;
            int iters = 0;

            do
            {
                Train(cortex, batch);
                right_answers_part = batch.RightAnswersPart(cortex);
                iters++;
            }
            while ((right_answers_part < rate) && (iters < max_iters_count));

            return String.Format("iters = {0}, right = {1}", iters, right_answers_part);
        }
    }
}
