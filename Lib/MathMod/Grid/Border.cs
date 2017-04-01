using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Numbers;
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
            set;
        }

        /// <summary>
        /// Block.
        /// </summary>
        public Block B
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I direction nodes interval</param>
        /// <param name="j">J direction nodes interval</param>
        /// <param name="k">K direction nodes interval </param>
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

            int s1 = d1.N;
            int s2 = d2.N;

            if (d1.Gen.N > d2.Gen.N)
            {
                s1 = d2.N;
                s2 = d1.N;
            }

            int i = I0;
            int j = J0;
            int k = K0;

            if (s1 == Dir.I1N)
            {
                i = I1;
            }
            else if (s1 == Dir.J1N)
            {
                j = J1;
            }

            if (s2 == Dir.J1N)
            {
                j = J1;
            }
            else if (s2 == Dir.K1N)
            {
                k = K1;
            }

            return new Point(B.C[i, j, k, 0], B.C[i, j, k, 1], B.C[i, j, k, 2]);
        }

        /// <summary>
        /// Check match of two borders with directions.
        /// </summary>
        /// <param name="b1">first border</param>
        /// <param name="od11">first direction of first border</param>
        /// <param name="od12">second direction of first border</param>
        /// <param name="b2">second border</param>
        /// <param name="od21">first direction of second border</param>
        /// <param name="od22">second direction of second border</param>
        /// <returns><c>true</c> - if objects match, <c>false</c> - otherwise</returns>
        public static bool IsMatch(Border b1, Dir od11, Dir od12,
                                   Border b2, Dir od21, Dir od22)
        {
            Point p1 = b1.CornerNode(od11, od12);
            Point p2 = b2.CornerNode(od21, od22);

            if ((p1 == null) || (p2 == null))
            {
                return false;
            }

            return (p1 - p2).Mod2 < Constants.Eps;
        }

        /// <summary>
        /// Find directions match for other thin descartes object.
        /// </summary>
        /// <param name="tdo">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <returns>directions - if objects math, null - otherwise</returns>
        public Dirs3 DirectionsMatchFixed(Border b, bool is_codirectional)
        {
            //      codirectional      not codirectional
            //        *     *             *         *
            //        |     |             |         |
            //        |---> |--->         |---> <---|
            //        |     |             |         |
            //        *     *             *         *

            // Get general directions.
            Dir d1 = D;
            Dir d2 = b.D;

            // Process codirectional flag.
            if (!is_codirectional)
            {
                d2 = !d2;
            }

            Dirs3 dirs = new Dirs3();
            dirs.Set(d1, d2);

            // Detect two pairs of orthogonal directions.
            Dir od11, od12, od21, od22;
            d1.GetPairOfOrthogonalDirs(out od11, out od12);
            d2.GetPairOfOrthogonalDirs(out od21, out od22);

            // Check 4 quarters.
            for (int j = 0; j < 4; j++)
            {
                if (IsMatch(this, od11, od12, b, od21, od22))
                {
                    if (IsMatch(this, !od11, od12, b, !od21, od22))
                    {
                        dirs.Set(od11, od21);
                        dirs.Set(od12, od22);
                    }
                    else
                    {
                        dirs.Set(od11, od22);
                        dirs.Set(od12, od21);
                    }

                    return dirs;
                }

                Dir.OrthogonalRot(ref od21, ref od22);
            }

            return null;
        }
    }
}
