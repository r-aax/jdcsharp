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
        /// Russian name.
        /// </summary>
        public PersonName RusName
        {
            get
            {
                return new PersonName(RusFirstName, RusSecondName, RusLastName);
            }
        }

        /// <summary>
        /// English name.
        /// </summary>
        public PersonName EngName
        {
            get
            {
                return new PersonName(EngFirstName, EngSecondName, EngLastName);
            }
        }

        /// <summary>
        /// Check if author has russian name.
        /// </summary>
        public bool HasRus
        {
            get
            {
                return RusName.IsNotEmpty;
            }
        }
        
        /// <summary>
        /// Check if author has english name.
        /// </summary>
        public bool HasEng
        {
            get
            {
                return EngName.IsNotEmpty;
            }
        }

        /// <summary>
        /// Check if author has name set in any language.
        /// </summary>
        public bool HasAnyLanguage
        {
            get
            {
                return HasRus || HasEng;
            }
        }

        /// <summary>
        /// Get name.
        /// </summary>
        /// <param name="style">style of name print</param>
        /// <returns>name</returns>
        public string Name(AuthorNamePrintStyle style)
        {
            switch (style)
            {
                case AuthorNamePrintStyle.RusLastFS:
                    return RusName.FormatLastFS;

                case AuthorNamePrintStyle.RusLastFirstSecond:
                    return RusName.FormatLastFirstSecond;

                case AuthorNamePrintStyle.EngLastFS:
                    return EngName.FormatLastFS;

                case AuthorNamePrintStyle.EngLastFirstSecond:
                    return EngName.FormatLastFirstSecond;

                case AuthorNamePrintStyle.BothLastFS:

                    if (HasRus)
                    {
                        if (HasEng)
                        {
                            // Both names.
                            return String.Format("{0} / {1}",
                                                 RusName.FormatLastFS,
                                                 EngName.FormatLastFS);
                        }
                        else
                        {
                            // Return only russian name.
                            return RusName.FormatLastFS;
                        }
                    }
                    else
                    {
                        // Try to return english name.
                        return EngName.FormatLastFS;
                    }

                case AuthorNamePrintStyle.BothLastFirstSecond:

                    if (HasRus)
                    {
                        if (HasEng)
                        {
                            // Both names.
                            return String.Format("{0} / {1}",
                                                 RusName.FormatLastFirstSecond,
                                                 EngName.FormatLastFirstSecond);
                        }
                        else
                        {
                            // Return only russian name.
                            return RusName.FormatLastFirstSecond;
                        }
                    }
                    else
                    {
                        // Try to return english name.
                        return EngName.FormatLastFirstSecond;
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
