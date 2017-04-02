using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Maths;
using Vector = Lib.Maths.Geometry.Geometry3D.Vector;
using Point = Lib.Maths.Geometry.Geometry3D.Point;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Corners of border.
    /// </summary>
    public class BorderCorners : ICloneable
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
        /// Set corner point.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="p">point</param>
        public void Set(Dir d1, Dir d2, Point p)
        {
            Debug.Assert(!d1.IsCollinear(d2), "wrong directions pair in border corners setter");

            if (d1.N < d2.N)
            {
                Points[d1.N, d2.N] = p;
            }
            else
            {
                Points[d2.N, d1.N] = p;
            }
        }

        /// <summary>
        /// Get corner.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>corner point</returns>
        public Point Get(Dir d1, Dir d2)
        {
            return (d1.N < d2.N) ? Points[d1.N, d2.N] : Points[d2.N, d1.N];
        }

        /// <summary>
        /// Set all corners from border.
        /// </summary>
        /// <param name="b">border</param>
        public void SetPoints(Border b)
        {
            //       Border corners matrix example (IJ directions).
            //         *------*------*------*------*------*------*
            //         |  I+  |  J+  |  K+  |  I-  |  J-  |  K-  |
            //    *----*------*------*------*------*------*------*
            //    | I+ |   c  | I+J+ | null |   c  | I+J- | null |
            //    | J+ |   o  |   c  | null | I-J+ |   c  | null |
            //    | K+ |   o  |   o  |   c  | null | null |   c  |
            //    | I- |   c  |   o  |   o  |   c  | I-J- | null |
            //    | J- |   o  |   c  |   o  |   o  |   c  | null |
            //    | K- |   o  |   o  |   c  |   o  |   o  |   c  |
            //    *----*------*------*------*------*------*------*
            //
            //       Border corners matrix example (IK directions).
            //         *------*------*------*------*------*------*
            //         |  I+  |  J+  |  K+  |  I-  |  J-  |  K-  |
            //    *----*------*------*------*------*------*------*
            //    | I+ |   c  | null | I+K+ |   c  | null | I+K- |
            //    | J+ |   o  |   c  | null | null |   c  | null |
            //    | K+ |   o  |   o  |   c  | I-K+ | null |   c  |
            //    | I- |   c  |   o  |   o  |   c  | null | I-K- |
            //    | J- |   o  |   c  |   o  |   o  |   c  | null |
            //    | K- |   o  |   o  |   c  |   o  |   o  |   c  |
            //    *----*------*------*------*------*------*------*
            //
            //       Border corners matrix example (JK directions).
            //         *------*------*------*------*------*------*
            //         |  I+  |  J+  |  K+  |  I-  |  J-  |  K-  |
            //    *----*------*------*------*------*------*------*
            //    | I+ |   c  | null | null |   c  | null | null |
            //    | J+ |   o  |   c  | J+K+ | null |   c  | J+K- |
            //    | K+ |   o  |   o  |   c  | null | J-K+ |   c  |
            //    | I- |   c  |   o  |   o  |   c  | null | null |
            //    | J- |   o  |   c  |   o  |   o  |   c  | J-K- |
            //    | K- |   o  |   o  |   c  |   o  |   o  |   c  |
            //    *----*------*------*------*------*------*------*
            //
            //         c - collinear directions
            //         o - order violation
            //         null - if wrong directions is used

            Dir od1, od2;

            D.GetPairOfOrthogonalDirs(out od1, out od2);

            // Set to matrix.
            Set(od1, od2, b.CornerNode(od1, od2).Clone() as Point);
            Set(od1, !od2, b.CornerNode(od1, !od2).Clone() as Point);
            Set(!od1, od2, b.CornerNode(!od1, od2).Clone() as Point);
            Set(!od1, !od2, b.CornerNode(!od1, !od2).Clone() as Point);
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public BorderCorners()
        {
            Points = new Point[Dir.Count, Dir.Count];

            // Set all corners to null.
            for (int i = 0; i < Dir.Count; i++)
            {
                for (int j = 0; j < Dir.Count; j++)
                {
                    Points[i, j] = null;
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="b">border</param>
        public BorderCorners(Border b)
            : this()
        {
            D = b.D;
            SetPoints(b);
        }

        /// <summary>
        /// Get center of border corners.
        /// </summary>
        /// <returns>center</returns>
        public Point Center()
        {
            Point p = new Point();

            for (int i = 0; i < Dir.Count; i++)
            {
                for (int j = i + 1; j < Dir.Count; j++)
                {
                    Point add = Points[i, j];

                    if (add != null)
                    {
                        p = p + add;
                    }
                }
            }

            p = 0.25 * p;

            return p;
        }

        /// <summary>
        /// Clone border corners.
        /// </summary>
        /// <returns>copy</returns>
        public object Clone()
        {
            BorderCorners bc = new BorderCorners();

            bc.D = D.Clone() as Dir;

            for (int i = 0; i < Dir.Count; i++)
            {
                for (int j = 0; j < Dir.Count; j++)
                {
                    Point p = Points[i, j];

                    if (p != null)
                    {
                        bc.Points[i, j] = p.Clone() as Point;
                    }
                }
            }

            return bc;
        }

        /// <summary>
        /// Move.
        /// </summary>
        /// <param name="v">vector</param>
        public void Move(Vector v)
        {
            for (int i = 0; i < Dir.Count; i++)
            {
                for (int j = i + 1; j < Dir.Count; j++)
                {
                    Point p = Points[i, j];

                    if (p != null)
                    {
                        Points[i, j] = p + v;
                    }
                }
            }
        }

        /// <summary>
        /// Rotation around X axis.
        /// </summary>
        /// <param name="s">sine</param>
        /// <param name="c">cosine</param>
        public void RotX(double s, double c)
        {
            for (int i = 0; i < Dir.Count; i++)
            {
                for (int j = i + 1; j < Dir.Count; j++)
                {
                    Point p = Points[i, j];

                    if (p != null)
                    {
                        p.RotX(s, c);
                    }
                }
            }
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
            Point p1 = bc1.Get(od11, od12);
            Point p2 = bc2.Get(od21, od22);

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

        /// <summary>
        /// Find match with parallel move.
        /// </summary>
        /// <param name="bc">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <returns>directions - if objects math, null - otherwise</returns>
        public Dirs3 DirectionsMatchParallelMove(BorderCorners bc, bool is_codirectional)
        {
            // First move to another border (vector than connects centers of borders).
            BorderCorners move = Clone() as BorderCorners;
            move.Move(bc.Center() - move.Center());

            return move.DirectionsMatchFixed(bc, is_codirectional);
        }

        /// <summary>
        /// Find match with OX rotation.
        /// </summary>
        /// <param name="bc">second object</param>
        /// <param name="is_codirectional">codirectional flag</param>
        /// <returns>directions - if objects math, null - otherwise</returns>
        public Dirs3 DirectionsMatchRotX(BorderCorners bc, bool is_codirectional)
        {
            // First rotate two borders to Y = 0.
            BorderCorners bc1 = Clone() as BorderCorners;
            BorderCorners bc2 = bc.Clone() as BorderCorners;
            Point c1 = bc1.Center();
            Point c2 = bc2.Center();
            double h1 = Maths.Maths.Hypot(c1.Y, c1.Z);
            double h2 = Maths.Maths.Hypot(c2.Y, c2.Z);

            Debug.Assert((h1 != 0.0) && (h2 != 0.0), "wrong coordinates for borders corners while RotX matching");

            bc1.RotX(-c1.Z / h1, c1.Y / h1);
            bc2.RotX(-c2.Z / h2, c2.Y / h2);

            return bc1.DirectionsMatchFixed(bc2, is_codirectional);
        }
    }
}
