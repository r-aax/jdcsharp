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
        /// Russian name.
        /// </summary>
        public PersonName RusName;

        /// <summary>
        /// English name.
        /// </summary>
        public PersonName EngName;

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
            RusName = a.RusName.Copy;
            EngName = a.EngName.Copy;
        }
    }
}
