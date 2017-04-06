using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry3D;

namespace Lib.MathMod.SolidGrid
{
    /// <summary>
    /// Solid grid.
    /// </summary>
    public class SolidGrid
    {
        /// <summary>
        /// Size of solid grid.
        /// </summary>
        public Vector Size;

        /// <summary>
        /// Delta l.
        /// </summary>
        public double Dl;

        /// <summary>
        /// Cells count along X coordinate.
        /// </summary>
        public int XISize;

        /// <summary>
        /// Cells count along Y coordinate.
        /// </summary>
        public int YISize;


        /// <summary>
        /// Cells count along Z coordinate.
        /// </summary>
        public int ZISize;


        /// <summary>
        /// Cells.
        /// </summary>
        public Cell[,,] Cells;

        /// <summary>
        /// Cell facet square.
        /// </summary>
        public double CellFacetS;

        /// <summary>
        /// Cell volume.
        /// </summary>
        public double CellV;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">size</param>
        /// <param name="dl">delta l</param>
        public SolidGrid(Vector size, double dl)
        {
            Size = size.Clone() as Vector;
            Dl = dl;

            // Integer sizes.
            XISize = (int)(Size.X / Dl);
            YISize = (int)(Size.Y / Dl);
            ZISize = (int)(Size.Z / Dl);

            // Cells.
            Cells = new Cell[XISize, YISize, ZISize];

            // Other data.
            CellFacetS = Dl * Dl;
            CellV = CellFacetS * Dl;
        }
    }
}
