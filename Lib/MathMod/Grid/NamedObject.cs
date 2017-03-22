using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Named grid object (border condition, scope).
    /// </summary>
    public class NamedObject
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public NamedObject(string type, string subtype, string name)
        {
            Type = type;
            Subtype = subtype;
            Name = name;
        }

        /// <summary>
        /// Type.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Subtype.
        /// </summary>
        public string Subtype
        {
            get;
            set;
        }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
