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
        /// List of blocks without partitions.
        /// </summary>
        public List<Block> NoPartitionBlocks
        {
            get
            {
                return Blocks.FindAll(b => b.IsNoPartition);
            }
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
        /// List of border conditions links.
        /// </summary>
        public List<BCondsLink> BCondsLinks
        {
            get;
            private set;
        }

        /// <summary>
        /// Count of border conditions links.
        /// </summary>
        public int BCondsLinksCount
        {
            get
            {
                return BCondsLinks.Count;
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
            BCondsLinks = new List<BCondsLink>();
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
        /// Grid description.
        /// </summary>
        /// <returns>description</returns>
        public string[] Description()
        {
            int bc = BlocksCount;
            int ic = IfacesCount;
            int bcc = BCondsCount;
            int sc = ScopesCount;
            int cc = CellsCount();

            string[] str = new string[2];

            str[0] = String.Format("Grid: {0} blocks, {1} ifaces, {2} bconds, {3} scopes", bc, ic, bcc, sc);
            str[1] = String.Format("Grid: {0} cells", cc);

            return str;
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

        /// <summary>
        /// Get maximum interface identifier.
        /// </summary>
        /// <returns>maximum interface identifier or -1 if there is no interfaces at all</returns>
        public int MaxIfaceId()
        {
            int max_id = -1;

            foreach (Iface ifc in Ifaces)
            {
                max_id = Math.Max(max_id, ifc.Id);
            }

            return max_id;
        }

        /// <summary>
        /// Get maximum border condition identifier.
        /// </summary>
        /// <returns>maximum border condition identifier or -1 if there is no border conditions at all</returns>
        public int MaxBCondId()
        {
            int max_id = -1;

            foreach (BCond bcond in BConds)
            {
                max_id = Math.Max(max_id, bcond.Id);
            }

            return max_id;
        }

        /// <summary>
        /// Get maximum scope identifier.
        /// </summary>
        /// <returns>maximum scope identifier or -1 if there is no scopes at all</returns>
        public int MaxScopeId()
        {
            int max_id = -1;

            foreach (Scope s in Scopes)
            {
                max_id = Math.Max(max_id, s.Id);
            }

            return max_id;
        }

        /// <summary>
        /// Get maximum block (according to given direction size).
        /// </summary>
        /// <param name="d">direction</param>
        /// <returns>maximum block</returns>
        public Block MaxBlock(Dir d)
        {
            if (BlocksCount == 0)
            {
                return null;
            }

            Block max = Blocks[0];

            for (int i = 1; i < BlocksCount; i++)
            {
                Block b = Blocks[i];

                if (d == null)
                {
                    if (b.CellsCount > max.CellsCount)
                    {
                        max = b;
                    }
                }
                else
                {
                    if (b.Size(d) > max.Size(d))
                    {
                        max = b;
                    }
                }
            }

            return max;
        }

        /// <summary>
        /// Get maximum block (according to cells count).
        /// </summary>
        /// <returns>maximum block</returns>
        public Block MaxBlock()
        {
            return MaxBlock(null);
        }

        /// <summary>
        /// Set partitions numbers for blocks.
        /// </summary>
        /// <param name="pn">partitions numbers array</param>
        public void SetBlocksPartitionsNumbers(int[] pn)
        {
            if (pn.Length != BlocksCount)
            {
                throw new Exception("wrong blocks partitions numbers");
            }

            for (int i = 0; i < BlocksCount; i++)
            {
                Blocks[i].PartitionNumber = pn[i];
            }
        }

        /// <summary>
        /// Get maximum partition id.
        /// </summary>
        /// <returns>max partition id</returns>
        public int GetMaxPartitionId()
        {
            int m = 0;

            foreach (Block b in Blocks)
            {
                m = Math.Max(m, b.PartitionNumber);
            }

            return m;
        }

        /// <summary>
        /// Get partitions weights.
        /// </summary>
        /// <param name="partitions_count">count of partitions</param>
        /// <returns>array of partitions weights</returns>
        public double[] PartitionsWeights(int partitions_count)
        {
            if (partitions_count < GetMaxPartitionId() + 1)
            {
                throw new Exception("not enough partitions");
            }

            double[] w = new double[partitions_count];

            for (int i = 0; i < w.Length; i++)
            {
                w[i] = 0.0;
            }

            for (int i = 0; i < BlocksCount; i++)
            {
                Block b = Blocks[i];
                w[b.PartitionNumber] += (double)b.CellsCount;
            }

            return w;
        }

        /// <summary>
        /// Find border condition by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>border condition</returns>
        public BCond FindBCond(int id)
        {
            return BConds.Find(bc => (bc.Id == id));
        }

        /// <summary>
        /// Find border condition link.
        /// </summary>
        /// <param name="bcond">border condition</param>
        /// <returns>border condition link</returns>
        public BCondsLink FindBCondLink(BCond bcond)
        {
            return BCondsLinks.Find(bcl => ((bcl.BCond1 == bcond) || (bcl.BCond2 == bcond)));
        }
    }
}
