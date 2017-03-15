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
    [XmlType("Book")]
    public class BookSpecial
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
        /// Keywords
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
        /// Authors identifiers for serialization.
        /// </summary>
        public List<int> AuthorsIds;

        /// <summary>
        /// Categories identifiers for serialization.
        /// </summary>
        public List<int> CategoriesIds;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public BookSpecial()
        {
        }

        /// <summary>
        /// Constructor from book.
        /// </summary>
        /// <param name="b">book</param>
        public BookSpecial(Book b)
        {
            Id = b.Id;
            Name = b.Name;
            Type = b.Type;
            Keywords = b.Keywords;
            Edition = b.Edition;
            Year = b.Year;
            File = b.File;
            AuthorsIds = b.AuthorsIds;
            CategoriesIds = b.CategoriesIds;
        }
    }
}
