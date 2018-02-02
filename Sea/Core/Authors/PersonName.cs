// Author: Alexey Rybakov

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sea.Core.Authors
{
    /// <summary>
    /// Name class.
    /// </summary>
    public class PersonName : IComparable<PersonName>, IEquatable<PersonName>, ICloneable
    {
        /// <summary>
        /// First name.
        /// </summary>
        [XmlAttribute]
        public string First { get; set; }

        /// <summary>
        /// Second name.
        /// </summary>
        [XmlAttribute]
        public string Second { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [XmlAttribute]
        public string Last { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="first">first name</param>
        /// <param name="second">second name</param>
        /// <param name="last">last name</param>
        public PersonName(string first, string second, string last)
        {
            First = first;
            Second = second;
            Last = last;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PersonName()
            : this("", "", "")
        {
        }

        /// <summary>
        /// Check if there is first name.
        /// </summary>
        public bool IsFirst
        {
            get
            {
                return First != "";
            }
        }

        /// <summary>
        /// Check if there is second name.
        /// </summary>
        public bool IsSecond
        {
            get
            {
                return Second != "";
            }
        }

        /// <summary>
        /// Check if there is last name.
        /// </summary>
        public bool IsLast
        {
            get
            {
                return Last != "";
            }
        }

        /// <summary>
        /// Check if name is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !IsFirst && !IsSecond && !IsLast;
            }
        }

        /// <summary>
        /// Check if name is not empty.
        /// </summary>
        public bool IsNotEmpty
        {
            get
            {
                return !IsEmpty;
            }
        }

        /// <summary>
        /// Extended first name.
        /// </summary>
        public string XFirst
        {
            get
            {
                return IsFirst ? First : "#";
            }
        }

        /// <summary>
        /// Extended second name.
        /// </summary>
        public string XSecond
        {
            get
            {
                return IsSecond ? Second : "#";
            }
        }

        /// <summary>
        /// Extended last name.
        /// </summary>
        public string XLast
        {
            get
            {
                return IsLast ? Last : "#";
            }
        }

        /// <summary>
        /// First character of extended first name.
        /// </summary>
        public char XFirst0
        {
            get
            {
                return XFirst[0];
            }
        }

        /// <summary>
        /// First character of extended second name.
        /// </summary>
        public char XSecond0
        {
            get
            {
                return XSecond[0];
            }
        }

        /// <summary>
        /// First character of extended last name.
        /// </summary>
        public char XLast0
        {
            get
            {
                return XLast[0];
            }
        }

        /// <summary>
        /// Name in format "Jackson M."
        /// </summary>
        public string FormatLastF
        {
            get
            {
                return String.Format("{0} {1}.", XLast, XFirst0);
            }
        }

        /// <summary>
        /// Name in format "Jackson M. J."
        /// </summary>
        public string FormatLastFS
        {
            get
            {
                return !IsSecond
                       ? FormatLastF
                       : String.Format("{0} {1}. {2}.", XLast, XFirst0, XSecond0);
            }            
        }

        /// <summary>
        /// Name in format "Jackson, Michael"
        /// </summary>
        public string FormatLastFirst
        {
            get
            {
                return String.Format("{0}, {1}", XLast, XFirst);
            }
        }

        /// <summary>
        /// Name in format "Jackson, Michael Joseph"
        /// </summary>
        public string FormatLastFirstSecond
        {
            get
            {
                return !IsSecond
                       ? FormatLastFirst
                       : String.Format("{0}, {1} {2}", XLast, XFirst, XSecond);
            }
        }

        /// <summary>
        /// Compare name to another name.
        /// </summary>
        /// <param name="pn">person name to compare</param>
        /// <returns>1 - if greater, -1 - if less, 0 - if equal</returns>
        public int CompareTo(PersonName pn)
        {
            if (pn == null)
            {
                return 1;
            }

            int last_compare = Last.CompareTo(pn.Last);

            if (last_compare != 0)
            {
                return last_compare;
            }
            else
            {
                int first_compare = First.CompareTo(pn.First);

                if (first_compare != 0)
                {
                    return first_compare;
                }
                else
                {
                    return Second.CompareTo(pn.Second);
                }
            }
        }

        /// <summary>
        /// Check equals.
        /// </summary>
        /// <param name="pn">another name</param>
        /// <returns><c>true</c> - if names are equal, <c>false</c> - otherwise</returns>
        public bool Equals(PersonName pn)
        {
            return CompareTo(pn) == 0;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone object</returns>
        public object Clone()
        {
            return new PersonName(First, Second, Last);
        }

        /// <summary>
        /// Copy of person name.
        /// </summary>
        public PersonName Copy
        {
            get
            {
                return Clone() as PersonName;
            }
        }
    }
}
