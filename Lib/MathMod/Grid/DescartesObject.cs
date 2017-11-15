using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Descartes object.
    /// </summary>
    abstract public class DescartesObject
    {
        /// <summary>
        /// Array of coordinates segments.
        /// Only this array contains real data.
        /// </summary>
        public IntervalI[] Coords;

        /// <summary>
        /// I coordinates segment.
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
        /// J coordinates segment.
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
        /// K coordinates segment
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
        /// Empty constructor.
        /// </summary>
        public DescartesObject()
        {
        }

        /// <summary>
        /// Constructor from sizes.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        public DescartesObject(IntervalI i, IntervalI j, IntervalI k)
        {
            Coords = new IntervalI[] { new IntervalI(i), new IntervalI(j), new IntervalI(k) };
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
        /// Size in I direction (in cells).
        /// </summary>
        public int ISize
        {
            get
            {
                return I.Length;
            }
        }

        /// <summary>
        /// Size in J direction (in cells).
        /// </summary>
        public int JSize
        {
            get
            {
                return J.Length;
            }
        }

        /// <summary>
        /// Size i K direction (in cells).
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
            if (d.IsGen)
            {
                return Coords[d.N].Length;
            }
            else
            {
                throw new Exception("wrong direction for getting descartes object size");
            }
        }

        /// <summary>
        /// Direction of maximum size.
        /// </summary>
        /// <returns></returns>
        public Dir MaxSizeDir()
        {
            if ((ISize >= JSize) && (ISize >= KSize))
            {
                return Dir.I;
            }
            else if (JSize >= KSize)
            {
                return Dir.J;
            }
            else
            {
                return Dir.K;
            }
        }

        /// <summary>
        /// Nodes count in I direction.
        /// </summary>
        public int INodes
        {
            get
            {
                return ISize + 1;
            }
        }

        /// <summary>
        /// Nodes count in J direction.
        /// </summary>
        public int JNodes
        {
            get
            {
                return JSize + 1;
            }
        }

        /// <summary>
        /// Nodes count in K direction.
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
                return 2 * (ISize * JSize + ISize * KSize + JSize * KSize);
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
    }
}
