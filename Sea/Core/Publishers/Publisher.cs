// Author: Alexey Rybakov

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sea.Core.Publishers
{
    /// <summary>
    /// Publisher class.
    /// </summary>
    public class Publisher : IComparable<Publisher>, IEquatable<Publisher>, ICloneable
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
        /// Constructor.
        /// </summary>
        /// <param name="name">name</param>
        public Publisher(string name)
        {
            Id = -1;
            Name = name;
        }

        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public Publisher()
            : this("")
        {
        }

        /// <summary>
        /// Compare to another publisher.
        /// </summary>
        /// <param name="publisher">publisher to compare</param>
        /// <returns>1 - if greater, -1 - if less, 0 - if equal</returns>
        public int CompareTo(Publisher publisher)
        {
            if (publisher == null)
            {
                return 1;
            }

            int compare_name = Name.CompareTo(publisher.Name);

            if (compare_name > 0)
            {
                return 1;
            }
            else if (compare_name < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Check equals.
        /// </summary>
        /// <param name="publisher">another publisher</param>
        /// <returns><c>true</c> - if publishers are equal, <c>false</c> - if publishers are not equal</returns>
        public bool Equals(Publisher publisher)
        {
            return Id == publisher.Id;
        }

        /// <summary>
        /// Publisher cloning.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            Publisher publisher = new Publisher(Name);

            publisher.Id = Id;

            return publisher;
        }
    }
}
