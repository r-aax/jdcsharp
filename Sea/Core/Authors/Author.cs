// Author: Alexey Rybakov

using System;
using System.Diagnostics;
using System.Xml.Serialization;

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
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// First russian name of author.
        /// </summary>
        [XmlAttribute]
        public string RusFirstName { get; set; }

        /// <summary>
        /// Second russian name of author.
        /// </summary>
        [XmlAttribute]
        public string RusSecondName { get; set; }

        /// <summary>
        /// Last russian name of author.
        /// </summary>
        [XmlAttribute]
        public string RusLastName { get; set; }

        /// <summary>
        /// First english name of author.
        /// </summary>
        [XmlAttribute]
        public string EngFirstName { get; set; }

        /// <summary>
        /// Second english name of author.
        /// </summary>
        [XmlAttribute]
        public string EngSecondName { get; set; }

        /// <summary>
        /// Last english name of author.
        /// </summary>
        [XmlAttribute]
        public string EngLastName { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rus_first_name">russian first name</param>
        /// <param name="rus_second_name">russian second name</param>
        /// <param name="rus_last_name">russian last name</param>
        /// <param name="eng_first_name">english first name</param>
        /// <param name="eng_last_name">english second name</param>
        /// <param name="eng_second_name">english last name</param>
        public Author(string rus_first_name, string rus_second_name, string rus_last_name,
                      string eng_first_name, string eng_second_name, string eng_last_name)
        {
            Id = -1;
            RusFirstName = rus_first_name;
            RusSecondName = rus_second_name;
            RusLastName = rus_last_name;
            EngFirstName = eng_first_name;
            EngSecondName = eng_second_name;
            EngLastName = eng_last_name;
        }

        /// <summary>
        /// Constructor (first of all for serialization).
        /// </summary>
        public Author()
            : this("", "", "", "", "", "")
        {
        }

        /// <summary>
        /// Check if author has russian name.
        /// </summary>
        /// <returns>true - if has, false - otherwise.</returns>
        public bool HasRus()
        {
            return (RusFirstName != "") && (RusLastName != "");
        }
        
        /// <summary>
        /// Check if author has english name.
        /// </summary>
        /// <returns>true - if has, false - otherwise.</returns>
        public bool HasEng()
        {
            return (EngFirstName != "") && (EngLastName != "");
        }

        /// <summary>
        /// Check if author has name set in any language.
        /// </summary>
        /// <returns>true - if has name, false - otherwise</returns>
        public bool HasAnyLanguage()
        {
            return HasRus() || HasEng();
        }

        /// <summary>
        /// Name in format Jackson M.
        /// </summary>
        /// <param name="first_name">first name</param>
        /// <param name="last_name">second name</param>
        /// <returns>name in given format</returns>
        public static string NameLastF(string first_name, string last_name)
        {
            Debug.Assert((first_name != "") && (last_name != ""), "name is not set in chosen language");

            return String.Format("{0} {1}.", last_name, first_name[0]);
        }

        /// <summary>
        /// Name in format Jacksom M. J.
        /// </summary>
        /// <param name="first_name">first name</param>
        /// <param name="second_name">second name</param>
        /// <param name="last_name">last name</param>
        /// <returns>name in given format</returns>
        public static string NameLastFS(string first_name, string second_name, string last_name)
        {
            if (second_name == "")
            {
                return NameLastF(first_name, last_name);
            }
            else
            {
                return String.Format("{0} {1}. {2}.", last_name, first_name[0], second_name[0]);
            }
        }

        /// <summary>
        /// Name in format Jackson, Michael.
        /// </summary>
        /// <param name="first_name">first name</param>
        /// <param name="last_name">second name</param>
        /// <returns>name in given format</returns>
        public static string NameLastFirst(string first_name, string last_name)
        {
            Debug.Assert((first_name != "") && (last_name != ""), "name is not set in chosen language");

            return String.Format("{0}, {1}", last_name, first_name);
        }

        /// <summary>
        /// Name in format Jackson, Michael Joseph.
        /// </summary>
        /// <param name="first_name">first name</param>
        /// <param name="second_name">second name</param>
        /// <param name="last_name">last name</param>
        /// <returns>name in given format</returns>
        public static string NameLastFirstSecond(string first_name, string second_name, string last_name)
        {
            if (second_name == "")
            {
                return NameLastFirst(first_name, last_name);
            }
            else
            {
                return String.Format("{0}, {1} {2}", last_name, first_name, second_name);
            }
        }

        /// <summary>
        /// Get name.
        /// </summary>
        /// <param name="style">style of name print</param>
        /// <returns>name</returns>
        public string Name(AuthorNamePrintStyle style)
        {
            bool has_rus = false;
            bool has_eng = false;

            switch (style)
            {
                case AuthorNamePrintStyle.RusLastFS:
                    return NameLastFS(RusFirstName, RusSecondName, RusLastName);

                case AuthorNamePrintStyle.RusLastFirstSecond:
                    return NameLastFirstSecond(RusFirstName, RusSecondName, RusLastName);

                case AuthorNamePrintStyle.EngLastFS:
                    return NameLastFS(EngFirstName, EngSecondName, EngLastName);

                case AuthorNamePrintStyle.EngLastFirstSecond:
                    return NameLastFirstSecond(EngFirstName, EngSecondName, EngLastName);

                case AuthorNamePrintStyle.BothLastFS:

                    has_rus = HasRus();
                    has_eng = HasEng();

                    if (has_rus)
                    {
                        if (has_eng)
                        {
                            // Both names.
                            return String.Format("{0} / {1}",
                                                 Name(AuthorNamePrintStyle.RusLastFS),
                                                 Name(AuthorNamePrintStyle.EngLastFS));
                        }
                        else
                        {
                            // Return only russian name.
                            return Name(AuthorNamePrintStyle.RusLastFS);
                        }
                    }
                    else
                    {
                        // Try to return english name.
                        return Name(AuthorNamePrintStyle.EngLastFS);
                    }

                case AuthorNamePrintStyle.BothLastFirstSecond:

                    has_rus = HasRus();
                    has_eng = HasEng();

                    if (has_rus)
                    {
                        if (has_eng)
                        {
                            // Both names.
                            return String.Format("{0} / {1}",
                                                 Name(AuthorNamePrintStyle.RusLastFirstSecond),
                                                 Name(AuthorNamePrintStyle.EngLastFirstSecond));
                        }
                        else
                        {
                            // Return only russian name.
                            return Name(AuthorNamePrintStyle.RusLastFirstSecond);
                        }
                    }
                    else
                    {
                        // Try to return english name.
                        return Name(AuthorNamePrintStyle.EngLastFirstSecond);
                    }

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

            string author_name = author.Name(AuthorNamePrintStyle.BothLastFirstSecond);
            int compare = Name(AuthorNamePrintStyle.BothLastFirstSecond).CompareTo(author_name);

            if (compare > 0)
            {
                return 1;
            }
            else if (compare < 0)
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
            Author author = new Author(RusFirstName, RusSecondName, RusLastName,
                                       EngFirstName, EngSecondName, EngLastName);

            author.Id = Id;

            return author;
        }
    }
}
