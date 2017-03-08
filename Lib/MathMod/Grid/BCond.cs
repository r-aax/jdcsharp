using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Border condition.
    /// </summary>
    public class BCond : Border
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
        /// <param name="i">I sizes</param>
        /// <param name="j">J sizes</param>
        /// <param name="k">K sizes</param>
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
    }
}
