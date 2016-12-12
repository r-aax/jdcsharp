using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

using Lib.DataStruct.Graph;

namespace Lib.DataStruct.Graph.Serialized
{
    /// <summary>
    /// Node information for serialization.
    /// </summary>
    [XmlType("Node")]
    public class NodeSerialized
    {
        /// <summary>
        /// 2D or 3D position.
        /// </summary>
        [XmlAttribute]
        public string Position;

        /// <summary>
        /// Drawproperties.
        /// </summary>
        [XmlAttribute]
        public string DrawProperties;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NodeSerialized()
        {
        }

        /// <summary>
        /// Constructor from node.
        /// </summary>
        /// <param name="n">node</param>
        public NodeSerialized(Node n)
        {
            Position = n.PositionString();

            if (n.IsParentDrawProperties)
            {
                DrawProperties = "default";
            }
            else
            {
                DrawProperties = n.DrawProperties.ToString();
            }
        }
    }
}
