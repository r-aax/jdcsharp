using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sea.Core.Publishers
{
    /// <summary>
    /// Publisher class.
    /// </summary>
    [XmlType("Publisher")]
    public class PublisherSpecial
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Name of publisher.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public PublisherSpecial()
        {
        }

        /// <summary>
        /// Constructor from publisher.
        /// </summary>
        /// <param name="p"></param>
        public PublisherSpecial(Publisher p)
        {
            Id = p.Id;
            Name = p.Name;
        }
    }
}
