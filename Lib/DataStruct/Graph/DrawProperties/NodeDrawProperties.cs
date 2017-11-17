// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Draw;
using Lib.Maths.Geometry;

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
        public double InnerRadius;

        /// <summary>
        /// Border radius.
        /// </summary>
        public double BorderRadius;

        /// <summary>
        /// Color.
        /// </summary>
        public Color Color;

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor;

        /// <summary>
        /// Visibility.
        /// </summary>
        public Visibility LabelVisibility;

        /// <summary>
        /// Label offset.
        /// </summary>
        public Vector LabelOffset;

        /// <summary>
        /// Size of font.
        /// </summary>
        public double FontSize;

        /// <summary>
        /// Defaul Constructor.
        /// </summary>
        public NodeDrawProperties()
        {
            InnerRadius = 2.5;
            BorderRadius = 4.0;
            Color = new Color(0xFF, 0xDD, 0xDD, 0xDD);
            BorderColor = new Color(0xFF, 0xAA, 0xAA, 0xAA);
            LabelVisibility = Visibility.Parent;
            LabelOffset = new Vector(5.0, 5.0);
            FontSize = 10.0;
        }

        /// <summary>
        /// Constructor from string.
        /// </summary>
        /// <param name="str">string</param>
        public NodeDrawProperties(string str)
        {
            string[] s = str.Split(new char[] { ';', '=', '(', ')', ',' });

            // Parse.
            InnerRadius = Double.Parse(s[1]);
            BorderRadius = Double.Parse(s[3]);
            Color = new Color(s[5]);
            BorderColor = new Color(s[7]);
            if (s[9] == "No")
            {
                LabelVisibility = Visibility.No;
            }
            else if (s[9] == "Yes")
            {
                LabelVisibility = Visibility.Yes;
            }
            else if (s[9] == "Parent")
            {
                LabelVisibility = Visibility.Parent;
            }
            else
            {
                throw new ApplicationException();
            }

            // In the old format offset vector has 2 components.
            // Is the new format offset vector has 3 components.
            // This code supports both versions (for supporting old graph files).
            if (s.Length == 17)
            {
                LabelOffset = new Vector(Double.Parse(s[12]), Double.Parse(s[13]));
                FontSize = Double.Parse(s[16]);
            }
            else if (s.Length == 18)
            {
                LabelOffset = new Vector(Double.Parse(s[12]), Double.Parse(s[13]), Double.Parse(s[14]));
                FontSize = Double.Parse(s[17]);
            }
            else
            {
                Debug.Assert(false);
            }
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

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("inner_radius={0};border_radius={1};color={2};border_color={3};visibility={4};label_offset={5};font_size={6}",
                                 InnerRadius, BorderRadius, Color, BorderColor, LabelVisibility.ToString(), LabelOffset.ToString(), FontSize);
        }
    }
}
