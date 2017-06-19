using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.SolidGrid;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Godunov's method 1 order of precision.
    /// </summary>
    public class Godunov1
    {
        /// <summary>
        /// Do several iterations of Godunov's method.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="dt">delta time</param>
        /// <param name="n">iterations count</param>
        public static void Iters(SolidGrid.SolidGrid g, double dt, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Iter(g, dt);
            }
        }

        /// <summary>
        /// Solid grid iteration.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="dt">delta time</param>
        public static void Iter(SolidGrid.SolidGrid g, double dt)
        {
            U ru;
            D flow;
            double k = g.CellS * dt;

            g.UtoD();

            // X faces
            for (int xi = 0; xi < g.NX - 1; xi++)
            {
                for (int yi = 0; yi < g.NY; yi++)
                {
                    for (int zi = 0; zi < g.NZ; zi++)
                    {
                        Cell left = g.Cells[xi, yi, zi];
                        Cell right = g.Cells[xi + 1, yi, zi];
                        ru = Riemann.X_Toro(left.U, right.U);
                        flow = ru.FlowX * k;
                        left.D -= flow;
                        right.D += flow;
                    }
                }
            }

            // X borders.
            for (int yi = 0; yi < g.NY; yi++)
            {
                for (int zi = 0; zi < g.NZ; zi++)
                {
                    Cell cell;
                        
                    cell = g.Cells[0, yi, zi];
                    ru = Riemann.X_Toro(cell.U.MirrorX, cell.U);
                    flow = ru.FlowX * k;
                    cell.D += flow;

                    cell = g.Cells[g.NX - 1, yi, zi];
                    ru = Riemann.X_Toro(cell.U, cell.U.MirrorX);
                    flow = ru.FlowX * k;
                    cell.D -= flow;
                }
            }

            // Y faces.
            for (int xi = 0; xi < g.NX; xi++)
            {
                for (int yi = 0; yi < g.NY - 1; yi++)
                {
                    for (int zi = 0; zi < g.NZ; zi++)
                    {
                        Cell left = g.Cells[xi, yi, zi];
                        Cell right = g.Cells[xi, yi + 1, zi];
                        ru = Riemann.Y_Toro(left.U, right.U);
                        flow = ru.FlowY * k;
                        left.D -= flow;
                        right.D += flow;
                    }
                }
            }

            // Y borders.
            for (int xi = 0; xi < g.NX; xi++)
            {
                for (int zi = 0; zi < g.NZ; zi++)
                {
                    Cell cell;

                    cell = g.Cells[xi, 0, zi];
                    ru = Riemann.Y_Toro(cell.U.MirrorY, cell.U);
                    flow = ru.FlowY * k;
                    cell.D += flow;

                    cell = g.Cells[xi, g.NY - 1, zi];
                    ru = Riemann.Y_Toro(cell.U, cell.U.MirrorY);
                    flow = ru.FlowY * k;
                    cell.D -= flow;
                }
            }

            g.DtoU();
        }
    }
}
