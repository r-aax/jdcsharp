using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

using Lib.DataFormats;

namespace Lib.MathMod.Grid.Load
{
    /// <summary>
    /// Grid loader from PFG/IBC format.
    /// </summary>
    public class GridLoaderPFG
    {
        /// <summary>
        /// Conversion.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="p">separator</param>
        /// <returns>converted string</returns>
        static string Conv(string s, string p)
        {
            if (s.Contains(".") && (p != "."))
            {
                return s.Replace('.', ',');
            }

            if (s.Contains(",") && (p != ","))
            {
                return s.Replace(',', '.');
            }

            return s;
        }

        /// <summary>
        /// Load blocks from PFG file.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream reader</param>
        /// <param name="is_blank">isblank feature</param>
        public static void LoadBlocks(StructuredGrid g, StreamReader sr, bool is_blank)
        {
            string line;

            // Get separator for read real numbers.
            string sep = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

            if ((line = sr.ReadLine()) != null)
            {
                int bc = Int32.Parse(line);

                // Allocate memory for blocks sizes.
                int[] ii = new int[bc];
                int[] jj = new int[bc];
                int[] kk = new int[bc];

                // Read blocks sizes.
                PFG.ReadBlocksSizes(sr, bc, ii, jj, kk);

                // Add all blocks and allocate memory for them
                for (int i = 0; i < bc; i++)
                {
                    Block b = new Block(i, ii[i], jj[i], kk[i]);
                    b.Allocate();
                    g.Blocks.Add(b);
                }

                // Read coordsinates.

                int cur_block_num = 0;
                Block cur_block = g.Blocks[cur_block_num];
                int cur_coord = 0;
                int cur_i = 0;
                int cur_j = 0;
                int cur_k = 0;
                int iblank_data_left = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(' ');

                    for (int i = 0; i < s.Count(); i++)
                    {
                        if (s[i] == "")
                        {
                            // If element if empty this is the end of the line.
                            break;
                        }

                        if (iblank_data_left > 0)
                        {
                            // If we use iblank data, we must read it out.

                            iblank_data_left--;

                            continue;
                        }

                        // Load value.
                        double val = Double.Parse(Conv(s[i], sep));
                        cur_block.Nodes[cur_i, cur_j, cur_k][cur_coord] = val;

                        cur_i++;

                        // Shift cur_*.
                        if (cur_i == cur_block.INodes)
                        {
                            cur_i = 0;
                            cur_j++;

                            if (cur_j == cur_block.JNodes)
                            {
                                cur_j = 0;
                                cur_k++;

                                if (cur_k == cur_block.KNodes)
                                {
                                    cur_k = 0;
                                    cur_coord++;

                                    if (cur_coord == 3)
                                    {
                                        cur_coord = 0;
                                        cur_block_num++;

                                        if (cur_block_num == bc)
                                        {
                                            // All blocks data is readed.
                                            return;
                                        }
                                        else
                                        {
                                            iblank_data_left = cur_block.NodesCount;
                                        }

                                        cur_block = g.Blocks[cur_block_num];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Load structured grid from PFG/IBC files.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="file_name">PFG file name</param>
        /// <param name="is_iblank">isblank feature</param>
        /// <returns><c>true</c> - if grid is loaded, <c>false</c> - otherwise</returns>
        public static bool Load(StructuredGrid g, string pfg_file_name, bool is_iblank)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader pfg_sr = new StreamReader(pfg_file_name))
                {
                    LoadBlocks(g, pfg_sr, is_iblank);
                }
            }
            catch (Exception)
            {
                is_succ = false;
            }

            return is_succ;
        }
    }
}
