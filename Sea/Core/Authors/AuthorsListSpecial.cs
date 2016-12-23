using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

using Sea.Tools;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Authors list class.
    /// </summary>
    [XmlType("AuthorsList")]
    public class AuthorsListSpecial
    {
        /// <summary>
        /// List of authors.
        /// </summary>
        public List<AuthorSpecial> Items { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public AuthorsListSpecial()
        {
        }

        /// <summary>
        /// List of authors for serialization.
        /// </summary>
        /// <param name="al">authors list</param>
        public AuthorsListSpecial(AuthorsList al)
        {
            Items = new List<AuthorSpecial>();
            
            foreach (Author a in al.Items)
            {
                Items.Add(new AuthorSpecial(a));
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
