// Author: Alexey Rybakov

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

using Sea.Tools;

namespace Sea.Core.Publishers
{
    /// <summary>
    /// Publishers list class.
    /// </summary>
    public class PublishersList : ICloneable
    {
        /// <summary>
        /// List of publishers.
        /// </summary>
        public List<Publisher> Items { get; set; }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>publisher</returns>
        public Publisher this[int i]
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
        public PublishersList()
        {
            Items = new List<Publisher>();
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
        /// Get max identifier of publisher.
        /// </summary>
        /// <returns>max identifier</returns>
        private int MaxId()
        {
            int id = InnerParameters.MinimalObjectId - 1;

            foreach (Publisher publisher in Items)
            {
                id = Math.Max(id, publisher.Id);
            }

            return id;
        }

        /// <summary>
        /// Adding new publisher.
        /// </summary>
        /// <param name="publisher">new publisher</param>
        public void Add(Publisher publisher)
        {
            if (publisher.Id < 0)
            {
                publisher.Id = MaxId() + 1;
            }

            Items.Add(publisher);
        }

        /// <summary>
        /// Remove given element.
        /// </summary>
        /// <param name="i">index</param>
        public void RemoveAt(int i)
        {
            Items.RemoveAt(i);
        }

        /// <summary>
        /// Sort list of publishers.
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
        static public PublishersList XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(PublishersList));
                PublishersList collection = serializer.Deserialize(reader) as PublishersList;

                reader.Close();

                return collection;
            }
            catch (IOException)
            {
                return null;
            }
        }

        /// <summary>
        /// Fill list box with publishers.
        /// </summary>
        /// <param name="lb">list box</param>
        public void ToListBox(ListBox lb)
        {
            lb.Items.Clear();

            for (int i = 0; i < Count; i++)
            {
                lb.Items.Add(Items[i].Name);
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy of object</returns>
        public object Clone()
        {
            PublishersList authors_list = new PublishersList();

            for (int i = 0; i < Count; i++)
            {
                authors_list.Items.Add(Items[i].Clone() as Publisher);
            }

            return authors_list;
        }

        /// <summary>
        /// Find publisher by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>publisher</returns>
        public Publisher Find(int id)
        {
            return Items.Find((publisher) => publisher.Id == id);
        }
    }
}
