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
        public static readonly string StoragePath = "D:/SeaStorage";

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
