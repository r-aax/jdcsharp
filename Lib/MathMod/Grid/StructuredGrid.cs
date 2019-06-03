using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Utils;
using Lib.MathMod.Grid.DescartesObjects;

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
        /// List of interfaces pairs.
        /// </summary>
        public List<IfacesPair> IfacesPairs
        {
            get;
            private set;
        }

        /// <summary>
        /// Count of interfaces pairs.
        /// </summary>
        public int IfacesPairsCount
        {
            get
            {
                return IfacesPairs.Count;
            }
        }

        /// <summary>
        /// Interfaces count.
        /// </summary>
        public int IfacesCount
        {
            get
            {
                return IfacesPairsCount * 2;
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
            IfacesPairs = new List<IfacesPair>();
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
                cc += b.Canvas.CellsCount;
            }

            return cc;
        }

        /// <summary>
        /// Get inner cells count.
        /// </summary>
        /// <returns>inner cells count</returns>
        public int InnerCellsCount()
        {
            int icc = 0;

            foreach (Block b in Blocks)
            {
                icc += b.InnerCellsCount();
            }

            return icc;
        }

        /// <summary>
        /// Border cells count.
        /// </summary>
        /// <returns>count of border cells</returns>
        public int BorderCellsCount()
        {
            int bcc = 0;

            foreach (Block b in Blocks)
            {
                bcc += b.BorderCellsCount();
            }

            return bcc;
        }

        /// <summary>
        /// Iface cells count multiple.
        /// </summary>
        /// <param name="is_only_cross_partition">is only cross partition flag</param>
        /// <returns>cells count</returns>
        public int IfaceCellsCountMultiple(bool is_only_cross_partition)
        {
            int iccm = 0;

            foreach (Block b in Blocks)
            {
                iccm += b.IfaceCellsCountMultiple(is_only_cross_partition);
            }

            return iccm;
        }

        /// <summary>
        /// Iface multiple cells count (only cross partition).
        /// </summary>
        /// <returns>cells count</returns>
        public int IfaceCellsCountCrossMultiple()
        {
            return IfaceCellsCountMultiple(true);
        }

        /// <summary>
        /// Iface multiple cells count.
        /// </summary>
        /// <returns>cells count</returns>
        public int IfaceCellsCountMultiple()
        {
            return IfaceCellsCountMultiple(false);
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
                nc += b.Canvas.NodesCount;
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
        /// <returns><c>true</c> - in success case, <c>false</c> - otherwise</returns>
        public bool SetIfacesNDirs()
        {
            bool is_succ = true;

            try
            {
                for (int i = 0; i < IfacesPairsCount; i++)
                {
                    Iface i1 = IfacesPairs[i].If;
                    Iface i2 = IfacesPairs[i].Mirror;

                    if ((i1 == null) || (i2 == null))
                    {
                        throw new Exception("null interface in interfaces pair");
                    }

                    // Reset all.
                    i1.ResetNDirs();
                    i2.ResetNDirs();

                    Dirs3 dirs = i1.DirectionsMatchFixed(i2, false, 1e-6);

                    if (dirs == null)
                    {
                        throw new Exception("error while detecting interfaces pair orientation");
                    }
                    else
                    {
                        i1.SetNDirs(i2, dirs);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ExeDebug.ReportError(e.Message));
                is_succ = false;
            }

            return is_succ;
        }

        /// <summary>
        /// Minimum block identifier.
        /// </summary>
        /// <returns>min id</returns>
        public int MinBlockId()
        {
            int min_id = -1;

            foreach (Block b in Blocks)
            {
                if (min_id == -1)
                {
                    min_id = b.Id;
                }
                else
                {
                    min_id = Math.Min(min_id, b.Id);
                }
            }

            return min_id;
        }

        /// <summary>
        /// Get maximum interface identifier.
        /// </summary>
        /// <returns>maximum interface identifier or -1 if there is no interfaces at all</returns>
        public int MaxIfaceId()
        {
            int max_id = -1;

            foreach (IfacesPair pair in IfacesPairs)
            {
                max_id = Math.Max(max_id, pair.MaxIfaceId);
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
                    if (b.Canvas.CellsCount > max.Canvas.CellsCount)
                    {
                        max = b;
                    }
                }
                else
                {
                    if (b.Canvas.Size(d) > max.Canvas.Size(d))
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
                w[b.PartitionNumber] += (double)b.Canvas.CellsCount;
            }

            return w;
        }

        /// <summary>
        /// Find block by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>block</returns>
        public Block FindBlock(int id)
        {
            return Blocks.Find(b => (b.Id == id));
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

        /// <summary>
        /// Init BConds links.
        /// </summary>
        /// <param name="eps_par_move">epsilon for parallel move</param>
        /// <param name="eps_rot">epsilon for rotation</param>
        public void InitBCondsLinks(double eps_par_move, double eps_rot)
        {
            // Check each pair of border conditions.
            for (int i = 0; i < BCondsCount; i++)
            {
                BCond bci = BConds[i];

                if (!bci.IsPeri)
                {
                    continue;
                }

                if (bci.IsLinked())
                {
                    continue;
                }

                for (int j = i + 1; j < BCondsCount; j++)
                {
                    BCond bcj = BConds[j];

                    if (!bcj.IsPeri)
                    {
                        continue;
                    }

                    if (bcj.IsLinked())
                    {
                        continue;
                    }

                    Dirs3 dirs = null;
                    string kind = "";

                    // Check pair of not linked PERI border conditions.
                    if (bci.Label.Name.StartsWith("PERI_RX")
                        && bcj.Label.Name.StartsWith("PERI_RX"))
                    {
                        // Two rotation RX border conditions.
                        dirs = bci.DirectionsMatchRotX(bcj, true, eps_rot);
                        kind = "Rot X";
                    }
                    else if ((bci.Label.Name == "PERI_C")
                             && (bcj.Label.Name == "PERI_C"))
                    {
                        // PERI_C conditions must belong to one block
                        // and placed opposite to each other on this block.
                        if (!bci.IsOppositeOnOneBlock(bcj))
                        {
                            continue;
                        }

                        // Main pair of parallel move PERI conditions.
                        dirs = bci.DirectionsMatchParallelMove(bcj, true, eps_par_move);
                        kind = "Parallel mv";
                    }
                    else if (bci.Label.Name.StartsWith("PERI_C-")
                             && bcj.Label.Name.StartsWith("PERI_C-"))
                    {
                        // Other pairs of parallel move PERI conditions.
                        dirs = bci.DirectionsMatchParallelMove(bcj, true, eps_par_move);
                        kind = "Parallel mv";
                    }
                    else
                    {
                        // No variants any more.
                        ;
                    }

                    // Coordination match is detected.
                    if (dirs != null)
                    {
                        BCondsLink bcl = new BCondsLink(bci, bcj, dirs.I, dirs.J, dirs.K);
                        bcl.Kind = kind;
                        BCondsLinks.Add(bcl);
                    }
                }

                // Check border condition link.
                Debug.Assert(bci.IsLinked(),
                             ExeDebug.ReportError("no mirror for PERI border condition is found : " + bci.ToString()));
            }
        }
    }
}
