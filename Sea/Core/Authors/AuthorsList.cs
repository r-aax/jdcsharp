// Author: Alexey Rybakov

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Authors list class.
    /// </summary>
    public class AuthorsList : ICloneable
    {
        /// <summary>
        /// List of authors.
        /// </summary>
        public List<Author> Items { get; set; }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>author</returns>
        public Author this[int i]
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
        public AuthorsList()
        {
            Items = new List<Author>();
        }

        /// <summary>
        /// Elements count.
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// Check if authors list empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        /// <summary>
        /// Get max identifier of author.
        /// </summary>
        /// <returns>max identifier</returns>
        private int MaxId()
        {
            int id = -1;

            foreach(Author author in Items)
            {
                id = Math.Max(id, author.Id);
            }            

            return id;
        }

        /// <summary>
        /// Adding new author.
        /// </summary>
        /// <param name="author">new author</param>
        public void Add(Author author)
        {
            author.Id = MaxId() + 1;
            Items.Add(author);
        }

        /// <summary>
        /// Remove item from authors list.
        /// </summary>
        /// <param name="i">index</param>
        public void RemoveAt(int i)
        {
            Items.RemoveAt(i);
        }

        /// <summary>
        /// Sort list of authors.
        /// </summary>
        public void Sort()
        {
            Items.Sort();
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
        static public AuthorsList XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(AuthorsList));
                AuthorsList collection = serializer.Deserialize(reader) as AuthorsList;

                reader.Close();

                return collection;
            }
            catch (IOException)
            {
                return null;
            }
        }

        /// <summary>
        /// Fill list box with authors.
        /// </summary>
        /// <param name="lb">list box</param>
        public void ToListBox(ListBox lb)
        {
            lb.Items.Clear();

            for (int i = 0; i < Count; i++)
            {
                lb.Items.Add(Items[i].Name(AuthorNamePrintStyle.LastFirstSecond));
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy of object</returns>
        public object Clone()
        {
            AuthorsList authors_list = new AuthorsList();

            for (int i = 0; i < Count; i++)
            {
                authors_list.Add(Items[i].Clone() as Author);
            }

            return authors_list;
        }
    }
}
