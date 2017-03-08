using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Numbers;

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
        /// Matrix of neighbour directions.
        /// </summary>
        public Dir[] NDirs
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">nodes interval in I direction</param>
        /// <param name="j">nodes interval in J direction</param>
        /// <param name="k">nodes interval in K direction</param>
        /// <param name="nb">neighbour block</param>
        public Iface(int id, Block b, ISegm i, ISegm j, ISegm k, Block nb)
            : base(id, b, i, j, k)
        {
            NB = nb;
            NDirs = new Dir[Dir.Count];
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

        /// <summary>
        /// Reset neighbour directions.
        /// </summary>
        public void ResetNDirs()
        {
            NDirs = new Dir[Dir.Count];
        }

        /// <summary>
        /// Set neighbour direction.
        /// </summary>
        /// <param name="d">direction</param>
        /// <param name="nd">neighbour direction</param>
        public void SetNDir(Dir d, Dir nd)
        {
            NDirs[d.N] = nd;
            NDirs[(!d).N] = !nd;
        }

        /// <summary>
        /// Set neighbour direction.
        /// </summary>
        /// <param name="d">direction</param>
        /// <param name="i">interface</param>
        /// <param name="nd">neighbour direction</param>
        public void SetNDirs(Dir d, Iface i, Dir nd)
        {
            SetNDir(d, nd);
            i.SetNDir(nd, d);
        }

        /// <summary>
        /// Set relation between pairs of directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="i">interface</param>
        /// <param name="nd1">first neighbour interface</param>
        /// <param name="nd2">second neighbour interface</param>
        public void SetNDirs(Dir d1, Dir d2, Iface i, Dir nd1, Dir nd2)
        {
            SetNDirs(d1, i, nd1);
            SetNDirs(d2, i, nd2);
        }

        /// <summary>
        /// Check if all NDirs are correct.
        /// </summary>
        /// <returns><c>true</c> - if all dirs are correct, <c>false</c> - otherwise</returns>
        public bool IsNDirsCorrect()
        {
            for (int i = 0; i < Dir.Count; i++)
            {
                Dir d = NDirs[i];

                if (d == null) 
                {
                    return false;
                }

                if (!d.IsCorrect)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check match of two ifaces with directions.
        /// </summary>
        /// <param name="i1">first interface</param>
        /// <param name="od11">first direction of first interface</param>
        /// <param name="od12">second direction of first interface</param>
        /// <param name="i2">second interface</param>
        /// <param name="od21">first direction of second interface</param>
        /// <param name="od22">second direction of second interface</param>
        /// <returns><c>true</c> - if interfaces match, <c>false</c> - otherwise</returns>
        public static bool IsMatch(Iface i1, Dir od11, Dir od12,
                                   Iface i2, Dir od21, Dir od22)
        {
            Point p1 = i1.CornerNode(od11, od12);
            Point p2 = i2.CornerNode(od21, od22);

            if ((p1 == null) || (p2 == null))
            {
                return false;
            }

            return (p1 - p2).Mod2 < Constants.Eps;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0}: {1} [{2}, {3}, {4}] -> {5} ({6})",
                                 Id.ToString(), B.Id.ToString(),
                                 I.ToString(), J.ToString(), K.ToString(),
                                 NB.Id.ToString(), D.ToString());
        }
    }
}
