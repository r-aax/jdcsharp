// Author: Alexey Rybakov

using System.Drawing;

namespace Sea.Tools
{
    /// <summary>
    /// Parameters.
    /// </summary>
    class Parameters
    {
        /// <summary>
        /// Path to storage.
        /// </summary>
        public static readonly string StoragePath = "C:/Data/Garbage/Sea";

        /// <summary>
        /// Categories tree XML file.
        /// </summary>
        public static readonly string CategoriesTreeXMLFilename = "categories_tree.xml";

        /// <summary>
        /// Full name of categories tree XML file.
        /// </summary>
        public static string CategoriesTreeXMLFullFilename
        {
            get
            {
                return StoragePath + "/" + CategoriesTreeXMLFilename;
            }
        }

        /// <summary>
        /// Authors XML file.
        /// </summary>
        public static readonly string AuthorsXMLFilename = "authors.xml";

        /// <summary>
        /// Full name of authors XML file.
        /// </summary>
        public static string AuthorsXMLFullFilename
        {
            get
            {
                return StoragePath + "/" + AuthorsXMLFilename;
            }
        }

        /// <summary>
        /// Publishers XML file.
        /// </summary>
        public static readonly string PublishersXMLFilename = "publishers.xml";

        /// <summary>
        /// Full name of publishers XML file.
        /// </summary>
        public static string PublishersXMLFullFilename
        {
            get
            {
                return StoragePath + "/" + PublishersXMLFilename;
            }
        }

        /// <summary>
        /// Data directory.
        /// </summary>
        public static readonly string DataDirectory = "Data";

        /// <summary>
        /// Full path to data directory.
        /// </summary>
        public static string DataPath
        {
            get
            {
                return StoragePath + "/" + DataDirectory;
            }
        }

        /// <summary>
        /// Full path to data file.
        /// </summary>
        /// <param name="file_name">name of file</param>
        /// <returns>full path</returns>
        public static string Data(string file_name)
        {
            return DataPath + "/" + file_name;
        }

        /// <summary>
        /// Select color.
        /// </summary>
        public static Color SelectColor = Color.Chocolate;
    }
}
