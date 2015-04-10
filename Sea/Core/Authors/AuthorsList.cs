// Author: Alexey Rybakov

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Authors list class.
    /// </summary>
    public class AuthorsList
    {
        /// <summary>
        /// List of authors.
        /// </summary>
        public List<Author> Items = new List<Author>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthorsList()
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
    }
}
