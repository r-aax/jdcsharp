using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.Draw;
using Lib.Utils;

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

                int cn = Lib.Utils.Convert.GetInt(n.Label);

                Debug.Assert(cn < cc, "Wrong color in graph node label.");

                n.DrawProperties.InnerRadius = n.DrawProperties.BorderRadius;
                n.DrawProperties.Color = colors[cn];
            }
        }
    }
}
