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
        /// Count of all block nodes.
        /// </summary>
        /// <param name="i">i size</param>
        /// <param name="j">j size</param>
        /// <param name="k">k size</param>
        /// <returns>nodes count</returns>
        private static int BlockNodesCount(int i, int j, int k)
        {
            return i * j * k;
        }

        /// <summary>
        /// Count of all blocks nodes.
        /// </summary>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        /// <returns>nodes count</returns>
        private static int BlocksNodesCount(int bc, int[] ii, int[] jj, int[] kk)
        {
            int c = 0;

            for (int i = 0; i < bc; i++)
            {
                c += BlockNodesCount(ii[i], jj[i], kk[i]);
            }

            return c;
        }

        /// <summary>
        /// Count of block skeleton nodes.
        /// </summary>
        /// <param name="i">i size</param>
        /// <param name="j">j size</param>
        /// <param name="k">k size</param>
        /// <returns>skeleton nodes count</returns>
        private static int BlockSkeletonNodesCount(int i, int j, int k)
        {
            return 4 * (i + j + k) - 16;
        }

        /// <summary>
        /// Count of blocks skeleton nodes.
        /// </summary>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        /// <returns></returns>
        private static int BlocksSkeletonNodesCount(int bc, int[] ii, int[] jj, int[] kk)
        {
            int c = 0;

            for (int i = 0; i < bc; i++)
            {
                c += BlockSkeletonNodesCount(ii[i], jj[i], kk[i]);
            }

            return c;
        }

        /// <summary>
        /// Read blocks sizes from stream.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        private static void ReadBlocksSizes(StreamReader sr, int bc, int[] ii, int[] jj, int[] kk)
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

        /// <summary>
        /// Read all block nodes coordinates.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="nodes_count">count of nodes</param>
        /// <param name="cs">coordinates array</param>
        /// <returns>count of coordinates readed</returns>
        private static int ReadNodesCoords(StreamReader sr, int nodes_count, double[] cs)
        {
            string line;

            // Get separator for read real numbers.
            string sep = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

            // Variables for splitting.
            string[] s;
            int s_count;

            // Positions.
            int c_pos = 0;

            // Base cycle of read.
            while ((line = sr.ReadLine()) != null)
            {
                s = line.Split(' ');
                s_count = s.Count();

                for (int i = 0; i < s_count; i++)
                {
                    if (s[i] == "")
                    {
                        // If it is empty string - this is end of it.
                        break;
                    }

                    cs[c_pos++] = Double.Parse(conversion(s[i], sep));

                    if (c_pos == 3 * nodes_count)
                    {
                        // All nodes coordinates are readed.
                        return c_pos;
                    }
                }
            }

            return c_pos;
        }

        /// <summary>
        /// Read blocks only skeleton nodes coordinates.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="blocks_count">blocks count</param>
        /// <param name="blocks_is">blocks i-sizes</param>
        /// <param name="blocks_js">blocks j-sizes</param>
        /// <param name="blocks_ks">blocks k-sizes</param>
        /// <param name="cs">coordinates array</param>
        /// <returns>count of coordinates readed</returns>
        private static int ReadSkeletonNodesCoords(StreamReader sr, int blocks_count,
                                                   int[] blocks_is, int[] blocks_js, int[] blocks_ks,
                                                   double[] cs)
        {
            string line;

            // Get separator for read real numbers.
            string sep = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

            // Variables for splitting.
            string[] s;
            int s_count;

            // Positions.
            int c_pos = 0;
            int cur_block = 0;
            int cur_dim = 0;
            int cur_i = 0;
            int cur_j = 0;
            int cur_k = 0;

            // Degree of edge.
            int deg = 0;

            // Base cycle of read.
            while ((line = sr.ReadLine()) != null)
            {
                s = line.Split(' ');
                s_count = s.Count();

                for (int i = 0; i < s_count; i++)
                {
                    if (s[i] == "")
                    {
                        // If it is empty string - this is end of it.
                        break;
                    }

                    deg = 0;
                    if ((cur_i == 0) || (cur_i == blocks_is[cur_block] - 1))
                    {
                        deg++;
                    }
                    if ((cur_j == 0) || (cur_j == blocks_js[cur_block] - 1))
                    {
                        deg++;
                    }
                    if ((cur_k == 0) || (cur_k == blocks_ks[cur_block] - 1))
                    {
                        deg++;
                    }
                    if (deg >= 2)
                    {
                        cs[c_pos++] = Double.Parse(conversion(s[i], sep));
                    }

                    // Chift counters;
                    cur_i++;
                    if (cur_i == blocks_is[cur_block])
                    {
                        cur_i = 0;
                        cur_j++;
                        if (cur_j == blocks_js[cur_block])
                        {
                            cur_j = 0;
                            cur_k++;
                            if (cur_k == blocks_ks[cur_block])
                            {
                                cur_k = 0;
                                cur_dim++;
                                if (cur_dim == 3)
                                {
                                    cur_dim = 0;
                                    cur_block++;
                                    if (cur_block == blocks_count)
                                    {
                                        return c_pos;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return c_pos;
        }

        /// <summary>
        /// Add block nodes (does not matter all or not).
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="cs">coordinates array (Xs, then Ys, and then Zs)</param>
        /// <param name="start_index">start index in array</param>
        /// <param name="c">block nodes count</param>
        private static void AddNodes(Graph g, double[] cs, int start_index, int c)
        {
            for (int i = 0; i < c; i++)
            {
                Node node = g.AddNode();
                node.Point3D = new Point(cs[start_index + i],
                                         cs[start_index + c + i],
                                         cs[start_index + 2 * c + i]);
            }
        }

        /// <summary>
        /// Add block nodes to graph.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="cs">coordinates array</param>
        /// <param name="start_index">start index</param>
        /// <param name="i">i size</param>
        /// <param name="j">j size</param>
        /// <param name="k">k size</param>
        /// <returns>count of nodes added</returns>
        private static int AddBlockNodes(Graph g, double[] cs, int start_index, int i, int j, int k)
        {
            int c = BlockNodesCount(i, j, k);

            AddNodes(g, cs, start_index, c);

            return c;
        }

        /// <summary>
        /// Add blocks nodes to graph.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="cs">coordinates array</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        /// <returns>count of nodes added</returns>
        private static int AddBlocksNodes(Graph g, double[] cs, int bc, int[] ii, int[] jj, int[] kk)
        {
            int c = 0;

            for (int i = 0; i < bc; i++)
            {
                c += AddBlockNodes(g, cs, c * 3, ii[i], jj[i], kk[i]);
            }

            return c;
        }

        /// <summary>
        /// Add block skeleton nodes to graph.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="cs">coordinates array</param>
        /// <param name="start_index">start index</param>
        /// <param name="i">i size</param>
        /// <param name="j">j size</param>
        /// <param name="k">k size</param>
        /// <returns>count of nodes added</returns>
        private static int AddBlockSkeletonNodes(Graph g, double[] cs, int start_index, int i, int j, int k)
        {
            int c = BlockSkeletonNodesCount(i, j, k);

            AddNodes(g, cs, start_index, c);

            return c;
        }

        /// <summary>
        /// Add blocks skeleton nodes to graph.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="cs">coordinates array</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        /// <returns></returns>
        private static int AddBlocksSkeletonNodes(Graph g, double[] cs, int bc, int[] ii, int[] jj, int[] kk)
        {
            int c = 0;

            for (int i = 0; i < bc; i++)
            {
                c += AddBlockSkeletonNodes(g, cs, c * 3, ii[i], jj[i], kk[i]);
            }

            return c;
        }

        /// <summary>
        /// Read from stream and add to graph nodes.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="g">graph</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        private static void ReadAndAddBlocksNodes(StreamReader sr, Graph g, int bc, int[] ii, int[] jj, int[] kk)
        {
            ReadBlocksSizes(sr, bc, ii, jj, kk);
            int c = BlocksNodesCount(bc, ii, jj, kk);
            double[] cs = new double[3 * c];
            ReadNodesCoords(sr, c, cs);
            g.ChangeDimensionality(GraphDimensionality.D3);
            AddBlocksNodes(g, cs, bc, ii, jj, kk);
        }

        /// <summary>
        /// Read from stream and add to graph skeleton nodes.
        /// </summary>
        /// <param name="sr">stream reader</param>
        /// <param name="g">graph</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        private static void ReadAndAddBlocksSkeletonNodes(StreamReader sr, Graph g, int bc, int[] ii, int[] jj, int[] kk)
        {
            ReadBlocksSizes(sr, bc, ii, jj, kk);
            double[] cs = new double[3 * BlocksSkeletonNodesCount(bc, ii, jj, kk)];
            ReadSkeletonNodesCoords(sr, bc, ii, jj, kk, cs);
            g.ChangeDimensionality(GraphDimensionality.D3);
            AddBlocksSkeletonNodes(g, cs, bc, ii, jj, kk);
        }

        /// <summary>
        /// Add skeleton edges for 1 block.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="off">offset</param>
        /// <param name="i">i size</param>
        /// <param name="j">j size</param>
        /// <param name="k">k size</param>
        private static void AddBlockSkeletonEdges(Graph g, int off, int i, int j, int k)
        {
            // i direction skeleton edges.
            GraphCreator.AddChain(g, off, off + i - 1);
            GraphCreator.AddChain(g, off + i + 2 * j - 4, off + 2 * i + 2 * j - 5);
            GraphCreator.AddChain(g, off + 2 * i + 2 * j + 4 * k - 12, off + 3 * i + 2 * j + 4 * k - 13);
            GraphCreator.AddChain(g, off + 3 * i + 4 * j + 4 * k - 16, off + 4 * i + 4 * j + 4 * k - 17);

            // j direction skeleton edges.
            g.AddEdge(off, off + i);
            GraphCreator.AddDistChain(g, off + i, off + i + 2 * j - 4, 2);
            //
            g.AddEdge(off + 2 * i + 2 * j + 4 * k - 12, off + 3 * i + 2 * j + 4 * k - 12);
            GraphCreator.AddDistChain(g, off + 3 * i + 2 * j + 4 * k - 12, off + 3 * i + 4 * j + 4 * k - 16, 2);
            //
            g.AddEdge(off + i + 2 * j - 5, off + 2 * i + 2 * j - 5);
            GraphCreator.AddDistChain(g, off + i - 1, off + i + 2 * j - 5, 2);
            //
            g.AddEdge(off + 3 * i + 4 * j + 4 * k - 17, off + 4 * i + 4 * j + 4 * k - 17);
            GraphCreator.AddDistChain(g, off + 3 * i + 2 * j + 4 * k - 13, off + 3 * i + 4 * j + 4 * k - 17, 2);

            // k direction skeleton edges.
            g.AddEdge(off, off + 2 * i + 2 * j - 4);
            GraphCreator.AddDistChain(g, off + 2 * i + 2 * j - 4, off + 2 * i + 2 * j + 4 * k - 12, 4);
            //
            g.AddEdge(off + i - 1, off + 2 * i + 2 * j - 3);
            g.AddEdge(off + 2 * i + 2 * j + 4 * k - 15, off + 3 * i + 2 * j + 4 * k - 13);
            GraphCreator.AddDistChain(g, off + 2 * i + 2 * j - 3, off + 2 * i + 2 * j + 4 * k - 15, 4);
            //
            g.AddEdge(off + i + 2 * j - 4, off + 2 * i + 2 * j - 2);
            g.AddEdge(off + 2 * i + 2 * j + 4 * k - 14, off + 3 * i + 4 * j + 4 * k - 16);
            GraphCreator.AddDistChain(g, off + 2 * i + 2 * j - 2, off + 2 * i + 2 * j + 4 * k - 14, 4);
            //
            g.AddEdge(off + 2 * i + 2 * j + 4 * k - 13, off + 4 * i + 4 * j + 4 * k - 17);
            GraphCreator.AddDistChain(g, off + 2 * i + 2 * j - 5, off + 2 * i + 2 * j + 4 * k - 13, 4);
        }

        /// <summary>
        /// Add skeleton edges.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="bc">blocks count</param>
        /// <param name="ii">array of i sizes</param>
        /// <param name="jj">array of j sizes</param>
        /// <param name="kk">array of k sizes</param>
        private static void AddBlocksSkeletonEdges(Graph g, int bc, int[] ii, int[] jj, int[] kk)
        {
            int off = 0;

            for (int i = 0; i < bc; i++)
            {
                AddBlockSkeletonEdges(g, off, ii[i], jj[i], kk[i]);
                off += BlockSkeletonNodesCount(ii[i], jj[i], kk[i]);
            }
        }

        /// <summary>
        /// Load whole grid representation.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="file_name">file name</param>
        /// <returns><c>true</c> - if success, <c>false</c> - otherwise</returns>
        public static bool LoadWhole(Graph g, string file_name)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader sr = new StreamReader(file_name))
                {
                    string line;

                    if ((line = sr.ReadLine()) != null)
                    {
                        int bc = Int32.Parse(line);

                        // Allocate memory for blocks sizes.
                        int[] ii = new int[bc];
                        int[] jj = new int[bc];
                        int[] kk = new int[bc];

                        // Read coordinates and add as graph nodes.
                        ReadAndAddBlocksNodes(sr, g, bc, ii, jj, kk);
                    }
                }
            }
            catch (Exception e)
            {
                is_succ = false;
            }

            return is_succ;
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

                    if ((line = sr.ReadLine()) != null)
                    {
                        int bc = Int32.Parse(line);

                        // Allocate memory for blocks sizes.
                        int[] ii = new int[bc];
                        int[] jj = new int[bc];
                        int[] kk = new int[bc];

                        // Read coordinates and add as graph nodes.
                        ReadAndAddBlocksSkeletonNodes(sr, g, bc, ii, jj, kk);

                        // Add edges.
                        AddBlocksSkeletonEdges(g, bc, ii, jj, kk);
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
