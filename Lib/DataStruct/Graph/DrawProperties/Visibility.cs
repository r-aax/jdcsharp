using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataStruct.Graph.DrawProperties
{
    /// <summary>
    /// Visibility of element (label for example).
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// No visible.
        /// </summary>
        No,

        /// <summary>
        /// Visible.
        /// </summary>
        Yes,

        /// <summary>
        /// The same as parent visibility.
        /// </summary>
        Parent
    }
}
