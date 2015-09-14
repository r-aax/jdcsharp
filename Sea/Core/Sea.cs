// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.DataStruct;
using Sea.Core.Authors;
using Sea.Core.Publishers;
using Sea.Core.Categories;
using Sea.Core.Books;
using Sea.Tools;

namespace Sea.Core
{
    /// <summary>
    /// Sea class.
    /// </summary>
    public class Sea
    {
        /// <summary>
        /// Authors list.
        /// </summary>
        public AuthorsList Authors;

        /// <summary>
        /// Publishers list.
        /// </summary>
        public PublishersList Publishers;

        /// <summary>
        /// Category root.
        /// </summary>
        public MPTTTree CategoryRoot;

        /// <summary>
        /// Books list.
        /// </summary>
        public BooksList Books;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Sea()
        {
        }

        /// <summary>
        /// Deserialize sea.
        /// </summary>
        public void Deserialize()
        {
            Authors = AuthorsList.XmlDeserialize(Parameters.AuthorsXMLFullFilename);

            if (Authors == null)
            {
                Authors = new AuthorsList();
            }

            Publishers = PublishersList.XmlDeserialize(Parameters.PublishersXMLFullFilename);

            if (Publishers == null)
            {
                Publishers = new PublishersList();
            }

            CategoryRoot = MPTTTree.XmlDeserialize(Parameters.CategoriesTreeXMLFullFilename);

            if (CategoryRoot == null)
            {
                // Empty tree.
                CategoryRoot = new MPTTTree(" ");
            }

            Books = BooksList.XmlDeserialize(Parameters.BooksXMLFullFilename, Authors, Publishers, CategoryRoot);

            if (Books == null)
            {
                Books = new BooksList();
            }
        }
    }
}
