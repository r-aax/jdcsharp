using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Numbers;

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
        /// Get cells count of the grid.
        /// </summary>
        /// <returns>cells count</returns>
        public int CellsCount()
        {
            int cc = 0;

            foreach (Block b in Blocks)
            {
                cc += b.CellsCount;
            }

            return cc;
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
        public override string ToString()
        {
            int bc = BlocksCount;
            int ic = IfacesCount;
            int bcc = BCondsCount;
            int sc = ScopesCount;
            int cc = CellsCount();

            return String.Format("[Grid: {0}b, {1}i, {2}bc, {3}s: {4}c]", bc, ic, bcc, sc, cc);
        }

        /// <summary>
        /// Set neighbour directions for all interfaces.
        /// </summary>
        public void SetIfacesNDirs()
        {
            for (int i = 0; i < IfacesCount; i += 2)
            {
                Iface i1 = Ifaces[i];
                Iface i2 = Ifaces[i + 1];

                // Reset all.
                i1.ResetNDirs();
                i2.ResetNDirs();

                // General directions.
                Dir d1 = i1.D;
                Dir d2 = i2.D;
                i1.SetNDirs(d1, i2, !d2);

                // Rest two directions.
                Dir od11, od12, od21, od22;
                d1.GetPairOfOrthogonalDirs(out od11, out od12);
                d2.GetPairOfOrthogonalDirs(out od21, out od22);

                // Check 4 quarters.
                for (int j = 0; j < 4; j++)
                {
                    if (Iface.IsMatch(i1, od11, od12, i2, od21, od22))
                    {
                        if (Iface.IsMatch(i1, !od11, od12, i2, !od21, od22))
                        {
                            i1.SetNDirs(od11, od12, i2, od21, od22);
                        }
                        else
                        {
                            i1.SetNDirs(od11, od12, i2, od22, od21);
                        }

                        break;
                    }

                    Dir.OrthogonalRot(ref od21, ref od22);
                }

                if (!(i1.IsNDirsCorrect()
                      && i2.IsNDirsCorrect()))
                {
                    throw new Exception("error while detecting interfaces pair orientation");
                }
            }
        }
    }
}
