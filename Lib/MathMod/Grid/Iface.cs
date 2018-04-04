using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Numbers;
using Lib.MathMod.Grid.DescartesObjects;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Interface between two blocks.
    /// </summary>
    public class Iface : LinkedBorder, ICloneable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">nodes interval in I direction</param>
        /// <param name="j">nodes interval in J direction</param>
        /// <param name="k">nodes interval in K direction</param>
        /// <param name="nb">neighbour block</param>
        public Iface(int id, Block b, IntervalI i, IntervalI j, IntervalI k, Block nb)
            : base(id, b, i, j, k, nb)
        {
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        /// <param name="nb">neighbour</param>
        public Iface(int id, Block b, DescartesObject3D canvas, Block nb)
            : base(id, b, canvas, nb)
        {
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
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0,4}: {1,4} [{2,3} - {3,3}, {4,3} - {5,3}, {6,3} - {7,3}] -> {8,4} ({9})",
                                 Id, B.Id,
                                 Canvas.I0, Canvas.I1,
                                 Canvas.J0, Canvas.J1,
                                 Canvas.K0, Canvas.K1, NB.Id, Canvas.D.ToString());
        }

        /// <summary>
        /// Get strong string of interface.
        /// </summary>
        /// <param name="iface">interface</param>
        /// <returns>strong string</returns>
        public static string StrongString(Iface iface)
        {
            return (iface != null) ? iface.ToString() : "null";
        }

        /// <summary>
        /// Iface string representation for save.
        /// </summary>
        /// <returns>string</returns>
        public string SaveString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                 Id, B.Id + 1,
                                 Canvas.I0 + 1, Canvas.I1 + 1,
                                 Canvas.J0 + 1, Canvas.J1 + 1,
                                 Canvas.K0 + 1, Canvas.K1 + 1, NB.Id + 1);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>new cloned interface</returns>
        public object Clone()
        {
            return new Iface(Id, B,
                             Canvas.I.Clone() as IntervalI,
                             Canvas.J.Clone() as IntervalI,
                             Canvas.K.Clone() as IntervalI, NB);
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
