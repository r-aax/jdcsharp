// Author: Alexey Rybakov

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
        /// Default constructor.
        /// </summary>
        public GraphDrawProperties()
        {
            DefaultNodeDrawProperties = new NodeDrawProperties();
            DefaultEdgeDrawProperties = new EdgeDrawProperties();
        }
    }
}
