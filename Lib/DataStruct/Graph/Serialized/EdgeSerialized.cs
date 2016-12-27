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
    /// Edge information for serialization.
    /// </summary>
    [XmlType("Edge")]
    public class EdgeSerialized
    {
        /// <summary>
        /// First node identifier.
        /// </summary>
        [XmlAttribute]
        public int AId;

        /// <summary>
        /// Second node identifier.
        /// </summary>
        [XmlAttribute]
        public int BId;

        /// <summary>
        /// Oriented property.
        /// </summary>
        [XmlAttribute]
        public bool IsOriented;

        /// <summary>
        /// Label.
        /// </summary>
        [XmlAttribute]
        public string Label;

        /// <summary>
        /// Weight.
        /// </summary>
        [XmlAttribute]
        public double Weight;

        /// <summary>
        /// Draw properties.
        /// </summary>
        [XmlAttribute]
        public string DrawProperties;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EdgeSerialized()
        {
        }

        /// <summary>
        /// Constructor from edge.
        /// </summary>
        /// <param name="e">edge</param>
        public EdgeSerialized(Edge e)
        {
            AId = e.A.Id;
            BId = e.B.Id;
            IsOriented = e.IsOriented;
            Label = e.Label;
            Weight = e.Weight;

            if (e.IsParentDrawProperties)
            {
                DrawProperties = null;
            }
            else
            {
                DrawProperties = e.DrawProperties.ToString();
            }
        }
    }
}
