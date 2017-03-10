using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Block scope.
    /// </summary>
    public class Scope : DescartesObject, ICloneable
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
        /// Label of object.
        /// </summary>
        public NamedObject Label;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I sizes</param>
        /// <param name="j">J sizes</param>
        /// <param name="k">K sizes</param>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public Scope(int id, Block b, ISegm i, ISegm j, ISegm k,
                     string type, string subtype, string name)
            : base(i, j, k)
        {
            Id = id;
            B = b;
            Label = new NamedObject(type, subtype, name);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public object Clone()
        {
            return new Scope(Id, B,
                             I.Clone() as ISegm,
                             J.Clone() as ISegm,
                             K.Clone() as ISegm,
                             Label.Type, Label.Subtype, Label.Name);
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0,4}: {1,4} [{2,3} - {3,3}, {4,3} - {5,3}, {6,3} - {7,3}] {8,10} {9,10} {10,10}",
                                 Id, B.Id, I0, I1, J0, J1, K0, K1,
                                 Label.Type, Label.Subtype, Label.Name);
        }
    }
}
