using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Utils
{
    /// <summary>
    /// Strings manipulations.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Count of leading symbols.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="c">character</param>
        /// <returns>count of leading symbols</returns>
        public static int LeadingSymbolsCount(string s, char c)
        {
            int r = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                {
                    r++;
                }
            }

            return r;
        }

        /// <summary>
        /// Check if string contains digits.
        /// </summary>
        /// <param name="str">string</param>
        /// <returns><c>true</c> - if string contains only digits, <c>false</c> - otherwise</returns>
        public static bool IsDigits(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsDigit(str[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check empty string.
        /// </summary>
        /// <param name="str">string</param>
        /// <returns><c>truc</c> - if it is empty, <c>fasle</c> - otherwise</returns>
        public static bool IsEmpty(string str)
        {
            if (str == "")
            {
                return true;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsWhiteSpace(str[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
