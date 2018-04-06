using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.MathMod.Grid.DescartesObjects;

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

            if (!b.Canvas.Coord(d).Cutted(MinMargin).Contains(pos))
            {
                CutRejectedString = "margin violation";

                return null;
            }

            Block new_b = PureCut(b, d, pos);

            CutObjects(b, d, new_b);
            b.Grid.SetIfacesNDirs();

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
        /// Copy points from one array to another usings canvases.
        /// </summary>
        /// <param name="src_v">source array</param>
        /// <param name="src_c">source canvas</param>
        /// <param name="dst_v">destination array</param>
        /// <param name="dst_c">destination canvas</param>
        public static void CopyPointsBetween3DArrays(float[,,,] src_v, DescartesObject3D src_c,
                                                     float[,,,] dst_v, DescartesObject3D dst_c)
        {
            Debug.Assert(DescartesObject3D.IsSameSizes(src_c, dst_c));

            CopyPointsBetween3DArrays(src_v, src_c.I0, src_c.J0, src_c.K0,
                                      dst_v, dst_c.I0, dst_c.J0, dst_c.K0,
                                      src_c.INodes, src_c.JNodes, src_c.KNodes);
        }

        /// <summary>
        /// Pure cut the block without other objects correction.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="d">direction</param>
        /// <param name="pos">position</param>
        /// <returns>new bloc</returns>
        public static Block PureCut(Block b, Dir d, int pos)
        {
            StructuredGrid g = b.Grid;

            // Cut block's canvas in direction I in position pos.
            DescartesObject3D new_canvas = b.Canvas.Cut(d, pos);
            DescartesObject3D new_canvas2 = new_canvas.Copy;
            new_canvas.DecTo0();

            // We have to create new block for cells with higher coordinates.
            Block new_b = new Block(g, g.BlocksCount, new_canvas);
            new_b.Allocate();

            // Copy high part of the block to the new block.
            CopyPointsBetween3DArrays(b.C, new_canvas2, new_b.C, new_canvas);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            float[,,,] old_c = b.C;

            // Allocate memory for current block (again).
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(old_c, b.Canvas, b.C, b.Canvas);

            // New interface between these two blocks.
            int max_iface_id = g.MaxIfaceId();
            Iface ifc1 = new Iface(max_iface_id + 1, b, b.Canvas.Facet(d), new_b);
            Iface ifc2 = new Iface(max_iface_id + 1, new_b, b.Canvas.Facet(!d), b);
            IfacesPair pair = new IfacesPair(ifc1, ifc2);
            g.IfacesPairs.Add(pair);

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

            // We do not cut last interfaces pair, because it has just came out.
            int ic = g.IfacesPairsCount - 1;

            for (int i = 0; i < ic; i++)
            {
                Iface i1 = g.IfacesPairs[i].If;
                Iface i2 = g.IfacesPairs[i].Mirror;

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
            StructuredGrid g = b.Grid;

            // Cut simple (not linked border conditions).
            for (int i = 0; i < g.BCondsCount; i++)
            {
                BCond bcond = g.BConds[i];

                if (!bcond.IsLinked())
                {
                    if (bcond.B == b)
                    {
                        Cut(bcond, b, d, new_b);
                    }
                }
            }

            // Cut linked border conditions.
            for (int i = 0; i < b.Grid.BCondsLinksCount; i++)
            {
                BCond bc1 = g.BCondsLinks[i].BCond1;
                BCond bc2 = g.BCondsLinks[i].BCond2;

                if (b == bc1.B)
                {
                    Cut(bc1, bc2, b, d, new_b, g.BCondsLinks[i].Kind);
                }
                else if (b == bc2.B)
                {
                    Cut(bc2, bc1, b, d, new_b, g.BCondsLinks[i].Kind);
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

            int bsize = b.Canvas.Size(d);
            int i1lo = i1.Canvas.Lo(d);
            int i1hi = i1.Canvas.Hi(d);

            if (i1lo >= bsize)
            {
                // Interface touches only new block.
                //
                //                    i1lo           i1hi
                //                     |      i1      |
                //    *---------------*----------------*
                //    |       b       |      new_b     |
                //    0             bsize
                //
        
                i1.B = new_b;
                i1.Canvas.Dec(d, bsize);
                i2.NB = new_b;
            }
            else if (i1hi > bsize)
            {
                // Split interface.
                //
                //              i1lo                 i1hi
                //               |      i1            |
                //    *---------------*----------------*
                //    |       b       |      new_b     |
                //    0             bsize
                //

                // New interfaces pair id.
                StructuredGrid g = b.Grid;
                int id = g.MaxIfaceId() + 1;

                // Trunc interfaces pair.
                int tr = bsize - i1lo;
                DescartesObject3D canv1 = i1.Canvas.TruncZ(d, tr);
                DescartesObject3D canv2 = i2.Canvas.Trunc(i1.NDirs[d.N], tr);
                Iface ifc1 = new Iface(id, new_b, canv1, i1.NB);
                Iface ifc2 = new Iface(id, i2.B, canv2, new_b);                
                g.IfacesPairs.Add(new IfacesPair(ifc1, ifc2));
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

            int bsize = b.Canvas.Size(d);
            int bcondlo = bcond.Canvas.Lo(d);
            int bcondhi = bcond.Canvas.Hi(d);

            if (bcondlo >= bsize)
            {
                // Border condition touches only new block.
                bcond.B = new_b;
                bcond.Canvas.Dec(d, bsize);
            }
            else if (bcondhi > bsize)
            {
                // Have to cut.
                StructuredGrid g = b.Grid;
                int tr = bsize - bcondlo;
                DescartesObject3D canv = bcond.Canvas.TruncZ(d, tr);
                BCond new_bcond = new BCond(g.MaxBCondId() + 1, new_b, canv, bcond.Label);
                g.BConds.Add(new_bcond);
            }
        }

        /// <summary>
        /// Cut pair of border conditions.
        /// </summary>
        /// <param name="bc1">first border condition</param>
        /// <param name="bc2">second (adjacent) border condition</param>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        /// <param name="kind">link kind</param>
        public static void Cut(BCond bc1, BCond bc2, Block b, Dir d, Block new_b, string kind)
        {
            Debug.Assert(d.IsGen, "wrong direction");
            Debug.Assert(b == bc1.B, "trying to cut wrong border condition");

            int bsize = b.Canvas.Size(d);
            int bc1lo = bc1.Canvas.Lo(d);
            int bc1hi = bc1.Canvas.Hi(d);

            if (bc1lo >= bsize)
            {
                bc1.B = new_b;
                bc1.Canvas.Dec(d, bsize);
            }
            else if (bc1hi > bsize)
            {
                // New border conditions pair id.
                StructuredGrid g = b.Grid;
                int id = g.MaxBCondId() + 1;

                // Trunc interfaces pair.
                int tr = bsize - bc1lo;
                DescartesObject3D canv1 = bc1.Canvas.TruncZ(d, tr);
                DescartesObject3D canv2 = bc2.Canvas.Trunc(bc1.NDirs[d.N], tr);
                BCond bcond1 = new BCond(id, new_b, canv1, bc1.Label);
                BCond bcond2 = new BCond(id + 1, bc2.B, canv2, bc2.Label);
                g.BConds.Add(bcond1);
                g.BConds.Add(bcond2);
                BCondsLink bcl = new BCondsLink(bcond1, bcond2, bc1.NDirs);
                bcl.Kind = kind;
                g.BCondsLinks.Add(bcl);
                bcl.AddNameSuffixIfPERI();
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

            int bsize = b.Canvas.Size(d);
            int slo = s.Canvas.Lo(d);
            int shi = s.Canvas.Hi(d);

            if (slo >= bsize)
            {
                // This scope belongs to new block.
                s.B = new_b;
                s.Canvas.Dec(d, bsize);
            }
            else if (shi > bsize)
            {
                // Have to cut.
                StructuredGrid g = b.Grid;
                int tr = bsize - slo;
                DescartesObject3D canv = s.Canvas.TruncZ(d, tr);
                Scope scope = new Scope(g.MaxScopeId() + 1, new_b, canv, s.Label.Type, s.Label.Subtype, s.Label.Name);
                g.Scopes.Add(scope);
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
            int pos = (b == null) ? 0 : (b.Canvas.Size(d) / 2);

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
            Dir d = (b == null) ? Dir.I : b.Canvas.MaxSizeDir();

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

            if (is_full && (b.Canvas.CellsCount >= weight))
            {
                c = new Cut(b, null, 0);
                dev = b.Canvas.CellsCount - weight;
            }

            if (is_cut)
            {
                // for (int di = 0; di < Dir.GenCount; di++)
                {
                    Dir d = b.Canvas.MaxSizeDir(); //new Dir(di);

                    for (int i = MinMargin; i < b.Canvas.Nodes(d) - MinMargin; i++)
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

            if (is_full && (b.Canvas.CellsCount <= weight))
            {
                c = new Cut(b, null, 0);
                dev = weight - b.Canvas.CellsCount;
            }

            if (is_cut)
            {
                // for (int di = 0; di < Dir.GenCount; di++)
                {
                    Dir d = b.Canvas.MaxSizeDir(); // new Dir(di);

                    for (int i = MinMargin; i < b.Canvas.Nodes(d) - MinMargin; i++)
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
