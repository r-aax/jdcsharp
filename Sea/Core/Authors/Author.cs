// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Author class.
    /// </summary>
    public class Author : IComparable<Author>, IEquatable<Author>, ICloneable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

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
        /// Constructor (first of all for serialization).
        /// </summary>
        public Author()
            : this("", "", "")
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

                case AuthorNamePrintStyle.LastFirstSecond:

                    if (SecondName != "")
                    {
                        return String.Format("{0}, {1} {2}", LastName, FirstName, SecondName);
                    }
                    else
                    {
                        return Name(AuthorNamePrintStyle.LastFirst);
                    }

                case AuthorNamePrintStyle.LastFirstS:

                    if (SecondName != "")
                    {
                        return String.Format("{0}, {1} {2}.", LastName, FirstName, SecondName[0]);
                    }
                    else
                    {
                        return Name(AuthorNamePrintStyle.LastFirst);
                    }

                case AuthorNamePrintStyle.LastFirst:
                    return String.Format("{0}, {1}", LastName, FirstName);

                default:
                    Debug.Assert(false, "unknown author name print style");
                    return "";
            }
        }

        /// <summary>
        /// Compare to another author.
        /// </summary>
        /// <param name="author">author to compare</param>
        /// <returns>1 - if greater, -1 - if less, 0 - if equal</returns>
        public int CompareTo(Author author)
        {
            if (author == null)
            {
                return 1;
            }

            int compare_last = LastName.CompareTo(author.LastName);

            if (compare_last > 0)
            {
                return 1;
            }
            else if (compare_last < 0)
            {
                return -1;
            }
            else
            {
                int compare_first = FirstName.CompareTo(author.FirstName);

                if (compare_first > 0)
                {
                    return 1;
                }
                else if (compare_first < 0)
                {
                    return -1;
                }
                else
                {
                    int compare_second = SecondName.CompareTo(author.SecondName);

                    if (compare_second > 0)
                    {
                        return 1;
                    }
                    else if (compare_second < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Check equals.
        /// </summary>
        /// <param name="author">another author</param>
        /// <returns><c>true</c> - if authors are equal, <c>false</c> - if authors are not equal</returns>
        public bool Equals(Author author)
        {
            return Id == author.Id;
        }

        /// <summary>
        /// Author cloning.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new Author(FirstName, SecondName, LastName);
        }
    }
}
