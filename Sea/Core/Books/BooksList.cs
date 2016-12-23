// Author: Alexey Rybakov

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

using Lib.DataStruct;
using Sea.Tools;
using Sea.Core.Authors;
using Sea.Core.Publishers;
using Sea.Core.Categories;

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
        /// Count of items.
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// Check if empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        /// <summary>
        /// Get max identifier of book.
        /// </summary>
        /// <returns>max identifier</returns>
        private int MaxId()
        {
            int id = InnerParameters.MinimalObjectId - 1;

            foreach (Book book in Items)
            {
                id = Math.Max(id, book.Id);
            }

            return id;
        }

        /// <summary>
        /// Add new book.
        /// </summary>
        /// <param name="book">new book</param>
        public void Add(Book book)
        {
            if (book.Id < 0)
            {
                book.Id = MaxId() + 1;
            }

            Items.Add(book);
        }

        /// <summary>
        /// Sorting books list.
        /// </summary>
        public void Sort()
        {
            Items.Sort((b1, b2) => b1.FullName(BookFullNamePrintStyle.Wide).CompareTo(b2.FullName(BookFullNamePrintStyle.Wide)));
        }

        /// <summary>
        /// Prepare to serialization.
        /// </summary>
        public void PrepareToSerialization()
        {
            foreach (Book b in Items)
            {
                b.PrepareToSerialization();
            }
        }

        /// <summary>
        /// Serialization.
        /// </summary>
        /// <param name="file_name">name of file</param>
        public void XmlSerialize(string file_name)
        {
            PrepareToSerialization();

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
        /// <param name="authors">global authors list</param>
        /// <param name="publishers">global publishers list</param>
        /// <param name="category_root">root of categories tree</param>
        /// <returns>data items collection</returns>
        static public BooksList XmlDeserialize(string file_name, AuthorsList authors, PublishersList publishers, MPTTTree category_root)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(BooksList));
                BooksList books = serializer.Deserialize(reader) as BooksList;

                reader.Close();

                foreach (Book book in books.Items)
                {
                    book.CorrectAfterDeserialization(authors, publishers, category_root);
                }

                return books;
            }
            catch (IOException)
            {
                return null;
            }
        }

        /// <summary>
        /// Fill listbox with books information.
        /// </summary>
        /// <param name="lb">list box</param>
        public void ToListBox(ListBox lb)
        {
            lb.Items.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                lb.Items.Add(Items[i].FullName(BookFullNamePrintStyle.Wide));
            }
        }

        /// <summary>
        /// Delete extra categories.
        /// </summary>
        public void DeleteExtraCategories()
        {
            foreach (Book b in Items)
            {
                b.Categories.DeleteExtra();
            }
        }

        /// <summary>
        /// Find book by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>book</returns>
        public Book FindById(int id)
        {
            foreach (Book b in Items)
            {
                if (b.Id == id)
                {
                    return b;
                }
            }

            return null;
        }
    }
}
