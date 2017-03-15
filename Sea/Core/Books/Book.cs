// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

using Lib.DataStruct;
using Sea.Core.Authors;
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
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Type of book.
        /// </summary>
        [XmlAttribute]
        public BookType Type { get; set; }

        /// <summary>
        /// Keywords.
        /// </summary>
        [XmlAttribute]
        public string Keywords { get; set; }

        /// <summary>
        /// Edition number.
        /// If 0 - no edition information.
        /// </summary>
        [XmlAttribute]
        public int Edition { get; set; }

        /// <summary>
        /// Year of publishing.
        /// If 0 - no year information.
        /// </summary>
        [XmlAttribute]
        public int Year { get; set; }

        /// <summary>
        /// Book file reference.
        /// </summary>
        [XmlAttribute]
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
        /// <param name="keywords">keywords</param>
        /// <param name="edition">edition number</param>
        /// <param name="year">year</param>
        /// <param name="file">file</param>
        public Book(string name, BookType type, string keywords,
                    int edition, int year, string file)
        {
            Id = -1;
            Name = name;
            Type = type;
            Keywords = keywords;
            Edition = edition;
            Year = year;
            File = file;
            Authors = new AuthorsList();
            Categories = new CategoriesList();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book()
            : this("", BookType.Book, "", 0, 0, "")
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
            book.Keywords = Keywords;
            book.Edition = Edition;
            book.Year = Year;
            book.File = File;
            book.Authors = Authors.Clone() as AuthorsList;
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
                        full_name = Authors[0].Name(AuthorNamePrintStyle.BothLastFS);

                        for (int i = 1; i < Authors.Count; i++)
                        {
                            full_name = full_name + "; " + Authors[i].Name(AuthorNamePrintStyle.BothLastFS);
                        }

                        full_name = full_name + " - ";
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
        /// Book type - string form.
        /// </summary>
        public string TypeString
        {
            get
            {
                switch (Type)
                {
                    case BookType.Book:
                        return "Book";

                    case BookType.Magazine:
                        return "Magazine";

                    case BookType.Article:
                        return "Article";

                    case BookType.Other:
                        return "Other";

                    default:
                        Debug.Assert(false, "Unknown book type.");
                        return "";
                }
            }
        }

        /// <summary>
        /// Serialization prepare.
        /// </summary>
        public void PrepareToSerialization()
        {
            PrepareAuthorsIds();
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
        /// <param name="category_root">global categories list</param>
        /// </summary>
        public void CorrectAfterDeserialization(AuthorsList authors, MPTTTree category_root)
        {
            CorrectAuthors(authors);
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
