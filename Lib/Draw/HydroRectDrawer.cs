using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Lib.Maths.Geometry;
using Lib.MathMod.SolidGrid;

namespace Lib.Draw
{
    public class HydroRectDrawer
    {
        /// <summary>
        /// Drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="drawer">drawer</param>
        public HydroRectDrawer(RectDrawer drawer)
        {
            Debug.Assert(drawer != null);
            Drawer = drawer;
        }

        /// <summary>
        /// Draw hydro for structured grid.
        /// </summary>
        /// <param name="grid">grid</param>
        public void DrawHydro(SolidGrid grid)
        {
            DrawCellsCanvas(grid);
        }

        /// <summary>
        /// Draw cells canvas (borders).
        /// </summary>
        /// <param name="grid">grid</param>
        public void DrawCellsCanvas(SolidGrid grid)
        {
            // x - dl step
            // y - dl step

            for (int i = 0; i <= grid.XISize; i++)
            {
                Drawer.DrawVLine(grid.Dl * i);
            }

            for (int i = 0; i <= grid.YISize; i++)
            {
                Drawer.DrawHLine(grid.Dl * i);
            }
        }
    }
}
