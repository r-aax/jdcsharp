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
        /// Identifier.
        /// </summary>
        [XmlAttribute]
        public int Id;

        /// <summary>
        /// 2D or 3D position.
        /// </summary>
        [XmlAttribute]
        public string Position;

        /// <summary>
        /// Weight.
        /// </summary>
        [XmlAttribute]
        public string Weight;

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
            Id = n.Id;
            Position = n.PositionString();
            Weight = n.Weight.ToString();

            if (n.IsParentDrawProperties)
            {
                DrawProperties = null;
            }
            else
            {
                DrawProperties = n.DrawProperties.ToString();
            }
        }
    }
}
