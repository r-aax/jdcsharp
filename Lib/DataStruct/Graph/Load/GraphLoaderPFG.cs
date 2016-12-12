using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.DataStruct.Graph;

namespace Lib.DataStruct.Graph.Load
{
    /// <summary>
    /// Graph loader from pfg (and ibc) file.
    /// </summary>
    public class GraphLoaderPFG
    {
        /// <summary>
        /// Load skeleton of block-structured 3D grid all block edges.
        /// </summary>
        /// <param name="file_name">file name</param>
        /// <returns>graph that contains grid skeleton</returns>
        public static Graph LoadSkeleton(string file_name)
        {
            Graph g = new Graph();

            return g;
        }
    }
}
