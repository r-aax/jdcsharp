using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Style of author name printing.
    /// Example on Michael Joseph Jackson.
    /// </summary>
    public enum AuthorNamePrintStyle
    {
        /// <summary>
        /// Michael Joseph Jackson
        /// </summary>
        FirstSecondLast,

        /// <summary>
        /// Michael J. Jackson
        /// </summary>
        FirstSLast,

        /// <summary>
        /// Michael Jackson
        /// </summary>
        FistLast,

        /// <summary>
        /// Jackson M. J.
        /// </summary>
        LastFS,

        /// <summary>
        /// Jackson M.
        /// </summary>
        LastF
    }
}
