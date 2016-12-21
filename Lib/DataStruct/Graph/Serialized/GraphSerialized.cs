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
            public string DefaultSelectedNodeColor;

            /// <summary>
            /// Default captured node draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultCapturedNodeColor;

            /// <summary>
            /// Default edge draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultEdgeDrawProperties;

            /// <summary>
            /// Default selected edge draw properties.
            /// </summary>
            [XmlAttribute]
            public string DefaultSelectedEdgeColor;
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
            DrawProperties.DefaultSelectedNodeColor = (gdp.DefaultSelectedNodeColor != null)
                                                      ? gdp.DefaultSelectedNodeColor.ToString()
                                                      : null;
            DrawProperties.DefaultCapturedNodeColor = (gdp.DefaultCapturedNodeColor != null)
                                                      ? gdp.DefaultCapturedNodeColor.ToString()
                                                      : null;
            DrawProperties.DefaultEdgeDrawProperties = (gdp.DefaultEdgeDrawProperties != null)
                                                       ? gdp.DefaultEdgeDrawProperties.ToString()
                                                       : null;
            DrawProperties.DefaultSelectedEdgeColor = (gdp.DefaultSelectedEdgeColor != null)
                                                      ? gdp.DefaultSelectedEdgeColor.ToString()
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

        /// <summary>
        /// Deserialized graph information from file.
        /// </summary>
        /// <param name="file_name">file name</param>
        /// <returns>graph information</returns>
        static public GraphSerialized XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(GraphSerialized));
                GraphSerialized collection = serializer.Deserialize(reader) as GraphSerialized;

                reader.Close();

                return collection;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
