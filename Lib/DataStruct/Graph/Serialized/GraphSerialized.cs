using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;

namespace Lib.DataStruct.Graph.Serialized
{
    /// <summary>
    /// Graph information for more flexible serialization.
    /// </summary>
    [XmlType("Graph")]
    public class GraphSerialized
    {
        /// <summary>
        /// Dimensionality.
        /// </summary>
        [XmlAttribute]
        public GraphDimensionality Dimensionality;

        /// <summary>
        /// Draw properties of graph.
        /// </summary>
        public class DrawPropertiesStrings
        {
            /// <summary>
            /// Default node draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultNodeDrawProperties;

            /// <summary>
            /// Default selected node draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultSelectedNodeDrawProperties;

            /// <summary>
            /// Default captured node draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultCapturedNodeDrawProperties;

            /// <summary>
            /// Default edge draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultEdgeDrawProperties;

            /// <summary>
            /// Default selected edge draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultSelectedEdgeDrawProperties;
        };

        /// <summary>
        /// Draw properties.
        /// </summary>
        public DrawPropertiesStrings DrawProperties;

        /// <summary>
        /// Nodes.
        /// </summary>
        public List<NodeSerialized> Nodes;

        /// <summary>
        /// Edges.
        /// </summary>
        public List<EdgeSerialized> Edges;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GraphSerialized()
        {
        }

        /// <summary>
        /// Constructor from graph.
        /// </summary>
        /// <param name="g">graph</param>
        public GraphSerialized(Graph g)
        {
            Dimensionality = g.Dimensionality;

            // Draw properties.
            DrawProperties = new DrawPropertiesStrings();
            GraphDrawProperties gdp = g.DrawProperties;
            DrawProperties.DefaultNodeDrawProperties = (gdp.DefaultNodeDrawProperties != null)
                                                       ? gdp.DefaultNodeDrawProperties.ToString()
                                                       : null;
            DrawProperties.DefaultSelectedNodeDrawProperties = (gdp.DefaultSelectedNodeDrawProperties != null)
                                                               ? gdp.DefaultSelectedNodeDrawProperties.ToString()
                                                               : null;
            DrawProperties.DefaultCapturedNodeDrawProperties = (gdp.DefaultCapturedNodeDrawProperties != null)
                                                               ? gdp.DefaultCapturedNodeDrawProperties.ToString()
                                                               : null;
            DrawProperties.DefaultEdgeDrawProperties = (gdp.DefaultEdgeDrawProperties != null)
                                                       ? gdp.DefaultEdgeDrawProperties.ToString()
                                                       : null;
            DrawProperties.DefaultSelectedEdgeDrawProperties = (gdp.DefaultSelectedEdgeDrawProperties != null)
                                                               ? gdp.DefaultSelectedEdgeDrawProperties.ToString()
                                                               : null;

            // Nodes.
            Nodes = new List<NodeSerialized>();
            foreach (Node n in g.Nodes)
            {
                Nodes.Add(new NodeSerialized(n));
            }

            // Edge.
            Edges = new List<EdgeSerialized>();
            foreach (Edge e in g.Edges)
            {
                Edges.Add(new EdgeSerialized(e));
            }
        }

        /// <summary>
        /// Serialize graph.
        /// </summary>
        /// <param name="file_name">file</param>
        public void XmlSerialize(string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            TextWriter writer = new StreamWriter(file_name);

            serializer.Serialize(writer, this);
            writer.Flush();
            writer.Close();
        }
    }
}
