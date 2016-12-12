using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

                    g.ChangeDimensionality(GraphDimensionality.D2);
                    GraphCreator.AddNodes(g, bc);
                    GraphLayoutManager.SetLayoutCircle(g, new Maths.Geometry.Geometry2D.Circle(new Maths.Geometry.Geometry2D.Point(50.0, 50.0), 10.0), 0.0);
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
