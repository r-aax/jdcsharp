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

        /// <summary>
        /// Convert U to D for all cells.
        /// </summary>
        public void UtoD()
        {
            for (int xi = 0; xi < NX; xi++)
            {
                for (int yi = 0; yi < NY; yi++)
                {
                    for (int zi = 0; zi < NZ; zi++)
                    {
                        Cells[xi, yi, zi].UtoD(CellV);
                    }
                }
            }
        }

        /// <summary>
        /// Convert D to U for all cells.
        /// </summary>
        public void DtoU()
        {
            for (int xi = 0; xi < NX; xi++)
            {
                for (int yi = 0; yi < NY; yi++)
                {
                    for (int zi = 0; zi < NZ; zi++)
                    {
                        Cells[xi, yi, zi].DtoU(CellV);
                    }
                }
            }
        }

        /// <summary>
        /// X next U data.
        /// </summary>
        /// <param name="xi">X direction index</param>
        /// <param name="yi">Y direction index</param>
        /// <param name="zi">Z direction index</param>
        /// <returns>U data</returns>
        public U XNextU(int xi, int yi, int zi)
        {
            return (xi < NX - 1)
                   ? Cells[xi + 1, yi, zi].U
                   : Cells[xi, yi, zi].U.MirrorX;
        }

        /// <summary>
        /// X prev U data.
        /// </summary>
        /// <param name="xi">X direction index</param>
        /// <param name="yi">Y direction index</param>
        /// <param name="zi">Z direction index</param>
        /// <returns>U data</returns>
        public U XPrevU(int xi, int yi, int zi)
        {
            return (xi > 0)
                   ? Cells[xi - 1, yi, zi].U
                   : Cells[xi, yi, zi].U.MirrorX;
        }

        /// <summary>
        /// Y next U data.
        /// </summary>
        /// <param name="xi">X direction index</param>
        /// <param name="yi">Y direction index</param>
        /// <param name="zi">Z direction index</param>
        /// <returns>U data</returns>
        public U YNextU(int xi, int yi, int zi)
        {
            return (yi < NY - 1)
                   ? Cells[xi, yi + 1, zi].U
                   : Cells[xi, yi, zi].U.MirrorY;
        }

        /// <summary>
        /// Y prev U data.
        /// </summary>
        /// <param name="xi">X direction index</param>
        /// <param name="yi">Y direction index</param>
        /// <param name="zi">Z direction index</param>
        /// <returns>U data</returns>
        public U YPrevU(int xi, int yi, int zi)
        {
            return (yi > 0)
                   ? Cells[xi, yi - 1, zi].U
                   : Cells[xi, yi, zi].U.MirrorY;
        }

        /// <summary>
        /// Sum of maximum Courant numbers (<c>X</c>, <c>Y</c>, <c>Z</c>).
        /// </summary>
        /// <param name="dt">delta <c>t</c></param>
        /// <returns>sum of max Courant numbers</returns>
        public double MaxCourantXYZ(double dt)
        {
            double mcx = 0.0;
            double mcy = 0.0;

            for (int i = 0; i < NX; i++)
            {
                for (int j = 0; j < NY; j++)
                {
                    for (int k = 0; k < NZ; k++)
                    {
                        Cell cell = Cells[i, j, k];
                        double cx = cell.CourantX(CellL, dt);
                        double cy = cell.CourantY(CellL, dt);

                        mcx = Math.Max(mcx, cx);
                        mcy = Math.Max(mcy, cy);
                    }
                }
            }

            return mcx + mcy;
        }
    }
}
