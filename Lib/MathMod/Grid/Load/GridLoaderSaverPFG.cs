using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;

using Lib.DataFormats;
using Lib.Maths.Geometry;
using Lib.Utils;

namespace Lib.MathMod.Grid.Load
{
    /// <summary>
    /// Grid loader from PFG/IBC format.
    /// </summary>
    public class GridLoaderSaverPFG
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
        private static void LoadBlocks(StructuredGrid g, StreamReader sr)
        {
            string line;

            // Get separator for read real numbers.
            string sep = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

            if ((line = PFG.ReadLine(sr)) != null)
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
                    // We read nodes count, but have to pass to block constructor cells count.
                    Block b = new Block(g, i, ii[i] - 1, jj[i] - 1, kk[i] - 1);

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

                while ((line = PFG.ReadLine(sr)) != null)
                {
                    List<string> s = line.Split(' ').ToList().FindAll(x => !Strings.IsEmpty(x));

                    for (int i = 0; i < s.Count(); i++)
                    {
                        if (iblank_data_left > 0)
                        {
                            // If we use iblank data, we must read it out.

                            iblank_data_left--;

                            continue;
                        }

                        // Load value.
                        double val = Double.Parse(Conv(s[i], sep));
                        cur_block.C[cur_i, cur_j, cur_k, cur_coord] = (float)val;

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
                                            if (GridLoadSavePFGProperties.IsIBlank)
                                            {
                                                iblank_data_left = cur_block.NodesCount;
                                            }
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

            if ((line = PFG.ReadLine(sr)) != null)
            {
                int ic = Int32.Parse(line);

                // Read all interfaces.
                for (int i = 0; i < ic; i++)
                {
                    line = PFG.ReadLine(sr);
                    List<string> s = line.Split(' ').ToList().FindAll(x => !Strings.IsEmpty(x));
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
                                            new IntervalI(i0 - 1, i1 - 1),
                                            new IntervalI(j0 - 1, j1 - 1),
                                            new IntervalI(k0 - 1, k1 - 1),
                                            g.Blocks[nbi - 1]);

                    // Paste into interfaces list.

                    for (int j = 0; j < g.IfacesPairsCount; j++)
                    {
                        IfacesPair pair = g.IfacesPairs[j];
                        Iface cur_iface = pair.If;

                        if (iface.DirectionsMatchFixed(cur_iface, true, 1e-6) != null)
                        {
                            Debug.Assert(pair.Mirror == null, "interfaces pair mirror double initialization");

                            pair.Mirror = iface;
                            iface = null;

                            break;
                        }
                    }

                    if (iface != null)
                    {
                        IfacesPair pair = new IfacesPair(iface, null);
                        g.IfacesPairs.Add(pair);
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

            if ((line = PFG.ReadLine(sr)) != null)
            {
                int bcc = Int32.Parse(line);

                // Read all interfaces.
                for (int i = 0; i < bcc; i++)
                {
                    line = PFG.ReadLine(sr);
                    List<string> s = line.Split(' ').ToList().FindAll(x => !Strings.IsEmpty(x));
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
                                            new IntervalI(i0 - 1, i1 - 1),
                                            new IntervalI(j0 - 1, j1 - 1),
                                            new IntervalI(k0 - 1, k1 - 1),
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
            string line;

            if ((line = PFG.ReadLine(sr)) != null)
            {
                int sc = Int32.Parse(line);

                // Read all interfaces.
                for (int i = 0; i < sc; i++)
                {
                    line = PFG.ReadLine(sr);
                    List<string> s = line.Split(' ').ToList().FindAll(x => !Strings.IsEmpty(x));
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
                    Scope scope = new Scope(id, g.Blocks[bi - 1],
                                            new IntervalI(i0 - 1, i1 - 1),
                                            new IntervalI(j0 - 1, j1 - 1),
                                            new IntervalI(k0 - 1, k1 - 1),
                                            type, subtype, name);
                    g.Scopes.Add(scope);
                }
            }
        }

        /// <summary>
        /// Load structured grid from PFG/IBC files.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="pfg_file_name">PFG file name</param>
        /// <param name="ibc_file_name">IBC file name</param>
        /// <param name="eps_par_move">epsilon for parallel move</param>
        /// <param name="eps_rot">epsilon for rotation</param>
        /// <returns><c>true</c> - if grid is loaded, <c>false</c> - otherwise</returns>
        public static bool Load(StructuredGrid g,
                                string pfg_file_name, string ibc_file_name,
                                double eps_par_move, double eps_rot)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader pfg_sr = new StreamReader(pfg_file_name))
                {
                    using (StreamReader ibc_sr = new StreamReader(ibc_file_name))
                    {
                        g.Clear();
                        LoadBlocks(g, pfg_sr);
                        LoadIfacesBCondsScopes(g, ibc_sr);
                        g.SetIfacesNDirs();

                        if (GridProperties.IsBcondsLinks)
                        {
                            g.InitBCondsLinks(eps_par_move, eps_rot);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(ExeDebug.ReportError(e.Message));
                is_succ = false;
            }

            return is_succ;
        }

        /// <summary>
        /// Save grid to PFG/IBC files.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="pfg_file_name">PFG file name</param>
        /// <param name="ibc_file_name">IBC file name</param>
        /// <returns><c>true</c> - if grid is saved, <c>false</c> - otherwise</returns>
        public static bool Save(StructuredGrid g, 
                                string pfg_file_name, string ibc_file_name)
        {
            bool is_succ = true;

            try
            {
                using (StreamWriter pfg_sw = new StreamWriter(pfg_file_name))
                {
                    using (StreamWriter ibc_sw = new StreamWriter(ibc_file_name))
                    {
                        SaveBlocks(g, pfg_sw);
                        SaveIfacesBCondsScopes(g, ibc_sw);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ExeDebug.ReportError(e.Message));
                is_succ = false;
            }

            return is_succ;
        }

        /// <summary>
        /// Save blocks.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sw">stream</param>
        public static void SaveBlocks(StructuredGrid g, StreamWriter sw)
        {
            int bc = g.BlocksCount;
            const int max_items_count = 5;
            const int max_iblank_items_count = 38;

            // Write blocks count.
            sw.WriteLine(bc.ToString());

            // Write blocks sizes.
            for (int bi = 0; bi < bc; bi++)
            {
                Block b = g.Blocks[bi];

                sw.WriteLine(b.INodes.ToString() + " " + b.JNodes.ToString() + " " + b.KNodes.ToString());
            }

            // Write each block.
            for (int bi = 0; bi < bc; bi++)
            {
                string line;
                int items_count;

                Block b = g.Blocks[bi];

                // Write X, Y, Z coordinates.
                for (int coord_num = 0; coord_num < 3; coord_num++)
                {
                    line = "";
                    items_count = 0;

                    for (int k = 0; k < b.KNodes; k++)
                    {
                        for (int j = 0; j < b.JNodes; j++)
                        {
                            for (int i = 0; i < b.INodes; i++)
                            {
                                if (items_count > 0)
                                {
                                    line += " ";
                                }

                                line += String.Format("{0:0.00000000e+00}", b.C[i, j, k, coord_num]);
                                items_count++;

                                if (items_count == max_items_count)
                                {
                                    sw.WriteLine(line);
                                    line = "";
                                    items_count = 0;
                                }
                            }
                        }
                    }

                    if (items_count > 0)
                    {
                        sw.WriteLine(line);
                    }
                }

                // Write blank data to the end of file.
                if (GridLoadSavePFGProperties.IsIBlank)
                {
                    line = "";
                    items_count = 0;

                    for (int i = 0; i < b.NodesCount; i++)
                    {
                        if (items_count > 0)
                        {
                            line += " ";
                        }

                        line += "1";
                        items_count++;

                        if (items_count == max_iblank_items_count)
                        {
                            sw.WriteLine(line);
                            line = "";
                            items_count = 0;
                        }
                    }

                    if (items_count > 0)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Save interfaces, border conditions, scopes.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sw">stream</param>
        public static void SaveIfacesBCondsScopes(StructuredGrid g, StreamWriter sw)
        {
            sw.WriteLine("! Version 3");
            sw.WriteLine("! DO NOT MODIFY THIS FILE.  RESULTS LIKELY TO BE UNDESIRABLE");
            SaveIfaces(g, sw);
            SaveBConds(g, sw);
            SaveScopes(g, sw);
        }

        /// <summary>
        /// Save interfaces.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sw">stream</param>
        public static void SaveIfaces(StructuredGrid g, StreamWriter sw)
        {
            sw.WriteLine(g.IfacesCount.ToString());

            for (int i = 0; i < g.IfacesPairsCount; i++)
            {
                sw.WriteLine(g.IfacesPairs[i].If.SaveString());
                sw.WriteLine(g.IfacesPairs[i].Mirror.SaveString());
            }
        }

        /// <summary>
        /// Save border conditions.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sw">stream</param>
        public static void SaveBConds(StructuredGrid g, StreamWriter sw)
        {
            sw.WriteLine(g.BCondsCount.ToString());

            for (int i = 0; i < g.BCondsCount; i++)
            {
                BCond b = g.BConds[i];

                sw.WriteLine(String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                                           b.Id, b.B.Id + 1,
                                           b.I0 + 1, b.I1 + 1,
                                           b.J0 + 1, b.J1 + 1,
                                           b.K0 + 1, b.K1 + 1,
                                           b.Label.Type, b.Label.Subtype, b.Label.Name));
            }
        }

        /// <summary>
        /// Save scopes.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sw">stream</param>
        public static void SaveScopes(StructuredGrid g, StreamWriter sw)
        {
            sw.WriteLine(g.ScopesCount.ToString());

            for (int i = 0; i < g.ScopesCount; i++)
            {
                Scope s = g.Scopes[i];

                sw.WriteLine(String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                                           s.Id, s.B.Id + 1,
                                           s.I0 + 1, s.I1 + 1,
                                           s.J0 + 1, s.J1 + 1,
                                           s.K0 + 1, s.K1 + 1,
                                           s.Label.Type, s.Label.Subtype, s.Label.Name));
            }
        }

        /// <summary>
        /// Export grid blocks distribution to *DIS/*dis file.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="file_name">PFG file name</param>
        /// <returns><c>true</c> - if grid is saved, <c>false</c> - otherwise</returns>
        public static bool ExportBlocksDistribution(StructuredGrid g, string file_name)
        {
            bool is_succ = true;

            try
            {
                using (StreamWriter sw = new StreamWriter(file_name))
                {
                    for (int i = 0; i < g.BlocksCount; i++)
                    {
                        Block b = g.Blocks[i];
                        sw.WriteLine(String.Format("{0}\t{1}", b.Id, b.PartitionNumber));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ExeDebug.ReportError(e.Message));
                is_succ = false;
            }

            return is_succ;
        }

        /// <summary>
        /// Load PERI data for grid.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="rep_file_name">REP file name</param>
        /// <returns><c>true</c> - if data is loaded, <c>false</c> - otherwise</returns>
        public static bool LoadPERI(StructuredGrid g, string peri_file_name)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader sr = new StreamReader(peri_file_name))
                {
                    string line = PFG.ReadLine(sr);

                    while (line != null)
                    {
                        string[] s = line.Split(' ');

                        int bcid1 = Int32.Parse(s[0]);
                        int bcid2 = Int32.Parse(s[1]);
                        string i1s = s[2];
                        string j1s = s[3];
                        string k1s = s[4];

                        BCond bc1 = g.FindBCond(bcid1);
                        BCond bc2 = g.FindBCond(bcid2);
                        BCondsLink link = new BCondsLink(bc1, bc2,
                                                         new Dir(i1s),
                                                         new Dir(j1s),
                                                         new Dir(k1s));
                        g.BCondsLinks.Add(link);

                        line = PFG.ReadLine(sr);
                    }
                }
            }
            catch (Exception)
            {
                is_succ = false;
            }

            return is_succ;
        }

        /// <summary>
        /// Save PERI information.
        /// </summary>
        /// <param name="g">gris</param>
        /// <param name="peri_file_name">PERI file name</param>
        /// <returns><c>true</c> if no errors occured, <c>false</c> - otherwise</returns>
        public static bool SavePERI(StructuredGrid g, string peri_file_name)
        {
            bool is_succ = true;

            try
            {
                using (StreamWriter sw = new StreamWriter(peri_file_name))
                {
                    for (int i = 0; i < g.BCondsLinksCount; i++)
                    {
                        BCondsLink bcl = g.BCondsLinks[i];

                        sw.WriteLine(String.Format("{0} {1} {2} {3} {4}",
                                     bcl.BCond1.Id, bcl.BCond2.Id,
                                     bcl.LDirs12[0].ToString(),
                                     bcl.LDirs12[1].ToString(),
                                     bcl.LDirs12[2].ToString()));
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
