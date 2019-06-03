using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Grid object.
    /// </summary>
    public class GObject
    {
        /// <summary>
        /// Grid.
        /// </summary>
        public StructuredGrid Grid;

        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Block.
        /// </summary>
        public Block B
        {
            get;
            set;
        }

        /// <summary>
        /// Check if grid object incident to block.
        /// </summary>
        /// <param name="b">block</param>
        /// <returns><c>true</c> - if incident, <c>false</c> - otherwise</returns>
        public bool IsIncident(Block b)
        {
            return B == b;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        public GObject(StructuredGrid g, int id, Block b)
        {
            Grid = g;
            Id = id;
            B = b;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="id">identifier</param>
        public GObject(StructuredGrid g, int id)
            : this(g, id, null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        public GObject(int id, Block b)
            : this(b.Grid, id, b)
        {
        }
    }
}
