// Author: Alexey Rybakov

using System.Collections.Generic;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Lib.DataStruct
{
    /// <summary>
    /// List of strings.
    /// </summary>
    public class StringsList
    {
        /// <summary>
        /// List elements.
        /// </summary>
        public List<string> Items;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StringsList()
        {
            Items = new List<string>();
        }

        /// <summary>
        /// Create from ListBox.
        /// </summary>
        /// <param name="list_box"><c>ListBox object</c></param>
        public StringsList(ListBox list_box)
            : this()
        {
            for (int i = 0; i < list_box.Items.Count; i++)
            {
                Add(list_box.Items[i] as string);
            }
        }

        /// <summary>
        /// Count of elements.
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// Get element with given index.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>element</returns>
        public string this[int index]
        {
            get
            {
                return Items[index];
            }
        }

        /// <summary>
        /// Add string.
        /// </summary>
        /// <param name="value">string</param>
        public void Add(string value)
        {
            Items.Add(value);
        }

        /// <summary>
        /// Fill <c>ListBox</c> with strings elements.
        /// </summary>
        /// <param name="list_box"><c>ListBox</c></param>
        public void FillListBox(ListBox list_box)
        {
            list_box.Items.Clear();

            for (int i = 0; i < Count; i++)
            {
                list_box.Items.Add(Items[i]);
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
        /// <returns>list</returns>
        static public StringsList XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(StringsList));
                StringsList list = serializer.Deserialize(reader) as StringsList;

                reader.Close();

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
