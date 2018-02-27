using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib.IO
{
    /// <summary>
    /// Object for file manipulations.
    /// </summary>
    public class File : ICloneable
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Extension.
        /// </summary>
        public string Ext
        {
            get
            {
                return System.IO.Path.GetExtension(Name);
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public File()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name</param>
        public File(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Constructor from file dialog.
        /// </summary>
        /// <param name="fd">file dialog</param>
        public File(FileDialog fd)
            : this(fd.FileName)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="f">file</param>
        public File(File f)
            : this(f.Name)
        {
        }

        /// <summary>
        /// Check if extension lowercase.
        /// Really we check only first letter.
        /// </summary>
        public bool IsLowerExt
        {
            get
            {
                return Char.IsLower(Ext[1]);
            }
        }

        /// <summary>
        /// Check if extension uppercase.
        /// Really we check only first letter.
        /// </summary>
        public bool IsUpperExt
        {
            get
            {
                return Char.IsUpper(Ext[1]);
            }
        }

        /// <summary>
        /// Change extension.
        /// </summary>
        /// <param name="ext">extension</param>
        public void ChangeExtensionCaseSensitive(string ext)
        {
            Name = Name.Replace(Ext, IsUpperExt ? ext.ToUpper() : ext.ToLower());
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new File(Name);
        }

        /// <summary>
        /// Copy.
        /// </summary>
        /// <returns>copy</returns>
        public File Copy()
        {
            return Clone() as File;
        }

        /// <summary>
        /// Check is file exists.
        /// </summary>
        /// <returns><c>true</c> - if file exists, <c>false</c> - otherwise</returns>
        public bool ConsoleExistCheck()
        {
            bool res = true;

            if (!System.IO.File.Exists(Name))
            {
                Console.WriteLine(String.Format("error : file {0} doesn't exist", Name));

                res = false;
            }

            return res;
        }
    }
}
