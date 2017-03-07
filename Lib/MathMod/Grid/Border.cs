using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Border of block.
    /// </summary>
    public abstract class Border : ThinDescartesObject
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Block.
        /// </summary>
        public Block B
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">count of nodes in I direction</param>
        /// <param name="j">count of nodes in J direction</param>
        /// <param name="k">count of noded in K direction</param>
        public Border(int id, Block b, ISegm i, ISegm j, ISegm k)
            : base(i, j, k)
        {
            Id = id;
            B = b;
        }

        /// <summary>
        /// Check if border is iface.
        /// </summary>
        /// <returns><c>true</c> - if it is iface, <c>false</c> - otherwise</returns>
        public abstract bool IsIface();

        /// <summary>
        /// Check if border if bcond.
        /// </summary>
        /// <returns><c>true</c> - if it is bcond, <c>false</c> - otherwise</returns>
        public abstract bool IsBCond();

        /// <summary>
        /// Get corner node by two directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns></returns>
        public Point CornerNode(Dir d1, Dir d2)
        {
            if (!Dir.IsBasis(D, d1, d2))
            {
                return null;
            }

            int s1 = (int)d1.N;
            int s2 = (int)d2.N;

            if (d1.Gen().N > d2.Gen().N)
            {
                s1 = (int)d2.N;
                s2 = (int)d1.N;
            }

            int i = I0;
            int j = J0;
            int k = K0;

            if (s1 == (int)Dir.Num.I1)
            {
                i = I1;
            }
            else if (s1 == (int)Dir.Num.J1)
            {
                j = J1;
            }

            if (s2 == (int)Dir.Num.J1)
            {
                j = J1;
            }
            else if (s2 == (int)Dir.Num.K1)
            {
                k = K1;
            }

            return B.Nodes[i, j, k];
        }
    }
}
