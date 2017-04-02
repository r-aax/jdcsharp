using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Numbers;
using Point = Lib.Maths.Geometry.Geometry3D.Point;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Border condition.
    /// </summary>
    public class BCond : Border, ICloneable
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
        public BCond(int id, Block b, ISegm i, ISegm j, ISegm k,
                     string type, string subtype, string name)
            : base(id, b, i, j, k)
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
                             I.Clone() as ISegm,
                             J.Clone() as ISegm,
                             K.Clone() as ISegm,
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
            return String.Format("{0,4}: {1,4} [{2,3} - {3,3}, {4,3} - {5,3}, {6,3} - {7,3}] {8,12} {9,12} {10,12} ({11})",
                                 Id, B.Id, I0, I1, J0, J1, K0, K1,
                                 Label.Type, Label.Subtype, Label.Name, D.ToString());
        }

        /// <summary>
        /// Check if two border conditions match through rotation X.
        /// </summary>
        /// <param name="bc1">first border condition</param>
        /// <param name="od11">first direction for first border condition</param>
        /// <param name="od12">second direction for first border condition</param>
        /// <param name="bc2">second border conndition</param>
        /// <param name="od21">first direction for second border condition</param>
        /// <param name="od22">second direction for second border condition</param>
        /// <returns></returns>
        public static bool IsMatchRotX(BCond bc1, Dir od11, Dir od12,
                                       BCond bc2, Dir od21, Dir od22)
        {
            Point p1 = bc1.CornerNode(od11, od12);
            Point p2 = bc2.CornerNode(od21, od22);

            if ((p1 == null) || (p2 == null))
            {
                return false;
            }

            // X coordinates must be equal.
            if (p1.DistX(p2) > Constants.Eps2)
            {
                return false;
            }

            double r1 = Maths.Maths.Hypot2(p1.Y, p1.Z);
            double r2 = Maths.Maths.Hypot2(p2.Y, p2.Z);

            return Math.Abs(r1 - r2) < Constants.Eps2;
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
