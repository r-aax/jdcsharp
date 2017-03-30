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
    }
}
