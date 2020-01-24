using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
