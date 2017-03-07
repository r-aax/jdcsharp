using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Structured grid.
    /// </summary>
    public class StructuredGrid : Grid
    {
        /// <summary>
        /// List of blocks.
        /// </summary>
        public List<Block> Blocks
        {
            get;
            private set;
        }

        /// <summary>
        /// Count of blocks.
        /// </summary>
        public int BlocksCount
        {
            get
            {
                return Blocks.Count;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StructuredGrid()
        {
            Blocks = new List<Block>();
        }

        /// <summary>
        /// Count of nods.
        /// </summary>
        /// <returns>nodes count</returns>
        public int NodesCount()
        {
            int nc = 0;

            foreach (Block b in Blocks)
            {
                nc += b.NodesCount;
            }
                
            return nc;
        }

        /// <summary>
        /// Brief statistic of the grid.
        /// </summary>
        /// <returns>brief statistic</returns>
        public string BriefStatistic()
        {
            int bc = BlocksCount;
            int nc = NodesCount();

            return String.Format("[Grid: {0}b, {1}n]", bc, nc);
        }
    }
}
