// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea.Core.Books
{
    /// <summary>
    /// Book (printed material item).
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Full name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Source of article.
        /// (name of magazine or science conference).
        /// </summary>
        public string ArticleSource;

        /// <summary>
        /// Edition number.
        /// If 0 - no edition information.
        /// </summary>
        public int Edition;

        /// <summary>
        /// Year of publishing.
        /// If 0 - no year information.
        /// </summary>
        public int Year;

        /// <summary>
        /// List of categories identifiers.
        /// </summary>
        public List<int> CategoriesIds;

        /// <summary>
        /// List of authors identifiers.
        /// </summary>
        public List<int> AuthorsIds;

        /// <summary>
        /// List of publishers identifiers.
        /// </summary>
        public List<int> PublishersIds;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book()
        {
            Name = "";
            ArticleSource = "";
            Edition = 0;
            Year = 0;
            CategoriesIds = new List<int>();
            AuthorsIds = new List<int>();
            PublishersIds = new List<int>();
        }
    }
}
