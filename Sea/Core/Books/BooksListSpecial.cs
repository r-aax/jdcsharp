using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

using Lib.DataStruct;
using Sea.Tools;
using Sea.Core.Authors;
using Sea.Core.Categories;

namespace Sea.Core.Books
{
    /// <summary>
    /// List of books.
    /// </summary>
    [XmlType("BooksList")]
    public class BooksListSpecial
    {
        /// <summary>
        /// Books items.
        /// </summary>
        public List<BookSpecial> Items { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BooksListSpecial()
        {
            Items = new List<BookSpecial>();
        }

        /// <summary>
        /// Constructor from books list.
        /// </summary>
        /// <param name="bl">books list</param>
        public BooksListSpecial(BooksList bl)
        {
            Items = new List<BookSpecial>();

            for (int i = 0; i < bl.Count; i++)
            {
                Items.Add(new BookSpecial(bl[i]));
            }
        }

        /// <summary>
        /// Serialization.
        /// </summary>
        /// <param name="file_name">name of file</param>
        public void XmlSerialize(string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            TextWriter writer = new StreamWriter(file_name);

            serializer.Serialize(writer, this);
            writer.Flush();
            writer.Close();
        }
    }
}
