// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea.Core.Books
{
    /// <summary>
    /// Type of book (printed material item).
    /// </summary>
    public enum BookType
    {
        /// <summary>
        /// Book.
        /// </summary>
        Book,

        /// <summary>
        /// Periodic.
        /// </summary>
        Magazine,

        /// <summary>
        /// Article.
        /// </summary>
        Article,

        /// <summary>
        /// Not one of above.
        /// </summary>
        Other
    }
}
