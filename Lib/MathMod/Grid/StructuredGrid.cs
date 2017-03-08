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
        /// List of interfaces.
        /// </summary>
        public List<Iface> Ifaces
        {
            get;
            private set;
        }

        /// <summary>
        /// Interfaces count.
        /// </summary>
        public int IfacesCount
        {
            get
            {
                return Ifaces.Count;
            }
        }

        /// <summary>
        /// List of border condditions.
        /// </summary>
        public List<BCond> BConds
        {
            get;
            private set;
        }

        /// <summary>
        /// Count of border conditions.
        /// </summary>
        public int BCondsCount
        {
            get
            {
                return BConds.Count;
            }
        }

        /// <summary>
        /// Scopes list.
        /// </summary>
        public List<Scope> Scopes
        {
            get;
            private set;
        }

        /// <summary>
        /// Count of scopes.
        /// </summary>
        public int ScopesCount
        {
            get
            {
                return Scopes.Count;
            }
        }

        /// <summary>
        /// Clear all objects.
        /// </summary>
        public void Clear()
        {
            Blocks = new List<Block>();
            Ifaces = new List<Iface>();
            BConds = new List<BCond>();
            Scopes = new List<Scope>();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StructuredGrid()
        {
            Clear();
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
