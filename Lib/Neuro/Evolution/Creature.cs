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
        /// Score.
        /// </summary>
        public double Score = 0.0;

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
        /// <param name="creature"></param>
        /// <returns></returns>
        public int CompareByScore(Creature creature)
        {
            if (Score > creature.Score)
            {
                return 1;
            }
            else if (Score < creature.Score)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Scoring.
        /// </summary>
        /// <param name="batch">batch</param>
        public void Scoring(Batch batch)
        {
            Trainer trainer = new Trainer(1.0);

            Cortex.ResetNeuronsBiasesAndLinksWeights();
            double a = (double)trainer.TrainWhileRightAnswers(Cortex, batch, 0.95, 1000);

            Cortex.ResetNeuronsBiasesAndLinksWeights();
            double b = (double)trainer.TrainWhileRightAnswers(Cortex, batch, 0.95, 1000);

            Cortex.ResetNeuronsBiasesAndLinksWeights();
            double c = (double)trainer.TrainWhileRightAnswers(Cortex, batch, 0.95, 1000);

            Score = Lib.Maths.Maths.DropMinAndMax(a, b, c);
        }
    }
}
