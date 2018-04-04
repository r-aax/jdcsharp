﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid.DescartesObjects
{
    public class DescartesObject3D : ICloneable
    {
        /// <summary>
        /// Array of coordinates segments.
        /// Only this array contains real data.
        /// </summary>
        public IntervalI[] Coords;

        /// <summary>
        /// <c>I</c> coordinates segment.
        /// </summary>
        public IntervalI I
        {
            get
            {
                return Coords[0];
            }

            set
            {
                Coords[0] = value;
            }
        }

        /// <summary>
        /// <c>J</c> coordinates segment.
        /// </summary>
        public IntervalI J
        {
            get
            {
                return Coords[1];
            }

            set
            {
                Coords[1] = value;
            }
        }

        /// <summary>
        /// <c>K</c> coordinates segment
        /// </summary>
        public IntervalI K
        {
            get
            {
                return Coords[2];
            }

            set
            {
                Coords[2] = value;
            }
        }

        /// <summary>
        /// Interval of size in given general direction.
        /// </summary>
        /// <param name="d">general direction</param>
        /// <returns>interval</returns>
        public IntervalI Coord(Dir d)
        {
            if (d.IsGen)
            {
                return Coords[d.N];
            }
            else
            {
                throw new Exception("wrong direction for getting descartes object coords interval");
            }
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public DescartesObject3D()
        {
        }

        /// <summary>
        /// Constructor from sizes.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        public DescartesObject3D(IntervalI i, IntervalI j, IntervalI k)
        {
            Coords = new IntervalI[] { new IntervalI(i), new IntervalI(j), new IntervalI(k) };
        }

        /// <summary>
        /// Constructor from another descartes object.
        /// </summary>
        /// <param name="do3"></param>
        public DescartesObject3D(DescartesObject3D do3)
            : this(do3.I, do3.J, do3.K)
        {
        }

        /// <summary>
        /// Constructor from string.
        /// </summary>
        /// <param name="str">string</param>
        public DescartesObject3D(string str)
        {
            int res;
            char[] ch = new char[] { '[', ']', '-', 'x' };
            List<string> ss = str.Split(ch).ToList().FindAll(s => Int32.TryParse(s, out res));

            Coords = new IntervalI[]
                {
                    new IntervalI(Int32.Parse(ss[0]), Int32.Parse(ss[1])),
                    new IntervalI(Int32.Parse(ss[2]), Int32.Parse(ss[3])),
                    new IntervalI(Int32.Parse(ss[4]), Int32.Parse(ss[5]))
                };
        }

        /// <summary>
        /// Low coordinate in I direction.
        /// </summary>
        public int I0
        {
            get
            {
                return I.L;
            }
        }

        /// <summary>
        /// High coordinate in I direction.
        /// </summary>
        public int I1
        {
            get
            {
                return I.H;
            }
        }

        /// <summary>
        /// Low coordinate in J direction.
        /// </summary>
        public int J0
        {
            get
            {
                return J.L;
            }
        }

        /// <summary>
        /// High cooridinate in J direction.
        /// </summary>
        public int J1
        {
            get
            {
                return J.H;
            }
        }

        /// <summary>
        /// Low coordinate in K direction.
        /// </summary>
        public int K0
        {
            get
            {
                return K.L;
            }
        }

        /// <summary>
        /// High coordinate in K direction.
        /// </summary>
        public int K1
        {
            get
            {
                return K.H;
            }
        }

        /// <summary>
        /// Size in <c>I</c> direction (in cells).
        /// </summary>
        public int ISize
        {
            get
            {
                return I.Length;
            }
        }

        /// <summary>
        /// Size in <c>J</c> direction (in cells).
        /// </summary>
        public int JSize
        {
            get
            {
                return J.Length;
            }
        }

        /// <summary>
        /// Size in <c>K</c> direction (in cells).
        /// </summary>
        public int KSize
        {
            get
            {
                return K.Length;
            }
        }

        /// <summary>
        /// Size in given direction.
        /// </summary>
        /// <param name="d">direction</param>
        /// <returns>size</returns>
        public int Size(Dir d)
        {
            return Coord(d).Length;
        }

        /// <summary>
        /// Direction of maximum size.
        /// </summary>
        /// <returns>direction with max size</returns>
        public Dir MaxSizeDir()
        {
            int isize = ISize;
            int jsize = JSize;
            int ksize = KSize;

            if (isize >= jsize)
            {
                // Max size is <c>I</c> or <c>K</c>.
                return (isize >= ksize) ? Dir.I : Dir.K;
            }
            else
            {
                // Max size is <c>J</c> or <c>K</c>.
                return (jsize >= ksize) ? Dir.J : Dir.K;
            }
        }

        /// <summary>
        /// Nodes count in <c>I</c> direction.
        /// </summary>
        public int INodes
        {
            get
            {
                return ISize + 1;
            }
        }

        /// <summary>
        /// Nodes count in <c>J</c> direction.
        /// </summary>
        public int JNodes
        {
            get
            {
                return JSize + 1;
            }
        }

        /// <summary>
        /// Nodes count in <c>K</c> direction.
        /// </summary>
        public int KNodes
        {
            get
            {
                return KSize + 1;
            }
        }

        /// <summary>
        /// Nodes count in given direction.
        /// </summary>
        /// <param name="d">direction</param>
        /// <returns>nodes count</returns>
        public int Nodes(Dir d)
        {
            return Size(d) + 1;
        }

        /// <summary>
        /// Square in <c>I</c> direction (in square cells).
        /// </summary>
        public int ISquare
        {
            get
            {
                return JSize * KSize;
            }
        }

        /// <summary>
        /// Square in <c>J</c> direction (in square cells).
        /// </summary>
        public int JSquare
        {
            get
            {
                return ISize * KSize;
            }
        }

        /// <summary>
        /// Square in <c>K</c> direction (in square cells).
        /// </summary>
        public int KSquare
        {
            get
            {
                return ISize * JSize;
            }
        }

        /// <summary>
        /// Square in given direction.
        /// </summary>
        /// <param name="d">direction</param>
        /// <returns>square</returns>
        public int Square(Dir d)
        {
            return CellsCount / Size(d);
        }

        /// <summary>
        /// Count of cells.
        /// </summary>
        public int CellsCount
        {
            get
            {
                return ISize * JSize * KSize;
            }
        }

        /// <summary>
        /// Count of inner cells.
        /// </summary>
        /// <param name="border_depth">border depth</param>
        /// <returns>count of inner cells</returns>
        public int InnerCellsCount(int border_depth)
        {
            int d2 = 2 * border_depth;
            int isize = ISize - d2;
            int jsize = JSize - d2;
            int ksize = KSize - d2;

            return ((isize > 0) && (jsize > 0) && (ksize > 0)) ? (isize * jsize * ksize) : 0;
        }

        /// <summary>
        /// Border cells count.
        /// </summary>
        /// <param name="border_depth">border depth</param>
        /// <returns>count of border cells</returns>
        public int BorderCellsCount(int border_depth)
        {
            return CellsCount - InnerCellsCount(border_depth);
        }

        /// <summary>
        /// Surface area.
        /// </summary>
        public int SurfaceArea
        {
            get
            {
                return 2 * (ISquare + JSquare + KSquare);
            }
        }

        /// <summary>
        /// Count of nodes.
        /// </summary>
        public int NodesCount
        {
            get
            {
                return INodes * JNodes * KNodes;
            }
        }

        /// <summary>
        /// Measure object.
        /// </summary>
        public virtual int Measure
        {
            get
            {
                return CellsCount;
            }
        }

        /// <summary>
        /// Clone object.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new DescartesObject3D(this);
        }

        /// <summary>
        /// Copy object.
        /// </summary>
        public DescartesObject3D Copy
        {
            get
            {
                return Clone() as DescartesObject3D;
            }
        }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("[{0} - {1}] x [{2} - {3}] x [{4} - {5}]", I0, I1, J0, J1, K0, K1);
        }

        /// <summary>
        /// Cut the given descartes objects in the given direction in fixed position.
        /// </summary>
        /// <param name="dir">direction</param>
        /// <param name="pos">position</param>
        /// <returns>new block or <c>null</c> if cut is impossible</returns>
        public DescartesObject3D Cut(Dir dir, int pos)
        {
            if (!Coord(dir).Cutted(1).Contains(pos))
            {
                return null;
            }

            DescartesObject3D copy = Copy;

            if (dir.IsPos)
            {
                //                  --> dir -->
                //
                //    lo                pos                hi
                //    *------------------*------------------*
                //                       |
                //                       V
                //  lo               pos   pos               hi
                //  *------------------*   *------------------*
                //      first object           second object

                Coord(dir).H = pos;
                copy.Coord(dir).L = pos;
            }
            else
            {
                //                  <-- dir <--
                //
                //    lo                pos                hi
                //    *------------------*------------------*
                //                       |
                //                       V
                //  lo               pos   pos               hi
                //  *------------------*   *------------------*
                //      second object          first object

                Coord(dir).L = pos;
                copy.Coord(dir).H = pos;
            }

            return copy;
        }

        /// <summary>
        /// Cut descartes object in oriented direction (truncate).
        /// </summary>
        /// <param name="dir">direction</param>
        /// <param name="w">width (oriented position)</param>
        /// <returns>new block or <c>null</c> if cut is impossible</returns>
        public DescartesObject3D Trunc(Dir dir, int w)
        {
            if (dir.IsPos)
            {
                // If direction is positive.
                //
                //               --> dir -->
                //
                // lo              lo + w               hi
                // *------------------*------------------*
                //     first object      second object

                return Cut(dir, Coord(dir).L + w);
            }
            else
            {
                // If direction is negative.
                //
                //               <-- dir <--
                //
                // lo              hi - w               hi
                // *------------------*------------------*
                //     second object      first object

                return Cut(dir, Coord(!dir).H - w);
            }
        }

        /// <summary>
        /// Decrement to zero.
        /// </summary>
        public void DecTo0()
        {
            I.DecTo0();
            J.DecTo0();
            K.DecTo0();
        }

        /// <summary>
        /// Return facet of common 3D descartes object.
        /// </summary>
        /// <param name="d">direction</param>
        /// <returns>facet</returns>
        public ThinDescartesObject3D Facet(Dir d)
        {
            IntervalI i = I.Copy;
            IntervalI j = J.Copy;
            IntervalI k = K.Copy;

            switch (d.N)
            {
                case Dir.I0N:
                    i.H = i.L;
                    break;

                case Dir.I1N:
                    i.L = i.H;
                    break;

                case Dir.J0N:
                    j.H = j.L;
                    break;

                case Dir.J1N:
                    j.L = j.H;
                    break;

                case Dir.K0N:
                    k.H = k.L;
                    break;

                case Dir.K1N:
                    k.L = k.H;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return new ThinDescartesObject3D(i, j, k);
        }

        /// <summary>
        /// Check if two descartes objects are the same sizes.
        /// </summary>
        /// <param name="o1">first descartes object</param>
        /// <param name="o2">second descartes object</param>
        /// <returns><c>true</c> - if two objects are the same sizes, <c>false</c> - otherwise</returns>
        public static bool IsSameSizes(DescartesObject3D o1, DescartesObject3D o2)
        {
            return (o1.ISize == o2.ISize) && (o1.JSize == o2.JSize) && (o1.KSize == o2.KSize);
        }
    }
}
