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
    }
}
