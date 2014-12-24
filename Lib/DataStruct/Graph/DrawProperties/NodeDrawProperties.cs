// Copyright Joy Developing.

using System;

using Lib.Draw;

namespace Lib.DataStruct.Graph.DrawProperties
{
    /// <summary>
    /// Node draw properties.
    /// </summary>
    public class NodeDrawProperties : ICloneable
    {
        /// <summary>
        /// Inner radius.
        /// </summary>
        public double InnerRadius = 0.0;

        /// <summary>
        /// Border radius.
        /// </summary>
        public double BorderRadius = 0.0;

        /// <summary>
        /// Color.
        /// </summary>
        public Color Color = null;

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor = null;

        /// <summary>
        /// Defaul Constructor.
        /// </summary>
        public NodeDrawProperties()
        {
            InnerRadius = 2.5;
            BorderRadius = 4.0;
            Color = new Color(0xFF, 0xDD, 0xDD, 0xDD);
            BorderColor = new Color(0xFF, 0xAA, 0xAA, 0xAA);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            NodeDrawProperties ndprops = new NodeDrawProperties();

            ndprops.InnerRadius = InnerRadius;
            ndprops.BorderColor = BorderColor;
            ndprops.Color = Color.Clone() as Color;
            ndprops.BorderColor = BorderColor.Clone() as Color;

            return ndprops;
        }
    }
}
