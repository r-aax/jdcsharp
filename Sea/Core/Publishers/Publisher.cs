// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Sea.Core.Publishers
{
    /// <summary>
    /// Publisher class.
    /// </summary>
    public class Publisher : IComparable<Publisher>, ICloneable
    {
        /// <summary>
        /// Name of publisher.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name</param>
        public Publisher(string name)
        {
            Name = name;
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
        /// Publisher cloning.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new Publisher(Name);
        }
    }
}
