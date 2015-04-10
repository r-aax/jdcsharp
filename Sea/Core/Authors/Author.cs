// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Author class.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// First name of author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Second name of author.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Last Name of author.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="first_name">first name</param>
        /// <param name="second_name">second name</param>
        /// <param name="last_name">last name</param>
        public Author(string first_name, string second_name, string last_name)
        {
            FirstName = first_name;
            SecondName = second_name;
            LastName = last_name;
        }

        /// <summary>
        /// Constructor by first and last names.
        /// </summary>
        /// <param name="first_name">first name</param>
        /// <param name="last_name">last name</param>
        public Author(string first_name, string last_name)
            : this(first_name, "", last_name)
        {
        }

        /// <summary>
        /// Get name.
        /// </summary>
        /// <param name="style">style of name print</param>
        /// <returns>name</returns>
        public string Name(AuthorNamePrintStyle style)
        {
            Debug.Assert((FirstName != "") && (LastName != ""), "unknown author first or last name");

            switch (style)
            {
                case AuthorNamePrintStyle.FirstSecondLast:

                    if (SecondName != "")
                    {
                        return String.Format("{0} {1} {2}", FirstName, SecondName, LastName);
                    }
                    else
                    {
                        return Name(AuthorNamePrintStyle.FistLast);
                    }

                case AuthorNamePrintStyle.FirstSLast:

                    if (SecondName != "")
                    {
                        return String.Format("{0} {1}. {2}", FirstName, SecondName[0], LastName);
                    }
                    else
                    {
                        return Name(AuthorNamePrintStyle.FistLast);
                    }

                case AuthorNamePrintStyle.FistLast:
                    return String.Format("{0} {1}", FirstName, LastName);

                case AuthorNamePrintStyle.LastFS:

                    if (SecondName != "")
                    {
                        return String.Format("{0} {1}. {2}.", LastName, FirstName[0], SecondName[0]);
                    }
                    else
                    {
                        return Name(AuthorNamePrintStyle.LastF);
                    }

                case AuthorNamePrintStyle.LastF:
                    return String.Format("{0} {1}.", LastName, FirstName[0]);

                default:
                    Debug.Assert(false, "unknown author name print style");
                    return "";
            }
        }
    }
}
