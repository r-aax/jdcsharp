using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.MathMod.Grid.Cut
{
    /// <summary>
    /// Class that performs grid cutting.
    /// </summary>
    public static class GridCutter
    {
        /// <summary>
        /// Minimum margin from block edges for cutting.
        /// </summary>
        public static int MinMargin = 1;

        /// <summary>
        /// Description of cut rejecting.
        /// </summary>
        public static string CutRejectedString = null;

        /// <summary>
        /// Cur block int given direction on given position.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="d">direction</param>
        /// <param name="pos">position</param>
        /// <returns>new block</returns>
        public static Block Cut(Block b, Dir d, int pos)
        {
            CutRejectedString = null;

            if (b == null)
            {
                CutRejectedString = "null block";

                return null;
            }

            Block new_b = null;

            if (d.IsI)
            {
                new_b = CutI(b, pos);
            }
            else if (d.IsJ)
            {
                new_b = CutJ(b, pos);
            }
            else if (d.IsK)
            {
                new_b = CutK(b, pos);
            }
            else
            {
                throw new Exception("undefined cut direction");
            }

            if (new_b != null)
            {
                CutObjects(b, d, new_b);
                b.Grid.SetIfacesNDirs();
                CutRejectedString = null;
            }

            return new_b;
        }

        /// <summary>
        /// Copy Points from one 3D array to another.
        /// </summary>
        /// <param name="src">source array</param>
        /// <param name="srci">source array I start</param>
        /// <param name="srcj">source array J start</param>
        /// <param name="srck">source array K start</param>
        /// <param name="dst">destination array</param>
        /// <param name="dsti">destination array I start</param>
        /// <param name="dstj">destination array J start</param>
        /// <param name="dstk">destination array K start</param>
        /// <param name="leni">length in I direction</param>
        /// <param name="lenj">length in J direction</param>
        /// <param name="lenk">length in K direction</param>
        public static void CopyPointsBetween3DArrays(float[,,,] src,
                                                     int srci, int srcj, int srck,
                                                     float[,,,] dst,
                                                     int dsti, int dstj, int dstk,
                                                     int leni, int lenj, int lenk)
        {
            for (int i = 0; i < leni; i++)
            {
                for (int j = 0; j < lenj; j++)
                {
                    for (int k = 0; k < lenk; k++)
                    {
                        dst[dsti + i, dstj + j, dstk + k, 0] = src[srci + i, srcj + j, srck + k, 0];
                        dst[dsti + i, dstj + j, dstk + k, 1] = src[srci + i, srcj + j, srck + k, 1];
                        dst[dsti + i, dstj + j, dstk + k, 2] = src[srci + i, srcj + j, srck + k, 2];
                    }
                }
            }            
        }

        /// <summary>
        /// Cut block in I direction.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="pos">position</param>
        /// <returns>new block</returns>
        private static Block CutI(Block b, int pos)
        {
            if (!((new ISegm(MinMargin, b.INodes - 1 - MinMargin)).Contains(pos)))
            {
                CutRejectedString = "margin violation";

                return null;
            }

            StructuredGrid g = b.Grid;

            // We have to create new block for cells with higher coordinates.
            Block new_b = new Block(g, g.BlocksCount, b.ISize - pos, b.JSize, b.KSize);
            new_b.Allocate();

            // Nodes: 0        ...       pos       ...    INodes - 1
            // Cells: *---------*---------*---------*---------*
            //             0      pos - 1     pos    ICells - 1
            //

            // Copy high part of the block to the new block.
            CopyPointsBetween3DArrays(b.C, pos, 0, 0, new_b.C, 0, 0, 0, b.INodes - pos, b.JNodes, b.KNodes);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            float[,,,] old_c = b.C;

            // Allocate memory for current block (again).
            b.Reshape(pos, b.J.H, b.K.H);
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(old_c, 0, 0, 0, b.C, 0, 0, 0, b.INodes, b.JNodes, b.KNodes);

            // New interface between these two blocks.
            int max_iface_id = g.MaxIfaceId();
            Iface ifc1 = new Iface(max_iface_id + 1, b, new ISegm(b.I1, b.I1), b.J, b.K, new_b);
            Iface ifc2 = new Iface(max_iface_id + 1, new_b, new ISegm(b.I0, b.I0), b.J, b.K, b);
            g.Ifaces.Add(ifc1);
            g.Ifaces.Add(ifc2);

            return new_b;
        }

        /// <summary>
        /// Cut block in J direction.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="pos">position</param>
        /// <returns>new block</returns>
        private static Block CutJ(Block b, int pos)
        {
            if (!((new ISegm(MinMargin, b.JNodes - 1 - MinMargin)).Contains(pos)))
            {
                CutRejectedString = "margin violation";

                return null;
            }

            StructuredGrid g = b.Grid;

            // We have to create new block for cells with higher coordinates.
            Block new_b = new Block(g, g.BlocksCount, b.ISize, b.JSize - pos, b.KSize);
            new_b.Allocate();

            // Nodes: 0        ...       pos       ...    JNodes - 1
            // Cells: *---------*---------*---------*---------*
            //             0      pos - 1     pos    JCells - 1
            //

            // Copy high part of the block to the new block.
            CopyPointsBetween3DArrays(b.C, 0, pos, 0, new_b.C, 0, 0, 0, b.INodes, b.JNodes - pos, b.KNodes);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            float[,,,] old_c = b.C;

            // Allocate memory for current block (again).
            b.Reshape(b.I.H, pos, b.K.H);
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(old_c, 0, 0, 0, b.C, 0, 0, 0, b.INodes, b.JNodes, b.KNodes);

            // New interface between these two blocks.
            int max_iface_id = g.MaxIfaceId();
            Iface ifc1 = new Iface(max_iface_id + 1, b, b.I, new ISegm(b.J1, b.J1), b.K, new_b);
            Iface ifc2 = new Iface(max_iface_id + 1, new_b, b.I, new ISegm(b.J0, b.J0), b.K, b);
            g.Ifaces.Add(ifc1);
            g.Ifaces.Add(ifc2);

            return new_b;
        }

        /// <summary>
        /// Cut block in K direction.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="pos">position</param>
        /// <returns>new block</returns>
        private static Block CutK(Block b, int pos)
        {
            if (!((new ISegm(MinMargin, b.KNodes - 1 - MinMargin)).Contains(pos)))
            {
                CutRejectedString = "margin violation";

                return null;
            }

            StructuredGrid g = b.Grid;

            // We have to create new block for cells with higher coordinates.
            Block new_b = new Block(g, g.BlocksCount, b.ISize, b.JSize, b.KSize - pos);
            new_b.Allocate();

            // Nodes: 0        ...       pos       ...    KNodes - 1
            // Cells: *---------*---------*---------*---------*
            //             0      pos - 1     pos    KCells - 1
            //

            // Copy high part of the block to the new block.
            CopyPointsBetween3DArrays(b.C, 0, 0, pos, new_b.C, 0, 0, 0, b.INodes, b.JNodes, b.KNodes - pos);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            float[,,,] old_c = b.C;

            // Allocate memory for current block (again).
            b.Reshape(b.I.H, b.J.H, pos);
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(old_c, 0, 0, 0, b.C, 0, 0, 0, b.INodes, b.JNodes, b.KNodes);

            // New interface between these two blocks.
            int max_iface_id = g.MaxIfaceId();
            Iface ifc1 = new Iface(max_iface_id + 1, b, b.I, b.J, new ISegm(b.K1, b.K1), new_b);
            Iface ifc2 = new Iface(max_iface_id + 1, new_b, b.I, b.J, new ISegm(b.K0, b.K0), b);
            g.Ifaces.Add(ifc1);
            g.Ifaces.Add(ifc2);

            return new_b;
        }

        /// <summary>
        /// Cut other objects (interfaces, borders conditions, scopes).
        /// </summary>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void CutObjects(Block b, Dir d, Block new_b)
        {
            CutIfaces(b, d, new_b);
            CutBConds(b, d, new_b);
            CutScopes(b, d, new_b);
        }

        /// <summary>
        /// Cut interfaces.
        /// </summary>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void CutIfaces(Block b, Dir d, Block new_b)
        {
            StructuredGrid g = b.Grid;

            // We do not cut last two interfaces, because they have just came out.
            int ic = g.IfacesCount - 2;

            for (int i = 0; i < ic; i += 2)
            {
                Iface i1 = g.Ifaces[i];
                Iface i2 = g.Ifaces[i + 1];

                if (b == i1.B)
                {
                    Cut(i1, i2, b, d, new_b);
                }
                else if (b == i2.B)
                {
                    Cut(i2, i1, b, d, new_b);
                }
            }
        }

        /// <summary>
        /// Cut border conditions.
        /// </summary>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void CutBConds(Block b, Dir d, Block new_b)
        {
            for (int i = 0; i < b.Grid.BCondsCount; i++)
            {
                BCond bcond = b.Grid.BConds[i];

                if (bcond.B == b)
                {
                    Cut(bcond, b, d, new_b);
                }
            }
        }

        /// <summary>
        /// Cut scopes.
        /// </summary>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void CutScopes(Block b, Dir d, Block new_b)
        {
            for (int i = 0; i < b.Grid.ScopesCount; i++)
            {
                Scope scope = b.Grid.Scopes[i];

                if (scope.B == b)
                {
                    Cut(scope, b, d, new_b);
                }
            }
        }

        /// <summary>
        /// Trunc interface in given direction.
        /// </summary>
        /// <param name="ifc">interface</param>
        /// <param name="d">direction</param>
        /// <param name="width">band width</param>
        /// <returns>new interface</returns>
        public static Iface Trunc(Iface ifc, Dir d, int width)
        {
            Debug.Assert(d.IsCorrect, "unknown direction");

            int g = d.Gen.N;

            // Check iface is big enough.
            Debug.Assert(ifc.Coords[g].Length > width, "iface is not big enough to trunc");

            Iface ifcc = ifc.Clone() as Iface;

            if (d.IsPos)
            {
                // Cut in positive direction.
                //
                //     0                           size
                //     *----------*----------------->
                //     |  width   |
                //
                //  0        width   0            size - width
                //  *---------->     *----------------->
                //  cutted iface     new iface

                int v = ifc.Coords[g][0] + width;
                ifcc.Coords[g] = new ISegm(v, ifc.Coords[g][1]);
                ifc.Coords[g][1] = v;
            }
            else
            {
                // Cut in negative direction.
                //
                //     0                           size
                //     *-----------------*---------->
                //                       |  width   |
                //
                //  0        size - width   0        width
                //  *----------------->     *---------->
                //  new iface               cutted iface

                int v = ifc.Coords[g][1] - width;
                ifcc.Coords[g] = new ISegm(ifc.Coords[g][0], v);
                ifc.Coords[g][0] = v;
            }

            return ifcc;
        }

        /// <summary>
        /// Cut pair of interfaces.
        /// </summary>
        /// <param name="i1">first interface</param>
        /// <param name="i2">second (adjacent) interface</param>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void Cut(Iface i1, Iface i2, Block b, Dir d, Block new_b)
        {
            Debug.Assert(d.IsGen, "wrong direction");
            Debug.Assert(b == i1.B, "trying to cut wrong interface");
            Debug.Assert(b != i1.NB, "trying to cut self-intersected block");

            ISegm c = i1.Coords[d.N];
            ISegm bc = b.Coords[d.N];

            if (c[0] >= bc[1])
            {
                // Interface touches only new block.
                i1.B = new_b;
                c.Dec(bc[1]);
                i2.NB = new_b;
            }
            else if (c[1] > bc[1])
            {
                // Interface splits.
                StructuredGrid g = b.Grid;
                int id = g.MaxIfaceId() + 1;
                Iface ifc = i1.Clone(id, new_b);
                ifc.Coords[d.N] = new ISegm(0, c[1] - bc[1]);
                g.Ifaces.Add(ifc);
                c[1] = bc[1];

                // Adjacent interface truncated on bc[1] - c[0].
                //
                //  bc[0] = 0           bc[1]  <- block
                //     *-----*------------*
                //           |   trunc    |  <- trunc - trunc size
                //           *------------*----------------*  <- interface
                //          c[0]             ^            c[1]
                //                           |
                //               mirror      v
                //           *------------*----------------*
                ifc = Trunc(i2, i1.NDirs[d.N], bc[1] - c[0]);
                ifc.NB = new_b;
                ifc.Id = id;
                g.Ifaces.Add(ifc);
            }
        }

        /// <summary>
        /// Cut border condition.
        /// </summary>
        /// <param name="bc">border condition</param>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void Cut(BCond bcond, Block b, Dir d, Block new_b)
        {
            Debug.Assert(d.IsGen, "wrong direction");
            Debug.Assert(bcond.B == b, "trying to cut wrong border condition");

            // Get coordinates of border condition and block.
            ISegm c = bcond.Coords[d.N];
            ISegm bc = b.Coords[d.N];

            if (c[0] >= bc[1])
            {
                // Border condition touches only new block.
                bcond.B = new_b;
                c.Dec(bc[1]);
            }
            else if (c[1] > bc[1])
            {
                // Have to cut.
                StructuredGrid g = b.Grid;
                BCond new_bcond = BCondCutter.Cut(bcond, d, bc[1]);
                new_bcond.B = new_b;
                new_bcond.Coords[d.N].DecTo0();
                g.BConds.Add(new_bcond);

                // Now we have to correct bconds if just cutted border condition
                // is in some border condition link.

                BCondsLink bcl = g.FindBCondLink(bcond);

                if (bcl != null)
                {
                    bcl.TruncLinkedBCond(bcond, d, bcond.Size(d));
                }
            }
        }

        /// <summary>
        /// Cut scope.
        /// </summary>
        /// <param name="s">scope</param>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void Cut(Scope s, Block b, Dir d, Block new_b)
        {
            Debug.Assert(d.IsGen, "wrong direction");
            Debug.Assert(s.B == b, "trying to cut wrong scope");

            // Get coordinates of scope and block.
            ISegm c = s.Coords[d.N];
            ISegm bc = b.Coords[d.N];

            if (c[0] >= bc[1])
            {
                // This scope belongs to new block.
                s.B = new_b;
                c.Dec(bc[1]);
            }
            else if (c[1] > bc[1])
            {
                // Have to cut.
                StructuredGrid g = b.Grid;
                Scope scope = s.Clone(g.MaxScopeId() + 1, new_b);
                scope.Coords[d.N] = new ISegm(0, c[1] - bc[1]);
                g.Scopes.Add(scope);
                c[1] = bc[1];
            }
        }

        /// <summary>
        /// Half block.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="d">direction</param>
        /// <returns>new block</returns>
        public static Block CutHalf(Block b, Dir d)
        {
            int pos = (b == null) ? 0 : (b.Size(d) / 2);

            return Cut(b, d, pos);
        }

        /// <summary>
        /// Half block in max size direction.
        /// </summary>
        /// <param name="b">block</param>
        /// <returns>new block</returns>
        public static Block CutHalf(Block b)
        {
            // If b is null block we call CutHalf,
            // because we want to get full diagnostics.
            Dir d = (b == null) ? Dir.I : b.MaxSizeDir();

            return CutHalf(b, d);
        }

        /// <summary>
        /// Cut half max block in given direction.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="d">direction</param>
        /// <returns></returns>
        public static Block CutHalfMaxBlock(StructuredGrid g, Dir d)
        {
            if (g == null)
            {
                CutRejectedString = "no grid";

                return null;
            }

            if (g.BlocksCount == 0)
            {
                CutRejectedString = "no blocks";

                return null;
            }

            return CutHalf(g.MaxBlock(), d);
        }

        /// <summary>
        /// Cut half max block in maximum direction.
        /// </summary>
        /// <param name="g">grid</param>
        /// <returns>new block</returns>
        public static Block CutHalfMaxBlock(StructuredGrid g)
        {
            if (g == null)
            {
                CutRejectedString = "no grid";

                return null;
            }

            if (g.BlocksCount == 0)
            {
                CutRejectedString = "no blocks";

                return null;
            }

            return CutHalf(g.MaxBlock());
        }

        /// <summary>
        /// Find nearest cut for given weight with overflow.
        /// Weight may be not integer.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="weight">weight</param>
        /// <param name="min_cut">min cut cells count</param>
        /// <param name="is_full">flag if full block may to be used</param>
        /// <param name="is_cut">flag if only not null cuts are considered</param>
        /// <param name="dev">deviation</param>
        /// <returns>cut</returns>
        /// <remarks>we consider only blocks without partition  number</remarks>
        public static Cut NearestCutOverflow(Block b, double weight, double min_cut,
                                             bool is_full, bool is_cut, out double dev)
        {
            Debug.Assert(b.IsNoPartition, "block already has a partition number");

            Cut c = null;
            dev = 0.0;

            if (is_full && (b.CellsCount >= weight))
            {
                c = new Cut(b, null, 0);
                dev = b.CellsCount - weight;
            }

            if (is_cut)
            {
                for (int di = 0; di < Dir.GenCount; di++)
                {
                    Dir d = new Dir(di);

                    for (int i = MinMargin; i < b.Nodes(d) - MinMargin; i++)
                    {
                        Cut cur_c = new Cut(b, d, i);
                        int cells_count = cur_c.OldBlockCellsCount;

                        if (cells_count >= weight)
                        {
                            double cur_dev = cells_count - weight;

                            if (cur_c.MinPartCellsCount < min_cut)
                            {
                                continue;
                            }

                            if ((c == null) || (cur_dev < dev))
                            {
                                c = cur_c;
                                dev = cur_dev;
                            }
                        }
                    }
                }
            }

            return c;
        }

        /// <summary>
        /// Find nearest cut for given weight with underflow.
        /// Weight may be not integer.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="weight">weight</param>
        /// <param name="min_cut">min cut cells count</param>
        /// <param name="is_full">flag if full block may to be used</param>
        /// <param name="is_cut">flag if not null cuts are considered</param>
        /// <param name="dev">deviation</param>
        /// <returns>cut</returns>
        /// <remarks>we consider only blocks without partition  number</remarks>
        public static Cut NearestCutUnderflow(Block b, double weight, double min_cut,
                                              bool is_full, bool is_cut, out double dev)
        {
            Debug.Assert(b.IsNoPartition, "block already has a partition number");

            Cut c = null;
            dev = 0.0;

            if (is_full && (b.CellsCount <= weight))
            {
                c = new Cut(b, null, 0);
                dev = weight - b.CellsCount;
            }

            if (is_cut)
            {
                for (int di = 0; di < Dir.GenCount; di++)
                {
                    Dir d = new Dir(di);

                    for (int i = MinMargin; i < b.Nodes(d) - MinMargin; i++)
                    {
                        Cut cur_c = new Cut(b, d, i);
                        int cells_count = cur_c.OldBlockCellsCount;

                        if (cells_count <= weight)
                        {
                            double cur_dev = weight - cells_count;

                            if (cur_c.MinPartCellsCount < min_cut)
                            {
                                continue;
                            }

                            if ((c == null) || (cur_dev < dev))
                            {
                                c = cur_c;
                                dev = cur_dev;
                            }
                        }
                    }
                }
            }

            return c;
        }

        /// <summary>
        /// Find nearest cut.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="weight">weight</param>
        /// <param name="min_cut">min cut cells count</param>
        /// <param name="is_full">check is full blocks are used</param>
        /// <param name="is_cut">check is cuts are used</param>
        /// <param name="is_overflow">overflow flag</param>
        /// <param name="dev">deviation</param>
        /// <returns>cut</returns>
        public static Cut NearestCut(Block b, double weight, double min_cut,
                                     bool is_full, bool is_cut, bool is_overflow, out double dev)
        {
            return is_overflow
                   ? NearestCutOverflow(b, weight, min_cut, is_full, is_cut, out dev)
                   : NearestCutUnderflow(b, weight, min_cut, is_full, is_cut, out dev);
        }

        /// <summary>
        /// Find nearest cut for grid with overflow.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="weight">weight</param>
        /// <<param name="min_cut">min cut cells count</param>
        /// <param name="is_full">check if full blocks are used</param>
        /// <param name="is_cut">check if cuts are used</param>
        /// <param name="is_overflow">overflow flag</param>
        /// <param name="dev">deviation</param>
        /// <returns>cut</returns>
        public static Cut NearestCut(StructuredGrid g, double weight, double min_cut,
                                     bool is_full, bool is_cut, bool is_overflow, out double dev)
        {
            Cut c = null;
            dev = 0.0;

            foreach (Block b in g.NoPartitionBlocks)
            {
                double cur_dev;
                Cut cur_c = NearestCut(b, weight, min_cut, is_full, is_cut, is_overflow, out cur_dev);

                if (cur_c != null)
                {
                    if ((c == null) || (cur_dev < dev))
                    {
                        c = cur_c;
                        dev = cur_dev;
                    }
                }
            }

            return c;
        }

        /// <summary>
        /// Find cut for grid and array of weights.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="weights">weighs</param>
        /// <param name="min_cut">min cut cells count</param>
        /// <param name="is_full">check if full blocks are used</param>
        /// <param name="is_cut">check if cuts are used</param>
        /// <param name="is_overflow">overflow flag</param>
        /// <param name="weight_num">number of weight</param>
        /// <param name="dev">deviation</param>
        /// <returns>cut</returns>
        public static Cut NearestCut(StructuredGrid g, double[] weights, double min_cut,
                                     bool is_full, bool is_cut, bool is_overflow,
                                     out int weight_num, out double dev)
        {
            Cut c = null;
            dev = 0.0;
            weight_num = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                double cur_dev;
                Cut cur_c = NearestCut(g, weights[i], min_cut, is_full, is_cut, is_overflow, out cur_dev);

                if (cur_c != null)
                {
                    if ((c == null) || (cur_dev < dev))
                    {
                        c = cur_c;
                        dev = cur_dev;
                        weight_num = i;
                    }
                }
            }

            return c;
        }
    }
}
