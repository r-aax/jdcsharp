// Author: Alexey Rybakov

using Lib.Draw;
using System.Xml.Serialization;

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
        /// Default draw properties for selected node.
        /// </summary>
        public NodeDrawProperties DefaultSelectedNodeDrawProperties = null;

        /// <summary>
        /// Default draw properties for captured node.
        /// </summary>
        public NodeDrawProperties DefaultCapturedNodeDrawProperties = null;

        /// <summary>
        /// Default draw properties for selected edge.
        /// </summary>
        public EdgeDrawProperties DefaultSelectedEdgeDrawProperties = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GraphDrawProperties()
        {
            DefaultNodeDrawProperties = new NodeDrawProperties();
            DefaultEdgeDrawProperties = new EdgeDrawProperties();

            DefaultSelectedNodeDrawProperties = new NodeDrawProperties();
            DefaultSelectedNodeDrawProperties.Color = new Color(System.Windows.Media.Colors.Red);

            DefaultCapturedNodeDrawProperties = new NodeDrawProperties();
            DefaultCapturedNodeDrawProperties.Color = new Color(System.Windows.Media.Colors.Orange);

            DefaultSelectedEdgeDrawProperties = new EdgeDrawProperties();
            DefaultSelectedEdgeDrawProperties.Color = new Color(System.Windows.Media.Colors.Red);
        }
    }
}
