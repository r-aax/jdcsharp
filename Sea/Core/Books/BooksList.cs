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
        public List<Book> Items;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BooksList()
        {
            Items = new List<Book>();
        }

        /// <summary>
        /// Fill listbox with books information.
        /// </summary>
        /// <param name="lb"></param>
        public void ToListBox(ListBox lb)
        {
            lb.Items.Clear();
            lb.Items.Add("TODO");
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
