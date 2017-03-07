using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

using Lib.DataFormats;
using Lib.Maths.Geometry;

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
        private static void LoadBlocks(StructuredGrid g, StreamReader sr, bool is_blank)
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
        /// Load interfaces, border conditions and scopes.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream</param>
        private static void LoadIfacesBCondsScopes(StructuredGrid g, StreamReader sr)
        {
            // Ignore two lines.
            for (int i = 0; i < 2; i++)
            {
                string line = sr.ReadLine();

                if (line == null)
                {
                    return;
                }
            }

            LoadIfaces(g, sr);
            LoadBConds(g, sr);
            LoadScopes(g, sr);
        }

        /// <summary>
        /// Load interfaces.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream reader</param>
        private static void LoadIfaces(StructuredGrid g, StreamReader sr)
        {
            string line;

            if ((line = sr.ReadLine()) != null)
            {
                int ic = Int32.Parse(line);

                // Read all interfaces.
                for (int i = 0; i < ic; i++)
                {
                    line = sr.ReadLine();
                    string[] s = line.Split(' ');
                    int id = Int32.Parse(s[0]);
                    int bi = Int32.Parse(s[1]);
                    int i0 = Int32.Parse(s[2]);
                    int i1 = Int32.Parse(s[3]);
                    int j0 = Int32.Parse(s[4]);
                    int j1 = Int32.Parse(s[5]);
                    int k0 = Int32.Parse(s[6]);
                    int k1 = Int32.Parse(s[7]);
                    int nbi = Int32.Parse(s[8]);

                    // Create interface.
                    Iface iface = new Iface(id, g.Blocks[bi - 1],
                                            new ISegm(i0 - 1, i1 - 1),
                                            new ISegm(j0 - 1, j1 - 1),
                                            new ISegm(k0 - 1, k1 - 1),
                                            g.Blocks[nbi - 1]);

                    // Paste into interfaces list.

                    for (int j = 0; j < g.IfacesCount; j++)
                    {
                        Iface cur_iface = g.Ifaces[j];

                        if (cur_iface.Id == iface.Id)
                        {
                            g.Ifaces.Insert(j, iface);
                            iface = null;

                            break;
                        }
                    }

                    if (iface != null)
                    {
                        g.Ifaces.Add(iface);
                    }
                }
            }
        }

        /// <summary>
        /// Load border conditions.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream reader</param>
        private static void LoadBConds(StructuredGrid g, StreamReader sr)
        {
            string line;

            if ((line = sr.ReadLine()) != null)
            {
                int bcc = Int32.Parse(line);

                // Read all interfaces.
                for (int i = 0; i < bcc; i++)
                {
                    line = sr.ReadLine();
                    string[] s = line.Split(' ');
                    int id = Int32.Parse(s[0]);
                    int bi = Int32.Parse(s[1]);
                    int i0 = Int32.Parse(s[2]);
                    int i1 = Int32.Parse(s[3]);
                    int j0 = Int32.Parse(s[4]);
                    int j1 = Int32.Parse(s[5]);
                    int k0 = Int32.Parse(s[6]);
                    int k1 = Int32.Parse(s[7]);
                    string type = s[8];
                    string subtype = s[9];
                    string name = s[10];

                    // Create border condition.
                    BCond bcond = new BCond(id, g.Blocks[bi - 1],
                                            new ISegm(i0 - 1, i1 - 1),
                                            new ISegm(j0 - 1, j1 - 1),
                                            new ISegm(k0 - 1, k1 - 1),
                                            type, subtype, name);
                    g.BConds.Add(bcond);
                }
            }
        }

        /// <summary>
        /// Load scopes.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream reader</param>
        private static void LoadScopes(StructuredGrid g, StreamReader sr)
        {
        }

        /// <summary>
        /// Load structured grid from PFG/IBC files.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="pfg_file_name">PFG file name</param>
        /// <param name="ibc_file_name">IBC file name</param>
        /// <param name="is_iblank">isblank feature</param>
        /// <returns><c>true</c> - if grid is loaded, <c>false</c> - otherwise</returns>
        public static bool Load(StructuredGrid g,
                                string pfg_file_name, string ibc_file_name,
                                bool is_iblank)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader pfg_sr = new StreamReader(pfg_file_name))
                {
                    using (StreamReader ibc_sr = new StreamReader(ibc_file_name))
                    {
                        g.Clear();
                        LoadBlocks(g, pfg_sr, is_iblank);
                        LoadIfacesBCondsScopes(g, ibc_sr);
                    }
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
