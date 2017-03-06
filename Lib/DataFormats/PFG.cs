using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// Read blocks sizes from stream.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of nodes counts in I direction</param>
        /// <param name="jj">array of nodes counts in J direction</param>
        /// <param name="kk">array of nodes counts in K direction</param>
        public static void ReadBlocksSizes(StreamReader sr, int bc, int[] ii, int[] jj, int[] kk)
        {
            for (int i = 0; i < bc; i++)
            {
                string line = sr.ReadLine();
                string[] s = line.Split(' ');

                ii[i] = Int32.Parse(s[0]);
                jj[i] = Int32.Parse(s[1]);
                kk[i] = Int32.Parse(s[2]);
            }
        }
    }
}
