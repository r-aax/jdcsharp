using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Draw;

namespace Lib.DataStruct.Graph.DrawProperties
{
    /// <summary>
    /// Manager for some general actions with draw properties.
    /// </summary>
    public class DrawPropertiesManager
    {
        /// <summary>
        /// Repaint nodes.
        /// Use random colors for this.
        /// Number of random color is written in node label.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="n">count of random colors.</param>
        public static void RepaintNodesAccordingToTheirLabels(Graph g, int cc)
        {
            Color[] colors = Color.ArrayOfRandoms(cc);

            foreach (Node n in g.Nodes)
            {
                if (n.IsParentDrawProperties)
                {
                    n.CreateOwnDrawProperties();
                }

                int cn = n.Partition;

                Debug.Assert(cn < cc, "Wrong color in graph node label.");

                n.DrawProperties.InnerRadius = n.DrawProperties.BorderRadius;
                n.DrawProperties.Color = colors[cn];
            }

            // After this we want to repaint edges too.
            foreach (Edge e in g.Edges)
            {
                if (e.A.Partition == e.B.Partition)
                {
                    if (e.IsParentDrawProperties)
                    {
                        e.CreateOwnDrawProperties();
                    }

                    e.DrawProperties.Color = colors[e.A.Partition];
                }
                else
                {
                    if (!e.IsParentDrawProperties)
                    {
                        e.DrawProperties.Color = e.Parent.DrawProperties.DefaultEdgeDrawProperties.Color;
                    }
                }
            }
        }
    }
}
