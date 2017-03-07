using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Interface between two blocks.
    /// </summary>
    public class Iface : Border
    {
        /// <summary>
        /// Neighbour block.
        /// </summary>
        public Block NB
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">nodes count in I direction</param>
        /// <param name="j">nodes count in J direction</param>
        /// <param name="k">nodes count in K direction</param>
        /// <param name="nb">neighbour block</param>
        public Iface(int id, Block b, ISegm i, ISegm j, ISegm k, Block nb)
            : base(id, b, i, j, k)
        {
            NB = nb;
        }

        /// <summary>
        /// Check if it is iface.
        /// </summary>
        /// <returns><c>true</c></returns>
        public override bool IsIface()
        {
            return true;
        }

        /// <summary>
        /// Check if it is bcond.
        /// </summary>
        /// <returns><c>false</c></returns>
        public override bool IsBCond()
        {
            return false;
        }
    }
}
