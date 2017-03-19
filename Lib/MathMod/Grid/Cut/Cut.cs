using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lib.MathMod.Grid.Cut
{
    /// <summary>
    /// Cut of the block.
    /// </summary>
    public class Cut
    {
        /// <summary>
        /// Block.
        /// </summary>
        public Block B
        {
            get;
            private set;
        }

        /// <summary>
        /// Direction.
        /// </summary>
        public Dir D
        {
            get;
            private set;
        }

        /// <summary>
        /// Check if there is a cut.
        /// </summary>
        public bool IsCut
        {
            get
            {
                return D != null;
            }
        }

        /// <summary>
        /// Check if there is no cut.
        /// </summary>
        public bool IsNoCut
        {
            get
            {
                return D == null;
            }
        }

        /// <summary>
        /// Position (number of node).
        /// </summary>
        public int Pos
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="b">block</param>
        /// <param name="d">direction</param>
        /// <param name="pos">position</param>
        public Cut(Block b, Dir d, int pos)
        {
            B = b;
            D = d;
            Pos = pos;
        }

        /// <summary>
        /// Count of cell remain in old block.
        /// </summary>
        public int OldBlockCellsCount
        {
            get
            {
                if (D == null)
                {
                    // Special case. D == null means it is no cut.
                    return B.CellsCount;
                }

                Debug.Assert(D.IsGen, "wrong direction");

                if (D.IsI)
                {
                    return Pos * B.JSize * B.KSize;
                }
                else if (D.IsJ)
                { 
                    return B.ISize * Pos * B.KSize;
                }
                else // if (D.IsK)
                {
                    return B.ISize * B.JSize * Pos;
                }
            }
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return (D == null)
                   ? B.ToString()
                   : String.Format("{0}, {1}, {2}", B, D, Pos);
        }
    }
}
