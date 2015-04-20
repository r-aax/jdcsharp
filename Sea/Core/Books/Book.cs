// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sea.Core.Authors;

namespace Sea.Core.Books
{
    /// <summary>
    /// Book (printed material item).
    /// </summary>
    public class Book : IComparable<Book>, ICloneable
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
        public AuthorsList AuthorsList;

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
            AuthorsList = new AuthorsList();
            PublishersIds = new List<int>();
        }

        /// <summary>
        /// Compare to another book.
        /// </summary>
        /// <param name="book">book to compare</param>
        /// <returns>1 - if greater, -1 - if less, 0 - if equal</returns>
        public int CompareTo(Book book)
        {
            if (book == null)
            {
                return 1;
            }

            int compare_name = Name.CompareTo(book.Name);

            if (compare_name > 0)
            {
                return 1;
            }
            else if (compare_name < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Clone book.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Book book = new Book();

            book.Name = Name;
            book.ArticleSource = ArticleSource;
            book.Edition = Edition;
            book.Year = Year;

            book.CategoriesIds = new List<int>();
            for (int i = 0; i < CategoriesIds.Count; i++)
            {
                book.CategoriesIds.Add(CategoriesIds[i]);
            }

            book.AuthorsList = AuthorsList.Clone() as AuthorsList;

            book.PublishersIds = new List<int>();
            for (int i = 0; i < PublishersIds.Count; i++)
            {
                book.PublishersIds.Add(PublishersIds[i]);
            }

            return book;
        }
    }
}
