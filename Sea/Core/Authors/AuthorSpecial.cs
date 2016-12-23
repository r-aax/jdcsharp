using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Author class (special version for serialization).
    /// </summary>
    [XmlType("Author")]
    public class AuthorSpecial
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// First russian name of author.
        /// </summary>
        [XmlAttribute]
        public string RusFirstName { get; set; }

        /// <summary>
        /// Second russian name of author.
        /// </summary>
        [XmlAttribute]
        public string RusSecondName { get; set; }

        /// <summary>
        /// Last russian name of author.
        /// </summary>
        [XmlAttribute]
        public string RusLastName { get; set; }

        /// <summary>
        /// First english name of author.
        /// </summary>
        [XmlAttribute]
        public string EngFirstName { get; set; }

        /// <summary>
        /// Second english name of author.
        /// </summary>
        [XmlAttribute]
        public string EngSecondName { get; set; }

        /// <summary>
        /// Last english name of author.
        /// </summary>
        [XmlAttribute]
        public string EngLastName { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public AuthorSpecial()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">author</param>
        public AuthorSpecial(Author a)
        {
            Id = a.Id;
            RusFirstName = a.RusFirstName;
            RusSecondName = a.RusSecondName;
            RusLastName = a.RusLastName;
            EngFirstName = a.EngFirstName;
            EngSecondName = a.EngSecondName;
            EngLastName = a.EngLastName;
        }
    }
}
