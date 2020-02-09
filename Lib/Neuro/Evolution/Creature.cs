using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Neuro.Net;
using Lib.Neuro.Tests;

namespace Lib.Neuro.Evolution
{
    /// <summary>
    /// Creature.
    /// </summary>
    public class Creature
    {
        /// <summary>
        /// Cortex.
        /// </summary>
        public Cortex Cortex = null;

        /// <summary>
        /// Age.
        /// </summary>
        public int Age = 0;

        /// <summary>
        /// Year of birth.
        /// </summary>
        public int BirthYear = 0;

        /// <summary>
        /// Part of right answers.
        /// </summary>
        public double RightAnswersPart = 0.0;

        /// <summary>
        /// Total cost.
        /// </summary>
        public double TotalCost;

        /// <summary>
        /// Minimum total cost.
        /// </summary>
        public double MinTotalCost = -1.0;

        /// <summary>
        /// Learn iterations.
        /// </summary>
        public int LearnIterations = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cortex">cortex</param>
        public Creature(Cortex cortex)
        {
            if (cortex != null)
            {
                Cortex = cortex.Clone() as Cortex;
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>cloned creature</returns>
        public object Clone()
        {
            Creature creature = new Creature(Cortex);

            return creature;
        }

        /// <summary>
        /// Compare by score.
        /// </summary>
        /// <param name="c">creature</param>
        /// <returns>compare result : 1, 0, -1</returns>
        public int CompareByScore(Creature c)
        {
            // 1 - this is greater than creature,
            // -1 - this is less than creature,
            // 0 - they are equal.

            int res = 0;

            if (RightAnswersPart > c.RightAnswersPart)
            {
                res = 1;
            }
            else if (RightAnswersPart < c.RightAnswersPart)
            {
                res = -1;
            }
            else if (MinTotalCost < c.MinTotalCost - 0.0001)
            {
                res = 1;
            }
            else if (MinTotalCost > c.MinTotalCost + 0.0001)
            {
                res = -1;
            }
            else if (Age < c.Age)
            {
                res = 1;
            }
            else if (Age > c.Age)
            {
                res = -1;
            }
            else if (Cortex.Links.Count < c.Cortex.Links.Count)
            {
                res = 1;
            }
            else if (Cortex.Links.Count > c.Cortex.Links.Count)
            {
                res = -1;
            }
            else if (Cortex.Neurons.Count < c.Cortex.Neurons.Count)
            {
                res = 1;
            }
            else if (Cortex.Neurons.Count > c.Cortex.Neurons.Count)
            {
                res = -1;
            }

            return -res;
        }

        /// <summary>
        /// Scoring.
        /// </summary>
        /// <param name="batch">batch</param>
        public void Scoring(Batch batch)
        {
            Trainer trainer = new Trainer(1.0);

            int iters_for_old = 50;
            int iters_for_new = 1000;

            int iters = 0;

            if (LearnIterations == 0)
            {
                Cortex.ResetNeuronsBiasesAndLinksWeights();
                iters = iters_for_new;
            }
            else
            {
                iters = iters_for_old;
            }

            trainer.Train(Cortex, batch, iters);
            LearnIterations += iters;

            TotalCost = batch.TotalCost(Cortex);

            if (MinTotalCost < 0.0)
            {
                MinTotalCost = TotalCost;
            }
            else
            {
                while (TotalCost > MinTotalCost)
                {
                    trainer.Train(Cortex, batch, iters_for_old);
                    LearnIterations += iters_for_old;
                    TotalCost = batch.TotalCost(Cortex);
                }

                MinTotalCost = TotalCost;
            }

            RightAnswersPart = batch.RightAnswersPart(Cortex);
        }
    }
}
