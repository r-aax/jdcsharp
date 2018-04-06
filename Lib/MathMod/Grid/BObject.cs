using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Block object - object belongs to block.
    /// </summary>
    public class BObject : GObject
    {
        /// <summary>
        /// Block.
        /// </summary>
        public Block B
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        public BObject(int id, Block b)
            : base(b.Grid, id)
        {
            B = b;
        }
    }
}
