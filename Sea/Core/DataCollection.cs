// Author: Alexey Rybakov

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Sea.Core
{
    /// <summary>
    /// Data collection.
    /// </summary>
    public class DataCollection
    {
        /// <summary>
        /// List of data items.
        /// </summary>
        public List<DataItem> Items = new List<DataItem>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataCollection()
        {
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
        /// <returns><c>MPTT</c> tree</returns>
        static public DataCollection XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(DataCollection));
                DataCollection collection = serializer.Deserialize(reader) as DataCollection;

                reader.Close();

                return collection;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
