// Author: Alexey Rybakov

using System;
using System.Xml.Serialization;

using Lib.Draw;

namespace Lib.DataStruct.Graph.DrawProperties
{
    /// <summary>
    /// Graph draw properties
    /// </summary>
    public class GraphDrawProperties
    {
        /// <summary>
        /// Default <c>NodeDrawProperties</c>.
        /// </summary>
        public NodeDrawProperties DefaultNodeDrawProperties = null;

        /// <summary>
        /// Default <c>EdgeDrawProperties</c>.
        /// </summary>
        public EdgeDrawProperties DefaultEdgeDrawProperties = null;

        /// <summary>
        /// Default selected node color.
        /// </summary>
        public Color DefaultSelectedNodeColor = null;

        /// <summary>
        /// Default captured node color.
        /// </summary>
        public Color DefaultCapturedNodeColor = null;

        /// <summary>
        /// Default selected edge color.
        /// </summary>
        public Color DefaultSelectedEdgeColor = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GraphDrawProperties()
        {
            // Draw properties.
            DefaultNodeDrawProperties = new NodeDrawProperties();
            DefaultNodeDrawProperties.LabelVisibility = Visibility.Yes;
            DefaultEdgeDrawProperties = new EdgeDrawProperties();

            // Colors.
            DefaultSelectedNodeColor = new Color(System.Windows.Media.Colors.Red);
            DefaultCapturedNodeColor = new Color(System.Windows.Media.Colors.Orange);
            DefaultSelectedEdgeColor = new Color(System.Windows.Media.Colors.Red);
        }
    }
}
