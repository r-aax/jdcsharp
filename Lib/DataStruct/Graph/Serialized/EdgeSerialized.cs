﻿using System;
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
        /// Oriented property.
        /// </summary>
        [XmlAttribute]
        public bool IsOriented;

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
            IsOriented = e.IsOriented;

            if (e.IsParentDrawProperties)
            {
                DrawProperties = "default";
            }
            else
            {
                DrawProperties = e.DrawProperties.ToString();
            }
        }
    }
}
