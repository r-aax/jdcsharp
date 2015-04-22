// Author: Alexey Rybakov

using System;
using System.Collections.Generic;

using Sea.Core.Authors;
using Sea.Core.Publishers;

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
        public string Name { get; set; }

        /// <summary>
        /// Source of article.
        /// (name of magazine or science conference).
        /// </summary>
        public string ArticleSource { get; set; }

        /// <summary>
        /// Edition number.
        /// If 0 - no edition information.
        /// </summary>
        public int Edition { get; set; }

        /// <summary>
        /// Year of publishing.
        /// If 0 - no year information.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// List of authors.
        /// </summary>
        public AuthorsList Authors { get; set; }

        /// <summary>
        /// List of publishers.
        /// </summary>
        public PublishersList Publishers { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book()
        {
            Name = "";
            ArticleSource = "";
            Edition = 0;
            Year = 0;
            Authors = new AuthorsList();
            Publishers = new PublishersList();
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
        /// <returns>clone</returns>
        public object Clone()
        {
            Book book = new Book();

            book.Name = Name;
            book.ArticleSource = ArticleSource;
            book.Edition = Edition;
            book.Year = Year;
            book.Authors = Authors.Clone() as AuthorsList;
            book.Publishers = Publishers.Clone() as PublishersList;

            return book;
        }
    }
}
