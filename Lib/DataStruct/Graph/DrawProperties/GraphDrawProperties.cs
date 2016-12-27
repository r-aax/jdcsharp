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
        /// Selected node color.
        /// </summary>
        public Color SelectedNodeColor = null;

        /// <summary>
        /// Captured node color.
        /// </summary>
        public Color CapturedNodeColor = null;

        /// <summary>
        /// Selected edge color.
        /// </summary>
        public Color SelectedEdgeColor = null;

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
            SelectedNodeColor = new Color(System.Windows.Media.Colors.Red);
            CapturedNodeColor = new Color(System.Windows.Media.Colors.Orange);
            SelectedEdgeColor = new Color(System.Windows.Media.Colors.Red);
        }
    }
}
