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
        /// </summary>
        public ISegm[] Coords;

        /// <summary>
        /// I coordinates segment.
        /// </summary>
        public ISegm I;

        /// <summary>
        /// J coordinates segment.
        /// </summary>
        public ISegm J;

        /// <summary>
        /// K coordinates segment
        /// </summary>
        public ISegm K;

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
        public DescartesObject(ISegm i, ISegm j, ISegm k)
        {
            I = new ISegm(i);
            J = new ISegm(j);
            K = new ISegm(k);
            Coords = new ISegm[Dir.GenCount];
            Coords[Dir.IN] = I;
            Coords[Dir.JN] = J;
            Coords[Dir.KN] = K;
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
        /// Size in given direction (in cells).
        /// </summary>
        /// <param name="dir">direction</param>
        /// <returns>size in given direction</returns>
        public int DirSize(Dir dir)
        {
            return Coords[(int)dir.N].Length;
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
            else if (JSize > KSize)
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
    }
}
