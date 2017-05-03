using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Lib.Utils;

namespace Lib.DataFormats
{
    /// <summary>
    /// Calculations grid data format in PFG/IBC format.
    /// </summary>
    public class PFG
    {
        /// <summary>
        /// Count of all block nodes.
        /// </summary>
        /// <param name="i">count of nodes in I direction</param>
        /// <param name="j">count of nodes in J direction</param>
        /// <param name="k">count of nodes in K direction</param>
        /// <returns>nodes count</returns>
        public static int BlockNodesCount(int i, int j, int k)
        {
            return i * j * k;
        }

        /// <summary>
        /// Count of all blocks nodes.
        /// </summary>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of nodes counts in I direction</param>
        /// <param name="jj">array of nodes counts in J direction</param>
        /// <param name="kk">array of nodes counts in K direction</param>
        /// <returns>nodes count</returns>
        public static int BlocksNodesCount(int bc, int[] ii, int[] jj, int[] kk)
        {
            int c = 0;

            for (int i = 0; i < bc; i++)
            {
                c += BlockNodesCount(ii[i], jj[i], kk[i]);
            }

            return c;
        }

        /// <summary>
        /// Check if string is comment.
        /// </summary>
        /// <param name="str">string</param>
        /// <returns><c>true</c> - if string is comment, <c>false</c> - otherwise</returns>
        public static bool IsComment(string str)
        {
            if (str != "")
            {
                List<string> s = str.Split(' ').ToList().FindAll(x => !Strings.IsEmpty(x));

                if (s.Count != 0)
                {
                    return s[0][0] == '!';
                }
            }

            return false;
        }

        /// <summary>
        /// Read next line from stream.
        /// </summary>
        /// <param name="sr">stream</param>
        /// <returns>string</returns>
        public static string ReadLine(StreamReader sr)
        {
            string line = sr.ReadLine();

            if (line == null)
            {
                return line;
            }

            // Re-read if line is empty or comment.
            while (Strings.IsEmpty(line) || IsComment(line))
            {
                line = sr.ReadLine();
            }

            return line;
        }

        /// <summary>
        /// Read blocks sizes from stream.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of nodes counts in I direction</param>
        /// <param name="jj">array of nodes counts in J direction</param>
        /// <param name="kk">array of nodes counts in K direction</param>
        public static void ReadBlocksSizes(StreamReader sr, int bc, int[] ii, int[] jj, int[] kk)
        {
            // The mode blocks sizes can be written is not fixed.
            // It may be 1 number per line or all numbers in the single line.
            // It does not matter at all.
            // After all numbers are written there must be \n symbol.

            if (bc <= 0)
            {
                // No blocks - no to read.
                return;
            }

            // Read all 3 * bc numbers.
            int[] nn = new int[3 * bc];
            int nn_index = 0;
            while (nn_index < 3 * bc)
            {
                string line = ReadLine(sr);
                string[] s = line.Split(' ');

                for (int i = 0; i < s.Length; i++)
                {
                    if (Strings.IsDigits(s[i]))
                    {
                        nn[nn_index++] = Int32.Parse(s[i]);
                    }
                }
            }

            // Now read sizes.
            for (int i = 0; i < bc; i++)
            {
                ii[i] = nn[3 * i];
                jj[i] = nn[3 * i + 1];
                kk[i] = nn[3 * i + 2];
            }
        }
    }
}
