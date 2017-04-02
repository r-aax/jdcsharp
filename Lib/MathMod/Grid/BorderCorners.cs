using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Numbers;
using Point = Lib.Maths.Geometry.Geometry3D.Point;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Corners of border.
    /// </summary>
    public class BorderCorners
    {
        /// <summary>
        /// Direction.
        /// </summary>
        public Dir D;

        /// <summary>
        /// Corners points.
        /// </summary>
        private Point[,] Points;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="b">border</param>
        public BorderCorners(Border b)
        {
            D = b.D;
            Points = new Point[Dir.Count, Dir.Count];

            // Set all corners to null.
            for (int i = 0; i < Dir.Count; i++)
            {
                for (int j = 0; j < Dir.Count; j++)
                {
                    Points[i, j] = null;
                }
            }

            Dir od1, od2;
            b.D.GetPairOfOrthogonalDirs(out od1, out od2);
            Points[od1.N, od2.N] = b.CornerNode(od1, od2).Clone() as Point;
            Points[od1.N, (!od2).N] = b.CornerNode(od1, !od2).Clone() as Point;
            Points[(!od1).N, od2.N] = b.CornerNode(!od1, od2).Clone() as Point;
            Points[(!od1).N, (!od2).N] = b.CornerNode(!od1, !od2).Clone() as Point;
        }

        /// <summary>
        /// Get corner node.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>corner point</returns>
        public Point CornerNode(Dir d1, Dir d2)
        {
            return Points[d1.N, d2.N];
        }

        /// <summary>
        /// Check matching.
        /// </summary>
        /// <param name="bc1">first border corners</param>
        /// <param name="od11">first direction of first border corners</param>
        /// <param name="od12">second direction of first border corners</param>
        /// <param name="bc2">second border corners</param>
        /// <param name="od21">first direction of second border corners</param>
        /// <param name="od22">second direction of second border corners</param>
        /// <returns></returns>
        public static bool IsMatch(BorderCorners bc1, Dir od11, Dir od12,
                                   BorderCorners bc2, Dir od21, Dir od22)
        {
            Point p1 = bc1.CornerNode(od11, od12);
            Point p2 = bc2.CornerNode(od21, od22);

            if ((p1 == null) || (p2 == null))
            {
                return false;
            }

            return (p1 - p2).Mod2 < Constants.Eps;
        }

        /// <summary>
        /// Find directions match for other border corners.
        /// </summary>
        /// <param name="bc">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <returns>directions - if objects math, null - otherwise</returns>
        public Dirs3 DirectionsMatchFixed(BorderCorners bc, bool is_codirectional)
        {
            //      codirectional      not codirectional
            //        *     *             *         *
            //        |     |             |         |
            //        |---> |--->         |---> <---|
            //        |     |             |         |
            //        *     *             *         *

            // Get general directions.
            Dir d1 = D;
            Dir d2 = bc.D;

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
                if (IsMatch(this, od11, od12, bc, od21, od22))
                {
                    if (IsMatch(this, !od11, od12, bc, !od21, od22))
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
