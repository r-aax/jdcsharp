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
        /// Cells count in OX direction.
        /// </summary>
        public int NX;

        /// <summary>
        /// Cells count in OY direction.
        /// </summary>
        public int NY;

        /// <summary>
        /// Cells count in OZ direction.
        /// </summary>
        public int NZ;

        /// <summary>
        /// Space step.
        /// </summary>
        public double CellL;

        /// <summary>
        /// Square of cell edge.
        /// </summary>
        public double CellS;

        /// <summary>
        /// Volume of cell.
        /// </summary>
        public double CellV;

        /// <summary>
        /// Cells.
        /// </summary>
        public Cell[,,] Cells;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nx">cells count in OX direction</param>
        /// <param name="ny">cells count in OY direction</param>
        /// <param name="nz">cells count in OZ direction</param>
        /// <param name="dl">space step</param>
        public SolidGrid(int nx, int ny, int nz, double dl)
        {
            NX = nx;
            NY = ny;
            NZ = nz;
            CellL = dl;
            CellS = CellL * CellL;
            CellV = CellS * CellL;

            Cells = new Cell[NX, NY, NZ];

            for (int i = 0; i < NX; i++)
            {
                for (int j = 0; j < NY; j++)
                {
                    for (int k = 0; k < NZ; k++)
                    {
                        Cells[i, j, k] = new Cell();
                    }
                }
            }
        }

        /// <summary>
        /// Size in OX direction.
        /// </summary>
        public double XSize
        {
            get
            {
                return NX * CellL;
            }
        }

        /// <summary>
        /// Size in OY direction.
        /// </summary>
        public double YSize
        {
            get
            {
                return NY * CellL;
            }
        }

        /// <summary>
        /// Size in OZ direction.
        /// </summary>
        public double ZSize
        {
            get
            {
                return NZ * CellL;
            }
        }
    }
}
