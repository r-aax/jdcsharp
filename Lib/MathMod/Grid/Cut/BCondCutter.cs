using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lib.MathMod.Grid.Cut
{
    /// <summary>
    /// Cut master for border condition.
    /// </summary>
    public static class BCondCutter
    {
        /// <summary>
        /// Cut border condition.
        /// </summary>
        /// <param name="bcond">border condition</param>
        /// <param name="d">direction</param>
        /// <param name="pos">position</param>
        /// <returns>new border condition</returns>
        public static BCond Cut(BCond bcond, Dir d, int pos)
        {
            Debug.Assert(d.IsCorrect, "not correct direction");

            if (!bcond.Coords[d.Gen.N].Contains(pos))
            {
                return null;
            }

            StructuredGrid grid = bcond.B.Grid;
            int new_id = grid.MaxBCondId() + 1;
            BCond new_bcond = bcond.Clone(new_id);

            if (d.IsPos)
            {
                // Positive direction.
                //
                //                    d1 *
                //       d1 *            | new bcond
                //  ^       |        pos *
                //  |   pos *  --->   
                //  |       |        pos *
                //       d0 *            | bcond
                //                    d0 *

                bcond.Coords[d.N][1] = pos;
                new_bcond.Coords[d.N][0] = pos;
            }
            else
            {
                // Negative direction.
                //
                //                    d1 *
                //       d1 *            | bcond
                //  |       |        pos *
                //  |   pos *  --->   
                //  V       |        pos *
                //       d0 *            | new bcond
                //                    d0 *

                Dir invd = !d;

                new_bcond.Coords[invd.N][1] = pos;
                bcond.Coords[invd.N][0] = pos;
            }

            return new_bcond;
        }

        /// <summary>
        /// Trunc border condition after given width.
        /// </summary>
        /// <param name="bcond">border condition</param>
        /// <param name="d">direction</param>
        /// <param name="width">width</param>
        public static BCond Trunc(BCond bcond, Dir d, int width)
        {
            if (width >= bcond.Size(d.Gen))
            {
                return null;
            }

            if (d.IsPos)
            {
                // Positive direction.
                //
                //     0                           size
                //     *----------*----------------->
                //     |  width   |
                //
                //  0        width   0            size - width
                //  *---------->     *----------------->
                //  cutted bcond     new bcond

                return Cut(bcond, d, bcond.Coords[d.N][0] + width);
            }
            else
            {
                // Negative direction.
                //
                //     0                           size
                //     *-----------------*---------->
                //                       |  width   |
                //
                //  0        size - width   0        width
                //  *----------------->     *---------->
                //  new bcond               cutted bcond

                return Cut(bcond, d, bcond.Coords[(!d).N][1] - width);
            }
        }
    }
}
