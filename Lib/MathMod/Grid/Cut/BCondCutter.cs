using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.MathMod.Grid.DescartesObjects;

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
            DescartesObject3D new_canvas = bcond.Canvas.Cut(d, pos);
            int new_id = bcond.B.Grid.MaxBCondId() + 1;

            return new BCond(new_id, bcond.B, new_canvas,
                             bcond.Label.Type, bcond.Label.Subtype, bcond.Label.Name);
        }

        /// <summary>
        /// Trunc border condition after given width.
        /// </summary>
        /// <param name="bcond">border condition</param>
        /// <param name="d">direction</param>
        /// <param name="width">width</param>
        public static BCond Trunc(BCond bcond, Dir d, int width)
        {
            if (width >= bcond.Canvas.Size(d.Gen))
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

                return Cut(bcond, d, bcond.Canvas.Coords[d.N][0] + width);
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

                return Cut(bcond, d, bcond.Canvas.Coords[(!d).N][1] - width);
            }
        }
    }
}
