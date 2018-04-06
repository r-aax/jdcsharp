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
    public class Label
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public Label(string type, string subtype, string name)
        {
            Type = type;
            Subtype = subtype;
            Name = name;
        }

        /// <summary>
        /// Constructor from full name.
        /// </summary>
        /// <param name="full_name">full name</param>
        public Label(string full_name)
        {
            string[] ss = full_name.Split('|');
            Type = ss[0];
            Subtype = ss[1];
            Name = ss[2];
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

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}", Type, Subtype, Name);
        }
    }
}
