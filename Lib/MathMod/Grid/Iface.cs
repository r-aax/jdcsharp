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
    public class Iface : Border, ICloneable
    {
        /// <summary>
        /// Neighbour block.
        /// </summary>
        public Block NB
        {
            get;
            set;
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
        /// Check if interface is cross partition.
        /// </summary>
        public bool IsCross
        {
            get
            {
                return B.PartitionNumber != NB.PartitionNumber;
            }
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
        /// <param name="nd1">first neighbour interface direction</param>
        /// <param name="nd2">second neighbour interface direction</param>
        public void SetNDirs(Dir d1, Dir d2, Iface i, Dir nd1, Dir nd2)
        {
            SetNDirs(d1, i, nd1);
            SetNDirs(d2, i, nd2);
        }

        /// <summary>
        /// Set relation between pairs of directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="d3">third direction</param>
        /// <param name="i">interface</param>
        /// <param name="nd1">first neighbour interface direction</param>
        /// <param name="nd2">second neighbour interface direction</param>
        /// <param name="nd3">third neightbour interface direction</param>
        public void SetNDirs(Dir d1, Dir d2, Dir d3, Iface i, Dir nd1, Dir nd2, Dir nd3)
        {
            SetNDirs(d1, i, nd1);
            SetNDirs(d2, i, nd2);
            SetNDirs(d3, i, nd3);
        }

        /// <summary>
        /// Set neighbour directions.
        /// </summary>
        /// <param name="i">interface</param>
        /// <param name="dirs">directions</param>
        public void SetNDirs(Iface i, Dirs3 dirs)
        {
            SetNDirs(Dir.I, Dir.J, Dir.K, i, dirs.I, dirs.J, dirs.K);
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
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0,4}: {1,4} [{2,3} - {3,3}, {4,3} - {5,3}, {6,3} - {7,3}] -> {8,4} ({9})",
                                 Id, B.Id, I0, I1, J0, J1, K0, K1, NB.Id, D.ToString());
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>new cloned interface</returns>
        public object Clone()
        {
            return new Iface(Id, B,
                             I.Clone() as ISegm,
                             J.Clone() as ISegm,
                             K.Clone() as ISegm, NB);
        }

        /// <summary>
        /// Clone with identifier and block.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <returns>new interface</returns>
        public Iface Clone(int id, Block b)
        {
            Iface iface = Clone() as Iface;

            iface.Id = id;
            iface.B = b;

            return iface;
        }
    }
}
