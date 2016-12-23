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
    [XmlType("PublishersList")]
    public class PublishersListSpecial
    {
        /// <summary>
        /// List of publishers.
        /// </summary>
        public List<PublisherSpecial> Items { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public PublishersListSpecial()
        {
        }

        /// <summary>
        /// Constructor from list of publishers.
        /// </summary>
        /// <param name="pl">publishers list</param>
        public PublishersListSpecial(PublishersList pl)
        {
            Items = new List<PublisherSpecial>();

            for (int i = 0; i < pl.Count; i++)
            {
                Items.Add(new PublisherSpecial(pl[i]));
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
