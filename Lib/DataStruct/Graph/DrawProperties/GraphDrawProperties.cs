// Author: Alexey Rybakov

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
        /// Default color for node selection.
        /// </summary>
        readonly public Color DefaultSelectedNodeColor = new Color(System.Windows.Media.Colors.Orange);

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GraphDrawProperties()
        {
            DefaultNodeDrawProperties = new NodeDrawProperties();
            DefaultEdgeDrawProperties = new EdgeDrawProperties();
        }
    }
}
