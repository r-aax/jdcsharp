// Author: Alexey Rybakov

using System.Collections.Generic;

namespace Sea.Core
{
    /// <summary>
    /// Data item.
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name = null;

        /// <summary>
        /// File name.
        /// </summary>
        public string Filename = null;

        /// <summary>
        /// Categories list.
        /// </summary>
        public List<int> CategoriesIds = new List<int>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="filename">name of file</param>
        public DataItem(string name, string filename)
        {
            Name = name;
            Filename = filename;
        }
    }
}
