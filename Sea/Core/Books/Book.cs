// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

using Lib.DataStruct;
using Sea.Core.Authors;
using Sea.Core.Publishers;
using Sea.Core.Categories;

namespace Sea.Core.Books
{
    /// <summary>
    /// Book (printed material item).
    /// </summary>
    public class Book : IComparable<Book>, IEquatable<Book>, ICloneable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of book.
        /// </summary>
        public BookType Type { get; set; }

        /// <summary>
        /// Source of article.
        /// (name of magazine or science conference).
        /// </summary>
        public string ArticleSource { get; set; }

        /// <summary>
        /// Number (for magazine).
        /// </summary>
        public int Number { get; set; }

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
        /// Book file reference.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Canonic name of book file.
        /// </summary>
        public string CanonFile
        {
            get
            {
                return Id.ToString() + File.Substring(File.LastIndexOf('.'));
            }
        }

        /// <summary>
        /// List of authors.
        /// </summary>
        [XmlIgnore]
        public AuthorsList Authors { get; set; }

        /// <summary>
        /// Authors identifiers for serialization.
        /// </summary>
        public List<int> AuthorsIds;

        /// <summary>
        /// List of publishers.
        /// </summary>
        [XmlIgnore]
        public PublishersList Publishers { get; set; }

        /// <summary>
        /// Publishers identifiers for serialization.
        /// </summary>
        public List<int> PublishersIds;

        /// <summary>
        /// List of categories.
        /// </summary>
        [XmlIgnore]
        public CategoriesList Categories { get; set; }

        /// <summary>
        /// Categories identifiers for serialization.
        /// </summary>
        public List<int> CategoriesIds;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="type">type</param>
        /// <param name="article_source">source of article</param>
        /// <param name="number">number of magazie</param>
        /// <param name="edition">edition number</param>
        /// <param name="year">year</param>
        /// <param name="file">file</param>
        public Book(string name, BookType type, string article_source,
                    int number, int edition, int year, string file)
        {
            Id = -1;
            Name = name;
            Type = type;
            ArticleSource = article_source;
            Number = number;
            Edition = edition;
            Year = year;
            File = file;
            Authors = new AuthorsList();
            Publishers = new PublishersList();
            Categories = new CategoriesList();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book()
            : this("", BookType.Book, "", 0, 0, 0, "")
        {
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
        /// Check equals.
        /// </summary>
        /// <param name="book">another book</param>
        /// <returns><c>true</c> - if books are equal, <c>false</c> - if books are not equal</returns>
        public bool Equals(Book book)
        {
            return Id == book.Id;
        }

        /// <summary>
        /// Clone book.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            Book book = new Book();

            book.Id = Id;
            book.Name = Name;
            book.Type = Type;
            book.ArticleSource = ArticleSource;
            book.Number = Number;
            book.Edition = Edition;
            book.Year = Year;
            book.File = File;
            book.Authors = Authors.Clone() as AuthorsList;
            book.Publishers = Publishers.Clone() as PublishersList;
            book.Categories = Categories.Clone() as CategoriesList;

            return book;
        }

        /// <summary>
        /// Construct full name of book.
        /// </summary>
        /// <param name="style">style of book printing</param>
        /// <returns>full name</returns>
        public string FullName(BookFullNamePrintStyle style)
        {
            switch (style)
            {
                case BookFullNamePrintStyle.Wide:

                    string full_name = "";

                    // First of all print authors.
                    if (Authors.Count > 0)
                    {
                        full_name = Authors[0].Name(AuthorNamePrintStyle.LastF) + " - ";
                    }

                    // Add name.
                    full_name = full_name + Name;

                    // Year.
                    if (Year > 0)
                    {
                        full_name += String.Format(" ({0})", Year);
                    }

                    // Edition.
                    if (Edition > 0)
                    {
                        full_name += String.Format(" e{0}", Edition);
                    }

                    // Number of magazine.
                    if (Type == BookType.Magazine)
                    {
                        full_name += String.Format(" n{0}", Number);
                    }

                    // Mark if is no file for book.
                    if (File == "")
                    {
                        full_name += " *";
                    }

                    return full_name;

                default:
                    Debug.Assert(false, "Unknown book full name print style.");
                    return "";
            }
        }

        /// <summary>
        /// Serialization prepare.
        /// </summary>
        public void PrepareToSerialization()
        {
            PrepareAuthorsIds();
            PreparePublishersIds();
            PrepareCategoriesIds();
        }

        /// <summary>
        /// Prepare authors identifiers.
        /// </summary>
        private void PrepareAuthorsIds()
        {
            AuthorsIds = new List<int>();

            foreach (Author author in Authors.Items)
            {
                AuthorsIds.Add(author.Id);
            }
        }

        /// <summary>
        /// Prepare publishers identifiers.
        /// </summary>
        private void PreparePublishersIds()
        {
            PublishersIds = new List<int>();

            foreach (Publisher publisher in Publishers.Items)
            {
                PublishersIds.Add(publisher.Id);
            }
        }

        /// <summary>
        /// Prepare categories identifiers.
        /// </summary>
        private void PrepareCategoriesIds()
        {
            CategoriesIds = new List<int>();

            foreach (MPTTTree category in Categories.Items)
            {
                CategoriesIds.Add(category.Id);
            }
        }

        /// <summary>
        /// Corrects lists after deserialization.
        /// <param name="authors">global authors list</param>
        /// <param name="publishers">global publishers list</param>
        /// <param name="category_root">global categories list</param>
        /// </summary>
        public void CorrectAfterDeserialization(AuthorsList authors, PublishersList publishers, MPTTTree category_root)
        {
            CorrectAuthors(authors);
            CorrectPublishers(publishers);
            CorrectCategories(category_root);
        }

        /// <summary>
        /// Correct authors after deserialization.
        /// <param name="authors">global authors list</param>
        /// </summary>
        private void CorrectAuthors(AuthorsList authors)
        {
            Authors = new AuthorsList();

            foreach (int id in AuthorsIds)
            {
                Authors.Items.Add(authors.Find(id));
            }
        }

        /// <summary>
        /// Correct publishers after deserialization.
        /// <param name="publishers">global publishers list</param>
        /// </summary>
        private void CorrectPublishers(PublishersList publishers)
        {
            Publishers = new PublishersList();

            foreach (int id in PublishersIds)
            {
                Publishers.Items.Add(publishers.Find(id));
            }
        }

        /// <summary>
        /// Corrects categories after deserialization.
        /// <param name="category_root">root of categories tree</param>
        /// </summary>
        private void CorrectCategories(MPTTTree category_root)
        {
            Categories = new CategoriesList();

            foreach (int id in CategoriesIds)
            {
                Categories.Items.Add(category_root.Find(id));
            }
        }
    }
}
