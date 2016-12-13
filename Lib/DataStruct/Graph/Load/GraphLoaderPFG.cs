using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

using Lib.DataStruct.Graph;
using Lib.Maths.Geometry.Geometry3D;

namespace Lib.DataStruct.Graph.Load
{
    /// <summary>
    /// Graph loader from pfg (and ibc) file.
    /// </summary>
    public class GraphLoaderPFG
    {
        /// <summary>
        /// Conversion.
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        static string conversion(string str1, string str2)
        {

            if (str1.Contains(".") && (str2 != "."))
                return str1.Replace('.', ',');
            if (str1.Contains(",") && (str2 != ","))
                return str1.Replace(',', '.');
            return str1;
        }
        /// <summary>
        /// Load skeleton of block-structured 3D grid all block edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="file_name">file name</param>
        /// <returns><c>true</c> - if success, <c>false</c> - otherwise</returns>
        public static bool LoadSkeleton(Graph g, string file_name)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader sr = new StreamReader(file_name))
                {
                    string line;
                    int bc = 0;

                    // Read count of blocks.
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        bc = Int32.Parse(line);
                    }

                    // Allocate memory for blocks sizes.
                    int[] ii = new int[bc];
                    int[] jj = new int[bc];
                    int[] kk = new int[bc];

                    // Read blocks sizes.
                    for (int i = 0; i < bc; i++)
                    {
                        line = sr.ReadLine();
                        string[] strs = line.Split(' ');
                        ii[i] = Int32.Parse(strs[0]);
                        jj[i] = Int32.Parse(strs[1]);
                        kk[i] = Int32.Parse(strs[2]);
                    }

                    // Allocate memory for coordinates.
                    int cc = 0;
                    for (int i = 0; i < bc; i++)
                    {
                        cc += ii[i] * jj[i] * kk[i];
                    }
                    double[] cs = new double[3 * cc];

                    // Read coordinates.
                    NumberFormatInfo nfi = NumberFormatInfo.CurrentInfo;
                    string CurrentDecimalSeparator = nfi.CurrencyDecimalSeparator;
                    int cur = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] strs = line.Split(' ');

                        for (int i = 0; i < strs.Count(); i++)
                        {
                            // Empty string is the end of processing.
                            if (strs[i] == "")
                            {
                                break;
                            }

                            cs[cur++] = Double.Parse(conversion(strs[i], CurrentDecimalSeparator));
                        }
                    }

                    // Add nodes to graph.
                    g.ChangeDimensionality(GraphDimensionality.D3);
                    int off = 0;
                    int loc_off = 0;
                    for (int i = 0; i < bc; i++)
                    {
                        loc_off += ii[i] * jj[i] * kk[i];

                        for (int j = 0; j < loc_off; j++)
                        {
                            Node node = g.AddNode();
                            node.Point3D = new Point(cs[off + j], cs[off + loc_off + j], cs[off + 2 * loc_off + j]);
                        }

                        off += 3 * loc_off;
                    }
                }
            }
            catch (Exception e)
            {
                is_succ = false;
            }

            return is_succ;
        }
    }
}
