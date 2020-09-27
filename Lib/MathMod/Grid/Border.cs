using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Numbers;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.MathMod.Grid.DescartesObjects;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Border of block.
    /// </summary>
    public abstract class Border : ThinGObject
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I direction nodes interval</param>
        /// <param name="j">J direction nodes interval</param>
        /// <param name="k">K direction nodes interval </param>
        public Border(int id, Block b, IntervalI i, IntervalI j, IntervalI k)
            : base(id, b, i, j, k)
        {
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        public Border(int id, Block b, DescartesObject3D canvas)
            : this(id, b, canvas.I, canvas.J, canvas.K)
        {
        }

        /// <summary>
        /// Check if border is iface.
        /// </summary>
        /// <returns><c>true</c> - if it is iface, <c>false</c> - otherwise</returns>
        public abstract bool IsIface();

        /// <summary>
        /// Check if border is bcond.
        /// </summary>
        /// <returns><c>true</c> - if it is bcond, <c>false</c> - otherwise</returns>
        public abstract bool IsBCond();

        /// <summary>
        /// Get corner node by two directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>corner node point</returns>
        public Point CornerNode(Dir d1, Dir d2)
        {
            if (!Dir.IsBasis(Canvas.D, d1, d2))
            {
                return null;
            }

            int s1 = d1.N;
            int s2 = d2.N;

            if (d1.Gen.N > d2.Gen.N)
            {
                s1 = d2.N;
                s2 = d1.N;
            }

            int i = Canvas.I0;
            int j = Canvas.J0;
            int k = Canvas.K0;

            if (s1 == Dir.I1N)
            {
                i = Canvas.I1;
            }
            else if (s1 == Dir.J1N)
            {
                j = Canvas.J1;
            }

            if (s2 == Dir.J1N)
            {
                j = Canvas.J1;
            }
            else if (s2 == Dir.K1N)
            {
                k = Canvas.K1;
            }

            return new Point(B.C[i, j, k, 0], B.C[i, j, k, 1], B.C[i, j, k, 2]);
        }

        /// <summary>
        /// Check sizes match of two borders.
        /// </summary>
        /// <param name="b">border to check with</param>
        /// <param name="dirs">dirs relations</param>
        /// <returns><c>true</c> - if sizes match, <c>false</c> - otherwise</returns>
        public bool IsSizesMatch(Border b, Dirs3 dirs)
        {
            return (Canvas.Size(Dir.I) == b.Canvas.Size(dirs.I.Gen))
                   && (Canvas.Size(Dir.J) == b.Canvas.Size(dirs.J.Gen))
                   && (Canvas.Size(Dir.K) == b.Canvas.Size(dirs.K.Gen));
        }

        /// <summary>
        /// Find directions match for other border.
        /// </summary>
        /// <param name="b">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <param name="eps">epsilon</param>
        /// <returns>directions - if objects match, null - otherwise</returns>
        public Dirs3 DirectionsMatchFixed(Border b, bool is_codirectional, double eps)
        {
            BorderCorners bc_this = new BorderCorners(this);
            BorderCorners bc = new BorderCorners(b);
            Dirs3 dirs = bc_this.DirectionsMatchFixed(bc, is_codirectional, eps);

            if (dirs == null)
            {
                return null;
            }

            return IsSizesMatch(b, dirs) ? dirs : null;
        }

        /// <summary>
        /// Find directions match with parallel move.
        /// </summary>
        /// <param name="b">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <param name="eps">epsilon</param>
        /// <returns>directions - if objects match, null - otherwise</returns>
        public Dirs3 DirectionsMatchParallelMove(Border b, bool is_codirectional, double eps)
        {
            BorderCorners bc_this = new BorderCorners(this);
            BorderCorners bc = new BorderCorners(b);
            Dirs3 dirs = bc_this.DirectionsMatchParallelMove(bc, is_codirectional, eps);

            if (dirs == null)
            {
                return null;
            }

            return IsSizesMatch(b, dirs) ? dirs : null;
        }

        /// <summary>
        /// Find directions match with RotX.
        /// </summary>
        /// <param name="b">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <param name="eps">epsilon</param>
        /// <returns>directions - if objects match, null - otherwise</returns>
        public Dirs3 DirectionsMatchRotX(Border b, bool is_codirectional, double eps)
        {
            BorderCorners bc_this = new BorderCorners(this);
            BorderCorners bc = new BorderCorners(b);
            Dirs3 dirs = bc_this.DirectionsMatchRotX(bc, is_codirectional, eps);

            if (dirs == null)
            {
                return null;
            }

            return IsSizesMatch(b, dirs) ? dirs : null;
        }
    }
}
