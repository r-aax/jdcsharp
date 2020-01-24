using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Neuro.Net;

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
        private Cortex Cortex = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cortex">cortex</param>
        public Creature(Cortex cortex)
        {
            Cortex = cortex.Clone() as Cortex;
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
    }
}
