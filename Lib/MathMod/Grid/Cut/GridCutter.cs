using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.MathMod.Grid.Cut
{
    /// <summary>
    /// Class that performs grid cutting.
    /// </summary>
    public class GridCutter
    {
        /// <summary>
        /// Cur block int given direction on given position.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="d">direction</param>
        /// <param name="pos">position</param>
        /// <returns>new block</returns>
        public static Block Cut(Block b, Dir d, int pos)
        {
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
        public static void CopyPointsBetween3DArrays(Point[,,] src,
                                                     int srci, int srcj, int srck,
                                                     Point[,,] dst,
                                                     int dsti, int dstj, int dstk,
                                                     int leni, int lenj, int lenk)
        {
            for (int i = 0; i < leni; i++)
            {
                for (int j = 0; j < lenj; j++)
                {
                    for (int k = 0; k < lenk; k++)
                    {
                        dst[dsti + i, dstj + j, dstk + k] = src[srci + i, srcj + j, srck + k];
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
        public static Block CutI(Block b, int pos)
        {
            if (!((new ISegm(1, b.INodes - 2)).Contains(pos)))
            {
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
            CopyPointsBetween3DArrays(b.Nodes, pos, 0, 0, new_b.Nodes, 0, 0, 0, b.INodes - pos, b.JNodes, b.KNodes);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            Point[,,] OldNodes = b.Nodes;

            // Allocate memory for current block (again).
            b.Reshape(pos, b.J.H, b.K.H);
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(OldNodes, 0, 0, 0, b.Nodes, 0, 0, 0, b.INodes, b.JNodes, b.KNodes);

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
        public static Block CutJ(Block b, int pos)
        {
            if (!((new ISegm(1, b.JNodes - 2)).Contains(pos)))
            {
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
            CopyPointsBetween3DArrays(b.Nodes, 0, pos, 0, new_b.Nodes, 0, 0, 0, b.INodes, b.JNodes - pos, b.KNodes);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            Point[,,] OldNodes = b.Nodes;

            // Allocate memory for current block (again).
            b.Reshape(b.I.H, pos, b.K.H);
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(OldNodes, 0, 0, 0, b.Nodes, 0, 0, 0, b.INodes, b.JNodes, b.KNodes);

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
        public static Block CutK(Block b, int pos)
        {
            if (!((new ISegm(1, b.KNodes - 2)).Contains(pos)))
            {
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
            CopyPointsBetween3DArrays(b.Nodes, 0, 0, pos, new_b.Nodes, 0, 0, 0, b.INodes, b.JNodes, b.KNodes - pos);

            // Insert into blocks list.
            g.Blocks.Add(new_b);

            // Define duplicate of block nodes.
            Point[,,] OldNodes = b.Nodes;

            // Allocate memory for current block (again).
            b.Reshape(b.I.H, b.J.H, pos);
            b.Allocate();

            // Copy lower part of node to reallocated block nodes.
            CopyPointsBetween3DArrays(OldNodes, 0, 0, 0, b.Nodes, 0, 0, 0, b.INodes, b.JNodes, b.KNodes);

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

            for (int i = 0; i < ic; i++)
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
            StructuredGrid g = b.Grid;
            int bc = g.BCondsCount;

            for (int i = 0; i < bc; i++)
            {
                Cut(g.BConds[i], b, d, new_b);
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
            StructuredGrid g = b.Grid;
            int sc = g.ScopesCount;

            for (int i = 0; i < sc; i++)
            {
                Cut(g.Scopes[i], b, d, new_b);
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
            if (!d.IsCorrect)
            {
                throw new Exception("unknown direction");
            }

            int g = d.Gen.N;

            // Check iface is big enough.
            if (!(ifc.Coords[g].Length > width))
            {
                throw new Exception("iface is not big enough to trunc");
            }

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

            return null;
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
            // TODO
        }

        /// <summary>
        /// Cut border condition.
        /// </summary>
        /// <param name="bc">border condition</param>
        /// <param name="b">cutted block</param>
        /// <param name="d">direction</param>
        /// <param name="new_b">new block</param>
        public static void Cut(BCond bc, Block b, Dir d, Block new_b)
        {
            // TODO
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
            // TODO
        }
    }
}
