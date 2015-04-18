// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace Sea.Core.Books
{
    /// <summary>
    /// List of books.
    /// </summary>
    public class BooksList
    {
        /// <summary>
        /// Books items.
        /// </summary>
        public List<Book> Items { get; set; }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>element</returns>
        public Book this[int i]
        {
            get
            {
                return Items[i];
            }

            set
            {
                Items[i] = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BooksList()
        {
            Items = new List<Book>();
        }

        /// <summary>
        /// Add new book.
        /// </summary>
        /// <param name="book">new book</param>
        public void Add(Book book)
        {
            Items.Add(book);
        }

        /// <summary>
        /// Sorting books list.
        /// </summary>
        public void Sort()
        {
            Items.Sort();
        }

        /// <summary>
        /// Fill listbox with books information.
        /// </summary>
        /// <param name="lb"></param>
        public void ToListBox(ListBox lb)
        {
            lb.Items.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                lb.Items.Add(Items[i].Name);
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

        /// <summary>
        /// Deserialization.
        /// </summary>
        /// <param name="file_name">name of file</param>
        /// <returns>data items collection</returns>
        static public BooksList XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(BooksList));
                BooksList books = serializer.Deserialize(reader) as BooksList;

                reader.Close();

                return books;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
