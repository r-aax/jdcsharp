using System;

using Lib.Maths.Geometry;
using Lib.MathMod.Grid.DescartesObjects;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Border condition.
    /// </summary>
    public class BCond : LinkedBorder, ICloneable
    {
        /// <summary>
        /// Label (set of names).
        /// </summary>
        public NamedObject Label;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I nodes interval</param>
        /// <param name="j">J nodes interval</param>
        /// <param name="k">K nodes interval</param>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public BCond(int id, Block b, IntervalI i, IntervalI j, IntervalI k,
                     string type, string subtype, string name)
            : base(id, b, i, j, k, null)
        {
            Label = new NamedObject(type, subtype, name);
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public BCond(int id, Block b, DescartesObject3D canvas,
                     string type, string subtype, string name)
            : base(id, b, canvas, null)
        {
            Label = new NamedObject(type, subtype, name);
        }

        /// <summary>
        /// Check if it is interface.
        /// </summary>
        /// <returns><c>false</c></returns>
        public override bool IsIface()
        {
            return false;
        }

        /// <summary>
        /// Check if it is border conndition.
        /// </summary>
        /// <returns><c>true</c></returns>
        public override bool IsBCond()
        {
            return true;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public object Clone()
        {
            return new BCond(Id, B,
                             Canvas.I.Clone() as IntervalI,
                             Canvas.J.Clone() as IntervalI,
                             Canvas.K.Clone() as IntervalI,
                             Label.Type, Label.Subtype, Label.Name);
        }

        /// <summary>
        /// Clone border condition with given identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>new border condition</returns>
        public BCond Clone(int id)
        {
            return Clone(id, B);
        }

        /// <summary>
        /// Clone border condition with given identifier and block.
        /// </summary>
        /// <param name="id">new identifier</param>
        /// <param name="b">new block</param>
        /// <returns>new border condition</returns>
        public BCond Clone(int id, Block b)
        {
            BCond bcond = Clone() as BCond;

            bcond.Id = id;
            bcond.B = b;

            return bcond;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0,4}: {1,4} [{2,3} - {3,3}, {4,3} - {5,3}, {6,3} - {7,3}] {8,-12} {9,-12} {10,-12} ({11})",
                                 Id, B.Id, Canvas.I0, Canvas.I1, Canvas.J0, Canvas.J1, Canvas.K0, Canvas.K1,
                                 Label.Type, Label.Subtype, Label.Name, Canvas.D.ToString());
        }

        /// <summary>
        /// Check if border condition is linked.
        /// </summary>
        /// <returns><c>true</c> - if it is linked, <c>false</c> - otherwise</returns>
        public bool IsLinked()
        {
            StructuredGrid g = B.Grid;

            for (int i = 0; i < g.BCondsLinksCount; i++)
            {
                BCondsLink bcl = g.BCondsLinks[i];

                if ((bcl.BCond1 == this) || (bcl.BCond2 == this))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if it is PERI corder condition.
        /// </summary>
        public bool IsPeri
        {
            get
            {
                return Label.Name.StartsWith("PERI");
            }
        }
    }
}
