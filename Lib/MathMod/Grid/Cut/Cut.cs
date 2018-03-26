using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Geometry;

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

            Debug.Assert((D == null) || (new IntervalI(1, B.Size(d) - 1)).Contains(pos), "wrong position");

            Pos = pos;
        }

        /// <summary>
        /// Count of cell remain in old block.
        /// </summary>
        public int OldBlockCellsCount
        {
            get
            {
                // Special case. D == null means it is no cut.
                return (D == null)
                       ? B.CellsCount
                       : Pos * B.Square(D);
            }
        }

        /// <summary>
        /// Minimal part cells count.
        /// </summary>
        public int MinPartCellsCount
        {
            get
            {
                // Special case. D == null means it is no cut.
                return (D == null)
                       ? B.CellsCount
                       : Math.Min(Pos, B.Size(D) - Pos) * B.Square(D);
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
