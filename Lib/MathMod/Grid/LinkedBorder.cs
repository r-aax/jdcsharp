using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.MathMod.Grid.DescartesObjects;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Border of the block linked to another block.
    /// </summary>
    public abstract class LinkedBorder : Border
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
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I direction nodes interval</param>
        /// <param name="j">J direction nodes interval</param>
        /// <param name="k">K direction nodes interval </param>
        /// <param name="nb">neighbour block</param>
        public LinkedBorder(int id, Block b, IntervalI i, IntervalI j, IntervalI k, Block nb)
            : base(id, b, i, j, k)
        {
            NB = nb;
            ResetNDirs();
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        /// <param name="nb">neighbour</param>
        public LinkedBorder(int id, Block b, DescartesObject3D canvas, Block nb)
            : this(id, b, canvas.I, canvas.J, canvas.K, nb)
        {
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
        /// <param name="lb">adjacent linked border</param>
        /// <param name="nd">neighbour direction</param>
        public void SetNDirs(Dir d, LinkedBorder lb, Dir nd)
        {
            SetNDir(d, nd);
            lb.SetNDir(nd, d);
        }

        /// <summary>
        /// Set relation between pairs of directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="lb">adjacent linked border</param>
        /// <param name="nd1">first neighbour interface direction</param>
        /// <param name="nd2">second neighbour interface direction</param>
        public void SetNDirs(Dir d1, Dir d2, LinkedBorder lb, Dir nd1, Dir nd2)
        {
            SetNDirs(d1, lb, nd1);
            SetNDirs(d2, lb, nd2);
        }

        /// <summary>
        /// Set relation between pairs of directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="d3">third direction</param>
        /// <param name="lb">adjacent linked border</param>
        /// <param name="nd1">first neighbour interface direction</param>
        /// <param name="nd2">second neighbour interface direction</param>
        /// <param name="nd3">third neightbour interface direction</param>
        public void SetNDirs(Dir d1, Dir d2, Dir d3, LinkedBorder lb, Dir nd1, Dir nd2, Dir nd3)
        {
            SetNDirs(d1, lb, nd1);
            SetNDirs(d2, lb, nd2);
            SetNDirs(d3, lb, nd3);
        }

        /// <summary>
        /// Set neighbour directions.
        /// </summary>
        /// <param name="lb">adjacent linked border</param>
        /// <param name="dirs">directions</param>
        public void SetNDirs(LinkedBorder lb, Dirs3 dirs)
        {
            SetNDirs(Dir.I, Dir.J, Dir.K, lb, dirs.I, dirs.J, dirs.K);
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
    }
}
